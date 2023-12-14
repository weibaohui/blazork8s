using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Common.Metadata;
using BlazorApp.Service;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common;

public partial class DrawerPageBase<T> : FeedbackComponent<T, bool>
{
    [Inject]
    protected IPageDrawerService PageDrawerService { get; set; }



}
