using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Endpoints;
public partial class EndpointsAction : ComponentBase
{
    [Parameter]
    public V1Endpoints Item { get; set; }
    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;

}
