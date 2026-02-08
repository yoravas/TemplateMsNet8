namespace MS00000_TemplateApi.Configurations;
public static class CorsExtension
{
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(opt =>
        {
            opt.AddPolicy("CorsPolicy",
                builder => builder.WithOrigins("https://localhost", "")
                    .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE")
                    .AllowAnyHeader());
        });
    }
}
