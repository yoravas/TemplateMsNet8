using Microsoft.Extensions.Primitives;

namespace MS00000_TemplateApi.Customizations.Middlewares;
public class RemoveInsecureHeadersMiddleware : IMiddleware
{
    private readonly ILogger<RemoveInsecureHeadersMiddleware> logger;

    public RemoveInsecureHeadersMiddleware(ILogger<RemoveInsecureHeadersMiddleware> logger)
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

            logger.LogInformationCustom("Intestazioni di sicurezza aggiunte alla risposta HTTP.");

            return Task.CompletedTask;

        }, null);

        await next(context);
    }

}

