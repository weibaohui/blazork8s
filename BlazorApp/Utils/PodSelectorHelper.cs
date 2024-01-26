using System.Collections.Generic;
using System.Linq;

namespace BlazorApp.Utils;

public static class PodSelectorHelper
{

    public static string ToFilter(IDictionary<string, string> selector)
    {
        if (selector is null || selector.Count == 0)
        {
            return string.Empty;
        }

        return string.Join(",", selector.Select(kv => $"{kv.Key}={kv.Value}"));
    }
}
