using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using k8s.Models;
using Mapster;

namespace BlazorApp.Pages.HorizontalPodAutoscaler
{
    public partial class HorizontalPodAutoscalerDetailView : DrawerPageBase<V2HorizontalPodAutoscaler>
    {
        private V2HorizontalPodAutoscaler HorizontalPodAutoscaler { get; set; }

        protected override async Task OnInitializedAsync()
        {
            HorizontalPodAutoscaler = base.Options;
            await base.OnInitializedAsync();
        }

        private V1ObjectReference TransferToRef()
        {
            var _ref = HorizontalPodAutoscaler.Spec.ScaleTargetRef.Adapt<V1ObjectReference>();
            _ref.NamespaceProperty = HorizontalPodAutoscaler.Metadata.NamespaceProperty;
            return _ref;
        }
    }
}
