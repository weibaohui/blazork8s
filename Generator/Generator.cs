using System.Collections.Generic;
using System.IO;
using JinianNet.JNTemplate;

namespace Generator;

public class Generator
{
    private IList<IDictionary<string, string>> _dictList = new List<IDictionary<string, string>>();


    public void SetDictList(IList<IDictionary<string, string>> dictList)
    {
        _dictList = dictList;
    }

    /// <summary>
    /// 替换关键字，形成目标模板文件名、目标模板路径、目标模板内容
    /// </summary>
    /// <param name="targetList"></param>
    /// <param name="dict"></param>
    private static void ProcessGenList(IList<GenInfo> targetList, IDictionary<string, string> dict)
    {
        foreach (var info in targetList)
        {
            info.TargetName    = info.Name.Replace(".g", ".cs");
            info.TargetPath    = info.Path;
            info.TargetContent = info.Content;

            info.TargetContent = ProcessTemplate(info.TargetContent, dict);
            info.TargetPath    = ProcessTemplate(info.TargetPath, dict);
            info.TargetName    = ProcessTemplate(info.TargetName, dict);
        }
    }


    /// <summary>
    /// 对指定模板文件进行处理
    /// </summary>
    /// <param name="content">模板文件内容</param>
    /// <param name="parameters">参数</param>
    /// <returns></returns>
    private static string ProcessTemplate(string content, IDictionary<string, string> parameters)
    {
        var template = TemplateEngineHelper.LoadTemplateByString(content);
        foreach (var (key, value) in parameters)
        {
            template.Set(key, value);
        }

        return template.Render();
    }

    private static IList<GenInfo> ReadAllTemplateFiles(string fullTemplateFolderPath, string fullFolderPath)
    {
        IList<GenInfo> targetList = new List<GenInfo>();
        ReadTemplateFiles(fullTemplateFolderPath, fullFolderPath, targetList);
        return targetList;
    }

    /// <summary>
    /// 遍历读取模板文件夹，读取每一个模板文件
    /// </summary>
    /// <param name="fullTemplateFolderPath"></param>
    /// <param name="fullFolderPath"></param>
    /// <param name="targetList"></param>
    private static void ReadTemplateFiles(string fullTemplateFolderPath, string fullFolderPath,
        IList<GenInfo>                           targetList)
    {
        var files = Directory.GetFiles(fullTemplateFolderPath);

        foreach (var file in files)
        {
            var filePath = Path.GetFullPath(fullTemplateFolderPath, file).Replace(fullFolderPath, "");
            targetList.Add(new GenInfo
            {
                Name    = Path.GetFileName(file),
                Path    = filePath,
                Content = File.ReadAllText(file)
            });
        }

        var directories = Directory.GetDirectories(fullTemplateFolderPath);

        foreach (var directory in directories)
        {
            ReadTemplateFiles(directory, fullFolderPath, targetList);
        }
    }

    /// <summary>
    /// 按照模板写入生成文件夹
    /// </summary>
    /// <param name="targetList"></param>
    /// <param name="generatorFolderPath"></param>
    private static void WriteTemplate(IList<GenInfo> targetList, string generatorFolderPath)
    {
        foreach (var info in targetList)
        {
            var realPath     = Path.Combine(generatorFolderPath, info.TargetPath);
            var realFilePath = Path.Combine(realPath, info.TargetName);
            if (!Directory.Exists(realPath))
            {
                Directory.CreateDirectory(realPath);
            }

            File.WriteAllText(realFilePath, info.TargetContent);
        }
    }

    /// <summary>
    /// 清空自动生成文件夹
    /// </summary>
    /// <param name="folderPath"></param>
    private static void RemoveFolder(string folderPath)
    {
        if (Directory.Exists(folderPath))
        {
            Directory.Delete(folderPath, true);
        }
    }


    public void Run()
    {
        var templatePath        = Directory.GetCurrentDirectory() + "../../../../template/";
        var generatorFolderPath = Directory.GetCurrentDirectory() + "/../generator/";


        var fullTemplateFolderPath  = Path.GetFullPath(templatePath);        // 替换为要读取的文件夹路径
        var fullFolderPath          = Path.GetFullPath(templatePath);        // 替换为要读取的文件夹路径
        var fullGeneratorFolderPath = Path.GetFullPath(generatorFolderPath); // 替换为要读取的文件夹路径


        var targetList = ReadAllTemplateFiles(fullTemplateFolderPath, fullFolderPath);
        RemoveFolder(fullGeneratorFolderPath);
        foreach (var dict in _dictList)
        {
            ProcessGenList(targetList, dict);
            WriteTemplate(targetList, fullGeneratorFolderPath);
        }
    }
}
