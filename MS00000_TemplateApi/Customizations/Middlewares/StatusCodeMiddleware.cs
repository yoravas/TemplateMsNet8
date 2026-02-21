using MS00000_TemplateApi.Customizations.Strategies.StatusCode;
using MS00000_TemplateApi.Model.Application;
using System.Text;
using System.Text.Json;

namespace MS00000_TemplateApi.Customizations.Middlewares;

public class StatusCodeMiddleware : IMiddleware
{
    private readonly ILogger<StatusCodeMiddleware> logger;
    private readonly IStatusCodeStrategyRegistry registry;

    public StatusCodeMiddleware(ILogger<StatusCodeMiddleware> logger,
                                IStatusCodeStrategyRegistry registry)
    {
        this.logger = logger;
        this.registry = registry;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        Stream originalBody = context.Response.Body;
        await using MemoryStream buffer = new();
        context.Response.Body = buffer;

        try
        {
            await next(context);

            buffer.Position = 0;

            if (await BodyContainsApiResponseReturnDetailsAsync(buffer, context.RequestAborted))
            {
                logger.LogWarningCustom("Body già valorizzato con ApiResponse<ReturnDetails>, bypasso l'intercettazione degli status code.");
                await CopyBufferToOriginalAsync(context, originalBody, buffer);
                return;
            }

            if (!context.Response.HasStarted &&
                registry.TryGet(context.Response.StatusCode, out IStatusCodeStrategy? strategy))
            {
                logger.LogWarningCustom($"Status code gestito {context.Response.StatusCode} con la strategy {strategy.GetType().Name}");
                await strategy.HandleAsync(context, originalBody, buffer, context.RequestAborted);
                return; // evita la copia del buffer originale
            }
            logger.LogWarningCustom($"Nessuna strategy trovata per lo status code {context.Response.StatusCode}, inviata la response orginale.");
            // Caso standard: manda al client quello che è stato scritto nel buffer
            await CopyBufferToOriginalAsync(context, originalBody, buffer);
        }
        finally
        {
            context.Response.Body = originalBody;
        }
    }

    private async Task<bool> BodyContainsApiResponseReturnDetailsAsync(MemoryStream buffer, CancellationToken ct)
    {
        if (buffer.Length == 0)
        {
            return false;
        }

        buffer.Position = 0;
        using StreamReader reader = new(buffer, Encoding.UTF8, true, 1024, true);
        string json = await reader.ReadToEndAsync(ct);
        buffer.Position = 0;

        if (string.IsNullOrWhiteSpace(json))
        {
            return false;
        }
        ApiResponse<ReturnDetails>? apiResponse = default;
        try
        {
            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true
            };

            apiResponse = JsonSerializer.Deserialize<ApiResponse<ReturnDetails>>(json, options);

            return apiResponse?.Response != null;
        }
        catch (Exception ex)
        {
            logger.LogErrorCustom(ex, ex.Message, additionalData: apiResponse);
            return false;
        }
    }

    private static async Task CopyBufferToOriginalAsync(HttpContext context, Stream originalBody, MemoryStream buffer)
    {
        context.Response.Body = originalBody;
        await buffer.CopyToAsync(originalBody, context.RequestAborted);
        await context.Response.Body.FlushAsync(context.RequestAborted);
    }
}