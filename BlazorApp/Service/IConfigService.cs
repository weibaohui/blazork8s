#nullable enable
using Microsoft.Extensions.Configuration;

namespace BlazorApp.Service;

public interface IConfigService
{
    public string? GetString(string section, string key);
    public bool    GetBool(string   section, string key);

    IConfigurationSection? GetSection(string section);

    string? GetString(IConfigurationSection section, string key);
    bool    GetBool(IConfigurationSection   section, string key);
}
