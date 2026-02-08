using System.Runtime.CompilerServices;

namespace MS00000_TemplateApi.Customizations.Helpers;
public static class LoggerInfoHelper
{
    public static string LogUsedItemInfo([CallerMemberName] string methodName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
    {

        string className = filePath.Split('\\')?.Last()?.Split('.')?.First() ?? "Classe-Sconosciuta";
        string nomeMetodo = string.IsNullOrWhiteSpace(methodName) ? "Metodo-Sconosciuto" : methodName;
        return $"(N° riga; {lineNumber}) - {className}.{nomeMetodo}";
    }
}
