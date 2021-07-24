using System;
using System.Collections.Generic;
using k8s.Models;
using Newtonsoft.Json;

namespace Entity
{
    public class JsonPod
    {
        [JsonProperty(PropertyName = "apiVersion")]
        public string ApiVersion { get; set; }

        [JsonProperty(PropertyName = "kind")]
        public string Kind { get; set; }

        [JsonProperty(PropertyName = "metadata")]
        public V1ObjectMeta Metadata { get; set; }

        [JsonProperty(PropertyName = "spec")]
        public JsonPodSpec Spec { get; set; }

        [JsonProperty(PropertyName = "status")]
        public V1PodStatus Status { get; set; }
    }

    public class JsonPodSpec
    {
       [JsonProperty(PropertyName = "activeDeadlineSeconds")]
        public long? ActiveDeadlineSeconds { get; set; }

        [JsonProperty(PropertyName = "affinity")]
        public V1Affinity Affinity { get; set; }

        [JsonProperty(PropertyName = "automountServiceAccountToken")]
        public bool? AutomountServiceAccountToken { get; set; }

        [JsonProperty(PropertyName = "containers")]
        public IList<V1Container> Containers { get; set; }
        [JsonProperty(PropertyName = "ephemeralContainers")]
        public IList<V1EphemeralContainer> EphemeralContainers { get; set; }
        [JsonProperty(PropertyName = "initContainers")]
        public IList<V1Container> InitContainers { get; set; }
        [JsonProperty(PropertyName = "dnsConfig")]
        public V1PodDNSConfig DnsConfig { get; set; }

        [JsonProperty(PropertyName = "dnsPolicy")]
        public string DnsPolicy { get; set; }

        [JsonProperty(PropertyName = "enableServiceLinks")]
        public bool? EnableServiceLinks { get; set; }


        [JsonProperty(PropertyName = "hostAliases")]
        public IList<V1HostAlias> HostAliases { get; set; }

        [JsonProperty(PropertyName = "hostIPC")]
        public bool? HostIPC { get; set; }

        [JsonProperty(PropertyName = "hostNetwork")]
        public bool? HostNetwork { get; set; }

        [JsonProperty(PropertyName = "hostPID")]
        public bool? HostPID { get; set; }

        [JsonProperty(PropertyName = "hostname")]
        public string Hostname { get; set; }

        [JsonProperty(PropertyName = "imagePullSecrets")]
        public IList<V1LocalObjectReference> ImagePullSecrets { get; set; }



        [JsonProperty(PropertyName = "nodeName")]
        public string NodeName { get; set; }

        [JsonProperty(PropertyName = "nodeSelector")]
        public IDictionary<string, string> NodeSelector { get; set; }

        [JsonProperty(PropertyName = "overhead")]
        public IDictionary<string, ResourceQuantity> Overhead { get; set; }

        [JsonProperty(PropertyName = "preemptionPolicy")]
        public string PreemptionPolicy { get; set; }

        [JsonProperty(PropertyName = "priority")]
        public int? Priority { get; set; }

        [JsonProperty(PropertyName = "priorityClassName")]
        public string PriorityClassName { get; set; }

        [JsonProperty(PropertyName = "readinessGates")]
        public IList<V1PodReadinessGate> ReadinessGates { get; set; }

        [JsonProperty(PropertyName = "restartPolicy")]
        public string RestartPolicy { get; set; }

        [JsonProperty(PropertyName = "runtimeClassName")]
        public string RuntimeClassName { get; set; }

        [JsonProperty(PropertyName = "schedulerName")]
        public string SchedulerName { get; set; }

        [JsonProperty(PropertyName = "securityContext")]
        public V1PodSecurityContext SecurityContext { get; set; }

        [JsonProperty(PropertyName = "serviceAccount")]
        public string ServiceAccount { get; set; }

        [JsonProperty(PropertyName = "serviceAccountName")]
        public string ServiceAccountName { get; set; }

        [JsonProperty(PropertyName = "setHostnameAsFQDN")]
        public bool? SetHostnameAsFQDN { get; set; }

        [JsonProperty(PropertyName = "shareProcessNamespace")]
        public bool? ShareProcessNamespace { get; set; }

        [JsonProperty(PropertyName = "subdomain")]
        public string Subdomain { get; set; }

        [JsonProperty(PropertyName = "terminationGracePeriodSeconds")]
        public long? TerminationGracePeriodSeconds { get; set; }

        [JsonProperty(PropertyName = "tolerations")]
        public IList<V1Toleration> Tolerations { get; set; }

        [JsonProperty(PropertyName = "topologySpreadConstraints")]
        public IList<V1TopologySpreadConstraint> TopologySpreadConstraints { get; set; }

        [JsonProperty(PropertyName = "volumes")]
        public IList<V1Volume> Volumes { get; set; }


    }
}
