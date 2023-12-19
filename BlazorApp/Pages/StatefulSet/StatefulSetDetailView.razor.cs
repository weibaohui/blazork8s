using System.Threading.Tasks;
using k8s.Models;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.StatefulSet
{
    public partial class StatefulSetDetailView :  DrawerPageBase<V1StatefulSet>
    {
        private V1StatefulSet StatefulSet { get; set; }
        protected override async Task OnInitializedAsync()
        {
            StatefulSet = base.Options;
            await base.OnInitializedAsync();
        }
    }
}