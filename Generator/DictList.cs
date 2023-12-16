using System.Collections.Generic;

namespace Generator;

public class DictList
{
    private readonly IList<IDictionary<string, string>> _dictList = new List<IDictionary<string, string>>();


    public void AddItem(string item, string itemType)
    {
        IDictionary<string, string> dict = new Dictionary<string, string>();
        dict.Add("Item", item);
        dict.Add("ItemType", itemType);
        _dictList.Add(dict);
    }
    public void Add(string key, string value)
    {
        IDictionary<string, string> dict = new Dictionary<string, string>();
        dict.Add(key, value);
        _dictList.Add(dict);
    }
    public void Add(IDictionary<string, string> dict)
    {
        _dictList.Add(dict);
    }

    public IList<IDictionary<string, string>> GetDictList()
    {
        return _dictList;
    }
}
