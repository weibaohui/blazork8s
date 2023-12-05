using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class ReplicationControllerService : CommonAction<V1ReplicationController>, IReplicationControllerService
{
    private readonly IBaseService                _baseService;

    public ReplicationControllerService(IBaseService baseService)
    {
        _baseService = baseService;
    }
}
