using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.StorageClass;
public partial class StorageClassAction : ComponentBase
{
    [Parameter]
    public V1StorageClass Item { get; set; }
    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;
    [Inject]
    IMessageService MessageService { get; set; }
    [Inject]
    private IStorageClassService StorageClassService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }


    private async Task OnDefaultClick(V1StorageClass item)
    {
        await  StorageClassService.SetDefault(item);
        await MessageService.Success($"{item.Name()} is set to Global Default ");

    }
}
