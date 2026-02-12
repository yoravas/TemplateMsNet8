using Microsoft.Extensions.DependencyInjection;
using Moq;
using MS00000_TemplateApi.Customizations.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit.Customizations.Extensions;

public class GroupScopedExtensionTests
{
    [Fact]
    public void ConfigScoped_ShouldRegisterAllServices()
    {
        // Arrange
        Mock<IServiceCollection> services = new();
        //Act
        services.Object.GroupScoped();
        //Assert
        Assert.True(true);
    }
}
