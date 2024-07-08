#nullable enable
using System.Collections.Generic;
using Entity.Crd.Gateway;
using k8s;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Utils.Swagger;

public class SwaggerHelper
{
    private readonly ILogger<SwaggerHelper> _logger = LoggingHelper<SwaggerHelper>.Logger();

    public static SwaggerHelper Instance => Nested.Instance;
    private SwaggerEntity Entity { get; set; }
    private Dictionary<string, SwaggerEntity> entities = new();

    public enum EntityType
    {
        K8SInTree,
        Gateway,
        Istio
    }

    /// <summary>
    /// 构造函数，初始化Swagger实体，可参考Gateway方式初始化其他的CRD文档
    /// </summary>
    private SwaggerHelper()
    {
        entities.Add(EntityType.K8SInTree.ToString(),
            KubernetesJson.Deserialize<SwaggerEntity>(SwaggerDefinition.KubeSwaggerDefinitionCN));
        entities.Add(EntityType.Gateway.ToString(),
            KubernetesJson.Deserialize<SwaggerEntity>(GatewaySwaggerDefinition.Definition));
    }

    private class Nested
    {
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Nested()
        {
        }

        internal static readonly SwaggerHelper Instance = new SwaggerHelper();
    }


    public SwaggerEntity GetAllEntity(EntityType entityType)
    {
        Entity = entities[entityType.ToString()];
        return Entity;
    }

    public Definition? GetEntityByName(EntityType entityType, string name)
    {
        Entity = entities[entityType.ToString()];

        Definition? d = null;
        var b = Entity.definitions?.TryGetValue(name, out d);
        if (b == false)
        {
            return null;
        }

        if (d == null)
        {
            return null;
        }

        d.name = name;
        return d;
    }


    /// <summary>
    /// 提取Item Name
    /// #/definitions/io.k8s.api.apps.v1.DeploymentSpec =>io.k8s.api.apps.v1.DeploymentSpec
    /// </summary>
    /// <param name="fullRef"></param>
    /// <returns></returns>
    public string GetRefKey(string? fullRef)
    {
        if (fullRef == null)
        {
            return "";
        }

        var refs = fullRef.Split("/");
        var length = refs.Length;
        return refs[length - 1];
    }
}
