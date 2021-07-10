using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using k8s.Models;

namespace ui.Pages
{
    public class Node
    {
        public string               Name            { get; set; }
        public string               HostName        { get; set; }
        public V1Node               OriginNode      { get; set; }
        public string               ClusterName     { get; set; }
        public string               KubeletVersion  { get; set; }
        public string               OperatingSystem { get; set; }
        public string               Architecture    { get; set; }
        public IList<V1NodeAddress> Addresses       { get; set; }
    }
}
