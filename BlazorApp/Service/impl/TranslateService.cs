using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        logger.LogInformation("translate {key}", text);
        var cnTask = await aiService.AIChat("请完整的将文字翻译为中文，请不要遗漏细节。正文如下：" + text);
        logger.LogInformation("translate {key} over", cnTask);
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
        //第零步，通过generator子项目中Explain() 方法，获得所有的资源的fieldList.json，完成准备工作
        //第一步获得explain的英文解释,并存储Field跟英文ID
        // await ProcessKubeExplainsEn();
        //第二步去重
        // await DistinctEnList();
        //第三步根据英文条目获得中文解释
        await ProcessKubeExplainsCN();
        //第四步将Field、中文解释和英文解释关联起来
        // await Connect();

        // await SaveToDb();
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

    private async Task Connect()
    {
        var json    = await File.ReadAllTextAsync("ref.json");
        var refList = JsonSerializer.Deserialize<List<KubeExplainRef>>(json);

        var cnListJson = await File.ReadAllTextAsync("cnList.json");
        var cnList     = JsonSerializer.Deserialize<List<KubeExplainCN>>(cnListJson);

        foreach (var rf in refList)
        {
            var cn = cnList.Find(cn => cn.EnId == rf.EnId);
            rf.CnId = cn.Id;
        }

        await File.WriteAllTextAsync("ref.json", JsonSerializer.Serialize(refList));
    }

    private async Task DistinctEnList()
    {
        var options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented          = true // 如果需要格式化输出，可以设置为true
        };
        var                    json   = await File.ReadAllTextAsync("enList.json");
        var                    enList = JsonSerializer.Deserialize<List<KubeExplainEN>>(json);
        HashSet<KubeExplainEN> set    = [];
        foreach (var en in enList.Where(en => set.All(x => x.Id != en.Id)))
        {
            set.Add(en);
        }

        await File.WriteAllTextAsync("enList.json", JsonSerializer.Serialize(set, options));
    }

    public async Task ProcessKubeExplainsCN()
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
            Console.WriteLine($"翻译Over {current}/{all} {en.Id} ");

            if (db.Queryable<KubeExplainCN>().Single(x=>x.Id==cnId)!=null)
            {
                continue;
            }

            await db.Insertable(new KubeExplainCN()
            {
                Id      = cnId,
                EnId    = en.Id,
                Explain = cn
            }).ExecuteCommandAsync();

            //及时保存，避免被中断
             Thread.Sleep(1000);
        }
    }

    public async Task ProcessKubeExplainsEn()
    {
        // 配置序列化选项
        var options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented          = true // 如果需要格式化输出，可以设置为true
        };
        IList<KubeExplainRef> fieldRef  = new List<KubeExplainRef>();
        IList<KubeExplainEN>  enList    = new List<KubeExplainEN>();
        var                   fieldJson = await File.ReadAllTextAsync("fieldList.json");
        var                   fieldList = JsonSerializer.Deserialize<IList<KubeExplainEntity>>(fieldJson);
        if (fieldList is { Count: 0 }) return;

        var all     = fieldList.Count;
        var current = 0;
        foreach (var explain in fieldList)
        {
            current += 1;
            Console.WriteLine($"处理 {current}/{all} {explain.ExplainFiled} ");
            if (!string.IsNullOrWhiteSpace(explain.Explain))
            {
                continue;
            }

            var en   = await kubectl.Explain(explain.ExplainFiled);
            var enId = en.ToMd5Str();
            explain.Explain = enId; //填充一个记录，如果断了重新来可以避免再次执行kubectl explain,断点续传
            enList.Add(new KubeExplainEN()
            {
                Id      = enId,
                Explain = en
            });
            fieldRef.Add(new KubeExplainRef()
            {
                EnId         = enId,
                ExplainFiled = explain.ExplainFiled
            });
            Console.WriteLine($"k8s  {current}/{all} {explain.ExplainFiled} ");

            Thread.Sleep(100);
            //及时保存，避免被中断
            await File.WriteAllTextAsync("fieldList.json", JsonSerializer.Serialize(fieldList, options));
            await File.WriteAllTextAsync("ref.json", JsonSerializer.Serialize(fieldRef, options));
            await File.WriteAllTextAsync("enList.json", JsonSerializer.Serialize(enList, options));
        }
    }
}
