using System;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using BlazorApp.Service.AI;
using BlazorApp.Service.k8s;
using BlazorApp.Utils.Swagger;
using Entity;
using Extension;
using Microsoft.Extensions.Logging;
using SqlSugar;

namespace BlazorApp.Service.impl;

public class TranslateService(
    IAiService                aiService,
    ILogger<TranslateService> logger,
    IKubectlService           kubectl,
    ISqlSugarClient           db)
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
    public async Task ProcessAll()
    {
        // 配置序列化选项
        var options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented          = true // 如果需要格式化输出，可以设置为true
        };
        var entity = SwaggerHelper.Instance.GetAllEntity();
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
        await ProcessKubeExplainsEn();
        //第三步根据英文条目获得中文解释
        await ProcessKubeExplainsCn();
        //第四步将Field、中文解释和英文解释关联起来
        await Connect();

        // await SaveToDb();
    }

    private async Task Connect()
    {
        await db.Ado.ExecuteCommandAsync(
            "update KubeExplainRef set CnId=(select Id from KubeExplainCN where EnId=KubeExplainRef.EnId) where CnId is null or CnId=''");
    }

    private async Task ProcessKubeExplainsEn()
    {
        var rfList = db.Queryable<KubeExplainRef>().Where(x => x.EnId == null).ToList();
        foreach (var rf in rfList)
        {

            var en   = await kubectl.Explain(rf.ExplainFiled);
            var enId = en.ToMd5Str();
            rf.EnId = enId;
            await db.Updateable<KubeExplainRef>(rf).ExecuteCommandAsync();


            if (await db.Queryable<KubeExplainEN>().CountAsync(x => x.Id == enId) == 0)
            {
                await db.Insertable<KubeExplainEN>(new KubeExplainEN[]
                {
                    new KubeExplainEN()
                    {
                        Id      = enId,
                        Explain = en,
                        done    = false
                    }
                }).ExecuteCommandAsync();
            }
        }
    }


    private async Task SaveToDb()
    {
        // var json    = await File.ReadAllTextAsync("ref.json");
        // var refList = JsonSerializer.Deserialize<List<KubeExplainRef>>(json);
        //
        // var cnListJson = await File.ReadAllTextAsync("cnList_all.json");
        // var cnList     = JsonSerializer.Deserialize<List<KubeExplainCN>>(cnListJson);
        //
        // var enListJson = await File.ReadAllTextAsync("enList.json");
        // var enList     = JsonSerializer.Deserialize<List<KubeExplainEN>>(enListJson);
        //
        // List<KubeExplainEN> uniqEnList = new List<KubeExplainEN>();
        // foreach (var en in enList)
        // {
        //     if (uniqEnList.Any(x => x.Id == en.Id))
        //     {
        //         continue;
        //     }
        //
        //     uniqEnList.Add(en);
        // }
        //
        // List<KubeExplainCN> uniqCnList = new List<KubeExplainCN>();
        // foreach (var cn in cnList)
        // {
        //     if (uniqCnList.Any(x => x.Id == cn.Id))
        //     {
        //         continue;
        //     }
        //
        //     uniqCnList.Add(cn);
        // }
        //
        // await db.Ado.ExecuteCommandAsync("delete from KubeExplainRef");
        // await db.Ado.ExecuteCommandAsync("delete from KubeExplainCN");
        // await db.Ado.ExecuteCommandAsync("delete from KubeExplainEN");
        // await db.Insertable(refList).ExecuteCommandAsync();
        // await db.Insertable(uniqCnList).ExecuteCommandAsync();
        // await db.Insertable(uniqEnList).ExecuteCommandAsync();
    }


    private async Task ProcessKubeExplainsCn()
    {
        var enList = db.Queryable<KubeExplainEN>().Where(x => x.done == false).ToList();
        if (enList is { Count: 0 }) return;

        var all     = enList.Count;
        var current = 0;
        foreach (var en in enList)
        {
            current += 1;
            Console.WriteLine($"处理 {current}/{all} {en.Id} ");
            if (string.IsNullOrWhiteSpace(en.Explain))
            {
                continue;
            }


            var cn   = await Translate(en.Explain);
            var cnId = cn.ToMd5Str();
            en.done = true;
            logger.LogInformation("翻译Over {Current}/{All} {EnId} ", current, all, en.Id);

            if (db.Queryable<KubeExplainCN>().Single(x => x.Id == cnId) != null)
            {
                await db.Updateable(en).ExecuteCommandAsync();
                continue;
            }

            var rest = await db.Insertable(new KubeExplainCN()
            {
                Id      = cnId,
                EnId    = en.Id,
                Explain = cn
            }).ExecuteCommandAsync();
            logger.LogInformation("保存 {Current}/{All} {EnId} {Rest} ", current, all, en.Id, rest);
            await db.Updateable(en).ExecuteCommandAsync();

            Thread.Sleep(1000);
        }
    }
}
