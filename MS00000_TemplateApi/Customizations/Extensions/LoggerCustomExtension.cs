using MS00000_TemplateApi.Customizations.Consts;
using Serilog.Context;
using System.Runtime.CompilerServices;

namespace MS00000_TemplateApi.Customizations.Extensions;
#pragma warning disable RS0030
public static class LoggerCustomExtension
{
    public static void LogDebugCustom(
        this ILogger logger,
        string? message = "",
        object? additionalData = null,
        [CallerMemberName] string metodo = "",
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int linea = 0)
    {
        using (PushCustomFields(metodo, filePath, linea, additionalData))
        {
            logger.LogDebug(message);
        }
    }
    public static void LogInformationCustom(
        this ILogger logger,
        string? message = "",
        [CallerMemberName] string metodo = "",
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int linea = 0)
    {
        using (PushCustomFields(metodo, filePath, linea))
        {
            logger.LogInformation(message);
        }
    }

    public static void LogWarningCustom(
        this ILogger logger,
        string? message = "",
        object? additionalData = null,
        [CallerMemberName] string metodo = "",
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int linea = 0)
    {
        using (PushCustomFields(metodo, filePath, linea, additionalData))
        {
            logger.LogWarning(message);
        }
    }

    public static void LogErrorCustom(
        this ILogger logger,
        Exception? ex,
        string? message = "",
        object? additionalData = null,
        [CallerMemberName] string metodo = "",
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int linea = 0)
    {
        using (PushCustomFields(metodo, filePath, linea, additionalData))
        {
            logger.LogError(ex, message);
        }
    }

    private static IDisposable PushCustomFields(
        string metodo,
        string filePath,
        int linea,
        object? additionalData = null)
    {
        string className = filePath.Split('\\')?.Last()?.Split('.')?.First() ?? "Classe-Sconosciuta";
        string nomeMetodoCompleto = $"{className}.{metodo} - Linea: {linea}";
        string percorsoDelFile = filePath.Replace("\\", "/");

        IDisposable p1 = LogContext.PushProperty(SerilogColumCustom.Metodo, nomeMetodoCompleto);
        IDisposable p2 = LogContext.PushProperty(SerilogColumCustom.FilePath, percorsoDelFile);
        IDisposable p3;

        if (additionalData is not null)
        {
            p3 = LogContext.PushProperty(SerilogColumCustom.AdditionalData, additionalData, destructureObjects: true);
            return new CombinedDisposable(p1, p2, p3);
        }

        return new CombinedDisposable(p1, p2);
    }

    private sealed class CombinedDisposable : IDisposable
    {
        private readonly IDisposable[] disposables;

        public CombinedDisposable(params IDisposable[] disposables)
        {
            this.disposables = disposables;
        }

        public void Dispose()
        {
            foreach (IDisposable d in disposables)
                d.Dispose();
        }
    }
}
