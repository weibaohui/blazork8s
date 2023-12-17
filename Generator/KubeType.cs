using System.Collections.Generic;

namespace Generator;

public class KubeType
{
    public string Name         { get; set; }
    public string FullName     { get; set; }
    public string ExplainFiled { get; set; }
    public int    FieldLevel   { get; set; }
    public string Type         { get; set; }

    /// <summary>
    /// 去掉容器的类型
    /// List[Person] =>Person
    /// </summary>
    public string CoreType { get; set; }

    public bool IsList { get; set; }

    /// <summary>
    /// 当IsList 为真，且child仅包含一个字段时，提取其字段名称
    /// </summary>
    public string OnlyOneChildItemName { get; set; }

    /// <summary>
    /// Child 具有字段，List count > 1
    /// </summary>
    public bool MultipleChildItem { get; set; }

    public IList<KubeType> Child { get; set; } = new List<KubeType>();
}
