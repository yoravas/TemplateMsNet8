using Asp.Versioning.ApiExplorer;
using MS00000_TemplateApi.Customizations.Helpers;

namespace MS00000_TemplateApi.Configurations.Swagger;
public static class UseSwaggerUIExtension
{
    public static void ToUseSwaggerUI(this WebApplication app)
    {
        IApiVersionDescriptionProvider apiVersionDescriptionProvider = ServiceLocatorHelper.GetRequiredService<IApiVersionDescriptionProvider>();

        app.UseSwaggerUI(opt =>
        {
            foreach (ApiVersionDescription description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                opt.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
            }
        });
    }
}
