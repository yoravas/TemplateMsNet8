using MS00000_TemplateApi.Customizations.Middlewares;

namespace MS00000_TemplateApi.Customizations.Extensions;
public static class GroupTransientExtension
{
    public static void GroupTransient(this IServiceCollection services)
    {

        services.AddTransient<StatusCodeMiddleware>();
    }
}
