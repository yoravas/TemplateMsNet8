using Microsoft.AspNetCore.Http;
using Moq;
using MS00000_TemplateApi.Customizations.Consts;
using MS00000_TemplateApi.Customizations.Middlewares;
using MS00000_TemplateApi.Services.Application.Logger;

namespace MS00000_TemplateApi.tests.unit.Customizations.Middlewares;

public class CorrelationIdMiddlewareTests
{
    [Fact]
    public async Task InvokeAsync_HeaderValido_NextChiamato()
    {
        // Arrange
        Mock<IApplicationLogger> loggerMock = new();
        bool nextCalled = false;
        RequestDelegate next = new(async ctx => { nextCalled = true; await Task.CompletedTask; });
        DefaultHttpContext httpContext = new();
        string id = Ulid.NewUlid().ToString();
        httpContext.Request.Headers[HeadersCustom.HeaderCorrelationID] = id;
        CorrelationIdMiddleware middleware = new(loggerMock.Object);

        // Act
        await middleware.InvokeAsync(httpContext, next);

        // Assert
        Assert.True(nextCalled);
    }

    [Fact]
    public async Task InvokeAsync_HeaderAssente_NextChiamato()
    {
        // Arrange
        Mock<IApplicationLogger> loggerMock = new();
        bool nextCalled = false;
        RequestDelegate next = new(async ctx => { nextCalled = true; await Task.CompletedTask; });
        DefaultHttpContext httpContext = new();
        CorrelationIdMiddleware middleware = new(loggerMock.Object);

        // Act
        await middleware.InvokeAsync(httpContext, next);

        // Assert
        Assert.True(nextCalled);
    }

    [Fact]
    public async Task InvokeAsync_HeaderNonValido_NextChiamato()
    {
        // Arrange
        Mock<IApplicationLogger> loggerMock = new();
        bool nextCalled = false;
        RequestDelegate next = new(async ctx => { nextCalled = true; await Task.CompletedTask; });
        DefaultHttpContext httpContext = new();
        httpContext.Request.Headers[HeadersCustom.HeaderCorrelationID] = "not-a-ulid";
        CorrelationIdMiddleware middleware = new(loggerMock.Object);

        // Act
        await middleware.InvokeAsync(httpContext, next);

        // Assert
        Assert.True(nextCalled);
    }
}