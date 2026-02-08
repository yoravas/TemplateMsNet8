using MS00000_TemplateApi.Customizations.Consts;

namespace MS00000_TemplateApi.Customizations.Middlewares;
public class RequestPathMiddleware : IMiddleware
{
    private readonly ILogger<RequestPathMiddleware> logger;

    public RequestPathMiddleware(ILogger<RequestPathMiddleware> logger)
    {
        this.logger = logger;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        HttpRequest req = context.Request;
        string fullUrl = $"{req.Scheme}://{req.Host}{req.Path}{req.QueryString}";
        logger.LogDebugCustom($"URL completo della richiesta: {fullUrl}", additionalData: fullUrl);
        context.Items[ContextItems.RequestUrl] = fullUrl;

        await next(context);
    }
}
