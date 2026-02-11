using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using MS00000_TemplateApi.Customizations.Extensions;
using MS00000_TemplateApi.Customizations.Middlewares;
using System.Net;

namespace MS00000_TemplateApi.tests.unit.Customizations.Middlewares;
public class ExceptionMiddlewareTests
{
    [Fact]
    public async Task InvokeAsync_SenzaEccezioni_NextChiamato()
    {
        // Arrange
        Mock<ILogger<ExceptionMiddleware>> loggerMock = new();
        bool nextCalled = false;
        RequestDelegate next = new(async ctx =>
        {
            nextCalled = true;
            await Task.CompletedTask;
        });

        DefaultHttpContext httpContext = new();
        httpContext.Response.Body = new MemoryStream();
        ExceptionMiddleware middleware = new(loggerMock.Object);

        // Act
        await middleware.InvokeAsync(httpContext, next);

        // Assert
        Assert.True(nextCalled);
    }

    [Fact]
    public async Task InvokeAsync_EccezioneConResponseNonAvviata_GeneraPayloadErrore()
    {
        // Arrange
        Mock<ILogger<ExceptionMiddleware>> loggerMock = new();

        RequestDelegate next = new(ctx =>
        {
            throw new Exception("boom");
        });

        DefaultHttpContext httpContext = new();
        MemoryStream original = new();
        httpContext.Response.Body = original;

        ExceptionMiddleware middleware = new(loggerMock.Object);

        // Act
        await middleware.InvokeAsync(httpContext, next);

        // Assert
        Assert.True(original.Length > 0);
    }
}