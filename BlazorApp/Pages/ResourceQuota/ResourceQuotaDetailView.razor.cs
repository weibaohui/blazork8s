using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using k8s.Models;
using Mapster;

namespace BlazorApp.Pages.ResourceQuota
{
    public partial class ResourceQuotaDetailView : DrawerPageBase<V1ResourceQuota>
    {
        private V1ResourceQuota ResourceQuota { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ResourceQuota = base.Options;
            await base.OnInitializedAsync();
        }

        private V1LabelSelector GetSelector()
        {
            if (ResourceQuota?.Spec?.ScopeSelector?.MatchExpressions is null)
            {
                return null;
            }

            var expressions = ResourceQuota?.Spec?.ScopeSelector?
                .MatchExpressions
                .Select(x => x.Adapt<V1LabelSelectorRequirement>())
                .ToList();
            var labels = new V1LabelSelector()
            {
                MatchLabels = null,
                MatchExpressions = expressions,
            };
            return labels;
        }

        private IDictionary<string, string> Calculate(IDictionary<string, ResourceQuantity> hard,
            IDictionary<string, ResourceQuantity> used)
        {
            var result = new Dictionary<string, string>();
            foreach (var (k, v) in hard)
            {
                used.TryGetValue(k, out var uv);

                if (uv == null) continue;
                result[k] = $"{uv}/{v}";
            }

            return result;
        }
    }
}
