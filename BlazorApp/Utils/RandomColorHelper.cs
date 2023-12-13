using AntDesign;
using Extension;

namespace BlazorApp.Utils;

public static class RandomColorHelper
{
    public static string RandomColor() => typeof(PresetColor).GetRandomEnumValue()?.ToString();
    public static string RandomInverseColor() => typeof(PresetColor).GetRandomEnumValue()?.ToString().ToLower()+"-inverse";
}
