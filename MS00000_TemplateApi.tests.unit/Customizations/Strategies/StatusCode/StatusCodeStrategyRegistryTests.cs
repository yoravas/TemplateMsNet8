using Microsoft.AspNetCore.Http;
using Moq;
using MS00000_TemplateApi.Customizations.Strategies.StatusCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit.Customizations.Strategies.StatusCode;


public class StatusCodeStrategyRegistryTests
{
    [Fact]
    public void TryGet_StatusPresente_RitornaTrue()
    {
        // Arrange
        Mock<IStatusCodeStrategy> s200Mock = new();
        s200Mock.Setup(x => x.StatusCode).Returns(StatusCodes.Status200OK);
        Mock<IStatusCodeStrategy> s404Mock = new();
        s404Mock.Setup(x => x.StatusCode).Returns(StatusCodes.Status404NotFound);
        IEnumerable<IStatusCodeStrategy> strategies = new[] { s200Mock.Object, s404Mock.Object };
        StatusCodeStrategyRegistry registry = new(strategies);

        // Act
        bool found = registry.TryGet(StatusCodes.Status404NotFound, out IStatusCodeStrategy strategy);

        // Assert
        Assert.True(found);
    }

    [Fact]
    public void TryGet_StatusAssente_RitornaFalse()
    {
        // Arrange
        Mock<IStatusCodeStrategy> s200Mock = new();
        s200Mock.Setup(x => x.StatusCode).Returns(StatusCodes.Status200OK);
        IEnumerable<IStatusCodeStrategy> strategies = new[] { s200Mock.Object };
        StatusCodeStrategyRegistry registry = new(strategies);

        // Act
        bool found = registry.TryGet(StatusCodes.Status500InternalServerError, out IStatusCodeStrategy strategy);

        // Assert
        Assert.False(found);
    }

    [Fact]
    public void Ctor_StatusDuplicato_LanciaArgumentException()
    {
        // Arrange
        Mock<IStatusCodeStrategy> s200a = new();
        s200a.Setup(x => x.StatusCode).Returns(StatusCodes.Status200OK);
        Mock<IStatusCodeStrategy> s200b = new();
        s200b.Setup(x => x.StatusCode).Returns(StatusCodes.Status200OK);
        IEnumerable<IStatusCodeStrategy> strategies = new[] { s200a.Object, s200b.Object };

        // Act + Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _ = new StatusCodeStrategyRegistry(strategies);
        });
    }
}

