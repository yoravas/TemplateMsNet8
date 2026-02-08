using MS00000_TemplateApi.Customizations.Consts;

namespace MS00000_TemplateApi.Customizations.Middlewares;
public class CorrelationIdMiddleware : IMiddleware
{
    private readonly ILogger<CorrelationIdMiddleware> logger;

    public CorrelationIdMiddleware(ILogger<CorrelationIdMiddleware> logger)
    {
        this.logger = logger;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        string correlationId = context.Request.Headers.ContainsKey(HeadersCustom.HeaderCorrelationID)
            ? context.Request.Headers[HeadersCustom.HeaderCorrelationID].ToString()
            : string.Empty;

        logger.LogDebugCustom($"Correlation ID prelevato dall'header: {correlationId}", additionalData: correlationId);

        if (!Ulid.TryParse(correlationId, out _))
        {
            correlationId = Ulid.NewUlid().ToString();
        }

        logger.LogDebugCustom($"Correlation ID utilizzato per la richiesta: {correlationId}", additionalData: correlationId);

        context.Items[ContextItems.CorrelationID] = correlationId;

        //context.Response.OnStarting(() =>
        //{
        //    context.Response.Headers[HeadersCustom.HeaderCorrelationID] = correlationId;
        //    return Task.CompletedTask;
        //});

        await next(context);
    }
}
