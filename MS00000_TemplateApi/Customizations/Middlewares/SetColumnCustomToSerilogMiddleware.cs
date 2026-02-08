using MS00000_TemplateApi.Customizations.Consts;
using MS00000_TemplateApi.Services.Application;
using Serilog.Context;

namespace MS00000_TemplateApi.Customizations.Middlewares;
public class SetColumnCustomToSerilogMiddleware : IMiddleware
{
    private readonly ILogger<SetColumnCustomToSerilogMiddleware> logger;
    private readonly ICorrelationIdAccessorService correlationIdAccessorService;
    private readonly IRequestPathAccessorService requestPathAccessorService;

    public SetColumnCustomToSerilogMiddleware(ILogger<SetColumnCustomToSerilogMiddleware> logger,
                                               ICorrelationIdAccessorService correlationIdAccessorService,
                                               IRequestPathAccessorService requestPathAccessorService)
    {
        this.logger = logger;
        this.correlationIdAccessorService = correlationIdAccessorService;
        this.requestPathAccessorService = requestPathAccessorService;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        string correlationId = correlationIdAccessorService.CorrelationId;

        logger.LogDebugCustom($"CorrelationId: {correlationId}");

        string requestPath = requestPathAccessorService.RequestPath;

        logger.LogDebugCustom($"RequestPath: {requestPath}");

        using (LogContext.PushProperty(SerilogColumCustom.RequestPath, requestPath))
        using (LogContext.PushProperty(SerilogColumCustom.CorrelationId, correlationId))
        {
            await next(context);
        }



    }
}
