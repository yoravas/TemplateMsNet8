using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MS00000_TemplateApi.Configurations.Swagger;
public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, IConfiguration configuration) : IConfigureNamedOptions<SwaggerGenOptions>
{
    public void Configure(string? name, SwaggerGenOptions options)
    {
        foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(
                description.GroupName,
                CreateVersionInfo(description));

        }
    }
    public void Configure(SwaggerGenOptions options)
    {
        if (options is not null)
        {
            Configure(null, options);
        }
    }

    private OpenApiInfo CreateVersionInfo(ApiVersionDescription desc)
    {
        OpenApiInfo info = new()
        {
            Title = configuration.GetValue<string>("TitoloSwagger"),
            Version = desc.ApiVersion.ToString(),
            Description = desc.IsDeprecated
                ? "Questa versione dell'API è stata deprecata. Utilizza una delle nuove versioni disponibili."
                : "Documentazione API."


        };

        return info;
    }
}
