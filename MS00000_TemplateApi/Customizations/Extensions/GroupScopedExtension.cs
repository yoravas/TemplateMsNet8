using MS00000_TemplateApi.Customizations.Attributes;
using MS00000_TemplateApi.Customizations.Consts;
using MS00000_TemplateApi.Customizations.Middlewares;
using MS00000_TemplateApi.Services.Application;
using MS00000_TemplateApi.Services.Infrastracture.Data;
using MS00000_TemplateApi.Services.Infrastracture.Data.SqlServer.Database;
using MS00000_TemplateApi.Services.Infrastracture.Data.SqlServer.Repository.ConfigApp;

namespace MS00000_TemplateApi.Customizations.Extensions;
public static class GroupScopedExtension
{
    public static void GroupScoped(this IServiceCollection services)
    {
        AddKeyedScoped(services);

        services.AddScoped<MediaTypeResorceFilterAttribute>();
        services.AddScoped<HeaderAcceptValidationFilterAttribute>();
        services.AddScoped<ModelStateValidationFilterAttribute>();
        services.AddScoped<IRequestPathAccessorService, RequestPathAccessorService>();
        services.AddScoped<ICorrelationIdAccessorService, CorrelationIdAccessorService>();
        services.AddScoped<CorrelationIdMiddleware>();
        services.AddScoped<ExceptionMiddleware>();
        services.AddScoped<RemoveInsecureHeadersMiddleware>();
        services.AddScoped<RequestPathMiddleware>();


        services.AddScoped<SetColumnCustomToSerilogMiddleware>();
        services.AddScoped<IConfigAppRepository, ConfigAppRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWorkDbAppMs>();
        services.AddScoped<IDapperExecutor, DapperExecutor>();
        services.AddScoped<ISqlQueryLoader, SqlQueryLoader>();






    }

    private static void AddKeyedScoped(IServiceCollection services)
    {
        services.AddKeyedScoped<IDBConnectionFactory, SqlServerConnectionDbAppMsFactory>(DbConnectionRepository.DBAppMicorservizio);

    }
}
