using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using k8s.Models;

namespace Entity
{
    public class Node
    {
        public string Name { get; set; }

        [JsonIgnore]
        public V1Node OriginNode { get; set; }

        public IDictionary<string, string> Capacity    { get; set; } = new Dictionary<string, string>();
        public IDictionary<string, string> Allocatable { get; set; } = new Dictionary<string, string>();

        public string HostName =>
            OriginNode.Metadata.Labels.AsEnumerable().FirstOrDefault(r => r.Key == "kubernetes.io/hostname").Value;

        public string                 ClusterName     => OriginNode.Metadata.ClusterName;
        public string                 KubeletVersion  => OriginNode.Status.NodeInfo.KubeletVersion;
        public string                 OperatingSystem => OriginNode.Status.NodeInfo.OperatingSystem;
        public string                 Architecture    => OriginNode.Status.NodeInfo.Architecture;
        public IList<V1NodeCondition> Conditions       => OriginNode.Status.Conditions;
        public IList<V1NodeAddress>   Addresses       => OriginNode.Status.Addresses;
     }
}
