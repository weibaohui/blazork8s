using System.Threading.Tasks;
using AntDesign;
using k8s.Models;

namespace BlazorApp.Pages.IngressClass
{
    public partial class IngressClassDetailView : FeedbackComponent<V1IngressClass, bool>
    {
        private V1IngressClass Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
