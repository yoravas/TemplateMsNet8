using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Moq;
using MS00000_TemplateApi.Customizations.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit.Customizations.Middlewares;
public class RemoveInsecureHeadersMiddlewareTests
{
    [Fact]
    public async Task InvokeAsync_OnStartingModificaHeaders_NextChiamato()
    {
        // Arrange
        Mock<ILogger<RemoveInsecureHeadersMiddleware>> loggerMock = new();
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
