using Microsoft.AspNetCore.Http;
using Moq;
using MS00000_TemplateApi.Customizations.Consts;
using MS00000_TemplateApi.Services.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit.Services.Application;

public class RequestPathAccessorServiceTests
{
    [Fact]
    public void RequestPath_AllPresent_ReturnString()
    {
        //Arrange
        DefaultHttpContext context = new();
        context.Items.Add(ContextItems.RequestUrl, "RequestPathTest");
        Mock<IHttpContextAccessor> httpContextAccessorMock = new();
        httpContextAccessorMock
            .Setup(x => x.HttpContext)
            .Returns(context);

        RequestPathAccessorService service = new(httpContextAccessorMock.Object);

        //Act
        string requestPath = service.RequestPath;

        //Assert
        Assert.NotNull(requestPath);
    }
    [Fact]
    public void RequestPath_WithoutRequestPath_ReturnString()
    {
        //Arrange
        DefaultHttpContext context = new();
        Mock<IHttpContextAccessor> httpContextAccessorMock = new();
        httpContextAccessorMock
            .Setup(x => x.HttpContext)
            .Returns(context);

        RequestPathAccessorService service = new(httpContextAccessorMock.Object);

        //Act
        string requestPath = service.RequestPath;

        //Assert
        Assert.NotNull(requestPath);
    }
    [Fact]
    public void RequestPath_WithoutHttpContext_ReturnString()
    {
        //Arrange
        Mock<IHttpContextAccessor> httpContextAccessorMock = new();
        httpContextAccessorMock
            .Setup(x => x.HttpContext)
            .Returns((HttpContext)null);

        RequestPathAccessorService service = new(httpContextAccessorMock.Object);

        //Act
        string requestPath = service.RequestPath;

        //Assert
        Assert.NotNull(requestPath);
    }    
}
