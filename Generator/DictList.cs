using System;
using System.Collections.Generic;
using Extension;

namespace Generator;

public class DictList
{
    private readonly IList<IDictionary<string, object>> _dictList = new List<IDictionary<string, object>>();


    public void AddItem(string item, string itemType)
    {
        var dict = new Dictionary<string, object>();
        dict.Add("Item", item);
        dict.Add("ItemType", itemType);
        _dictList.Add(dict);
    }

    public void AddItem(string item, string itemType, IList<KubeType> properties)
    {
        var dict = new Dictionary<string, object>();
        dict.Add("Item", item);
        dict.Add("ItemType", itemType);
        dict.Add("ExplainResourceName", item.ToCamelCase());
        dict.Add("Properties", properties);
        _dictList.Add(dict);
    }

    public void AddItem(string resourceName, Type kubeType)
    {
        var list = EntityPrepare.GetK8SEntity(kubeType, resourceName);
        AddItem(resourceName, kubeType.Name.ToPascalCase(), list);
    }


    public IList<IDictionary<string, object>> GetDictList()
    {
        return _dictList;
    }
}
