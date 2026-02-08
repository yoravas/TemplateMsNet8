using System.Net;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Moq;

using MS00000_TemplateApi.Customizations.Extensions;
using MS00000_TemplateApi.Customizations.Helpers;
using MS00000_TemplateApi.tests.unit.SupportoTests;

namespace MS00000_TemplateApi.tests.unit.Customizations.Middlewares;
public class Status406NotAccettableMiddlewareTests
{
    [Fact]
    public async Task Returns_CustomJson_When_Unauthorized()
    {
        // Arrange




        Mock<IHttpContextAccessor> httpContextAccessorMock = new();
        Mock<ISession> sessionMock = new();




        var httpContext = new DefaultHttpContext();
        httpContext.Session = sessionMock.Object;
        httpContextAccessorMock.Setup(a => a.HttpContext).Returns(httpContext);

        ServiceLocatorHelper.Configure(serviceProvider: new TestServiceProvider(httpContextAccessorMock.Object));


        Mock<ILogger> loggerMock = new();

        IHostBuilder builder = Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseTestServer();
                webBuilder.Configure(app =>
                {
                    // Middleware da testare
                    app.CongigureResponseStatusCode();

                    // Forzo 406
                    app.Run(context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                        return Task.CompletedTask;
                    });
                });
            });

        using IHost host = await builder.StartAsync();
        HttpClient client = host.GetTestClient();

        // Act
        HttpResponseMessage response = await client.GetAsync("/");

        // Assert
        Assert.Equal(HttpStatusCode.NotAcceptable, response.StatusCode);

        string content = await response.Content.ReadAsStringAsync();

        Assert.NotNull(content);
    }
}
