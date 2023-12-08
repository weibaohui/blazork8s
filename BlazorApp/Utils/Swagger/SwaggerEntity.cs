#nullable enable
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlazorApp.Utils.Swagger;

public class SwaggerEntity
{
    public IDictionary<string, Definition> definitions { get; set; }
}

public class Definition
{
    public string                         name        { get; set; }
    public string                         description { get; set; }
    public IDictionary<string, Property>? properties  { get; set; }
    public string                         type        { get; set; }
    public string?                        format      { get; set; }

}

public class RefItem
{
    [JsonPropertyName("$ref")]
    public string? Ref { get; set; }
}

public class Property
{
    public string   description { get; set; }
    public string   type        { get; set; }
    public string?  format      { get; set; }
    public RefItem? items       { get; set; }



    [JsonPropertyName("$ref")]
    public string? Ref { get; set; }



    [JsonPropertyName("x-kubernetes-list-map-keys")]
    public string[]? XKubernetesListMapKeys { get; set; }

    [JsonPropertyName("x-kubernetes-list-type")]
    public string? XKubernetesListType { get; set; }

    [JsonPropertyName("x-kubernetes-patch-strategy")]
    public string? XKubernetesPatchStrategy { get; set; }
}
