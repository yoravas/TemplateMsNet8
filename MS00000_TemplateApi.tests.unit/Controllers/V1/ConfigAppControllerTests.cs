using Flowify.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MS00000_TemplateApi.Controllers.V1;
using MS00000_TemplateApi.Model.Application.DTOs;
using MS00000_TemplateApi.Services.Application.ConfigApp.Queries.ConfigApp.GetAll;
using MS00000_TemplateApi.Services.Application.Logger;

namespace MS00000_TemplateApi.tests.unit.Controllers.V1;

public class ConfigAppControllerTests
{
    [Fact]
    public async Task GetConfigAppAsync_ListNotEmpty_ReturnsOk()
    {
        // Arrange
        Mock<IMediator> mediatorMock = new();
        List<ConfigAppDto> list = new() { new ConfigAppDto() };
        mediatorMock
            .Setup(x => x.Send(It.IsAny<GetConfigAppAllQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(list);

        Mock<IApplicationLogger> loggerMock = new();
        ConfigAppController controller = new(mediatorMock.Object, loggerMock.Object)
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        // Act
        IActionResult result = await controller.GetConfigAppAsync(new CancellationToken(false));

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetConfigAppAsync_ListEmpty_ReturnsNoContent()
    {
        // Arrange
        Mock<IMediator> mediatorMock = new();
        List<ConfigAppDto> emptyList = new();
        mediatorMock
            .Setup(x => x.Send(It.IsAny<GetConfigAppAllQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(emptyList);

        Mock<IApplicationLogger> loggerMock = new();
        ConfigAppController controller = new(mediatorMock.Object, loggerMock.Object)
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        // Act
        IActionResult result = await controller.GetConfigAppAsync(new CancellationToken(false));

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}