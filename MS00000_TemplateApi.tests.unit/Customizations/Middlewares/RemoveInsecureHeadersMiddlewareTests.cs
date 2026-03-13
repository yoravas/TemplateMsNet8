using Microsoft.AspNetCore.Http;
using Moq;
using MS00000_TemplateApi.Customizations.Middlewares;
using MS00000_TemplateApi.Services.Application.Logger;

namespace MS00000_TemplateApi.tests.unit.Customizations.Middlewares;

public class RemoveInsecureHeadersMiddlewareTests
{
    [Fact]
    public async Task InvokeAsync_OnStartingModificaHeaders_NextChiamato()
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

        RemoveInsecureHeadersMiddleware middleware = new(loggerMock.Object);

        // Act
        await middleware.InvokeAsync(httpContext, next);

        // Assert
        Assert.True(nextCalled);
    }
}