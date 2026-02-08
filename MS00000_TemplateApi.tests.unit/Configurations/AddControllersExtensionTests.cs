using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using MS00000_TemplateApi.Configurations;

namespace MS00000_TemplateApi.tests.unit.Configurations;

public class AddControllersExtensionTests
{
    [Fact]
    public void ToAddControllers_ConfiguresMvcAndJsonOptions()
    {
        // Arrange
        ServiceCollection services = new();

        // Act
        services.ToAddControllers();
        ServiceProvider provider = services.BuildServiceProvider();

        MvcOptions mvcOptions = provider.GetRequiredService<IOptions<MvcOptions>>().Value;
        JsonOptions jsonOptions = provider.GetRequiredService<IOptions<JsonOptions>>().Value;

        // Assert
        Assert.True(mvcOptions.RespectBrowserAcceptHeader);
        Assert.True(mvcOptions.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes);
        Assert.True(mvcOptions.ReturnHttpNotAcceptable);
        Assert.True(mvcOptions.AllowEmptyInputInBodyModelBinding);

        Assert.IsType<PascalCaseNamingPolicy>(jsonOptions.JsonSerializerOptions.PropertyNamingPolicy);
    }
}
