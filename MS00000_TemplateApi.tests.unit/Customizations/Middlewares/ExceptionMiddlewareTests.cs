using System.Net;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using MS00000_TemplateApi.Customizations.Extensions;

namespace MS00000_TemplateApi.tests.unit.Customizations.Middlewares;
public class ExceptionMiddlewareTests
{
    [Theory]
    [InlineData("Development")]
    [InlineData("Production")] //Environments.Production
    public async Task ConfigureExceptionHandler_HandlesException(string testEnvironment)
    {
        // Arrange
        WebApplicationBuilder builder = WebApplication.CreateBuilder(new[] { "--urls", "http://localhost:5557" });
        builder.Logging.AddConsole();
        builder.Environment.EnvironmentName = testEnvironment;

        WebApplication app = builder.Build();

        ILogger<ExceptionMiddlewareTests> logger = app.Services.GetRequiredService<ILogger<ExceptionMiddlewareTests>>();
        IWebHostEnvironment env = app.Services.GetRequiredService<IWebHostEnvironment>();

        app.ConfigureCatchUnhandledException();

        app.MapGet("/", context => throw new Exception("Test exception"));

        await app.StartAsync();
        HttpClient client = new() { BaseAddress = new Uri("http://localhost:5557") };

        // Act
        HttpResponseMessage response = await client.GetAsync("/");

        // Assert
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);

        await app.StopAsync();
    }
}
