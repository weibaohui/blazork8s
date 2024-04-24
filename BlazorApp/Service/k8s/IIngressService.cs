using System.Collections.Generic;
using System.Threading.Tasks;
using Entity.Analyze;
using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface IIngressService : ICommonAction<V1Ingress>
{
    Task<List<Result>> Analyze();

    IList<V1Ingress> ListByServiceList(IList<V1Service> services);
    string GetRulePathDisplayUrl(V1IngressRule rule, V1HTTPIngressPath path, IList<V1IngressTLS> specTls);
}
