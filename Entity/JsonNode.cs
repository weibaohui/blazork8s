using System;
using System.Collections.Generic;
using k8s.Models;
using Newtonsoft.Json;

namespace Entity
{
    public class JsonNode
    {
        [JsonProperty(PropertyName = "apiVersion")]
        public string ApiVersion { get; set; }

        [JsonProperty(PropertyName = "kind")]
        public string Kind { get; set; }

        [JsonProperty(PropertyName = "metadata")]
        public V1ObjectMeta Metadata { get; set; }

        [JsonProperty(PropertyName = "spec")]
        public V1NodeSpec Spec { get; set; }

        [JsonProperty(PropertyName = "status")]
        public JsonNodeStatus Status { get; set; }
    }

    public class JsonNodeStatus
    {
        [JsonProperty(PropertyName = "addresses")]
        public IList<V1NodeAddress> Addresses { get; set; }

        [JsonProperty(PropertyName = "allocatable")]
        public IDictionary<string, String> Allocatable { get; set; }

        [JsonProperty(PropertyName = "capacity")]
        public IDictionary<string, String> Capacity { get; set; }

        [JsonProperty(PropertyName = "conditions")]
        public IList<V1NodeCondition> Conditions { get; set; }

        [JsonProperty(PropertyName = "config")]
        public V1NodeConfigStatus Config { get; set; }

        [JsonProperty(PropertyName = "daemonEndpoints")]
        public V1NodeDaemonEndpoints DaemonEndpoints { get; set; }

        [JsonProperty(PropertyName = "images")]
        public IList<V1ContainerImage> Images { get; set; }

        [JsonProperty(PropertyName = "nodeInfo")]
        public V1NodeSystemInfo NodeInfo { get; set; }

        [JsonProperty(PropertyName = "phase")]
        public string Phase { get; set; }

        [JsonProperty(PropertyName = "volumesAttached")]
        public IList<V1AttachedVolume> VolumesAttached { get; set; }

        [JsonProperty(PropertyName = "volumesInUse")]
        public IList<string> VolumesInUse { get; set; }
    }
}
