using System.Collections.Generic;
using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface IRoleService : ICommonAction<V1Role>
{
      IList<V1Subject> ListManagedSubjectByRole(V1Role role);

}
