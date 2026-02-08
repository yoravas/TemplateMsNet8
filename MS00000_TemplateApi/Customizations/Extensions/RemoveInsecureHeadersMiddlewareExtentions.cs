using MS00000_TemplateApi.Customizations.Middlewares;

namespace MS00000_TemplateApi.Customizations.Extensions;
public static class RemoveInsecureHeadersMiddlewareExtentions
{
    public static WebApplication RemoveInsecureHeaders(this WebApplication app)
    {
        return (WebApplication)app.UseMiddleware<RemoveInsecureHeadersMiddleware>();
    }
}
