using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using Extension;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Namespace;

public partial class NamespaceLimitRange : DrawerPageBase<V1Namespace>
{
    private List<string> _keys = new List<string>();
    private Dictionary<string, ResourceQuantity> _maxQuotas = new Dictionary<string, ResourceQuantity>();
    private Dictionary<string, ResourceQuantity> _minQuotas = new Dictionary<string, ResourceQuantity>();
    [Inject] ILimitRangeService LimitRangeService { get; set; }
    private V1Namespace Namespace { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Namespace = base.Options;
        LoadLimitRange();
        await base.OnInitializedAsync();
    }

    private void LoadLimitRange()
    {
        //计算最大值
        var quotas = LimitRangeService.ListByNamespace(Namespace.Metadata.Name);
        foreach (var q in quotas)
        {
            foreach (var limit in q.Spec.Limits)
            {
                if (limit.Max == null)
                {
                    continue;
                }

                foreach (var (key, value) in limit.Max)
                {
                    var nkey = limit.Type + "." + key;
                    if (!_keys.Contains(nkey))
                    {
                        _keys.Add(nkey);
                    }

                    if (!_maxQuotas.TryAdd(nkey, value))
                    {
                        // 如果添加不进去，说明 key 已经存在，则取最小值作为最终限制
                        if (value.HumanizeValue() < _maxQuotas[nkey].HumanizeValue())
                        {
                            _maxQuotas[nkey] = value;
                        }
                    }
                }
            }
        }

        _maxQuotas = _maxQuotas.OrderBy(pair => pair.Key).ToDictionary(pair => pair.Key, pair => pair.Value);

        //计算最小值
        foreach (var q in quotas)
        {
            foreach (var limit in q.Spec.Limits)
            {
                if (limit.Min == null)
                {
                    continue;
                }

                foreach (var (key, value) in limit.Min)
                {
                    var nkey = limit.Type + "." + key;
                    if (!_keys.Contains(nkey))
                    {
                        _keys.Add(nkey);
                    }

                    if (!_minQuotas.TryAdd(nkey, value))
                    {
                        // 如果添加不进去，说明 key 已经存在，则取最小值作为最终限制
                        if (value.HumanizeValue() < _minQuotas[nkey].HumanizeValue())
                        {
                            _minQuotas[nkey] = value;
                        }
                    }
                }
            }
        }

        _minQuotas = _minQuotas.OrderBy(pair => pair.Key).ToDictionary(pair => pair.Key, pair => pair.Value);
    }
}
