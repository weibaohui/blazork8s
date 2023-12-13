#nullable enable
using k8s;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Utils.Swagger;

public class SwaggerHelper
{
    private readonly ILogger<SwaggerHelper> _logger = LoggingHelper<SwaggerHelper>.Logger();

    public static SwaggerHelper Instance => Nested.Instance;
    private       SwaggerEntity Entity   { get; set; }

    public SwaggerHelper()
    {
        Entity = KubernetesJson.Deserialize<SwaggerEntity>(SwaggerDefinition.KubeSwaggerDefinition);
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

    public Definition? GetEntityByName(string name)
    {
        if (Entity == null)
        {
            return null;
        }

        Definition? d = null;
        var         b = Entity.definitions?.TryGetValue(name, out  d);
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

        var refs   = fullRef.Split("/");
        var length = refs.Length;
        return refs[length - 1];
    }
}
