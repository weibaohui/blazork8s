using System;

namespace BlazorApp.Utils;

/// <summary>
/// 转换为core
/// </summary>
public class ResourceQuantityUnitConverter
{
    public static decimal CpuUnitConvert(string value)
    {
        if (value.Contains("u"))
        {
            return Math.Round(Convert.ToDecimal(value.Replace("u", "")) / 1000 / 1000 / 1000, 2);
        }

        if (value.Contains("n"))
        {
            return Math.Round(Convert.ToDecimal(value.Replace("n", "")) / 1000 / 1000, 2);
        }

        if (value.Contains("m"))
        {
            return Convert.ToDecimal(value.Replace("m", ""));
        }

        return Math.Round(Convert.ToDecimal(value) / 1000, 2);
    }

    /// <summary>
    /// 转换为Gi
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static decimal MemoryUnitConvert(string value)
    {
        if (value.Contains("Ki"))
        {
            return Math.Round(Convert.ToDecimal(value.Replace("Ki", "")) / 1024 / 1024, 2);
        }

        if (value.Contains("Mi"))
        {
            return Math.Round(Convert.ToDecimal(value.Replace("Mi", "")) / 1024, 2);
        }

        if (value.Contains("Gi"))
        {
            return Convert.ToDecimal(value.Replace("Gi", ""));
        }

        //如果是从humanValue转换过来，那么humanValue应该是Mi
        return Math.Round(Convert.ToDecimal(value) / 1024, 2);
    }
}
