using System.Threading.Tasks;
using AntDesign;

namespace BlazorApp.Service.impl;

public class PageDrawerService : IPageDrawerService
{
    public PageDrawerService(DrawerService drawerService)
    {
        DrawerService = drawerService;
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

    public DrawerService DrawerService { get; }

    public Task<DrawerRef<TResult>> ShowDrawerAsync<TComponent, TComponentOptions, TResult>(DrawerOptions options,
        TComponentOptions component) where TComponent : FeedbackComponent<TComponentOptions, TResult>
    {
        return DrawerService.CreateAsync<TComponent, TComponentOptions, TResult>(options, component);
    }
}
