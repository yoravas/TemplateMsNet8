using MS00000_TemplateApi.Customizations.Middlewares;

namespace MS00000_TemplateApi.Customizations.Extensions;
public static class ResponseStatusCodeExtension
{
    public static WebApplication CongigureResponseStatusCode(this IApplicationBuilder app)
    {
        app.UseMiddleware<StatusCodeMiddleware>();
        return (WebApplication)app;
    }

}
