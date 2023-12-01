using System.Threading.Tasks;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Container;

public partial class ProbeKindStatus : ComponentBase
{
    private string Kind { get; set; }

    [Parameter]
    public V1Probe Probe { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (Probe.HttpGet != null)
        {
            Kind = "HttpGet";
        }
        else if (Probe.Exec != null)
        {
            Kind = "Exec";
        }
        else if (Probe.TcpSocket != null)
        {
            Kind = "TcpSocket";
        }
        else if (Probe.Grpc != null)
        {
            Kind = "Grpc";
        }

        await base.OnInitializedAsync();
    }
}
