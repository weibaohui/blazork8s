using k8s;
using k8s.Models;

namespace Generator;

public class Item<T> where T : IKubernetesObject<V1ObjectMeta>
{
    public T KubeObject { get; set; }

    public string TypeName => typeof(T).Name;

    public string ActName()
    {
        var ret = "";
        if (TypeName.StartsWith("V1"))
        {
            ret = TypeName.Replace("V1", "");
        }

        return ret;
    }
}
