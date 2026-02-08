using MS00000_TemplateApi.Customizations.Middlewares;

namespace MS00000_TemplateApi.Customizations.Extensions;
public static class CatchUnhandledExceptionsExtension
{
    public static WebApplication ConfigureCatchUnhandledException(this WebApplication app)
    {
        return (WebApplication)app.UseMiddleware<ExceptionMiddleware>();
    }
}
