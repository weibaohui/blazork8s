using System.Collections.Generic;
using k8s.Models;

namespace Entity
{
    public class NodeVO
    {
        public V1Node       Node { get; set; }
        public IList<V1Pod> Pods { get; set; }
        public IList<V1beta1EventList> Events { get; set; }
    }
}
