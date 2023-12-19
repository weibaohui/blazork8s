using System.Threading.Tasks;
using k8s.Models;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.PriorityClass
{
    public partial class PriorityClassDetailView :  DrawerPageBase<V1PriorityClass>
    {
        private V1PriorityClass PriorityClass { get; set; }
        protected override async Task OnInitializedAsync()
        {
            PriorityClass = base.Options;
            await base.OnInitializedAsync();
        }
    }
}