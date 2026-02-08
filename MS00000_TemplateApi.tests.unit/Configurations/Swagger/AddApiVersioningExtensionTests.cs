using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MS00000_TemplateApi.Configurations.Swagger;

namespace MS00000_TemplateApi.tests.unit.Configurations.Swagger;
public class AddApiVersioningExtensionTests
{
    [Fact]
    public void ToAddApiVersioning_ConfiguresApiVersioningAndExplorerOptions()
    {
        // Arrange
        IServiceCollection services = new ServiceCollection();

        // Act
        AddApiVersioningExtension.ToAddApiVersioning(services);
        ServiceProvider provider = services.BuildServiceProvider();

        IOptions<ApiVersioningOptions> versioningOptions = provider.GetRequiredService<IOptions<ApiVersioningOptions>>();
        IOptions<ApiExplorerOptions> explorerOptions = provider.GetRequiredService<IOptions<ApiExplorerOptions>>();

        // Assert
        Assert.Equal(new ApiVersion(1, 0), versioningOptions.Value.DefaultApiVersion);
        Assert.True(versioningOptions.Value.AssumeDefaultVersionWhenUnspecified);
        Assert.True(versioningOptions.Value.ReportApiVersions);

        Assert.NotNull(versioningOptions.Value.ApiVersionReader);
        Assert.IsAssignableFrom<IApiVersionReader>(versioningOptions.Value.ApiVersionReader);

        Assert.Equal("'v'VVV", explorerOptions.Value.GroupNameFormat);
        Assert.True(explorerOptions.Value.SubstituteApiVersionInUrl);
    }
}
