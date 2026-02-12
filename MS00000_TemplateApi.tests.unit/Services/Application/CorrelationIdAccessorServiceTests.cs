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

public class CorrelationIdAccessorServiceTests
{
    [Fact]
    public void CorrelationId_AllPresent_ReturnString()
    {
        //Arrange
        DefaultHttpContext context = new();
        context.Items.Add(ContextItems.CorrelationID, "CorrelationIDTest");
        Mock<IHttpContextAccessor> httpContextAccessorMock = new();
        httpContextAccessorMock
            .Setup(x => x.HttpContext)
            .Returns(context);

        CorrelationIdAccessorService service = new(httpContextAccessorMock.Object);

        //Act
        string correlationId = service.CorrelationId;

        //Assert
        Assert.NotNull(correlationId);
    }
    [Fact]
    public void CorrelationId_WithoutCorrelationID_ReturnString()
    {
        //Arrange
        DefaultHttpContext context = new();
        Mock<IHttpContextAccessor> httpContextAccessorMock = new();
        httpContextAccessorMock
            .Setup(x => x.HttpContext)
            .Returns(context);

        CorrelationIdAccessorService service = new(httpContextAccessorMock.Object);

        //Act
        string correlationId = service.CorrelationId;

        //Assert
        Assert.NotNull(correlationId);
    }
    [Fact]
    public void CorrelationId_WithoutHttpContext_ReturnString()
    {
        //Arrange
        Mock<IHttpContextAccessor> httpContextAccessorMock = new();
        httpContextAccessorMock
            .Setup(x => x.HttpContext)
            .Returns((HttpContext)null);

        CorrelationIdAccessorService service = new(httpContextAccessorMock.Object);

        //Act
        string correlationId = service.CorrelationId;

        //Assert
        Assert.NotNull(correlationId);
    }
    
}
