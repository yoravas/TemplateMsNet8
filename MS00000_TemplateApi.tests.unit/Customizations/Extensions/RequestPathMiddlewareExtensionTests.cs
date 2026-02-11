using Microsoft.AspNetCore.Builder;
using MS00000_TemplateApi.Customizations.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit.Customizations.Extensions;

public class RequestPathMiddlewareExtensionTests
{
    [Fact]
    public void ReadRequestPath_ReturnWebApplication()
    {
        //Arrange + Act
        WebApplication extension = RequestPathMiddlewareExtension.ReadRequestPath(WebApplication.Create());

        //Assert
        Assert.NotNull(extension);
    }
}
