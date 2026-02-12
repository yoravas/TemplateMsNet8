using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using MS00000_TemplateApi.Customizations.Middlewares;
using MS00000_TemplateApi.Services.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit.Customizations.Middlewares;

public class SetColumnCustomToSerilogMiddlewareTests
{
    [Fact]
    public async Task InvokeAsync_PropagaCorrelationIdERequestPath_EseguitoNext()
    {
        // Arrange
        Mock<ILogger<SetColumnCustomToSerilogMiddleware>> loggerMock = new();
        Mock<ICorrelationIdAccessorService> correlationMock = new();
        Mock<IRequestPathAccessorService> requestPathMock = new();

        correlationMock.Setup(x => x.CorrelationId).Returns("CID-123");
        requestPathMock.Setup(x => x.RequestPath).Returns("/api/test");

        bool nextCalled = false;
        RequestDelegate next = new(async ctx =>
        {
            nextCalled = true;
            await Task.CompletedTask;
        });

        DefaultHttpContext httpContext = new();
        SetColumnCustomToSerilogMiddleware middleware =
            new(loggerMock.Object, correlationMock.Object, requestPathMock.Object);

        // Act
        await middleware.InvokeAsync(httpContext, next);

        // Assert
        Assert.True(nextCalled);
    }
}
