using MS00000_TemplateApi.Customizations.Consts;
using MS00000_TemplateApi.Services.Application.Logger;

namespace MS00000_TemplateApi.Customizations.Middlewares;

public class RequestPathMiddleware : IMiddleware
{
    private readonly IApplicationLogger logger;

    public RequestPathMiddleware(IApplicationLogger logger)
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