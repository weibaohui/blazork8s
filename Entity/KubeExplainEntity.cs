namespace Entity;

public class KubeExplainEntity
{
    // public string Name         { get; set; }
    // public string FullName     { get; set; }
    public string ExplainFiled   { get; set; }
    public string ExplainVersion { get; set; }
    public string Explain        { get; set; }
    public string ExplainCN      { get; set; }
}

public class KubeExplainEN
{
    public string Id      { get; set; }
    public string Explain { get; set; }
    public bool done     { get; set; }
}

public class KubeExplainCN
{
    public string Id      { get; set; }
    public string EnId    { get; set; }
    public string Explain { get; set; }
}

public class KubeExplainRef
{
    public string ExplainFiled { get; set; }
    public string EnId         { get; set; }
    public string CnId         { get; set; }
}
