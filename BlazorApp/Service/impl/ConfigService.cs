#nullable enable
using Microsoft.Extensions.Configuration;

namespace BlazorApp.Service.impl;

public class ConfigService : IConfigService
{
    private readonly IConfigurationRoot _config;

    public ConfigService()
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
        _config = builder.Build();
    }

    public IConfigurationSection? GetSection(string section)
    {
        return _config.GetSection(section);
    }

    public string? GetString(IConfigurationSection section, string key)
    {
        return section.GetValue<string>(key);
    }

    public string? GetString(string section, string key)
    {
        return _config.GetSection(section).GetValue<string>(key);
    }


    public bool GetBool(IConfigurationSection section, string key)
    {
        return section.GetValue<bool>(key);
    }

    public bool GetBool(string section, string key)
    {
        return _config.GetSection(section).GetValue<bool>(key);
    }
}
