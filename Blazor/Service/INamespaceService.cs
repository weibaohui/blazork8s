using System;
using System.Threading.Tasks;
using k8s.Models;

namespace Blazor.Service
{
    public interface INamespace
    {
        Task<V1NamespaceList> List();
    }
}
