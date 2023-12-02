namespace Generator;

public class DictList
{
    private IList<IDictionary<string, string>> _DictList = new List<IDictionary<string, string>>();

    public void Add()
    {
        IDictionary<string, string> dict = new Dictionary<string, string>();
        dict.Add("$Namespace$", "BlazorApp.Service");
        dict.Add("$Item$", "ReplicaSet");
        dict.Add("$ItemType$", "V1ReplicaSet");
        _DictList.Add(dict);
    }
    public void AddItem(string Item, string ItemType)
    {
        IDictionary<string, string> dict = new Dictionary<string, string>();
        dict.Add("$Item$", Item);
        dict.Add("$ItemType$", ItemType);
        _DictList.Add(dict);
    }
    public void Add(string key, string value)
    {
        IDictionary<string, string> dict = new Dictionary<string, string>();
        dict.Add(key, value);
        _DictList.Add(dict);
    }
    public void Add(IDictionary<string, string> dict)
    {
        _DictList.Add(dict);
    }

    public IList<IDictionary<string, string>> GetDictList()
    {
        return _DictList;
    }
}
