using System;
using System.Collections.Generic;
using Entity.Analyze;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Utils;

public class ClusterInspectionResultContainer
{
    private static readonly ILogger<ClusterInspectionResultContainer> Logger =
        LoggingHelper<ClusterInspectionResultContainer>.Logger();

    private static readonly List<Result> Results = [];

    public        DateTime                         LastInspection;
    public static ClusterInspectionResultContainer Instance => Nested.Instance;

    private class Nested
    {
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Nested()
        {
        }

        internal static readonly ClusterInspectionResultContainer Instance = new ClusterInspectionResultContainer();
    }

    public List<Result> GetResults()
    {
        return Results;
    }
}
