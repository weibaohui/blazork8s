using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using k8s.Models;

namespace BlazorApp.Service
{
    public interface INamespaceService
    {
        Task<V1NamespaceList> List();
        Task<IList<V1Namespace>> GetNamespaces();
    }
}
