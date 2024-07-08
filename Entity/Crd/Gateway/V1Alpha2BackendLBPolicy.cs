using System.Collections.Generic;
using System.Text.Json.Serialization;
using k8s;
using k8s.Models;

namespace Entity.Crd.Gateway;

public class V1Alpha2BackendLBPolicyList : IKubernetesObject<V1ListMeta>, IItems<V1Alpha2BackendLBPolicy>
{
    [JsonPropertyName("items")] public IList<V1Alpha2BackendLBPolicy> Items { get; set; }
    [JsonPropertyName("apiVersion")] public string ApiVersion { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("metadata")] public V1ListMeta Metadata { get; set; }
}

[KubernetesEntity(Group = KubeGroup, Kind = KubeKind, ApiVersion = KubeApiVersion, PluralName = KubePluralName)]
public class V1Alpha2BackendLBPolicy : IKubernetesObject<V1ObjectMeta>, ISpec<BackendLBPolicySpec>
{
    public const string KubeApiVersion = "v1alpha2";
    public const string KubeKind = "BackendLBPolicy";
    public const string KubeGroup = "gateway.networking.k8s.io";
    public const string KubePluralName = "backendlbpolicies";
    [JsonPropertyName("status")] public BackendLBPolicyStatus Status { get; set; }
    [JsonPropertyName("apiVersion")] public string ApiVersion { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("metadata")] public V1ObjectMeta Metadata { get; set; }

    [JsonPropertyName("spec")] public BackendLBPolicySpec Spec { get; set; }
}

public class BackendLBPolicySpec
{
    [JsonPropertyName("targetRefs")] public IList<LocalPolicyTargetReference> TargetRefs { get; set; }

    [JsonPropertyName("sessionPersistence")]
    public SessionPersistence SessionPersistence { get; set; }
}

public class LocalPolicyTargetReference
{
    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("group")] public string Group { get; set; }
}

public class BackendLBPolicyStatus
{
    [JsonPropertyName("ancestors")] public IList<PolicyAncestorStatus> Ancestors { get; set; }
}

public class PolicyAncestorStatus
{
    [JsonPropertyName("ancestorRef")] public ParentReference AncestorRef { get; set; }
    [JsonPropertyName("controllerName")] public string ControllerName { get; set; }
    [JsonPropertyName("conditions")] public IList<Condition> Conditions { get; set; }
}
