using Extension;

namespace BlazorApp.Utils;

/// <summary>
/// 相比preset color ，去掉了浅色
/// </summary>
public enum RandomColor {
    Pink,
    Red,
    // Yellow,
    Orange,
    Cyan,
    Green,
    Blue,
    Purple,
    GeekBlue,
    Magenta,
    Volcano,
    Gold,
    // Lime
}
public static class RandomColorHelper
{
    public static string RandomColor() => typeof(RandomColor).GetRandomEnumValue()?.ToString();
    public static string RandomInverseColor() => typeof(RandomColor).GetRandomEnumValue()?.ToString().ToLower()+"-inverse";
}
