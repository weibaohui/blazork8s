using System.Collections.Generic;
using System.Text.Json.Serialization;
using k8s;
using k8s.Models;

namespace Entity.Crd.Gateway;

public class V1Alpha3BackendTLSPolicyList : IKubernetesObject<V1ListMeta>, IItems<V1Alpha3BackendTLSPolicy>
{
    [JsonPropertyName("items")] public IList<V1Alpha3BackendTLSPolicy> Items { get; set; }
    [JsonPropertyName("apiVersion")] public string ApiVersion { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("metadata")] public V1ListMeta Metadata { get; set; }
}

[KubernetesEntity(Group = KubeGroup, Kind = KubeKind, ApiVersion = KubeApiVersion, PluralName = KubePluralName)]
public class V1Alpha3BackendTLSPolicy : IKubernetesObject<V1ObjectMeta>, ISpec<BackendTLSPolicySpec>
{
    public const string KubeApiVersion = "v1alpha3";
    public const string KubeKind = "BackendTLSPolicy";
    public const string KubeGroup = "gateway.networking.k8s.io";
    public const string KubePluralName = "backendtlspolicies";
    [JsonPropertyName("status")] public BackendTLSPolicyStatus Status { get; set; }
    [JsonPropertyName("apiVersion")] public string ApiVersion { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("metadata")] public V1ObjectMeta Metadata { get; set; }

    [JsonPropertyName("spec")] public BackendTLSPolicySpec Spec { get; set; }
}

public class BackendTLSPolicySpec
{
    [JsonPropertyName("targetRefs")] public IList<LocalPolicyTargetReferenceWithSectionName> TargetRefs { get; set; }
    [JsonPropertyName("validation")] public BackendTLSPolicyValidation Validation { get; set; }
}

public class BackendTLSPolicyValidation
{
    [JsonPropertyName("caCertificateRefs")]
    public IList<LocalObjectReference> CACertificateRefs { get; set; }

    [JsonPropertyName("wellKnownCACertificates")]
    public WellKnownCACertificatesType WellKnownCACertificates { get; set; }

    [JsonPropertyName("hostname")] public string Hostname { get; set; }
}

public enum WellKnownCACertificatesType
{
    System
}

public class LocalPolicyTargetReferenceWithSectionName
{
    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("group")] public string Group { get; set; }
    [JsonPropertyName("sectionName")] public string SectionName { get; set; }
}

public class BackendTLSPolicyStatus
{
    [JsonPropertyName("ancestors")] public IList<PolicyAncestorStatus> Ancestors { get; set; }
}
