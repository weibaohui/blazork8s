using System;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using BlazorApp.Service.AI;
using BlazorApp.Service.k8s;
using BlazorApp.Utils.Prometheus.Models;
using BlazorApp.Utils.Swagger;
using Entity;
using Extension;
using Microsoft.Extensions.Logging;
using SqlSugar;

namespace BlazorApp.Service.impl;

public class TranslateService(
    IAiService aiService,
    ILogger<TranslateService> logger,
    IKubectlService kubectl,
    ISqlSugarClient db)
    : ITranslateService
{
    public async Task<string> Translate(string text)
    {
        logger.LogInformation("translate {Key}", text);
        var cnTask = await aiService.AIChat("请完整的将文字翻译为中文，请不要遗漏细节。正文如下：" + text);
        logger.LogInformation("translate {Key} over", cnTask);
        return cnTask;
    }

    /// <summary>
    /// 使用大模型将docTree中的字段进行翻译，保存到descriptionCN中
    /// </summary>
    public async Task ProcessDocTree()
    {
        // 配置序列化选项
        var options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = true // 如果需要格式化输出，可以设置为true
        };
        var entity = SwaggerHelper.Instance.GetAllEntity(SwaggerHelper.EntityType.K8SInTree);
        if (entity.definitions == null) return;
        foreach (var (key, definition) in entity.definitions)
        {
            definition.descriptionCN = RandomNumberGenerator.GetInt32(1, 9999).ToString();
            definition.descriptionCN = await Translate(definition.description);
            Thread.Sleep(1000 * 2);
            //及时保存，避免被中断
            await File.WriteAllTextAsync("new.json", JsonSerializer.Serialize(entity, options));

            if (definition.properties == null) continue;
            foreach (var (s, value) in definition.properties)
            {
                value.descriptionCN = RandomNumberGenerator.GetInt32(10000, 99999).ToString();
                value.descriptionCN = await Translate(value.description);
                Thread.Sleep(1000 * 2);
                //及时保存，避免被中断
                await File.WriteAllTextAsync("new.json", JsonSerializer.Serialize(entity, options));
            }
        }


        Console.WriteLine(JsonSerializer.Serialize(entity));
    }

    public async Task ProcessKubeExplains()
    {
        // db.CodeFirst.InitTables<KubeExplainCN>();
        // db.CodeFirst.InitTables<KubeExplainEN>();
        // db.CodeFirst.InitTables<KubeExplainRef>();
        //第零步，通过generator子项目中Explain() 方法，获得所有的资源的explainField,插入Ref表，完成准备工作
        //第一步，重置翻译错误的记录
        await FixTranslateAllError();
        await ProcessKubeExplainsEn();
        //第三步根据英文条目获得中文解释
        await ProcessKubeExplainsCn();
        //第四步将Field、中文解释和英文解释关联起来
        await Connect();

        // await SaveToDb();
    }


    /// <summary>
    /// 翻译指标
    /// </summary>
    public async Task TranslateMetrics()
    {
        var list = await db.Queryable<Metric>().Where(x => x.Cn == null).ToListAsync();
        foreach (var metric in list)
        {
            var cn = await Translate(metric.Description);
            metric.Cn = cn;
            await db.Updateable<Metric>()
                .SetColumns(x => x.Cn == cn)
                .Where(x => x.Name == metric.Name)
                .ExecuteCommandAsync();
        }
    }

    private async Task Connect()
    {
        await db.Ado.ExecuteCommandAsync(
            "update KubeExplainRef set CnId=(select Id from KubeExplainCN where EnId=KubeExplainRef.EnId) where CnId is null or CnId=''");
    }


    private async Task FixTranslateAllError()
    {
        await FixNoneTranslateButSetOk();
        await FixTranslateError();
    }

    /// <summary>
    /// 修复EN表中翻译条目没有将翻译结果保存到CN表，但是EN表标准了Done
    /// </summary>
    private async Task FixNoneTranslateButSetOk()
    {
        await db.Ado.ExecuteCommandAsync(
            "update KubeExplainEN set done=0 where  not exists(select 1 from KubeExplainCN where EnId=KubeExplainEN.Id)");
    }

    /// <summary>
    /// 重置翻译报错的条目
    /// </summary>
    private async Task FixTranslateError()
    {
        // update KubeExplainEN set done=0 where exists(select 1 from KubeExplainCN where EnId=KubeExplainEN.Id and KubeExplainCN.Explain like 'Error%');
        // delete from KubeExplainCN where Explain like 'Error%';
        await db.Ado.ExecuteCommandAsync(
            "update KubeExplainEN set done=0 where exists(select 1 from KubeExplainCN where EnId=KubeExplainEN.Id and KubeExplainCN.Explain like 'Error%')");
        await db.Ado.ExecuteCommandAsync("delete from KubeExplainCN where Explain like 'Error%'");
    }

    private async Task ProcessKubeExplainsEn()
    {
        var rfList = await db.Queryable<KubeExplainRef>().Where(x => x.EnId == null).ToListAsync();
        foreach (var rf in rfList)
        {
            var en = await kubectl.Explain(rf.ExplainFiled);
            var enId = en.ToMd5Str();
            rf.EnId = enId;
            await db.Updateable<KubeExplainRef>(rf).ExecuteCommandAsync();


            if (await db.Queryable<KubeExplainEN>().CountAsync(x => x.Id == enId) == 0)
            {
                await db.Insertable<KubeExplainEN>(new KubeExplainEN[]
                {
                    new KubeExplainEN()
                    {
                        Id = enId,
                        Explain = en,
                        done = false
                    }
                }).ExecuteCommandAsync();
            }
        }
    }

    private async Task ProcessKubeExplainsCn()
    {
        var enList = await db.Queryable<KubeExplainEN>().Where(x => x.done == false).ToListAsync();
        if (enList is { Count: 0 }) return;

        var all = enList.Count;
        var current = 0;
        foreach (var en in enList)
        {
            current += 1;
            Console.WriteLine($"处理 {current}/{all} {en.Id} ");
            if (string.IsNullOrWhiteSpace(en.Explain))
            {
                continue;
            }


            var cn = await Translate(en.Explain);
            var cnId = cn.ToMd5Str();
            en.done = true;
            logger.LogInformation("翻译Over {Current}/{All} {EnId} ", current, all, en.Id);
            logger.LogInformation("翻译Over  {EnId} {Cn} ", en.Id, cn);

            if (await db.Queryable<KubeExplainCN>().SingleAsync(x => x.Id == cnId) != null)
            {
                await db.Updateable(en).ExecuteCommandAsync();
                continue;
            }

            var rest = await db.Insertable(new KubeExplainCN()
            {
                Id = cnId,
                EnId = en.Id,
                Explain = cn
            }).ExecuteCommandAsync();
            logger.LogInformation("保存 {Current}/{All} {EnId} {Rest} ", current, all, en.Id, rest);
            await db.Updateable(en).ExecuteCommandAsync();

            Thread.Sleep(1000);
        }
    }
}
