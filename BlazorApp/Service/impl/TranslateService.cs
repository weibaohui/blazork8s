using System;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using BlazorApp.Service.AI;
using BlazorApp.Service.k8s;
using BlazorApp.Utils.KubeExplain;
using BlazorApp.Utils.Swagger;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Service.impl;

public class TranslateService(IAiService aiService, ILogger<TranslateService> logger, IKubectlService Kubectl)
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
        // 配置序列化选项
        var options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented          = true // 如果需要格式化输出，可以设置为true
        };
        var entity = KubeExplainHelper.Instance.GetAllKubeExplainList();
        if (entity is { Count: 0 }) return;

        var all     = entity.Count;
        var current = 0;
        foreach (var explain in entity)
        {
            current += 1;
            Console.WriteLine($"处理 {current}/{all} {explain.ExplainFiled} ");
            explain.Explain = await Kubectl.Explain(explain.ExplainFiled);
            Console.WriteLine($"k8s  {current}/{all} {explain.ExplainFiled} ");
            explain.ExplainCN = await Translate(explain.Explain);
            Console.WriteLine($"翻译Over {current}/{all} {explain.ExplainCN} ");

            Thread.Sleep(1000 * 2);
            //及时保存，避免被中断
            await File.WriteAllTextAsync("new.json", JsonSerializer.Serialize(entity, options));
        }

        Console.WriteLine(JsonSerializer.Serialize(entity, options));
    }
}
