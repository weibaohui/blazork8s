using System;
using k8s.Models;

namespace Extension;

public static class ResourceQuantityExtension
{
    /// <summary>
    /// 提高可读性，单位统一到m、Mi
    /// cpu:1000m=1core
    /// memory:1024Mi=1Gi
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string Humanize(this ResourceQuantity value)
    {
        return value.Format switch
        {
            ResourceQuantity.SuffixFormat.DecimalSI => Math.Round(value.ToDecimal() * 1000, 2) + "m",
            ResourceQuantity.SuffixFormat.BinarySI  => Math.Round(value.ToDecimal() / 1024 / 1024, 2) + "Mi",
            _                                       => value.CanonicalizeString(ResourceQuantity.SuffixFormat.BinarySI)
        };
    }


    public static decimal HumanizeValue(this ResourceQuantity value)
    {
        return value.Format switch
        {
            ResourceQuantity.SuffixFormat.DecimalSI => Math.Round(value.ToDecimal() * 1000, 2),
            ResourceQuantity.SuffixFormat.BinarySI  => Math.Round(value.ToDecimal() / 1024 / 1024, 2),
            _                                       => value.ToDecimal()
        };
    }
}
