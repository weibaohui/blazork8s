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
}
