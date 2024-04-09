using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;

namespace BlazorApp.Pages.PodDisruptionBudget;

public partial class PodDisruptionBudgetAction : PageBase
{
    [Parameter]
    public V1PodDisruptionBudget Item { get; set; }
    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;


}
