using System;
using Extension;

namespace BlazorApp.Utils;

/// <summary>
/// 相比preset color ，去掉了浅色
/// </summary>
public enum RandomColor
{
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

public enum ColorfulTagHexRgb
{
    MediumBlue = 0x0000CD,
    MediumSlateBlue = 0x7B68EE,
    DodgerBlue = 0x1E90FF,
    SteelBlue = 0x4682B4,
    RoyalBlue = 0x4169E1,
    DarkSlateBlue = 0x483D8B,
    CornflowerBlue = 0x6495ED,
    DarkSlateGray = 0x2F4F4F,
    SlateGray = 0x708090,
    DarkCyan = 0x008B8B,
    Teal = 0x008080,
    DarkOliveGreen = 0x556B2F,
    OliveDrab = 0x6B8E23,
    SeaGreen = 0x2E8B57,
    DarkGoldenrod = 0xB8860B,
    Peru = 0xCD853F,
    Chocolate = 0xD2691E,
    SaddleBrown = 0x8B4513,
    DarkRed = 0x8B0000,
    Firebrick = 0xB22222,
    Crimson = 0xDC143C,
    IndianRed = 0xCD5C5C,
    MediumVioletRed = 0xC71585,
    DarkMagenta = 0x8B008B,
    DarkSlateGray4 = 0x528B8B,
    DarkSlateGray5 = 0x4F94CD,
    DarkSlateGray6 = 0x436EEE,
    DarkSlateGray7 = 0x2F4F4F,
    DarkSlateGray8 = 0x2D3E50,
    LightSlateGrey = 0x778899,
    Gray = 0x808080,
    DimGray = 0x696969,
    DarkGray = 0xA9A9A9,
    White = 0xFFFFFF,
    Maroon = 0x800000,
    Brown = 0xA52A2A,
    Red = 0xFF0000,
    Tomato = 0xFF6347,
    Coral = 0xFF7F50,
    DarkSalmon = 0xE9967A,
    Salmon = 0xFA8072,
    LightCoral = 0xF08080,
    OrangeRed = 0xFF4500,
    DarkOrange = 0xFF8C00,
    Orange = 0xFFA500,
    Goldenrod = 0xDAA520,
    DarkKhaki = 0xBDB76B,
    SkyBlue = 0x87CEEB,
    ForestGreen = 0x228B22,
    GoldenRod = 0xDAA520,
    FireBrick = 0xB22222,
    Navy = 0x000080,
    RosyBrown = 0xBC8F8F,
    WarmGray = 0x808069
}

public static class RandomColorHelper
{
    public static string RandomColor() => typeof(RandomColor).GetRandomEnumValue()?.ToString();

    public static string RandomColor(string str)
    {
        return typeof(RandomColor).GetRandomEnumValue(str)?.ToString();
    }

    public static string RandomColorHexRgb()
    {
        Random random = new Random();
        var colors = (ColorfulTagHexRgb[])Enum.GetValues(typeof(ColorfulTagHexRgb));
        // 随机选择一个颜色
        var colorful = colors[random.Next(colors.Length)];
        var str = $"#{(int)colorful:X6}";
        return str;
    }

    public static string ColorfulTagHexRgb(string str)
    {
        var colors = (ColorfulTagHexRgb[])Enum.GetValues(typeof(ColorfulTagHexRgb));
        var index = str.ToNumeric() % colors.Length;
        var randomColor = colors[index];
        var ret = $"#{(int)randomColor:X6}";
        return ret;
    }

    public static string RandomInverseColor()
    {
        return typeof(RandomColor).GetRandomEnumValue()?.ToString().ToLower() + "-inverse";
    }
}
