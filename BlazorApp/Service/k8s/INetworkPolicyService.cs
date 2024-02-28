using System.Collections.Generic;
using System.Threading.Tasks;
using Entity.Analyze;
using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface INetworkPolicyService : ICommonAction<V1NetworkPolicy>
{
     Task<List<Result>> Analyze();

}
