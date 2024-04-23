using System;
using System.Linq;

namespace Extension;

public static class EnumExtension
{
    public static Enum? GetRandomEnumValue(this Type t)
    {
        return Enum.GetValues(t)
            .OfType<Enum>().MinBy(e => Guid.NewGuid());
    }

    public static Enum? GetRandomEnumValue(this Type t, string str)
    {
        var index = (str.Length + str.GetHashCode()) % Enum.GetValues(t).Length;
        var x = Enum.GetValues(t)
                .OfType<Enum>().Skip(index).First()
            ;
        return x;
    }

    public static Enum? GetRandomEnumValue(this Type t, int num)
    {
        return Enum.GetValues(t)
            .OfType<Enum>().MinBy(e => num % 7);
    }
}