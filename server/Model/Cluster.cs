using System.Collections.Generic;

namespace server.Model
{
    public class Cluster
    {
        public string     ClusterName { get; set; }
        public List<Node> Nodes       { get; set; }
    }
}
