using System.Threading.Tasks;
using BlazorApp.Service.k8s;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common.Metadata;

public partial class KubectlCommand : DrawerPageBase<string>
{
    [Inject]
    IKubectlService Kubectl { get; set; }

    private string _command =string.Empty;
    private string _result   =string.Empty;

    protected override async Task OnInitializedAsync()
    {
        _command = base.Options;
        Kubectl.SetOutputEventHandler(EventHandler);
        if (!string.IsNullOrWhiteSpace(_command))
        {
           await Kubectl.Command(_command);
        }
        await base.OnInitializedAsync();
    }
    private async void EventHandler(object sender, string resp)
    {
        _result += resp;
        await InvokeAsync(StateHasChanged);
    }
}
