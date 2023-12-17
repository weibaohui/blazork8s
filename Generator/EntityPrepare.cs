using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using k8s;

namespace Generator;

public class EntityPrepare
{
    public static List<KubeType> GetK8SEntity(Type type, string resourceName)
    {
        var kubeList = new List<KubeType>();
        ReflectHelper<Type>.ListProperty(type, resourceName, kubeList);

        ReflectHelper<Type>.ProcessOnlyOneChildItem(kubeList);
        ReflectHelper<Type>.ProcessMultipleChildItem(kubeList);

        var zList = new List<KubeType>();
        ZipList(zList, kubeList);
        zList.Sort((a, b) => a.FieldLevel - b.FieldLevel);
        zList.Sort((a, b) => String.CompareOrdinal(a.FullName, b.FullName));


        var list = zList
            .Where(x => (x.FieldLevel == 2 || x.FieldLevel == 3)
                        && !x.FullName.Contains(".Metadata")
                        && !x.FullName.Contains(".ApiVersion")
                        && !x.FullName.Contains(".Kind")
            )
            .ToList();

        list = RemoveMultipleItem(list);
        list = RemoveOnlyOneChildItem(list);
        File.WriteAllText("pod-zip3.json", KubernetesJson.Serialize(list));
        return list;
    }


    /// <summary>
    /// 将列表中的child抽取打平为一个扁平list
    /// </summary>
    /// <param name="topList"></param>
    /// <param name="childList"></param>
    private static void ZipList(IList<KubeType> topList, IList<KubeType> childList)
    {
        foreach (var kubeType in childList)
        {
            if (kubeType.Child != null)
            {
                ZipList(topList, kubeType.Child);
            }

            kubeType.Child = null;
            topList.Add(kubeType);
        }
    }


    /// <summary>
    /// 剔除具有子列表并且是多项的情况
    /// 在模板中不需要这种
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    private static List<KubeType> RemoveMultipleItem(IList<KubeType> list)
    {
        var keys = new List<string>();
        foreach (var kt in list)
        {
            if (kt.MultipleChildItem)
            {
                keys.Add(kt.FullName);
            }
        }

        var ret = list.ToList();

        keys.ForEach(k =>
        {
            ret.RemoveAll(x => x.IsList == false && x.MultipleChildItem == false && x.FullName.StartsWith(k));
        });
        return ret;
    }


    /// <summary>
    /// 删除只有一个child item 的情况
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    private static List<KubeType> RemoveOnlyOneChildItem(IList<KubeType> list)
    {
        var keys = new List<string>();
        foreach (var kt in list)
        {
            if (kt.IsList && kt.OnlyOneChildItemName != null)
            {
                keys.Add(kt.FullName);
            }
        }

        var ret = list.ToList();

        keys.ForEach(k =>
        {
            //只有下一级才会带".",以此保证自身不会被删除
            ret.RemoveAll(x => x.FullName.StartsWith(k+"."));
        });
        return ret;
    }
}
