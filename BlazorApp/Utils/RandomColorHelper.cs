using System;
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
public enum RandomColorHexRgb {

    MediumBlue       = 0x0000CD, // #0000CD
    MediumSlateBlue  = 0x7B68EE, // #7B68EE
    DodgerBlue       = 0x1E90FF, // #1E90FF
    SteelBlue        = 0x4682B4,
    RoyalBlue        = 0x4169E1,
    DarkSlateBlue    = 0x483D8B,
    // MediumTurquoise  = 0x48D1CC,
    // LightSeaGreen    = 0x20B2AA,
    MediumAquamarine = 0x66CDAA,
    CornflowerBlue   = 0x6495ED,
    // MediumPurple     = 0x9370DB,
    DarkSlateGray    = 0x2F4F4F,
    SlateGray        = 0x708090,
    DarkCyan         = 0x008B8B,
    Teal             = 0x008080,
    DarkOliveGreen   = 0x556B2F,
    OliveDrab        = 0x6B8E23,
    SeaGreen         = 0x2E8B57,
    DarkGoldenrod    = 0xB8860B,
    Peru             = 0xCD853F,
    Chocolate        = 0xD2691E,
    SaddleBrown      = 0x8B4513,
    DarkRed          = 0x8B0000,
    Firebrick        = 0xB22222,
    Crimson          = 0xDC143C,
    IndianRed        = 0xCD5C5C,
    MediumVioletRed  = 0xC71585,
    DarkMagenta      = 0x8B008B,
    // MediumOrchid     = 0xBA55D3,
    DarkSlateGray2   = 0x8DEEEE,
    DarkSlateGray3   = 0x79CDCD,
    DarkSlateGray4   = 0x528B8B,
    DarkSlateGray5   = 0x4F94CD,
    DarkSlateGray6   = 0x436EEE,
    DarkSlateGray7   = 0x2F4F4F,
    DarkSlateGray8   = 0x2D3E50
}
public static class RandomColorHelper
{
    public static string RandomColor() => typeof(RandomColor).GetRandomEnumValue()?.ToString();
    public static string RandomColorHexRgb()
    {
        Random random = new Random();
        RandomColorHexRgb[] colors = (RandomColorHexRgb[])Enum.GetValues(typeof(RandomColorHexRgb));
        // 随机选择一个颜色
        RandomColorHexRgb randomColor = colors[random.Next(colors.Length)];
        var color = typeof(RandomColorHexRgb).GetRandomEnumValue();
        var str= $"#{((int)randomColor).ToString("X")}";
        return str;
    }

    public static string RandomInverseColor() => typeof(RandomColor).GetRandomEnumValue()?.ToString().ToLower()+"-inverse";
}
