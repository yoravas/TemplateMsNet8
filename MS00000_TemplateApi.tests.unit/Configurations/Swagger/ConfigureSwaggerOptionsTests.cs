using Asp.Versioning;
using Asp.Versioning.ApiExplorer;

using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

using Moq;

using MS00000_TemplateApi.Configurations.Swagger;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace MS00000_TemplateApi.tests.unit.Configurations.Swagger;
public class ConfigureSwaggerOptionsTests
{
    [Fact]
    public void Configure_AddsSwaggerDocs_ForEachApiVersion()
    {
        // Arrange
        List<ApiVersionDescription> apiVersionDescriptions = new()
        {
            new ApiVersionDescription(new ApiVersion(1, 0), "v1", false),
            new ApiVersionDescription(new ApiVersion(2, 0), "v2", true)
        };

        Mock<IApiVersionDescriptionProvider> providerMock = new();
        providerMock.Setup(p => p.ApiVersionDescriptions).Returns(apiVersionDescriptions);

        // Devo usare un oggettp reale, il mock non funziona
        Dictionary<string, string?> inMemorySettings = new() {
            {"TitoloSwagger", "Test API"}
        };
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        SwaggerGenOptions options = new();

        ConfigureSwaggerOptions sut = new(providerMock.Object, configuration);

        // Act
        sut.Configure(options);

        // Assert
        Assert.True(options.SwaggerGeneratorOptions.SwaggerDocs.ContainsKey("v1"));
        Assert.True(options.SwaggerGeneratorOptions.SwaggerDocs.ContainsKey("v2"));

        OpenApiInfo v1Info = options.SwaggerGeneratorOptions.SwaggerDocs["v1"];
        Assert.Equal("Test API", v1Info.Title);
        Assert.Equal("1.0", v1Info.Version);
        //Assert.Equal("MS01450-RettCUprev", v1Info.Description);

        OpenApiInfo v2Info = options.SwaggerGeneratorOptions.SwaggerDocs["v2"];
        Assert.Equal("Test API", v2Info.Title);
        Assert.Equal("2.0", v2Info.Version);
        Assert.Contains("deprecata", v2Info.Description, System.StringComparison.OrdinalIgnoreCase);
    }
}
