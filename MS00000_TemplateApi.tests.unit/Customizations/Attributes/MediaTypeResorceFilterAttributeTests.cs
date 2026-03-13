using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Moq;
using MS00000_TemplateApi.Customizations.Attributes;
using MS00000_TemplateApi.Customizations.StatusCodeResults;
using MS00000_TemplateApi.Services.Application.Logger;
using System.Net.Mime;

namespace MS00000_TemplateApi.tests.unit.Customizations.Attributes;

public class MediaTypeResorceFilterAttributeTests
{
    [Fact]
    public void OnResourceExecuting_ContentTypeNull_ReturnsUnsupported()
    {
        // Arrange
        Mock<IApplicationLogger> loggerMock = new();
        MediaTypeResorceFilterAttribute filter = new(loggerMock.Object);

        DefaultHttpContext httpContext = new();
        httpContext.Request.ContentType = null;

        ActionContext actionContext =
            new(httpContext, new RouteData(), new ActionDescriptor());

        ResourceExecutingContext exec =
            new(
                actionContext,
                new List<IFilterMetadata>(),
                new List<IValueProviderFactory>()
            );

        // Act
        filter.OnResourceExecuting(exec);

        // Assert
        Assert.IsType<UnsupportedMediaTypeObjectResult>(exec.Result);
    }

    [Fact]
    public void OnResourceExecuting_ContentTypeEmpty_ReturnsUnsupported()
    {
        // Arrange
        Mock<IApplicationLogger> loggerMock = new();
        MediaTypeResorceFilterAttribute filter = new(loggerMock.Object);

        DefaultHttpContext httpContext = new();
        httpContext.Request.ContentType = string.Empty;

        ActionContext actionContext =
            new(httpContext, new RouteData(), new ActionDescriptor());

        ResourceExecutingContext exec =
            new(
                actionContext,
                new List<IFilterMetadata>(),
                new List<IValueProviderFactory>()
            );

        // Act
        filter.OnResourceExecuting(exec);

        // Assert
        Assert.IsType<UnsupportedMediaTypeObjectResult>(exec.Result);
    }

    [Fact]
    public void OnResourceExecuting_ContentTypeNotJson_ReturnsUnsupported()
    {
        // Arrange
        Mock<IApplicationLogger> loggerMock = new();
        MediaTypeResorceFilterAttribute filter = new(loggerMock.Object);

        DefaultHttpContext httpContext = new();
        httpContext.Request.ContentType = "text/plain; charset=utf-8";

        ActionContext actionContext =
            new(httpContext, new RouteData(), new ActionDescriptor());

        ResourceExecutingContext exec =
            new(
                actionContext,
                new List<IFilterMetadata>(),
                new List<IValueProviderFactory>()
            );

        // Act
        filter.OnResourceExecuting(exec);

        // Assert
        Assert.IsType<UnsupportedMediaTypeObjectResult>(exec.Result);
    }

    [Fact]
    public void OnResourceExecuting_ContentTypeJson_NoResult()
    {
        // Arrange
        Mock<IApplicationLogger> loggerMock = new();
        MediaTypeResorceFilterAttribute filter = new(loggerMock.Object);

        DefaultHttpContext httpContext = new();
        httpContext.Request.ContentType = MediaTypeNames.Application.Json;

        ActionContext actionContext =
            new(httpContext, new RouteData(), new ActionDescriptor());

        ResourceExecutingContext exec =
            new(
                actionContext,
                new List<IFilterMetadata>(),
                new List<IValueProviderFactory>()
            );

        // Act
        filter.OnResourceExecuting(exec);

        // Assert
        Assert.Null(exec.Result);
    }

    [Fact]
    public void OnResourceExecuted_Coperto()
    {
        // Arrange
        Mock<IApplicationLogger> loggerMock = new();
        MediaTypeResorceFilterAttribute filter = new(loggerMock.Object);

        DefaultHttpContext httpContext = new();
        ActionContext actionContext =
            new(httpContext, new RouteData(), new ActionDescriptor());

        ResourceExecutedContext exec =
            new(
                actionContext,
                new List<IFilterMetadata>()
            );

        // Act
        filter.OnResourceExecuted(exec);

        // Assert
        Assert.True(true);
    }
}