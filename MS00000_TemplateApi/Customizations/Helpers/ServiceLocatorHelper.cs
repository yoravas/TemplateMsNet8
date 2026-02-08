namespace MS00000_TemplateApi.Customizations.Helpers;
public static class ServiceLocatorHelper
{
    private static IServiceProvider? serviceProvider;
    private static IServiceScopeFactory? scopeFactory;

    public static void Configure(IServiceProvider serviceProvider)
    {
        Reset();
        ServiceLocatorHelper.serviceProvider = serviceProvider;
        scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
    }

    public static T GetRequiredService<T>() where T : notnull =>
        serviceProvider is null
            ? throw new InvalidOperationException("ServiceProvider is not configured.")
            : serviceProvider.GetRequiredService<T>();


    public static IServiceScope CreateScope()
    {
        if (scopeFactory is null)
            throw new InvalidOperationException("ServiceProvider is not configured.");
        return scopeFactory.CreateScope();
    }


    public static void Reset()
    {
        serviceProvider = null;
        scopeFactory = null;
    }
}
