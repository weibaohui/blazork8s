using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Markdig;

namespace Extension;

public static class StringExtensions
{
    public static string ToHtmlDisplay(this string? str)
    {
        if (str == null) return "";
        //强化link类型的字符串识别
        str = str.Replace("https://", " https://")
            .Replace("http://", " http://")
            .Replace("ftp://", " ftp://")
            .Replace("www.", " www.");
        var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
        str = Markdown.ToHtml(str, pipeline);
        str = str.Replace("\\n", "<br/>")
                .Replace("\\r", "<br/>")
                .Replace("\r", "<br/>")
                .Replace("\n", "<br/>")
                .Replace("\\u003c", "<")
                .Replace("\\u003e", ">")
                .Replace("\\\"", "\"")
                .Replace("\\r\\n", "<br/>")
                .Replace("\\t", "&nbsp;&nbsp;&nbsp;&nbsp;")
            ;
        return str;
    }

    public static string ToHtmlDisplayNoMarkdown(this string? str)
    {
        if (str == null) return "";
        str = str.Replace("\\n", "<br/>")
                .Replace("\\r", "<br/>")
                .Replace("\r", "<br/>")
                .Replace("\n", "<br/>")
                .Replace("\\u003c", "<")
                .Replace("\\u003e", ">")
                .Replace("\\r\\n", "<br/>")
                .Replace("\\\"", "\"")
                .Replace(" ", "&nbsp;&nbsp;")
                .Replace("\\t", "&nbsp;&nbsp;&nbsp;&nbsp;")
                .Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;")
            ;
        return str;
    }

    public static string ToMd5Str(this string input)
    {
        // Use input string to calculate MD5 hash
        var md5 = MD5.Create();
        var inputBytes = Encoding.ASCII.GetBytes(input);
        var hashBytes = md5.ComputeHash(inputBytes);

        // Convert the byte array to hexadecimal string
        var sb = new StringBuilder();
        foreach (var t in hashBytes)
        {
            sb.Append(t.ToString("X2"));
            // To force the hex string to lower-case letters instead of
            // upper-case, use the following line instead:
            // sb.Append(hashBytes[i].ToString("x2"));
        }

        return sb.ToString();
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="input"></param>
    /// <param name="maxLength"></param>
    /// <returns></returns>
    public static string Truncate(this string input, int maxLength)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        if (input.Length <= maxLength)
        {
            return input;
        }

        return input.Substring(0, maxLength) + "...";
    }

    public static long ToNumeric(this string input)
    {
        // ASCII 码的范围是 0 到 127
        // 通过乘以 128，可以确保任何一个字符的 ASCII 码在生成的数字值中都有足够的空间表示，而不会与其他字符的 ASCII 码相互重叠。
        // 这种方式可以使得生成的数字值在一定程度上保持唯一性，尽管这并不是绝对的，因为仍然存在可能的冲突，特别是对于较短的字符串或较小的数字范围。

        return Math.Abs(input.Aggregate<char, long>(0, (current, c) => current * 2 + c));
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
    /// 去除字符串最后的换行符
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string TrimEndNewLine(this string str)
    {
        var ret = Regex.Replace(str, @"\r\n$", string.Empty);
        ret = Regex.Replace(str, @"\r$", string.Empty);
        ret = Regex.Replace(str, @"\n$", string.Empty);
        return ret;
    }

    /// <summary>
    /// 字符串转换为下划线
    /// SelfLink=>self_link
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string ToSnakeCaseLower(this string str)
    {
        return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString()))
            .ToLower();
    }

    /// <summary>
    /// 字符串转换为中划线
    /// SelfLink=>self-link
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string ToKebabCaseLower(this string str)
    {
        return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "-" + x.ToString() : x.ToString()))
            .ToLower();
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
        return str.Count(value.Contains);
    }

    /// <summary>
    /// A function that takes a string input and returns the substring after the first digit found in the input string.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string GetSubstringAfterFirstDigit(this string input)
    {
        // 使用正则表达式找到第一个数字的索引
        var match = Regex.Match(input, @"\d");

        if (match.Success)
        {
            // 找到第一个数字后面的部分
            int index = match.Index + 1;
            return input.Substring(index);
        }
        else
        {
            // 如果没有找到数字，返回空字符串或者其他适当的值
            return string.Empty;
        }
    }


    public static bool IsNullOrWhiteSpace(this string? str)
    {
        return string.IsNullOrWhiteSpace(str);
    }

    public static bool IsNullOrEmpty(this string? str)
    {
        return string.IsNullOrEmpty(str);
    }
}
