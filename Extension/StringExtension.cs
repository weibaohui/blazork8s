using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Extension;

public static class StringExtensions
{
    public static string ToHtmlDisplay(this string? str)
    {
        return str?.Replace("\n", "<br/>")
                   .Replace("\r", "<br/>")
                   .Replace(" ", "&nbsp;&nbsp;")
               ?? string.Empty;
    }
    public static string ToMd5Str(this string input)
    {
        // Use input string to calculate MD5 hash
        MD5    md5        = MD5.Create();
        var inputBytes = Encoding.ASCII.GetBytes(input);
        var hashBytes  = md5.ComputeHash(inputBytes);

        // Convert the byte array to hexadecimal string
        StringBuilder sb = new StringBuilder();
        foreach (var t in hashBytes)
        {
            sb.Append(t.ToString("X2"));
            // To force the hex string to lower-case letters instead of
            // upper-case, use he following line instead:
            // sb.Append(hashBytes[i].ToString("x2"));
        }
        return sb.ToString();
    }
    public static long ToMd5Digits(this string str)
    {
        return Convert.ToInt64(str.ToMd5Str().RemoveStringOfNonDigits());
    }
    /// <summary>
    /// 去除非数字字符，只保留数字
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string RemoveStringOfNonDigits(this string str)
    {
        return new string(str.Where(char.IsDigit).ToArray());
    }

    /// <summary>
    /// 字符串转换为下划线
    /// SelfLink=>self_link
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string ToSnakeCaseLower(this string str)
    {
        return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
    }
    /// <summary>
    /// 字符串转换为中划线
    /// SelfLink=>self-link
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string ToKebabCaseLower(this string str)
    {
        return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "-" + x.ToString() : x.ToString())).ToLower();
    }

    /// <summary>
    /// 字符串转换为驼峰
    /// SelfLink=>selfLink
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string ToCamelCase(this string str)
    {
        if (!string.IsNullOrEmpty(str) && str.Length > 1)
        {
            return char.ToLowerInvariant(str[0]) + str[1..];
        }
        return str.ToLowerInvariant();
    }
    /// <summary>
    /// 字符串转换为帕斯卡
    /// selfLink=>SelfLink
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string ToPascalCase(this string str)
    {
        if (!string.IsNullOrEmpty(str) && str.Length > 1)
        {
            return char.ToUpperInvariant(str[0]) + str[1..];
        }

        return str.ToUpperInvariant();
    }
    public static string ToCamelCase(this string str, bool hasDot)
    {
        if (!hasDot)
        {
            return str.ToCamelCase();
        }
        return string.Join(".",
            str.Split('.')
                .AsEnumerable()
                .Select(ToCamelCase)
                .ToArray()
        );
    }
    public static string ToPascalCase(this string str, bool hasDot)
    {
        if (!hasDot)
        {
            return str.ToPascalCase();
        }
        return string.Join(".",
            str.Split('.')
                .AsEnumerable()
                .Select(ToPascalCase)
                .ToArray()
        );
    }
    public static int CountBy(this string str, string value)
    {
        return str.Count(i => value.Contains(i));
    }

}
