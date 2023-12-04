using System.Collections.Generic;
using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface ICommonAction<T> where T : IKubernetesObject<V1ObjectMeta>
{
    public bool     Changed();
    public IList<T> ListByOwnerUid(string  controllerByUid);
    public T        GetByUid(string        uid);
    public IList<T> ListByNamespace(string name);
    public IList<T> List();
    public T        GetByName(string name);
    public Task<T>  Delete(string    ns, string name);
}
