using System.Collections.Generic;
using System.Threading.Tasks;
using Entity.Crd;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class CustomResourceDefinitionService(IKubeService kubeService)
    : CommonAction<V1CustomResourceDefinition>, ICustomResourceDefinitionService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return await kubeService.Client().DeleteCustomResourceDefinitionAsync(name);
    }


    /// <summary>
    /// 注意，本方法只能获取基本信息，CustomResource类中没有CR的具体实例Spec定义内容
    /// </summary>
    /// <param name="crd"></param>
    /// <returns></returns>
    public async Task<List<CustomResource>> GetCrInstanceList(V1CustomResourceDefinition crd)
    {
        List<CustomResource> crInstanceList = [];

        var group    = crd.Spec.Group;
        var versions = crd.Spec.Versions;
        var plural   = crd.Spec.Names.Plural;
        foreach (var version in versions)
        {
            var generic = new GenericClient(kubeService.Client(), group, version.Name, plural);
            var list    = await generic.ListAsync<CustomResourceList<CustomResource>>();
            crInstanceList.AddRange(list.Items);
        }

        return crInstanceList;
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="crd"></param>
    /// <param name="cr">CustomResource只有基础信息</param>
    /// <returns></returns>

    public async Task<object> GetCrInstanceWithSpec(V1CustomResourceDefinition crd, CustomResource cr)
    {
        var    group        = crd.Spec.Group;
        var    versions     = crd.Spec.Versions;
        var    plural       = crd.Spec.Names.Plural;
        object customObject = null;
        if (crd.Spec.Scope == "Namespaced")
        {
            customObject = await kubeService.Client()
                .GetNamespacedCustomObjectAsync(group, cr.ApiGroupAndVersion().Item2, cr.Namespace(), plural,
                    cr.Name());
        }
        else
        {
            customObject = await kubeService.Client()
                .GetClusterCustomObjectAsync(group, cr.ApiGroupAndVersion().Item2, plural, cr.Name());
        }


        var json = KubernetesJson.Serialize(customObject);
        var item = KubernetesYaml.Deserialize<object>(json);

        return item;
    }
}
