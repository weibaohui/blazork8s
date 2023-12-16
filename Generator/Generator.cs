using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JinianNet.JNTemplate;

namespace Generator;

public class Generator
{
    private IList<IDictionary<string, object>> _dictList = new List<IDictionary<string, object>>();

    /// <summary>
    /// 模板根目录，完全路径
    /// </summary>
    public string RootFolderTemplatePath { get; set; }

    /// <summary>
    /// 生成文件夹根目录，完全路径
    /// </summary>
    public string GeneratorFolderPath { get; set; }

    public void SetDictList(IList<IDictionary<string, object>> dictList)
    {
        _dictList = dictList;
    }

    /// <summary>
    /// 替换关键字，形成目标模板文件名、目标模板路径、目标模板内容
    /// </summary>
    /// <param name="templateFileList"></param>
    /// <param name="dict"></param>
    private IEnumerable<GenFileInfo> ProcessGenList(IEnumerable<GenFileInfo> templateFileList,
        IDictionary<string, object>                                          dict)
    {
        //将模板拷贝出来
        var processGenList = templateFileList.ToList();

        foreach (var info in processGenList)
        {
            info.TargetName    = info.Name.Replace(".g", ".cs");
            info.TargetPath    = info.Path;
            info.TargetContent = info.Content;

            info.TargetContent = ProcessTemplate(info.TargetContent, dict);
            info.TargetPath    = ProcessTemplate(info.TargetPath, dict);
            info.TargetName    = ProcessTemplate(info.TargetName, dict);

            info.TargetFullPath     = Path.Combine(GeneratorFolderPath, info.TargetPath);
            info.TargetFullFilePath = Path.Combine(info.TargetFullPath, info.TargetName);
        }

        return processGenList;
    }


    /// <summary>
    /// 对指定模板文件进行处理
    /// </summary>
    /// <param name="content">模板文件内容</param>
    /// <param name="parameters">参数</param>
    /// <returns></returns>
    private static string ProcessTemplate(string content, IDictionary<string, object> parameters)
    {
        var template = TemplateEngineHelper.LoadTemplateByString(content);
        foreach (var (key, value) in parameters)
        {
            template.Set(key, value);
        }

        return template.Render();
    }

    private IList<GenFileInfo> ReadAllTemplateFiles()
    {
        IList<GenFileInfo> genFileList = new List<GenFileInfo>();
        ReadTemplateFiles(RootFolderTemplatePath, genFileList);
        return genFileList;
    }

    /// <summary>
    /// 遍历读取模板文件夹，读取每一个模板文件
    /// </summary>
    /// <param name="folderPath"></param>
    /// <param name="genFileList"></param>
    private void ReadTemplateFiles(string folderPath,
        IList<GenFileInfo>                genFileList)
    {
        var files = Directory.GetFiles(folderPath);

        foreach (var file in files)
        {
            var fileRelativePath = Path.GetFullPath(folderPath, file).Replace(RootFolderTemplatePath, "");
            genFileList.Add(new GenFileInfo
            {
                Name    = Path.GetFileName(file),
                Path    = fileRelativePath,
                Content = File.ReadAllText(file)
            });
        }

        var directories = Directory.GetDirectories(folderPath);

        foreach (var directory in directories)
        {
            ReadTemplateFiles(directory, genFileList);
        }
    }

    /// <summary>
    /// 按照模板写入生成文件夹
    /// </summary>
    /// <param name="genFileList"></param>
    private void WriteTemplate(IEnumerable<GenFileInfo> genFileList)
    {
        foreach (var info in genFileList)
        {
            info.WriteToFile();
        }
    }

    /// <summary>
    /// 清空自动生成文件夹
    /// </summary>
    private void RemoveGeneratorFolder()
    {
        string folderPath = GeneratorFolderPath;
        if (Directory.Exists(folderPath))
        {
            Directory.Delete(folderPath, true);
        }
    }


    public void Run()
    {
        if (RootFolderTemplatePath == null)
        {
            throw new ArgumentNullException(nameof(RootFolderTemplatePath));
        }

        if (GeneratorFolderPath == null)
        {
            throw new ArgumentNullException(nameof(GeneratorFolderPath));
        }

        //清空自动生成文件夹
        RemoveGeneratorFolder();

        //读取所有模板文件
        var tempFileList = ReadAllTemplateFiles();

        //每一个dict 对应一个 k8s 资源类型
        //按类型处理模板文件，并保存
        foreach (var dict in _dictList)
        {
            var genFileList = ProcessGenList(tempFileList, dict);
            WriteTemplate(genFileList);
        }
    }
}
