using MS00000_TemplateApi.Customizations.Consts;
using MS00000_TemplateApi.Services.Application.Logger;

namespace MS00000_TemplateApi.Customizations.Middlewares;

public class CorrelationIdMiddleware : IMiddleware
{
    private readonly IApplicationLogger logger;

    public CorrelationIdMiddleware(IApplicationLogger logger)
    {
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        string correlationId = context.Request.Headers.ContainsKey(HeadersCustom.HeaderCorrelationID)
            ? context.Request.Headers[HeadersCustom.HeaderCorrelationID].ToString()
            : string.Empty;

        logger.Debug($"Correlation ID prelevato dall'header: {correlationId}", additionalData: correlationId);

        if (!Ulid.TryParse(correlationId, out _))
        {
            correlationId = Ulid.NewUlid().ToString();
        }

        logger.Debug($"Correlation ID utilizzato per la richiesta: {correlationId}", additionalData: correlationId);

        context.Items[ContextItems.CorrelationID] = correlationId;

        //context.Response.OnStarting(() =>
        //{
        //    context.Response.Headers[HeadersCustom.HeaderCorrelationID] = correlationId;
        //    return Task.CompletedTask;
        //});

        await next(context);
    }
}