namespace Extension;

public static class StringExtensions
{
    public static string ToHtmlDisplay(this string str)
    {
        return str.Replace("\n", "<br/>")
            .Replace("\r", "<br/>");
    }
}
