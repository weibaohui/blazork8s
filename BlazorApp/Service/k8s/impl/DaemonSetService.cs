using System;
using System.Globalization;
using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl
{
    public class DaemonSetService : CommonAction<V1DaemonSet>, IDaemonSetService
    {
        private readonly IBaseService                _baseService;

        public DaemonSetService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public new async Task<object> Delete(string ns, string name)
        {
            return await _baseService.Client().DeleteNamespacedDaemonSetAsync(name, ns);
        }

        public async Task Restart(V1DaemonSet item)
        {
            if (item == null) return;


            var patchStr = """
                    {
                           "spec": {
                             "template": {
                               "metadata": {
                                 "annotations": {
                                   "kubectl.kubernetes.io/restartedAt": "${now}",
                                   "kubectl.kubernetes.io/origin": "BlazorK8s"
                                 }
                               }
                             }
                           }
                    }
                    """
                    .Replace("${now}", DateTime.Now.ToLocalTime().ToString(CultureInfo.CurrentCulture))
                ;
            var resp = await _baseService.Client().PatchNamespacedDaemonSetAsync(
                new V1Patch(patchStr, V1Patch.PatchType.MergePatch)
                , item.Name(), item.Namespace());
        }

    }
}
