using Microsoft.Extensions.Primitives;
using MS00000_TemplateApi.Services.Application.Logger;

namespace MS00000_TemplateApi.Customizations.Middlewares;

public class RemoveInsecureHeadersMiddleware : IMiddleware
{
    private readonly IApplicationLogger logger;

    public RemoveInsecureHeadersMiddleware(IApplicationLogger logger)
    {
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        context.Response.OnStarting((state) =>
        {
            context.Response.Headers.Remove("Server");
            context.Response.Headers.Remove("X-Powered-By");
            context.Response.Headers.Remove("X-Aspnet-version");
            context.Response.Headers.Remove("X-AspnetMvc-version");
            //context.Response.Headers.Remove("X-Frame-Options");

            context.Response.Headers.Append("X-Content-Type-Options", new StringValues("nosniff"));
            //context.Response.Headers.Append("X-Frame-Options", new StringValues("DENY"));

            logger.Information("Intestazioni di sicurezza aggiunte alla risposta HTTP.");

            return Task.CompletedTask;
        }, context);

        await next(context);
    }
}