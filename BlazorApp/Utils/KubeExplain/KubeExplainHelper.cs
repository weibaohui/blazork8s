#nullable enable
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Entity;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Utils.KubeExplain;

public class KubeExplainHelper
{
    private readonly ILogger<KubeExplainHelper> _logger = LoggingHelper<KubeExplainHelper>.Logger();

    public static KubeExplainHelper Instance => Nested.Instance;

    private IList<KubeExplainRef>? KubeExplainRefList { get; set; } =
        JsonSerializer.Deserialize<IList<KubeExplainRef>>(File.ReadAllText("refList.json"));

    private IList<KubeExplainEN>? KubeExplainEnList { get; set; } =
        JsonSerializer.Deserialize<IList<KubeExplainEN>>(File.ReadAllText("enList.json"));

    private IList<KubeExplainCN>? KubeExplainCnList { get; set; } =
        JsonSerializer.Deserialize<IList<KubeExplainCN>>(File.ReadAllText("cnList.json"));

    private class Nested
    {
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Nested()
        {
        }

        internal static readonly KubeExplainHelper Instance = new KubeExplainHelper();
    }


    public KubeExplainEntity? GetExplainByField(string fieldPath)
    {
        if (KubeExplainRefList is { Count: 0 }) return null;
        var rf = KubeExplainRefList?.First(x => x.ExplainFiled == fieldPath);
        if (rf == null)
        {
            return null;
        }

        var enId = rf.EnId;
        var cnId = rf.CnId;

        var en = KubeExplainEnList?.ToList().Find(x => x.Id == enId);
        var cn = KubeExplainCnList?.ToList().Find(x => x.Id == cnId);

        return new KubeExplainEntity()
        {
            Explain      = en?.Explain,
            ExplainCN    = cn?.Explain,
            ExplainFiled = fieldPath
        };
    }
}
