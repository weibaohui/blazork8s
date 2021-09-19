using System.Collections.Generic;
using k8s.Models;

namespace Entity
{
    public record NodeVO
    {
        public V1Node       Node { get; set; }
        public IList<V1Pod> Pods { get; set; }
    }
}