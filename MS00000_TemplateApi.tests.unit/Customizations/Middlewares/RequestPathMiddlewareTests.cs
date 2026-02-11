using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using MS00000_TemplateApi.Customizations.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit.Customizations.Middlewares;

public class RequestPathMiddlewareTests
{
    [Fact]
    public async Task InvokeAsync_UrlCompostaENextChiamato()
    {
        // Arrange
        Mock<ILogger<RequestPathMiddleware>> loggerMock = new();
        bool nextCalled = false;

        RequestDelegate next = new(async ctx =>
        {
            nextCalled = true;
            await Task.CompletedTask;
        });

        DefaultHttpContext httpContext = new();
        httpContext.Request.Scheme = "https";
        httpContext.Request.Host = new HostString("example.com");
        httpContext.Request.Path = "/api/test";
        httpContext.Request.QueryString = new QueryString("?a=1");

        RequestPathMiddleware middleware = new(loggerMock.Object);

        // Act
        await middleware.InvokeAsync(httpContext, next);

        // Assert
        Assert.True(nextCalled);
    }
}
