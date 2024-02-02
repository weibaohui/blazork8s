using SqlSugar;

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

[SugarTable]
public class KubeExplainEN
{
    [SugarColumn(IsPrimaryKey = true)] //设置主键
    public string Id      { get; set; }
    public string Explain { get; set; }
    public bool done     { get; set; }
}

[SugarTable]
public class KubeExplainCN
{

    [SugarColumn(IsPrimaryKey = true)] //设置主键
    public string Id      { get; set; }
    public string EnId    { get; set; }
    public string Explain { get; set; }
}
[SugarTable]
public class KubeExplainRef
{
    [SugarColumn(IsPrimaryKey = true)] //设置主键
    public string ExplainFiled { get; set; }
    public string EnId         { get; set; }
    public string CnId         { get; set; }
}
