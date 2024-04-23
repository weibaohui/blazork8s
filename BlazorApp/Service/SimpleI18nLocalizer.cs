using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json.Linq;

namespace BlazorApp.Service;

/// <summary>
///     源 https://github.com/BrunoCremaFerreira/SimpleI18n
/// </summary>
public class SimpleI18NStringLocalizer : IStringLocalizer
{
    private const string GenderMark = "[f]";

    public static readonly Dictionary<string, string> LanguageMap = new()
    {
        { "en-US", "英语" },
        { "zh-CN", "简体中文" },
        { "es", "西班牙语" },
        { "ru", "俄罗斯语" },
        { "pt-br", "西班牙语" },
        { "pl", "波兰语" },
        { "ko", "韩语" },
        { "ja", "日语" },
        { "fr", "法语" },
        { "de", "德语" },
        { "hi", "印地语" },
        { "it", "意大利语" }
    };

    private readonly IDictionary<string, JObject> _inMemoryResources = new Dictionary<string, JObject>();

    private CultureInfo _culture;

    private string _localeFilesPath;

    private JObject ResourceContent
    {
        get
        {
            if (_inMemoryResources.All(e => e.Key != _culture.Name))
                LoadLocaleFile();

            return _inMemoryResources[_culture.Name];
        }
    }


    private string LocaleFileName
        => Path.Combine(_localeFilesPath, $"{_culture.Name}.json");

    private LocalizedString GetTranslation(string keyName, params object[] arguments)
    {
        //Autoset gender if translation ends with "[f]"
        var hasLocalizedGenderF = arguments.Any(e => e is LocalizedString && e.ToString().EndsWith(GenderMark));
        if (hasLocalizedGenderF)
        {
            var kyHasF = keyName.EndsWith("{f}");
            var args = RemoveGenderMarkOfArguments(arguments);
            return GetTranslation(kyHasF ? keyName : $"{keyName}{{f}}", args);
        }

        var hasTranslation = ResourceContent.TryGetValue(keyName, out var jToken)
                             && (jToken.HasValues || jToken.Value<string>() != null);

        //如果没有定义的翻译，那么返回key本身
        if (!hasTranslation)
            return new LocalizedString(keyName, keyName, true, LocaleFileName);
        //Plural form translation
        if (jToken.HasValues)
        {
            var canBePluralForm = IsNumericType(arguments.FirstOrDefault());
            var pluralQuantity = canBePluralForm
                ? Math.Abs(double.Parse(arguments.First().ToString()))
                : 1;
            dynamic dynToken = jToken;
            string val = pluralQuantity == 0
                ? dynToken.Zero
                : pluralQuantity == 1
                    ? dynToken.One
                    : dynToken.Other;
            return new LocalizedString(
                keyName, string.Format(val, arguments), string.IsNullOrWhiteSpace(val));
        }

        //Single translation
        var value = jToken.Value<string>() ?? string.Empty;
        return new LocalizedString(
            keyName, string.Format(value, arguments), string.IsNullOrWhiteSpace(value));
    }

    /// <summary>
    ///     加载配置
    /// </summary>
    /// <param name="configuration"></param>
    /// <exception cref="Exception"></exception>
    private void LoadConfiguration(IConfiguration configuration)
    {
        try
        {
            _localeFilesPath = configuration["SimpleI18n:LocaleFilesPath"] ??
                               Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LocaleFiles");
            //如果找不到本地文件的路径，
            //尝试通过更改路径的AltDirectorySeparatorChar来解决它
            if (!Directory.Exists(_localeFilesPath))
                _localeFilesPath = _localeFilesPath
                    .Replace('/', Path.AltDirectorySeparatorChar)
                    .Replace('\\', Path.AltDirectorySeparatorChar);

            var cultureName = configuration["SimpleI18n:DefaultCultureName"] ?? "en-US";
            _culture = new CultureInfo(cultureName);
        }
        catch (Exception e)
        {
            throw new Exception(
                "Error to load Localizer configuration! Take a look at InnerException for more details.", e);
        }
    }

    /// <summary>
    ///     读取 JSON 翻译文件资源并将其加载到内存中
    /// </summary>
    private void LoadLocaleFile()
    {
        try
        {
            var fileContent = File.ReadAllText(LocaleFileName);
            if (string.IsNullOrWhiteSpace(fileContent))
                return;

            //每次切换Culture时都重新加载
            if (!_inMemoryResources.Keys.Any(k => k.Equals(_culture.Name)))
                _inMemoryResources.Add(_culture.Name, JObject.Parse(fileContent));
        }
        catch (Exception e)
        {
            throw new Exception($"Error to load Localizer content from '{LocaleFileName}'.", e);
        }
    }

    private static bool IsNumericType(object obj)
    {
        if (obj == null)
            return false;
        return Type.GetTypeCode(obj.GetType()) switch
        {
            TypeCode.UInt16 or TypeCode.UInt32 or TypeCode.UInt64 or TypeCode.Int16 or TypeCode.Int32 or TypeCode.Int64
                or TypeCode.Decimal or TypeCode.Double or TypeCode.Byte or TypeCode.SByte or TypeCode.Single => true,
            _ => false
        };
    }

    private object[] RemoveGenderMarkOfArguments(object[] attributes)
    {
        return attributes
            .Select(e => e is LocalizedString
                ? RemoveGenderMark(e.ToString())
                : e)
            .ToArray();
    }

    public override string ToString()
    {
        var str = base.ToString();
        return RemoveGenderMark(str);
    }

    private string RemoveGenderMark(string source)
    {
        return source.EndsWith(GenderMark)
            ? source.Replace(GenderMark, string.Empty)
            : source;
    }

    #region :: Constructors

    public SimpleI18NStringLocalizer(IConfiguration configuration)
    {
        LoadConfiguration(configuration);
    }

    public SimpleI18NStringLocalizer(IConfiguration configuration, CultureInfo culture)
        : this(configuration)
    {
        _culture = culture;
    }

    public void SetCulture(CultureInfo culture)
    {
        _culture = culture;
        LoadLocaleFile();
    }

    public CultureInfo GetCulture()
    {
        return _culture;
    }

    #endregion

    #region :: IStringLocalizer Implementations

    public LocalizedString this[string name]
        => GetTranslation(name);

    public LocalizedString this[string name, params object[] arguments]
        => GetTranslation(name, arguments);

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        return ResourceContent.Values().Select(i => new LocalizedString(i.Type.ToString(), i.Value<string>()));
    }

    #endregion
}
