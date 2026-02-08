using MS00000_TemplateApi.Customizations.Middlewares;

namespace MS00000_TemplateApi.Customizations.Extensions;
public static class CorrelationIdMiddlewareExtension
{
    public static WebApplication ReadHeaderCorrelationId(this WebApplication app)
    {
        app.UseMiddleware<CorrelationIdMiddleware>();
        return app;
    }
}
