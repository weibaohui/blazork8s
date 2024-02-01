#nullable enable
using System.Collections.Generic;
using System.Text.Json;
using Entity;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Utils.KubeExplain;

public class KubeExplainHelper
{
    private readonly ILogger<KubeExplainHelper> _logger = LoggingHelper<KubeExplainHelper>.Logger();

    public static KubeExplainHelper         Instance        => Nested.Instance;
    private       IList<KubeExplainEntity>? KubeExplainList { get; set; }

    public KubeExplainHelper()
    {
        KubeExplainList =
            JsonSerializer.Deserialize<IList<KubeExplainEntity>>(KubeExplainDefinition.KubeExplainDefinitionOrigin);
    }

    private class Nested
    {
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Nested()
        {
        }

        internal static readonly KubeExplainHelper Instance = new KubeExplainHelper();
    }


    public IList<KubeExplainEntity>? GetAllKubeExplainList()
    {
        return KubeExplainList;
    }

    public KubeExplainEntity? GetEntityByName(string name)
    {
        return null;
    }
}
