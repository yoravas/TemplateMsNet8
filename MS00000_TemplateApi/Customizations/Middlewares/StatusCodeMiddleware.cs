using MS00000_TemplateApi.Customizations.Strategies.StatusCode;

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

            if (!context.Response.HasStarted &&
                registry.TryGet(context.Response.StatusCode, out IStatusCodeStrategy? strategy))
            {
                logger.LogDebugCustom($"Status code gestito {context.Response.StatusCode} con la strategy {strategy.GetType().Name}");
                await strategy.HandleAsync(context, originalBody, buffer, context.RequestAborted);
                return; // evita la copia del buffer originale
            }
            logger.LogDebugCustom($"Nessuna strategy trovata per lo status code {context.Response.StatusCode}, inviata la response orginale.");
            // Caso standard: manda al client quello che è stato scritto nel buffer
            await CopyBufferToOriginalAsync(context, originalBody, buffer);
        }
        finally
        {
            context.Response.Body = originalBody;
        }
    }

    private static async Task CopyBufferToOriginalAsync(HttpContext context, Stream originalBody, MemoryStream buffer)
    {
        context.Response.Body = originalBody;
        await buffer.CopyToAsync(originalBody, context.RequestAborted);
        await context.Response.Body.FlushAsync(context.RequestAborted);
    }
}
