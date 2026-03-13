using System.Runtime.CompilerServices;

namespace MS00000_TemplateApi.Services.Application.Logger;

public interface IApplicationLogger
{
    void LogInformationCustom(string message,
                          [CallerMemberName] string metodo = "",
                          [CallerFilePath] string filePath = "",
                          [CallerLineNumber] int linea = 0);

    void LogDebugCustom(string message,
                    object? additionalData = null,
                    [CallerMemberName] string metodo = "",
                    [CallerFilePath] string filePath = "",
                    [CallerLineNumber] int linea = 0);

    void LogWarningCustom(string message,
                      object? additionalData = null,
                      [CallerMemberName] string metodo = "",
                      [CallerFilePath] string filePath = "",
                      [CallerLineNumber] int linea = 0);

    void LogErrorCustom(Exception ex,
                    string message,
                    object? additionalData = null,
                    [CallerMemberName] string metodo = "",
                    [CallerFilePath] string filePath = "",
                    [CallerLineNumber] int linea = 0);
}