using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.Extensions.Options;
using MS00000_TemplateApi.Configurations;
using MS00000_TemplateApi.Configurations.Swagger;
using MS00000_TemplateApi.Customizations.Extensions;
using MS00000_TemplateApi.Customizations.Helpers;
using MS00000_TemplateApi.Model.Options;
using Polly;
using System.Globalization;
using System.Reflection;

namespace MS00000_TemplateApi;
public class Startup(IConfiguration configuration, IWebHostEnvironment env)
{

    public void ConfigureServices(IServiceCollection services)
    {
        services.ToAddControllers();

        services.AddEndpointsApiExplorer();

        SetCulture(services);

        services.AddHttpContextAccessor();

        services.ToAddApiVersioning();

        services.ConfigureOptions<ConfigureSwaggerOptions>();

        services.ToAddSwaggerGen();

        //services.ConfigureCors();

        services.Configure<ApiBehaviorOptions>(opt =>
        {
            opt.SuppressModelStateInvalidFilter = true;
        });

        services.AddHealthChecks()
           .AddCheck("Liveness", () => HealthCheckResult.Healthy())
           .AddCheck("Readiness", () => HealthCheckResult.Healthy());

        services.AddHttpClient("ServerApi", (sp, client) =>
        {
            ServerApiOption opt = sp.GetRequiredService<IOptionsMonitor<ServerApiOption>>().CurrentValue;

            client.BaseAddress = new Uri(opt.BaseUrl);

        }).AddStandardResilienceHandler(options =>
            {
                // Timeout complessivo della singola request (inclusi i retry)
                options.TotalRequestTimeout.Timeout = TimeSpan.FromSeconds(45);
                // Timeout complessivo della singola request (inclusi i retry)
                // Retry: riduciamo a 2 (consigliato) con backoff esponenziale + jitter
                options.Retry.MaxRetryAttempts = 3;
                options.Retry.BackoffType = DelayBackoffType.Exponential;
                options.Retry.Delay = TimeSpan.FromMilliseconds(1000);
                options.Retry.UseJitter = true;

                // Rispetta header Retry-After quando presente (es. 429/503)
                options.Retry.ShouldRetryAfterHeader = true;

                // Evita retry su metodi "unsafe" (POST/PUT/PATCH/DELETE)
                options.Retry.DisableForUnsafeHttpMethods();

                // Circuit Breaker: copiamo i tuoi parametri
                options.CircuitBreaker.FailureRatio = 0.5;
                options.CircuitBreaker.MinimumThroughput = 10;
                options.CircuitBreaker.SamplingDuration = TimeSpan.FromSeconds(30);
                options.CircuitBreaker.BreakDuration = TimeSpan.FromSeconds(30);

                // Attempt timeout (budget per singolo tentativo)
                options.AttemptTimeout.Timeout = TimeSpan.FromSeconds(15);
            });

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        //Transient
        services.GroupTransient();

        //Scoped
        services.GroupScoped();

        //Singleton
        services.GroupSingleton();

        //Options
        services.GroupOptions(configuration);

        services.AddDistributedSqlServerCache(options =>
        {
            options.ConnectionString = configuration.GetSection("SessionCacheDistr").GetValue<string>("ConnectionString");
            options.SchemaName = "dbo";
            options.TableName = "SessionCache";
        });

        services.AddSession(options =>
        {
            int sessionTimeout = configuration.GetValue<int>("SessionCacheDistr:TimeSessionExpiration");

            options.IdleTimeout = TimeSpan.FromMinutes(sessionTimeout);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

    }

    public void Configure(WebApplication app)
    {
        app.ConfigureCatchUnhandledException();

        app.UseRequestLocalization();

        ServiceLocatorHelper.Configure(app.Services);

        app.UseSwagger();

        app.ToUseSwaggerUI();

        app.UseRouting();

        app.UseSession();

        app.ReadHeaderCorrelationId();
        app.ReadRequestPath();

        app.SetColumnCustomToSerilog();

        app.CongigureResponseStatusCode();

        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.All
        });


        app.RemoveInsecureHeaders();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/actuator/health/liveness");
            endpoints.MapHealthChecks("/actuator/health/readiness");
        });

    }

    private static void SetCulture(IServiceCollection services)
    {
        CultureInfo[] supportedCultures = new[] { new CultureInfo("it-IT") };
        services.Configure<RequestLocalizationOptions>(opt =>
        {
            opt.DefaultRequestCulture = new RequestCulture("it-IT");
            opt.SupportedCultures = supportedCultures;
            opt.SupportedUICultures = supportedCultures;
        });
    }
}
