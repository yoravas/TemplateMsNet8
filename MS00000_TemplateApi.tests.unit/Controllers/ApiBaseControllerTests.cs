using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS00000_TemplateApi.Controllers;
using MS00000_TemplateApi.Customizations.StatusCodeResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit.Controllers;
public class ApiBaseControllerTests
{
    private class TestController : ApiBaseController { }

    [Fact]
    public void NotAcceptable_ReturnsNotAcceptableResult()
    {
        // Arrange
        TestController controller = new();

        // Act
        IActionResult result = controller.NotAcceptable();

        // Assert
        NotAcceptableResult notAcceptableResult = Assert.IsType<NotAcceptableResult>(result);
        Assert.Equal(StatusCodes.Status406NotAcceptable, notAcceptableResult.StatusCode);
    }

    [Fact]
    public void NotAcceptable_WithValue_ReturnsNotAcceptableObjectResult()
    {
        // Arrange
        TestController controller = new();
        object value = new { Message = "Not acceptable" };

        // Act
        IActionResult result = controller.NotAcceptable(value);

        // Assert
        NotAcceptableObjectResult objectResult = Assert.IsType<NotAcceptableObjectResult>(result);
        Assert.Equal(StatusCodes.Status406NotAcceptable, objectResult.StatusCode);
        Assert.Equal(value, objectResult.Value);
    }
}
