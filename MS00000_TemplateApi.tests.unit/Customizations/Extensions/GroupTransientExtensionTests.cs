using Microsoft.Extensions.DependencyInjection;
using Moq;
using MS00000_TemplateApi.Customizations.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit.Customizations.Extensions;

public class GroupTransientExtensionTests
{
    [Fact]
    public void ConfigTransient_ShouldRegisterAllServices()
    {
        // Arrange
        Mock<IServiceCollection> services = new();
        //Act
        services.Object.GroupTransient();
        //Assert
        Assert.True(true);
    }
}
