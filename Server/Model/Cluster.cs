using System.Collections.Generic;
using Entity;

namespace Server.Model
{
    public class Cluster
    {
        public string     ClusterName { get; set; }
        public List<Node> Nodes       { get; set; }
    }
}
