using Microsoft.AspNetCore.Builder;
using Moq;
using MS00000_TemplateApi.Customizations.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit.Customizations.Extensions;

public class ResponseStatusCodeExtensionTests
{
    [Fact]
    public void CongigureResponseStatusCode_ReturnWebApplication()
    {
        //Arrange + Act
        WebApplication extension = ResponseStatusCodeExtension.CongigureResponseStatusCode(WebApplication.Create());

        //Assert
        Assert.NotNull(extension);
    }
}
