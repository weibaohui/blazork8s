using BlazorApp.Service;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common;

public abstract partial class PageBase:ComponentBase
{
    [Inject]
    protected IPageDrawerService PageDrawerService { get; set; }
}
