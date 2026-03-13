using Microsoft.AspNetCore.Http;
using Moq;
using MS00000_TemplateApi.Customizations.Middlewares;
using MS00000_TemplateApi.Services.Application.Logger;

namespace MS00000_TemplateApi.tests.unit.Customizations.Middlewares;

public class ExceptionMiddlewareTests
{
    [Fact]
    public async Task InvokeAsync_SenzaEccezioni_NextChiamato()
    {
        // Arrange
        Mock<IApplicationLogger> loggerMock = new();
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
        Mock<IApplicationLogger> loggerMock = new();

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