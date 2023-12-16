using System;
using System.Collections.Generic;
using System.IO;
using JinianNet.JNTemplate;

namespace Generator;

public class Generator
{
    private static readonly string TemplatePath        = Directory.GetCurrentDirectory() + "../../../../template/";
    private static readonly string GeneratorFolderPath = Directory.GetCurrentDirectory() + "/../generator/";



    private          IDictionary<string, string> _dict    = new Dictionary<string, string>();
    private          IList<IDictionary<string, string>> _dictList    = new List<IDictionary<string, string>>();
    private readonly IList<GenInfo>              _genList = new List<GenInfo>();


    /// <summary>
    /// 设置替换参数字典
    /// </summary>
    /// <param name="dict"></param>
    public void SetDict(IDictionary<string, string> dict)
    {
        _dict = dict;
    }
    public void SetDictList( IList<IDictionary<string, string>> dictList)
    {
        _dictList = dictList;
    }

    /// <summary>
    /// 替换关键字，形成目标模板文件名、目标模板路径、目标模板内容
    /// </summary>
    public void PrepareGenList()
    {
        if (_dict.Count == 0)
        {
            Console.WriteLine("没有替换参数，无法生成代码。");
            return;
        }

        foreach (var info in _genList)
        {
            info.TargetName    = info.Name.Replace(".g", ".cs");
            info.TargetPath    = info.Path;
            info.TargetContent = info.Content;

            info.TargetContent = ProcessTemplate(info.TargetContent, _dict);
            info.TargetPath    = ProcessTemplate(info.TargetPath, _dict);
            info.TargetName    = ProcessTemplate(info.TargetName, _dict);

        }
    }


    /// <summary>
    /// 对指定模板文件进行处理
    /// </summary>
    /// <param name="content">模板文件内容</param>
    /// <param name="parameters">参数</param>
    /// <returns></returns>
    private string ProcessTemplate(string content, IDictionary<string, string> parameters)
    {
        var template = TemplateEngineHelper.LoadTemplateByString(content);
        foreach (var (key, value) in parameters)
        {
            template.Set(key,value);
        }
        return template.Render();
    }

    /// <summary>
    /// 遍历读取模板文件夹，读取每一个模板文件
    /// </summary>
    /// <param name="fullTemplateFolderPath"></param>
    /// <param name="fullFolderPath"></param>
    public void RecursivelyReadFiles(string fullTemplateFolderPath, string fullFolderPath)
    {
        var files = Directory.GetFiles(fullTemplateFolderPath);

        foreach (var file in files)
        {
            var filePath = Path.GetFullPath(fullTemplateFolderPath, file).Replace(fullFolderPath, "");
            _genList.Add(new GenInfo
            {
                Name    = Path.GetFileName(file),
                Path    = filePath,
                Content = File.ReadAllText(file)
            });
        }

        var directories = Directory.GetDirectories(fullTemplateFolderPath);

        foreach (var directory in directories)
        {
            RecursivelyReadFiles(directory, fullFolderPath);
        }
    }


    /// <summary>
    /// 回显原始模板内容
    /// </summary>
    public void PrintGenList()
    {
        foreach (GenInfo info in _genList)
        {
            Console.WriteLine($"{info.Path} {info.Name} ");
            Console.WriteLine($" {info.Content} ");
            Console.WriteLine($" ===== END ===== ");
        }
    }

    /// <summary>
    /// 回显生成模板内容
    /// </summary>
    public void PrintGenTargetList()
    {
        foreach (GenInfo info in _genList)
        {
            Console.WriteLine($"{info.TargetPath} {info.TargetName} ");
            Console.WriteLine($"{info.TargetContent}");
            Console.WriteLine($" ===== END ===== ");
        }
    }


    /// <summary>
    /// 按照模板写入生成文件夹
    /// </summary>
    /// <param name="generatorFolderPath"></param>
    public void GenTemplate(string generatorFolderPath)
    {
        foreach (GenInfo info in _genList)
        {
            var realPath     = Path.Combine(generatorFolderPath, info.TargetPath);
            var realFilePath = Path.Combine(realPath, info.TargetName);
            Console.WriteLine($"realPath {realFilePath} ");
            if (!Directory.Exists(realPath))
            {
                Directory.CreateDirectory(realPath);
            }

            Console.WriteLine($"{info.TargetPath} {info.TargetName} ");
            Console.WriteLine($"{info.TargetContent}");
            File.WriteAllText(realFilePath, info.TargetContent);
            Console.WriteLine($" ===== END ===== ");
        }
    }

    /// <summary>
    /// 清空自动生成文件夹
    /// </summary>
    /// <param name="folderPath"></param>
    public void RemoveFolder(string folderPath)
    {
        if (Directory.Exists(folderPath))
        {
            Directory.Delete(folderPath, true);
        }
    }



    public void Run()
    {
        var fullTemplateFolderPath  = Path.GetFullPath(TemplatePath);        // 替换为要读取的文件夹路径
        var fullFolderPath          = Path.GetFullPath(TemplatePath);        // 替换为要读取的文件夹路径
        var fullGeneratorFolderPath = Path.GetFullPath(GeneratorFolderPath); // 替换为要读取的文件夹路径


        RecursivelyReadFiles(fullTemplateFolderPath, fullFolderPath);
        RemoveFolder(fullGeneratorFolderPath);
        foreach (var dict in _dictList)
        {
            SetDict(dict);
            PrepareGenList();
            GenTemplate(fullGeneratorFolderPath);
        }
    }
}
