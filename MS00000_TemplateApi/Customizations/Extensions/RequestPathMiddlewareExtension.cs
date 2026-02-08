using MS00000_TemplateApi.Customizations.Middlewares;

namespace MS00000_TemplateApi.Customizations.Extensions;
public static class RequestPathMiddlewareExtension
{
    public static WebApplication ReadRequestPath(this WebApplication app)
    {
        app.UseMiddleware<RequestPathMiddleware>();
        return app;
    }
}
