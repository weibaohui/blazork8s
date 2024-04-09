using BlazorApp.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BlazorApp.Pages.Common;

public abstract partial class PageBase : ComponentBase
{
    [Inject]
    protected IPageDrawerService PageDrawerService { get; set; }

    [Inject]
    public IStringLocalizer L { get; set; }
}
