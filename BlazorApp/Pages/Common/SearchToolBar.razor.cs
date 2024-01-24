using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common;

public partial class SearchToolBar : ComponentBase
{
    [Parameter]
    public string Title { get; set; }

    [Parameter]
    public int Count { get; set; }

    [Parameter]
    public bool HideNsSelector { get; set; } = false;

    [Parameter]
    public EventCallback<string> OnNsSelected { get; set; }

    [Parameter]
    public EventCallback<string> OnSearch { get; set; }

    private string TxtValue { get; set; }

    private void OnNsSelectedHandler(string ns)
    {
        OnNsSelected.InvokeAsync(ns);
    }

    private async Task OnSearchHandler()
    {
        await OnSearch.InvokeAsync(TxtValue);
    }
}
