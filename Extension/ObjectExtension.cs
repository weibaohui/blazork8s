using System.Text.Json;
using System.Text.Json.Serialization;

namespace Extension;

public static class ObjectExtension
{
    public static string ToPrettyJson(this object item)
    {
        JsonSerializerOptions options = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented          = true
        };
        return JsonSerializer.Serialize(item,options);
    }
    public static string ToJson(this object item)
    {
        JsonSerializerOptions options = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };
        return JsonSerializer.Serialize(item,options);
    }
}
