using System.Runtime.CompilerServices;

namespace MS00000_TemplateApi.Services.Application.Logger;

public interface IApplicationLogger
{
    void Information(string message,
                          [CallerMemberName] string metodo = "",
                          [CallerFilePath] string filePath = "",
                          [CallerLineNumber] int linea = 0);

    void Debug(string message,
                    object? additionalData = null,
                    [CallerMemberName] string metodo = "",
                    [CallerFilePath] string filePath = "",
                    [CallerLineNumber] int linea = 0);

    void Warning(string message,
                      object? additionalData = null,
                      [CallerMemberName] string metodo = "",
                      [CallerFilePath] string filePath = "",
                      [CallerLineNumber] int linea = 0);

    void Error(Exception ex,
                    string message,
                    object? additionalData = null,
                    [CallerMemberName] string metodo = "",
                    [CallerFilePath] string filePath = "",
                    [CallerLineNumber] int linea = 0);
}