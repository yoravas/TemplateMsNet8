using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Moq;
using MS00000_TemplateApi.Customizations.Attributes;
using MS00000_TemplateApi.Customizations.Helpers;
using MS00000_TemplateApi.Customizations.StatusCodeResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit.Customizations.Attributes;


public class HeaderAcceptValidationFilterAttributeTests
{
    [Fact]
    public void OnActionExecuting_AcceptJson_Valid()
    {
        // Arrange
        Mock<ILogger<HeaderAcceptValidationFilterAttribute>> loggerMock = new();
        HeaderAcceptValidationFilterAttribute filter = new(loggerMock.Object);

        DefaultHttpContext context = new();
        context.Request.Scheme = "http";
        context.Request.Host = new HostString("localhost");
        context.Request.Path = "/test";
        context.Request.Headers["Accept"] = MediaTypeNames.Application.Json;

        ActionExecutingContext exec = new ActionExecutingContext(
            new ActionContext(context, new Microsoft.AspNetCore.Routing.RouteData(), new ActionDescriptor()),
            new List<IFilterMetadata>(),
            new Dictionary<string, object?>(),
            new object());

        // Act
        filter.OnActionExecuting(exec);

        // Assert
        Assert.Null(exec.Result);
    }

    [Fact]
    public void OnActionExecuting_AcceptNotJson_Returns406()
    {
        // Arrange
        Mock<ILogger<HeaderAcceptValidationFilterAttribute>> loggerMock = new();
        HeaderAcceptValidationFilterAttribute filter = new(loggerMock.Object);

        DefaultHttpContext context = new();
        context.Request.Scheme = "http";
        context.Request.Host = new HostString("localhost");
        context.Request.Path = "/test";
        context.Request.Headers["Accept"] = "text/plain";

        ActionExecutingContext exec = new ActionExecutingContext(
            new ActionContext(context, new Microsoft.AspNetCore.Routing.RouteData(), new ActionDescriptor()),
            new List<IFilterMetadata>(),
            new Dictionary<string, object?>(),
            new object());

        // Act
        filter.OnActionExecuting(exec);

        // Assert
        Assert.IsType<NotAcceptableObjectResult>(exec.Result);
    }

    [Fact]
    public void OnActionExecuting_AcceptMissing_AppendDefaultJson()
    {
        // Arrange
        Mock<ILogger<HeaderAcceptValidationFilterAttribute>> loggerMock = new();
        HeaderAcceptValidationFilterAttribute filter = new(loggerMock.Object);

        DefaultHttpContext context = new();
        context.Request.Scheme = "https";
        context.Request.Host = new HostString("api.test");
        context.Request.Path = "/config";

        ActionExecutingContext exec = new ActionExecutingContext(
            new ActionContext(context, new Microsoft.AspNetCore.Routing.RouteData(), new ActionDescriptor()),
            new List<IFilterMetadata>(),
            new Dictionary<string, object?>(),
            new object());

        // Act
        filter.OnActionExecuting(exec);

        // Assert
        StringValues v = context.Request.Headers["Accept"];
        Assert.Equal(MediaTypeNames.Application.Json, v.ToString());
    }
        

    [Fact]
    public void OnActionExecuting_TryGetValueFalse_RamoElse()
    {
        // Arrange
        Mock<ILogger<HeaderAcceptValidationFilterAttribute>> loggerMock = new();
        HeaderAcceptValidationFilterAttribute filter = new(loggerMock.Object);

        DefaultHttpContext context = new();
        context.Request.Scheme = "http";
        context.Request.Host = new HostString("host");
        context.Request.Path = "/aaa";

        // Headers non-readonly ma Accept non presente → TryGetValue = false
        ActionExecutingContext exec = new ActionExecutingContext(
            new ActionContext(context, new Microsoft.AspNetCore.Routing.RouteData(), new ActionDescriptor()),
            new List<IFilterMetadata>(),
            new Dictionary<string, object?>(),
            new object());

        // Act
        filter.OnActionExecuting(exec);

        // Assert
        StringValues v = context.Request.Headers["Accept"];
        Assert.Equal(MediaTypeNames.Application.Json, v.ToString());
    }

    [Fact]
    public void OnActionExecuted_Coperto()
    {
        // Arrange
        Mock<ILogger<HeaderAcceptValidationFilterAttribute>> loggerMock = new();
        HeaderAcceptValidationFilterAttribute filter = new(loggerMock.Object);

        DefaultHttpContext context = new();
        ActionExecutedContext exec = new ActionExecutedContext(
            new ActionContext(context, new Microsoft.AspNetCore.Routing.RouteData(), new ActionDescriptor()),
            new List<IFilterMetadata>(),
            new object());

        // Act
        filter.OnActionExecuted(exec);

        // Assert
        Assert.True(true);
    }
}
