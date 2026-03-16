using MS00000_TemplateApi.Customizations.Consts;
using MS00000_TemplateApi.Customizations.Helpers;
using MS00000_TemplateApi.Model.Application.DTOs;
using MS00000_TemplateApi.Services.Application.Enqueue;
using MS00000_TemplateApi.Services.Infrastracture.Data.SqlServer.Repository.SequenceApiLog;
using Serilog.Context;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable RS0030

namespace MS00000_TemplateApi.Services.Application.Logger;

public class ApplicationLogger : IApplicationLogger
{
    private readonly ILogger logger;
    private readonly ISequenceApi sequence;
    private readonly IChannelService<AdditionalDataLogDto> channelService;
    private readonly IHttpContextAccessor httpContextAccessor;

    public ApplicationLogger(ILogger<ApplicationLogger> logger,
                             ISequenceApi sequence,
                             IChannelService<AdditionalDataLogDto> channelService,
                             IHttpContextAccessor httpContextAccessor)
    {
        this.logger = logger;
        this.sequence = sequence;
        this.channelService = channelService;
        this.httpContextAccessor = httpContextAccessor;
    }

    public void Debug(string message, object? additionalData = null, [CallerMemberName] string metodo = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int linea = 0)
    {
        using (PushCustomFieldsAsync(metodo, filePath, linea, additionalData))
        {
            logger.LogDebug(message);
        }
    }

    public void Error(Exception ex, string message, object? additionalData = null, [CallerMemberName] string metodo = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int linea = 0)
    {
        using (PushCustomFieldsAsync(metodo, filePath, linea, additionalData, ex))
        {
            logger.LogError(ex, message);
        }
    }

    public void Information(string message, [CallerMemberName] string metodo = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int linea = 0)
    {
        using (PushCustomFieldsAsync(metodo, filePath, linea))
        {
            logger.LogInformation(message);
        }
    }

    public void Warning(string message, object? additionalData = null, [CallerMemberName] string metodo = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int linea = 0)
    {
        using (PushCustomFieldsAsync(metodo, filePath, linea, additionalData))
        {
            logger.LogWarning(message);
        }
    }

    private IDisposable PushCustomFieldsAsync(string metodo,
                                              string filePath,
                                              int linea,
                                              object? additionalData = null,
                                              Exception? ex = null)
    {
        string className = filePath.Split('\\')?.Last()?.Split('.')?.First() ?? "Classe-Sconosciuta";
        string nomeMetodoCompleto = $"{className}.{metodo} - Linea: {linea}";
        string percorsoDelFile = filePath.Replace("\\", "/");

        // Fix CS8602: controlla se HttpContext è null prima di accedere a RequestAborted
        CancellationToken ct = GetCancellationToken();

        long? additionalDataLogID = 0;

        if (additionalData is not null || ex is not null)
        {
            additionalDataLogID = sequence.GetNextValueApiAsync(ct).GetAwaiter().GetResult();

            AdditionalDataLogDto additionalDataLog = new()
            {
                AdditionalDataLogID = additionalDataLogID.Value,
                RequestPath = httpContextAccessor.HttpContext?.Request.Path,
                FilePath = percorsoDelFile,
                AdditionalData = additionalData is not null ? SerializeAdditionalData(additionalData) : null,
                Exception = ex?.ToString()
            };

            channelService.WriteAsync(additionalDataLog, ct).GetAwaiter().GetResult();
        }

        IDisposable p1 = LogContext.PushProperty(SerilogColumCustom.Metodo, nomeMetodoCompleto);
        IDisposable p2;

        if (additionalDataLogID.HasValue)
        {
            p2 = LogContext.PushProperty(SerilogColumCustom.ApiSerilogID, additionalDataLogID);

            return new CombinedDisposableHelper(p1, p2);
        }

        return new CombinedDisposableHelper(p1);
    }

    private static string SerializeAdditionalData(object additionalData)
    {
        string json = JsonSerializer.Serialize(additionalData, new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = false
        });

        return json;
    }

    private CancellationToken GetCancellationToken()
    {
        return httpContextAccessor.HttpContext != null
            ? httpContextAccessor.HttpContext.RequestAborted
            : CancellationToken.None;
    }
}