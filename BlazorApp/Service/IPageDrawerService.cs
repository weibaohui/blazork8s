using System.Threading.Tasks;
using AntDesign;

namespace BlazorApp.Service;

//弹窗通用服务
public interface IPageDrawerService
{
    DrawerOptions DefaultOptions(string title, int width = 800);

    /// <summary>
    /// Create and open a drawer with the template
    /// </summary>
    /// <typeparam name="TComponent">The type of DrawerTemplate implement</typeparam>
    /// <typeparam name="TComponentOptions">The </typeparam>
    /// <typeparam name="TResult">The type of return value</typeparam>
    /// <param name="options"></param>
    /// <param name="component"></param>
    /// <returns>The reference of drawer</returns>
    Task<DrawerRef<TResult>> ShowDrawerAsync<TComponent, TComponentOptions, TResult>
    (
        DrawerOptions     options,
        TComponentOptions component
    ) where TComponent : FeedbackComponent<TComponentOptions, TResult>;
}
