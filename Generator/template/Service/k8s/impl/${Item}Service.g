using k8s.Models;
using System.Threading.Tasks;
using k8s;

namespace BlazorApp.Service.k8s.impl;

public class ${Item}Service(IKubeService kubeService) : CommonAction<${ItemType}>, I${Item}Service
{
    public new async Task<object> Delete(string ns, string name)
    {
        return await kubeService.Client().DeleteNamespaced${Item}Async(name, ns);
    }
}
