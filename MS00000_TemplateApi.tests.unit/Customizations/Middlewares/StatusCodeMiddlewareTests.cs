using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using MS00000_TemplateApi.Customizations.Middlewares;
using MS00000_TemplateApi.Customizations.Strategies.StatusCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit.Customizations.Middlewares;

public class StatusCodeMiddlewareTests
{
    [Fact]
    public async Task InvokeAsync_StrategyTrovata_ChiamaHandleERitorna()
    {
        // Arrange
        Mock<ILogger<StatusCodeMiddleware>> loggerMock = new();
        Mock<IStatusCodeStrategyRegistry> registryMock = new();
        Mock<IStatusCodeStrategy> strategyMock = new();

        DefaultHttpContext httpContext = new();
        MemoryStream original = new();
        httpContext.Response.Body = original;
        httpContext.Response.StatusCode = StatusCodes.Status418ImATeapot;

        strategyMock
            .Setup(x => x.HandleAsync(It.IsAny<HttpContext>(), It.IsAny<Stream>(), It.IsAny<MemoryStream>(), It.IsAny<CancellationToken>()))
            .Callback<HttpContext, Stream, MemoryStream, CancellationToken>((ctx, originalBody, buffer, token) =>
            {
                byte[] data = Encoding.UTF8.GetBytes("S");
                originalBody.Write(data, 0, data.Length);
                originalBody.Flush();
            })
            .Returns(Task.CompletedTask);

        registryMock
            .Setup(x => x.TryGet(httpContext.Response.StatusCode, out It.Ref<IStatusCodeStrategy>.IsAny))
            .Callback(new TryGetCallback((int code, out IStatusCodeStrategy? s) => { s = strategyMock.Object; }))
            .Returns(true);

        bool nextCalled = false;
        RequestDelegate next = new(async ctx =>
        {
            nextCalled = true;
            byte[] bytes = Encoding.UTF8.GetBytes("X");
            await ctx.Response.Body.WriteAsync(bytes);
            await Task.CompletedTask;
        });

        StatusCodeMiddleware middleware = new(loggerMock.Object, registryMock.Object);

        // Act
        await middleware.InvokeAsync(httpContext, next);

        // Assert
        Assert.Equal("S", Encoding.UTF8.GetString(original.ToArray()));
    }

    [Fact]
    public async Task InvokeAsync_StrategyAssente_CopiaBufferSuOriginale()
    {
        // Arrange
        Mock<ILogger<StatusCodeMiddleware>> loggerMock = new();
        Mock<IStatusCodeStrategyRegistry> registryMock = new();

        DefaultHttpContext httpContext = new();
        MemoryStream original = new();
        httpContext.Response.Body = original;
        httpContext.Response.StatusCode = StatusCodes.Status200OK;

        registryMock
            .Setup(x => x.TryGet(httpContext.Response.StatusCode, out It.Ref<IStatusCodeStrategy>.IsAny))
            .Callback(new TryGetCallback((int code, out IStatusCodeStrategy? s) => { s = null; }))
            .Returns(false);

        bool nextCalled = false;
        RequestDelegate next = new(async ctx =>
        {
            nextCalled = true;
            byte[] bytes = Encoding.UTF8.GetBytes("XYZ");
            await ctx.Response.Body.WriteAsync(bytes);
            await Task.CompletedTask;
        });

        StatusCodeMiddleware middleware = new(loggerMock.Object, registryMock.Object);

        // Act
        await middleware.InvokeAsync(httpContext, next);

        // Assert
        Assert.Equal("XYZ", Encoding.UTF8.GetString(original.ToArray()));
    }

    [Fact]
    public async Task InvokeAsync_NextLancia_RipristinaOriginalBody()
    {
        // Arrange
        Mock<ILogger<StatusCodeMiddleware>> loggerMock = new();
        Mock<IStatusCodeStrategyRegistry> registryMock = new();

        DefaultHttpContext httpContext = new();
        MemoryStream original = new();
        httpContext.Response.Body = original;

        registryMock
            .Setup(x => x.TryGet(It.IsAny<int>(), out It.Ref<IStatusCodeStrategy>.IsAny))
            .Callback(new TryGetCallback((int code, out IStatusCodeStrategy? s) => { s = null; }))
            .Returns(false);

        RequestDelegate next = new(ctx => throw new InvalidOperationException("boom"));

        StatusCodeMiddleware middleware = new(loggerMock.Object, registryMock.Object);

        // Act
        try
        {
            await middleware.InvokeAsync(httpContext, next);
        }
        catch
        {
        }

        // Assert
        Assert.Same(original, httpContext.Response.Body);
    }

    private delegate void TryGetCallback(int statusCode, out IStatusCodeStrategy? strategy);
}