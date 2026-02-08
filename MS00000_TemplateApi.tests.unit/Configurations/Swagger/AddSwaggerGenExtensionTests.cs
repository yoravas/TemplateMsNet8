using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MS00000_TemplateApi.Configurations.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MS00000_TemplateApi.tests.unit.Configurations.Swagger;
public class AddSwaggerGenExtensionTests
{
    [Fact]
    public void ToAddSwaggerGen_RegistersSwaggerGenOptions()
    {
        // Arrange
        IServiceCollection services = new ServiceCollection();

        // Act
        AddSwaggerGenExtension.ToAddSwaggerGen(services);
        ServiceProvider provider = services.BuildServiceProvider();

        // Assert
        SwaggerGenOptions? options = provider.GetService<IOptions<SwaggerGenOptions>>()?.Value;
        Assert.NotNull(options);
    }

}
