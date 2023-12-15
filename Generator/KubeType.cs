using System.Collections.Generic;

namespace Generator;

public class KubeType
{
    public string          Name         { get; set; }
    public string          FullName     { get; set; }
    public string          ExplainFiled { get; set; }
    public int             FieldLevel   { get; set; }
    public string          Type         { get; set; }
    public bool            IsList       { get; set; }
    public IList<KubeType> Child        { get; set; } = new List<KubeType>();
}
