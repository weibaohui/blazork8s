using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using Extension;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Namespace;

public partial class NamespaceResourceQuota : DrawerPageBase<V1Namespace>
{
    private Dictionary<string, ResourceQuantity> _finalQuotas = new Dictionary<string, ResourceQuantity>();
    private Dictionary<string, ResourceQuantity> _usedQuotas = new Dictionary<string, ResourceQuantity>();
    [Inject] IResourceQuotaService ResourceQuotaService { get; set; }
    private V1Namespace Namespace { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Namespace = base.Options;
        LoadResourceQuota();
        await base.OnInitializedAsync();
    }

    private void LoadResourceQuota()
    {
        // 当对一个 namespace 设置了多个 resource quota 时，Kubernetes 会根据所有 resource quota 的交集来决定最终的资源限制。换句话说，最终的限制是各个 quota 中最严格的限制条件的组合。//
        // 取所有相关 resource quota 中的最小值作为最终的限制。
        // Pods 总数：两个 quota 分别是 10 和 15，取最小值为 10。
        // CPU requests 总量：分别是 4 和 3，取最小值为 3。
        // CPU limits 总量：分别是 8 和 6，取最小值为 6。
        var quotas = ResourceQuotaService.ListByNamespace(Namespace.Metadata.Name);
        foreach (var q in quotas)
        {
            foreach (var (key, value) in q.Spec.Hard)
            {
                if (!_finalQuotas.TryAdd(key, value))
                {
                    // 如果添加不进去，说明 key 已经存在，则取最小值作为最终限制
                    if (value.HumanizeValue() < _finalQuotas[key].HumanizeValue())
                    {
                        _finalQuotas[key] = value;
                    }
                }
            }
        }

        _finalQuotas = _finalQuotas.OrderBy(pair => pair.Key).ToDictionary(pair => pair.Key, pair => pair.Value);


        // 计算已使用的资源
        foreach (var q in quotas)
        {
            foreach (var (key, value) in q.Status.Used)
            {
                if (!_usedQuotas.TryAdd(key, value))
                {
                    // 如果添加不进去，说明 key 已经存在，则取最大值作为已使用的资源
                    if (value.HumanizeValue() > _usedQuotas[key].HumanizeValue())
                    {
                        _usedQuotas[key] = value;
                    }
                }
            }
        }

        _usedQuotas = _usedQuotas.OrderBy(pair => pair.Key).ToDictionary(pair => pair.Key, pair => pair.Value);
    }
}
