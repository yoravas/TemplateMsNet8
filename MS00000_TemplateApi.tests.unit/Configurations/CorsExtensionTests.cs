using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MS00000_TemplateApi.Configurations;

namespace MS00000_TemplateApi.tests.unit.Configurations;
public class CorsExtensionTests
{
    [Fact]
    public async Task ConfigureCors_AddsCorsPolicyWithExpectedSettings()
    {
        // Arrange
        IServiceCollection services = new ServiceCollection();

        // Act
        CorsExtension.ConfigureCors(services);
        ServiceProvider provider = services.BuildServiceProvider();
        ICorsPolicyProvider corsPolicyProvider = provider.GetRequiredService<ICorsPolicyProvider>();
        CorsPolicy? corsPolicy = await corsPolicyProvider.GetPolicyAsync(new DefaultHttpContext(), "CorsPolicy");

        // Assert
        Assert.NotNull(corsPolicy);
        Assert.Contains("https://localhost", corsPolicy.Origins);
        Assert.Contains("", corsPolicy.Origins);
        Assert.Contains("GET", corsPolicy.Methods);
        Assert.Contains("POST", corsPolicy.Methods);
        Assert.Contains("PUT", corsPolicy.Methods);
        Assert.Contains("PATCH", corsPolicy.Methods);
        Assert.Contains("DELETE", corsPolicy.Methods);
        Assert.True(corsPolicy.AllowAnyHeader);
    }
}
