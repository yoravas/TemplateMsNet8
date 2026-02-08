using MS00000_TemplateApi.Customizations.Strategies.StatusCode;

namespace MS00000_TemplateApi.Customizations.Extensions;
public static class GroupSingletonExtension
{
    public static void GroupSingleton(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromApplicationDependencies()
            .AddClasses(c => c.AssignableTo<IStatusCodeStrategy>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime());

        services.AddSingleton<IStatusCodeStrategyRegistry, StatusCodeStrategyRegistry>();
    }
}
