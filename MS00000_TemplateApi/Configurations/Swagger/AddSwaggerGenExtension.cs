using System.Reflection;

namespace MS00000_TemplateApi.Configurations.Swagger;
public static class AddSwaggerGenExtension
{
    public static void ToAddSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(opt =>
        {
            string dirName = Path.GetDirectoryName(Assembly.GetExecutingAssembly()?.Location) ?? string.Empty;
            string pathDirName = Path.Combine(dirName, "MS00000_TemplateApi.xml");
            opt.IncludeXmlComments(pathDirName);

        });
    }
}
