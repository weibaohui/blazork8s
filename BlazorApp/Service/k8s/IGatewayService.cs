using System.Collections.Generic;
using Entity.Crd.Gateway;

namespace BlazorApp.Service.k8s;

public interface IGatewayService : ICommonAction<V1Gateway>
{
    IList<V1Gateway> ListByParentRefs(IList<ParentReference> parentRefs);
}
