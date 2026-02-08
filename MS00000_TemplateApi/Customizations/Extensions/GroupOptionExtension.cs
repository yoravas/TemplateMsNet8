using MS00000_TemplateApi.Model.Options;

namespace MS00000_TemplateApi.Customizations.Extensions;
public static class GroupOptionExtension
{
    public static void GroupOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SessionCacheOption>(configuration.GetSection("SessionCacheDistr"));
        services.Configure<ConnectionStringsOptions>(configuration.GetSection("ConnectionStrings"));
    }
}
