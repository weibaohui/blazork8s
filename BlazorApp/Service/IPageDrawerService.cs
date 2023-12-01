using System.Threading.Tasks;
using AntDesign;

namespace BlazorApp.Service;

//弹窗通用服务
public interface IPageDrawerService
{
    DrawerOptions DefaultOptions(string title, int width = 800);

    DrawerService DrawerService { get; }
    Task<DrawerRef<TResult>> ShowDrawerAsync<TComponent, TComponentOptions, TResult>
    (
        DrawerOptions     options,
        TComponentOptions component
    ) where TComponent : FeedbackComponent<TComponentOptions, TResult>;
}
