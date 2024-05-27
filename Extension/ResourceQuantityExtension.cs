using System;
using k8s.Models;

namespace Extension;

public static class ResourceQuantityExtension
{
    /// <summary>
    /// 提高可读性，单位统一到core、Gi
    /// cpu:1000m=1core
    /// memory:1024Mi=1Gi
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string Humanize(this ResourceQuantity value)
    {
        return value.Format switch
        {
            ResourceQuantity.SuffixFormat.DecimalSI => Math.Round(value.ToDecimal(), 2) + " Core",
            ResourceQuantity.SuffixFormat.BinarySI => Math.Round(value.ToDecimal() / 1024 / 1024 / 1024, 2) + "GiB",
            _ => value.CanonicalizeString(ResourceQuantity.SuffixFormat.BinarySI),
        };
    }

    /// <summary>
    /// 将类似1800m、1000Mi的字符串转化为可读性更好的字符串
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private static string ConvertString2Humanize(string str)
    {
        var ret = "";
        if (str.EndsWith("m"))
        {
            ret = Math.Round(double.Parse(str.Substring(0, str.Length - 1)) / 1000, 2) + " Core";
        }
        else if (str.EndsWith("M") || str.EndsWith("Mi"))
        {
            ret = Math.Round(double.Parse(str.Substring(0, str.Length - 2)) / 1024, 2) + " GiB";
        }

        return ret;
    }


    public static decimal HumanizeValue(this ResourceQuantity value)
    {
        return value.Format switch
        {
            ResourceQuantity.SuffixFormat.DecimalSI => Math.Round(value.ToDecimal() * 1000, 2),
            ResourceQuantity.SuffixFormat.BinarySI => Math.Round(value.ToDecimal() / 1024 / 1024, 2),
            _ => value.ToDecimal()
        };
    }
}
