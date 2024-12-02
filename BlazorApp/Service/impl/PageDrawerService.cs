using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;

namespace BlazorApp.Service.impl;

public class PageDrawerService(DrawerService drawerService) : IPageDrawerService
{
    private readonly IList<DrawerRef> _drawerRefs = new List<DrawerRef>();

    public DrawerOptions DefaultOptions(string title, int width = 800)
    {
        var options = new DrawerOptions
        {
            Title = title,
            Width = $"{width}",
        };
        return options;
    }

    public DrawerService DrawerService { get; } = drawerService;

    public async Task<DrawerRef<TResult>> ShowDrawerAsync<TComponent, TComponentOptions, TResult>(DrawerOptions options,
        TComponentOptions component) where TComponent : FeedbackComponent<TComponentOptions, TResult>
    {
        var x = await DrawerService.CreateAsync<TComponent, TComponentOptions, TResult>(options, component);
        _drawerRefs.Add(x);
        return x;
    }

    /// <summary>
    /// 获取所有弹窗
    /// </summary>
    /// <returns></returns>
    public IList<DrawerRef> GetDrawerRefs()
    {
        //使用样例
        // var refs = PageDrawerService.GetDrawerRefs();
        // foreach (var drawerRef in refs)
        // {
        //     await drawerRef.CloseAsync();
        // }
        return _drawerRefs;
    }
}
