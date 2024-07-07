using System.Collections.Generic;
using System.Text.Json.Serialization;
using k8s;
using k8s.Models;

namespace Entity.Crd.Gateway;

public class V1GatewayList : IKubernetesObject<V1ListMeta>, IItems<V1Gateway>
{
    public IList<V1Gateway> Items { get; set; }

    [JsonPropertyName("apiVersion")] public string ApiVersion { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("metadata")] public V1ListMeta Metadata { get; set; }
}

public class V1Gateway : IKubernetesObject<V1ObjectMeta>, ISpec<GatewaySpec>
{
    [JsonPropertyName("status")] public GatewayStatus Status { get; set; }
    [JsonPropertyName("apiVersion")] public string ApiVersion { get; set; }
    [JsonPropertyName("kind")] public string Kind { get; set; }
    [JsonPropertyName("metadata")] public V1ObjectMeta Metadata { get; set; }
    [JsonPropertyName("spec")] public GatewaySpec Spec { get; set; }
}

public class GatewaySpec
{
    [JsonPropertyName("addresses")] public IList<GatewayAddress> Addresses { get; set; }

    [JsonPropertyName("gatewayClassName")] public string GatewayClassName { get; set; }

    [JsonPropertyName("listeners")] public List<Listener> Listeners { get; set; }

    [JsonPropertyName("infrastructure")] public GatewayInfrastructure Infrastructure { get; set; }
}

public class GatewayInfrastructure
{
    [JsonPropertyName("labels")] public Dictionary<string, string> Labels { get; set; }
    [JsonPropertyName("annotations")] public Dictionary<string, string> Annotations { get; set; }
    [JsonPropertyName("parametersRef")] public LocalParametersReference ParametersRef { get; set; }
}

public class LocalParametersReference
{
    [JsonPropertyName("group")] public string Group { get; set; }
    [JsonPropertyName("kind")] public string Kind { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
}

public class GatewayStatus
{
    [JsonPropertyName("addresses")] public List<GatewayStatusAddress> Addresses { get; set; }
    [JsonPropertyName("conditions")] public List<Condition> Conditions { get; set; }
    [JsonPropertyName("listeners")] public List<ListenerStatus> Listeners { get; set; }
}

public class Listener
{
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("hostname")] public string Hostname { get; set; }
    [JsonPropertyName("port")] public int Port { get; set; }
    [JsonPropertyName("protocol")] public ProtocolType Protocol { get; set; }
    [JsonPropertyName("tls")] public GatewayTLSConfig TLS { get; set; }
    [JsonPropertyName("allowedRoutes")] public AllowedRoutes AllowedRoutes { get; set; }
}

public class GatewayTLSConfig
{
    [JsonPropertyName("mode")] public TLSModeType? Mode { get; set; }
    [JsonPropertyName("certificateRefs")] public List<SecretObjectReference> CertificateRefs { get; set; }

    [JsonPropertyName("frontendValidation")]
    public FrontendTLSValidation FrontendValidation { get; set; }

    [JsonPropertyName("options")] public Dictionary<string, string> Options { get; set; }
}

public class FrontendTLSValidation
{
    [JsonPropertyName("caCertificateRefs")]
    public List<ObjectReference> CACertificateRefs { get; set; }
}

public class AllowedRoutes
{
    [JsonPropertyName("namespaces")] public RouteNamespaces Namespaces { get; set; }
    [JsonPropertyName("kinds")] public List<RouteGroupKind> Kinds { get; set; }
}

public class RouteNamespaces
{
    [JsonPropertyName("from")] public FromNamespaces? From { get; set; }
    [JsonPropertyName("selector")] public LabelSelector Selector { get; set; }
}

public class RouteGroupKind
{
    [JsonPropertyName("group")] public string Group { get; set; }
    [JsonPropertyName("kind")] public string Kind { get; set; }
}

public class GatewayAddress
{
    [JsonPropertyName("type")] public AddressType Type { get; set; }
    [JsonPropertyName("value")] public string Value { get; set; }
}

public class GatewayStatusAddress
{
    [JsonPropertyName("type")] public string Type { get; set; }
    [JsonPropertyName("value")] public string Value { get; set; }
}

public class SecretObjectReference
{
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("namespace")] public string Namespace { get; set; }
}

public class ObjectReference
{
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("namespace")] public string Namespace { get; set; }
}

public class LabelSelector
{
    [JsonPropertyName("matchLabels")] public Dictionary<string, string> MatchLabels { get; set; }
    [JsonPropertyName("matchExpressions")] public List<LabelSelectorRequirement> MatchExpressions { get; set; }
}

public class LabelSelectorRequirement
{
    [JsonPropertyName("key")] public string Key { get; set; }
    [JsonPropertyName("operator")] public string Operator { get; set; }
    [JsonPropertyName("values")] public List<string> Values { get; set; }
}

public class ListenerStatus
{
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("supportedKinds")] public List<RouteGroupKind> SupportedKinds { get; set; }
    [JsonPropertyName("attachedRoutes")] public int AttachedRoutes { get; set; }
    [JsonPropertyName("conditions")] public List<Condition> Conditions { get; set; }
}
