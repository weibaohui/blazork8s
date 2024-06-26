using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Utils;
using k8s;
using k8s.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Service.k8s.impl;

public abstract class CommonAction<T> : ICommonAction<T> where T : IKubernetesObject<V1ObjectMeta>
{
    private readonly ResourceCache<T> _cache = ResourceCacheHelper<T>.Instance.Build();
    private readonly ILogger<CommonAction<T>> _logger = LoggingHelper<CommonAction<T>>.Logger();
    private readonly IServiceScope _scope;

    public CommonAction(IServiceScopeFactory serviceScopeFactory)
    {
        _scope = serviceScopeFactory.CreateScope();
        // _watchService = _scope.ServiceProvider.GetService<IWatchService>();
        // Console.WriteLine("PodService 初始化");
    }

    protected CommonAction()
    {
    }


    public bool Changed()
    {
        return _cache.Changed();
    }

    public IList<T> ListByOwnerUid(string controllerByUid)
    {
        return List().Where(x => x.GetController() != null && x.GetController().Uid == controllerByUid)
            .ToList();
    }

    public T GetByUid(string uid)
    {
        return List().FirstOrDefault(x => x.Uid() == uid);
    }

    public IList<T> ListByNamespace(string ns)
    {
        return List().Where(x => x.Namespace() == ns).ToList();
    }

    public T GetByName(string name)
    {
        return List().FirstOrDefault(x => x.Name() == name);
    }

    public T GetByName(string ns, string name)
    {
        return List().FirstOrDefault(x => x.Name() == name && x.Namespace() == ns);
    }


    public IList<T> List()
    {
        return _cache.Get();
    }

    public Task<object> Delete(string ns, string name)
    {
        _logger.LogError("CommonAction Delete 请在继承类方法中实现");
        return (Task<object>)Task.CompletedTask;
    }
}