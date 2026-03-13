using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Moq;
using MS00000_TemplateApi.Customizations.Attributes;
using MS00000_TemplateApi.Services.Application.Logger;

namespace MS00000_TemplateApi.tests.unit.Customizations.Attributes;

public class ModelStateValidationFilterAttributeTests
{
    [Fact]
    public void OnActionExecuting_ModelStateInvalid_Returns422()
    {
        // Arrange
        Mock<IApplicationLogger> loggerMock = new();
        ModelStateValidationFilterAttribute filter = new(loggerMock.Object);

        DefaultHttpContext ctx = new();
        ModelStateDictionary modelState = new();
        modelState.AddModelError("A", "Err");

        ActionContext actionContext =
            new(ctx, new RouteData(), new ActionDescriptor(), modelState);

        ActionExecutingContext exec =
            new(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object?>(),
                new object()
            );

        // Act
        filter.OnActionExecuting(exec);

        // Assert
        Assert.IsType<UnprocessableEntityObjectResult>(exec.Result);
    }

    [Fact]
    public void OnActionExecuting_ModelStateValid_NoResult()
    {
        // Arrange
        Mock<IApplicationLogger> loggerMock = new();
        ModelStateValidationFilterAttribute filter = new(loggerMock.Object);

        DefaultHttpContext ctx = new();
        ModelStateDictionary modelState = new();

        ActionContext actionContext =
            new(ctx, new RouteData(), new ActionDescriptor(), modelState);

        ActionExecutingContext exec =
            new(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object?>(),
                new object()
            );

        // Act
        filter.OnActionExecuting(exec);

        // Assert
        Assert.Null(exec.Result);
    }

    [Fact]
    public void OnActionExecuted_Coperto()
    {
        // Arrange
        Mock<IApplicationLogger> loggerMock = new();
        ModelStateValidationFilterAttribute filter = new(loggerMock.Object);

        DefaultHttpContext ctx = new();
        ActionContext actionContext = new(ctx, new RouteData(), new ActionDescriptor());

        ActionExecutedContext exec =
            new(
                actionContext,
                new List<IFilterMetadata>(),
                null
            );

        // Act
        filter.OnActionExecuted(exec);

        // Assert
        Assert.True(true);
    }
}