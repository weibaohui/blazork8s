namespace Generator;

public class GenFileInfo
{
    /// <summary>
    /// 原始模板文件名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 原始模板文件路径，相对路径
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// 原始模板文件内容
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// 替换关键字后的目标模板内容
    /// </summary>
    public string TargetContent { get; set; }

    /// <summary>
    /// 替换关键字后的目标模板路径，相对路径
    /// </summary>
    public string TargetPath { get; set; }

    /// <summary>
    /// 替换关键字后的目标模板文件名
    /// </summary>
    public string TargetName { get; set; }

    /// <summary>
    /// 完整的目标文件保存地址
    /// </summary>
    public string TargetFullFilePath { get; set; }

    /// <summary>
    /// 完整的目标文件保存目录
    /// </summary>
    public string TargetFullPath { get; set; }
}
