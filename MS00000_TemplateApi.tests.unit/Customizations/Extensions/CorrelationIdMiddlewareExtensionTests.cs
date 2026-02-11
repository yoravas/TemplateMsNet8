using Microsoft.AspNetCore.Builder;
using MS00000_TemplateApi.Customizations.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit.Customizations.Extensions;

public class CorrelationIdMiddlewareExtensionTests
{
    [Fact]
    public void CorrelationIdMiddlewareExtension_ReturnWebApplication()
    {
        //Arrange + Act
        WebApplication extension = CorrelationIdMiddlewareExtension.ReadHeaderCorrelationId(WebApplication.Create());

        //Assert
        Assert.NotNull(extension);
    }
}
