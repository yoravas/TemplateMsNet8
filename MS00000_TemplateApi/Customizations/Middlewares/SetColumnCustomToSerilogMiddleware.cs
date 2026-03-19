using MS00000_TemplateApi.Customizations.Consts;
using MS00000_TemplateApi.Services.Application;
using MS00000_TemplateApi.Services.Application.Logger;
using Serilog.Context;

namespace MS00000_TemplateApi.Customizations.Middlewares;

public class SetColumnCustomToSerilogMiddleware : IMiddleware
{
    private readonly IApplicationLogger logger;
    private readonly ICorrelationIdAccessorService correlationIdAccessorService;
    private readonly IRequestPathAccessorService requestPathAccessorService;

    public SetColumnCustomToSerilogMiddleware(IApplicationLogger logger,
                                               ICorrelationIdAccessorService correlationIdAccessorService,
                                               IRequestPathAccessorService requestPathAccessorService)
    {
        this.logger = logger;
        this.correlationIdAccessorService = correlationIdAccessorService;
        this.requestPathAccessorService = requestPathAccessorService;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        ////Activity? activity = Activity.Current;

        ////string? traceId = activity?.TraceId.ToString();
        ////string? spanId = activity?.SpanId.ToString();

        string correlationId = correlationIdAccessorService.CorrelationId;

        logger.Debug($"CorrelationId: {correlationId}");

        string requestPath = requestPathAccessorService.RequestPath;

        logger.Debug($"RequestPath: {requestPath}");

        //using (LogContext.PushProperty(SerilogColumCustom.TraceId, traceId))
        //using (LogContext.PushProperty(SerilogColumCustom.SpanId, spanId))
        using (LogContext.PushProperty(SerilogColumCustom.RequestPath, requestPath))
        using (LogContext.PushProperty(SerilogColumCustom.CorrelationId, correlationId))
        {
            await next(context);
        }
    }
}