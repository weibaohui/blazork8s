using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using k8s;
using k8s.Models;

namespace Generator;

public class EntityPrepare
{
    public static List<KubeType> GetK8SEntity(Type type,string resourceName)
    {
        var kubeList = new List<KubeType>();
        ReflectHelper<Type>.ListProperty(type, resourceName, kubeList);


        var zList = new List<KubeType>();
        ZipList(zList, kubeList);
        zList.Sort((a, b) => a.FieldLevel - b.FieldLevel);
        zList.Sort((a, b) => String.CompareOrdinal(a.FullName, b.FullName));


        var list = zList
            .Where(x => x.FieldLevel == 3)
            .ToList();
        File.WriteAllText("pod-zip.json", KubernetesJson.Serialize(list));
        return list;
    }

    /// <summary>
    /// 准备k8s资源，为下一步生成字段明细做准备
    /// </summary>
    public static void PrepareK8SEntity()
    {
        var kubeList = new List<KubeType>();
        ReflectHelper<V1Pod>.ListProperty(typeof(V1Pod), "Pod", kubeList);
        var s = KubernetesJson.Serialize(kubeList);
        File.WriteAllText("pod.json", s);

        var zList = new List<KubeType>();
        ZipList(zList, kubeList);
        zList.Sort((a, b) => a.FieldLevel - b.FieldLevel);
        zList.Sort((a, b) => String.CompareOrdinal(a.FullName, b.FullName));

        File.WriteAllText("pod-zip.json",
            KubernetesJson.Serialize(zList
                .Where(x => x.FieldLevel == 3)
                .ToList()
            )
        );
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
}
