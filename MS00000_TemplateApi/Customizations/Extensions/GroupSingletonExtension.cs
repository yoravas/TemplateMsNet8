using MS00000_TemplateApi.Customizations.Strategies.StatusCode;
using MS00000_TemplateApi.Services.Application.Enqueue;

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
        services.AddSingleton(typeof(IChannelService<>), typeof(ChannelService<>));
    }
}