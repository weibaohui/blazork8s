using System.Threading.Tasks;
using Blazor.Service;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.Pod
{
    public partial class PodAction : ComponentBase
    {
        [Parameter]
        public V1Pod PodItem { get; set; }

        [Inject]
        private IPodService PodService { get; set; }

        async Task DeletePod(V1Pod pod)
        {
            await PodService.DeletePod(pod.Namespace(), pod.Name());
        }
    }
}
