using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using k8s.Models;

namespace Entity
{
    public class NodeVO
    {
        public string                      Name            { get; set; }
        public string                      HostName        { get; set; }
        public IDictionary<string, string> Capacity        { get; set; }
        public IDictionary<string, string> Allocatable     { get; set; }
        public string                      ClusterName     { get; set; }
        public string                      KubeletVersion  { get; set; }
        public string                      OperatingSystem { get; set; }
        public string                      Architecture    { get; set; }
        public IList<V1NodeAddress>        Addresses       { get; set; }
        public IList<V1NodeCondition>      Conditions      { get; set; }
    }
}
