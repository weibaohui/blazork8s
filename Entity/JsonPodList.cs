using System;
using System.Collections.Generic;
using k8s.Models;
using Newtonsoft.Json;

namespace Entity
{
    public class JsonPodList
    {
        [JsonProperty(PropertyName = "apiVersion")]
        public string ApiVersion { get; set; }

        [JsonProperty(PropertyName = "kind")]
        public string Kind { get; set; }

        [JsonProperty(PropertyName = "metadata")]
        public V1ObjectMeta Metadata { get; set; }

        [JsonProperty(PropertyName = "items")]
        public IList<JsonPod> Items { get; set; }

    }

    
}
