using MS00000_TemplateApi.Customizations.Middlewares;

namespace MS00000_TemplateApi.Customizations.Extensions;
public static class SetColumnCustomToSerilogExtension
{
    public static WebApplication SetColumnCustomToSerilog(this WebApplication app)
    {
        return (WebApplication)app.UseMiddleware<SetColumnCustomToSerilogMiddleware>();
    }
}
