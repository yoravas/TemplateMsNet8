using MS00000_TemplateApi.Customizations.Middlewares;
using MS00000_TemplateApi.Services.Infrastracture.Data;

namespace MS00000_TemplateApi.Customizations.Extensions;
public static class GroupTransientExtension
{
    public static void GroupTransient(this IServiceCollection services)
    {

        services.AddTransient<StatusCodeMiddleware>();
    }
}
