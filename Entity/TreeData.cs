using System.Collections.Generic;

namespace Entity;

public class TreeData
{
    public string descriptionCN { get; set; }
    public string Title         { get; set; }
    public string description   { get; set; }
    public string type          { get; set; }
    public string format        { get; set; }
    public string RefKey        { get; set; }

    public List<TreeData> ChildList { get; set; } = new();
}
