using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.IngressClass;
public partial class IngressClassAction : ComponentBase
{
    [Parameter]
    public V1IngressClass Item { get; set; }
    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;
    [Inject]
    private IIngressClassService IngressClassService { get; set; }
    [Inject]
    IMessageService MessageService { get; set; }


    private async Task OnDefaultClick(V1IngressClass item)
    {
      await  IngressClassService.SetDefault(item);
      await MessageService.Success($"{item.Name()} is set to Global Default ");
    }
}
