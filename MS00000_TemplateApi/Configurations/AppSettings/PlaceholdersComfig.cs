using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace MS00000_TemplateApi.Configurations.AppSettings;
public static class PlaceholdersComfig
{
    public static void SetPlaceholderConfig(WebApplicationBuilder builder)
    {

        // Esegui solo in Development
        if (!builder.Environment.IsDevelopment())
            return;

        string placeholdersPath = Path.Combine(AppContext.BaseDirectory, "devplaceholders.json");
        Dictionary<string, string> replacements = LoadReplacements(placeholdersPath);

        Dictionary<string, string?> substituted = BuildSubstitutedDictionary(builder.Configuration, replacements);

        builder.Configuration.AddInMemoryCollection(substituted);
    }

    private static Dictionary<string, string> LoadReplacements(string path)
    {
        Dictionary<string, string> result = new(StringComparer.OrdinalIgnoreCase);

        if (!File.Exists(path))
            return new(StringComparer.OrdinalIgnoreCase);

        string json = File.ReadAllText(path);
        Dictionary<string, JsonElement>? dict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json);

        if (dict is null)
            return result;


        foreach (KeyValuePair<string, JsonElement> kvp in dict)
        {
            if (TryJsonElementToString(kvp.Value, out string? asString))
            {
                asString = SetPswDb(kvp, asString);
                result[kvp.Key] = asString;
            }

        }

        return result;

    }

    private static string SetPswDb(KeyValuePair<string, JsonElement> kvp, string asString)
    {
        if (kvp.Key == PswPlaceholderEnviroment.AppMsDB)
        {
            asString = string.Format(asString, Environment.GetEnvironmentVariable(PswPlaceholderEnviroment.PswAppMsDB));
        }
        else if (kvp.Key == PswPlaceholderEnviroment.LogDatabaseDB)
        {
            asString = string.Format(asString, Environment.GetEnvironmentVariable(PswPlaceholderEnviroment.PswLogDatabaseDB));
        }
        else if (kvp.Key == PswPlaceholderEnviroment.SessionCacheDB)
        {
            asString = string.Format(asString, Environment.GetEnvironmentVariable(PswPlaceholderEnviroment.PswSessionCacheDB));
        }


        return asString;
    }

    private static Dictionary<string, string?> BuildSubstitutedDictionary(
        IConfiguration configuration,
        IDictionary<string, string> replacements)
    {
        Regex regex = new(@"#\{(.+?)\}#", RegexOptions.Compiled | RegexOptions.CultureInvariant, TimeSpan.FromMilliseconds(100));
        Dictionary<string, string?> dict = new(StringComparer.OrdinalIgnoreCase);

        foreach (KeyValuePair<string, string?> kvp in configuration.AsEnumerable(makePathsRelative: false))
        {
            if (kvp.Value is null)
            {
                dict[kvp.Key] = null;
                continue;
            }

            string newValue = regex.Replace(kvp.Value, m =>
            {
                string key = m.Groups[1].Value;
                return replacements.TryGetValue(key, out string? repl) && repl is not null
                    ? repl
                    : m.Value; // lascia il placeholder se mancante
            });

            dict[kvp.Key] = newValue;
        }

        // Converti a Dictionary<string,string> per AddInMemoryCollection
        Dictionary<string, string> notNull = new(StringComparer.OrdinalIgnoreCase);
        foreach ((string k, string v) in dict)
        {
            if (v is not null)
                notNull[k] = v;
        }
        return notNull;
    }

    private static bool TryJsonElementToString(JsonElement element, out string value)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.String:
                value = element.GetString() ?? string.Empty;
                return true;
            case JsonValueKind.Number:
                if (element.TryGetInt64(out long l))
                { value = l.ToString(CultureInfo.InvariantCulture); return true; }
                if (element.TryGetUInt64(out ulong ul))
                { value = ul.ToString(CultureInfo.InvariantCulture); return true; }
                if (element.TryGetDecimal(out decimal dec))
                { value = dec.ToString(CultureInfo.InvariantCulture); return true; }
                if (element.TryGetDouble(out double d))
                { value = d.ToString("R", CultureInfo.InvariantCulture); return true; }
                value = element.ToString();
                return true;
            case JsonValueKind.True:
                value = bool.TrueString;
                return true;
            case JsonValueKind.False:
                value = bool.FalseString;
                return true;
            case JsonValueKind.Array:
            case JsonValueKind.Object:
                value = JsonSerializer.Serialize(element);
                return true;
            default:
                value = string.Empty;
                return false;
        }
    }
}
