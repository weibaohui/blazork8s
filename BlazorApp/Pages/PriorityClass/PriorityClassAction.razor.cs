using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;

namespace BlazorApp.Pages.PriorityClass;

public partial class PriorityClassAction : PageBase
{
    [Parameter]
    public V1PriorityClass Item { get; set; }
    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;
    [Inject]
    IMessageService MessageService { get; set; }

    [Inject]
    private IPriorityClassService PriorityClassService { get; set; }



    private async Task OnCancelDefaultClick(V1PriorityClass item)
    {
      await  PriorityClassService.ChangeGlobalDefaultTo(item, false);
      await MessageService.Success($"{item.Name()} Set Global Default to False");
    }
}
