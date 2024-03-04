using System.Collections.Generic;
using System.Threading.Tasks;
using Entity.Crd;
using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface ICustomResourceDefinitionService : ICommonAction<V1CustomResourceDefinition>
{
    Task<List<CustomResource>> GetCrInstanceList(V1CustomResourceDefinition     crd);
    Task<object>               GetCrInstanceWithSpec(V1CustomResourceDefinition crd, CustomResource cr);
}
