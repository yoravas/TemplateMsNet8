using Moq;
using MS00000_TemplateApi.Model.Application.DTOs;
using MS00000_TemplateApi.Services.Infrastracture.Data;
using MS00000_TemplateApi.Services.Infrastracture.Data.SqlServer.Repository.ConfigApp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit.Services.Infrastracture.Data.SqlServer.Repository.ConfigApp;

public class ConfigAppRepositoryTests
{
    [Fact]
    public async Task GetAllAsync_AllPresent_RitornaLista()
    {
        // Arrange
        Mock<IDBConnectionFactory> factoryMock = new();
        Mock<IDbConnection> connMock = new();
        factoryMock.Setup(f => f.Create()).Returns(connMock.Object);

        Mock<ISqlQueryLoader> loaderMock = new();
        loaderMock.Setup(l => l.Load(It.IsAny<string>())).Returns("SELECT 1");

        Mock<IDapperExecutor> execMock = new();
        List<ConfigAppDto> lista = new() { new ConfigAppDto() };
        execMock.Setup(e => e.QueryAsync<ConfigAppDto>(
                It.IsAny<IDbConnection>(),
                It.IsAny<string>(),
                It.IsAny<object?>(),
                It.IsAny<IDbTransaction?>(),
                It.IsAny<int?>(),
                It.IsAny<CommandType?>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(lista);

        ConfigAppRepository repo = new(factoryMock.Object, execMock.Object, loaderMock.Object);

        // Act
        IEnumerable<ConfigAppDto> result = await repo.GetAllAsync();

        // Assert
        Assert.NotNull(result);
    }
}
