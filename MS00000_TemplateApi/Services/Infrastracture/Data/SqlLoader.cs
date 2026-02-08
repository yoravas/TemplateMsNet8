using System.Reflection;

namespace MS00000_TemplateApi.Services.Infrastracture.Data;
public static class SqlLoader
{
    public static string Load(string resourcePath)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        string fullName = assembly.GetManifestResourceNames().First(x => x.EndsWith(resourcePath.Replace("/", ".")));

        using Stream stream = assembly.GetManifestResourceStream(fullName) ??
            throw new InvalidOperationException($"SQL resource not found: {resourcePath}");

        using StreamReader reader = new(stream);
        return reader.ReadToEnd();
    }
}
