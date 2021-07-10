using System.Linq;
using k8s.Models;

namespace server.Model
{
    public class Node
    {
        public   string Name       { get; set; }
        internal V1Node OriginNode { get; set; }
       public string HostName =>
            OriginNode.Metadata.Labels.AsEnumerable().FirstOrDefault(r => r.Key == "kubernetes.io/hostname").Value;
        public string ClusterName     => OriginNode.Metadata.ClusterName;
        public string KubeletVersion  => OriginNode.Status.NodeInfo.KubeletVersion;
        public string OperatingSystem => OriginNode.Status.NodeInfo.OperatingSystem;
        public string Architecture    => OriginNode.Status.NodeInfo.Architecture;
    }
}
