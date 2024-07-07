using System.Threading.Tasks;
using k8s.Models;
using BlazorApp.Pages.Common;
using Entity.Crd.Gateway;

namespace BlazorApp.Pages.Gateway.${Item};
public partial class ${Item}DetailView :  DrawerPageBase<${ItemType}>
{
    private ${ItemType} ${Item} { get; set; }

    protected override async Task OnInitializedAsync()
    {
        ${Item} = base.Options;
        await base.OnInitializedAsync();
    }
}
