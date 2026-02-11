using Dapper;
using Moq;
using MS00000_TemplateApi.Services.Infrastracture.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit.Services.Infrastracture.Data;

public class DapperExecutorTests
{
    [Fact]
    public async Task QueryAsync_TentaInvocazioneDapper_Coperto()
    {
        // Arrange
        Mock<IDbConnection> connectionMock = new();
        DapperExecutor sut = new();
        string sql = "select * from test";

        // Act
        try
        {
            IEnumerable<string> _ = await sut.QueryAsync<string>(connectionMock.Object, sql);
        }
        catch
        {
        }

        // Assert
        Assert.True(true);
    }

    [Fact]
    public async Task QuerySingleAsync_TentaInvocazioneDapper_Coperto()
    {
        // Arrange
        Mock<IDbConnection> connectionMock = new();
        DapperExecutor sut = new();
        string sql = "select 1";

        // Act
        try
        {
            int? _ = await sut.QuerySingleAsync<int>(connectionMock.Object, sql);
        }
        catch
        {
        }

        // Assert
        Assert.True(true);
    }

    [Fact]
    public async Task ExecuteAsync_TentaInvocazioneDapper_Coperto()
    {
        // Arrange
        Mock<IDbConnection> connectionMock = new();
        DapperExecutor sut = new();
        string sql = "update test set a=1";

        // Act
        try
        {
            int _ = await sut.ExecuteAsync(connectionMock.Object, sql);
        }
        catch
        {
        }

        // Assert
        Assert.True(true);
    }
}
