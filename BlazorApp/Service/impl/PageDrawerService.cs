using System.Threading.Tasks;
using AntDesign;

namespace BlazorApp.Service.impl;

public class PageDrawerService : IPageDrawerService
{

    private readonly DrawerService _drawerService;

    public PageDrawerService(DrawerService drawerService)
    {
        _drawerService = drawerService;
    }

    public DrawerOptions DefaultOptions(string title, int width = 800)
    {
        var options = new DrawerOptions
        {
            Title = title,
            Width = width,

        };
        return options;
    }

    public Task<DrawerRef<TResult>> CreateAsync<TComponent, TComponentOptions, TResult>(DrawerOptions options, TComponentOptions component) where TComponent : FeedbackComponent<TComponentOptions, TResult>
    {
        return _drawerService.CreateAsync<TComponent, TComponentOptions, TResult>(options, component);
    }
}
