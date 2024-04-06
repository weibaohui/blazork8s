using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BlazorApp.Pages.Namespace;

public partial class NamespaceSelect : ComponentBase
{
    private IList<V1Namespace> _ns;

    [Inject]
    public IStringLocalizer L { get; set; }

    [Parameter]
    public EventCallback<string> OnNsSelected { get; set; }

    [Inject]
    private INamespaceService NamespaceService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _ns = NamespaceService.List();
        await base.OnInitializedAsync();
    }

    private void HandleChange(string value)
    {
        OnNsSelected.InvokeAsync(value);
    }
}