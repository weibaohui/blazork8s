namespace Generator;

public class Generator
{
    private          IDictionary<string, string> _dict    = new Dictionary<string, string>();
    private readonly IList<GenInfo>              _genList = new List<GenInfo>();


    /// <summary>
    /// 设置替换参数字典
    /// </summary>
    /// <param name="dict"></param>
    public void SetDict(IDictionary<string, string> dict)
    {
        _dict = dict;
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

            foreach (KeyValuePair<string, string> kvp in _dict)
            {
                info.TargetContent = info.TargetContent.Replace(kvp.Key, kvp.Value);
            }

            foreach (KeyValuePair<string, string> kvp in _dict)
            {
                info.TargetPath = info.TargetPath.Replace(kvp.Key, kvp.Value);
            }

            foreach (KeyValuePair<string, string> kvp in _dict)
            {
                info.TargetName = info.TargetName.Replace(kvp.Key, kvp.Value);
            }
        }
    }

    /// <summary>
    /// 遍历读取模板文件夹，读取每一个模板文件
    /// </summary>
    /// <param name="fullTemplateFolderPath"></param>
    /// <param name="fullFolderPath"></param>
    public void RecursivelyReadFiles(string fullTemplateFolderPath, string fullFolderPath)
    {
        string[] files = Directory.GetFiles(fullTemplateFolderPath);

        foreach (string file in files)
        {
            var filePath = Path.GetFullPath(fullTemplateFolderPath, file).Replace(fullFolderPath, "");
            _genList.Add(new GenInfo
            {
                Name    = Path.GetFileName(file),
                Path    = filePath,
                Content = File.ReadAllText(file)
            });
        }

        string[] directories = Directory.GetDirectories(fullTemplateFolderPath);

        foreach (string directory in directories)
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
    public void RemoveGenFolder(string folderPath)
    {
        if (Directory.Exists(folderPath))
        {
            Directory.Delete(folderPath, true);
        }
    }
}
