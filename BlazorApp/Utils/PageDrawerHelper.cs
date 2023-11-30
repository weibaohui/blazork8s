using System.Threading.Tasks;
using AntDesign;
using k8s;
using k8s.Models;

namespace BlazorApp.Utils;

public class PageDrawerHelper<T> where T : class, IKubernetesObject<V1ObjectMeta>
{
    public static PageDrawerHelper<T> Instance => Nested.Instance;

    private class Nested
    {
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Nested()
        {
        }

        internal static readonly PageDrawerHelper<T> Instance = new PageDrawerHelper<T>();
    }

    private DrawerService _drawerService;

    public PageDrawerHelper<T> SetDrawerService(DrawerService drawerService)
    {
        _drawerService = drawerService;
        return this;
    }

    private DrawerOptions DefaultOptions(string title, int width = 800)
    {
        var options = new DrawerOptions
        {
            Title = title,
            Width = width,
        };
        return options;
    }


    public Task<DrawerRef<TResult>> ShowDrawerAsync<TComponent, TComponentOptions, TResult>(
        TComponentOptions component) where TComponent : FeedbackComponent<TComponentOptions, TResult>
    {
        var options = DefaultOptions($"{(component as T)?.Kind}:{(component as T)?.Name()}");
        return _drawerService.CreateAsync<TComponent, TComponentOptions, TResult>(options, component);
    }
}
