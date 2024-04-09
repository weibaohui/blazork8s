using AntDesign;
using BlazorApp.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BlazorApp.Pages.Common;

public partial class DrawerPageBase<T> : FeedbackComponent<T, bool>
{
    [Inject]
    public IStringLocalizer L { get; set; }

    [Inject]
    protected IPageDrawerService PageDrawerService { get; set; }
}