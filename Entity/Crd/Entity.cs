using System.Collections.Generic;
using System.Text.Json.Serialization;
using k8s;
using k8s.Models;

namespace Entity.Crd;

public class CustomResource : KubernetesObject, IMetadata<V1ObjectMeta>
{
    [JsonPropertyName("metadata")]
    public V1ObjectMeta Metadata { get; set; }
}

public abstract class CustomResource<TSpec, TStatus> : CustomResource
{
    [JsonPropertyName("spec")]
    public TSpec Spec { get; set; }

    [JsonPropertyName("status")]
    public TStatus Status { get; set; }
}

public class CustomResourceList<T> : KubernetesObject
    where T : CustomResource
{
    public V1ListMeta Metadata { get; set; }
    public List<T>    Items    { get; set; }
}
