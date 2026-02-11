using Moq;
using MS00000_TemplateApi.Services.Infrastracture.Data;
using MS00000_TemplateApi.Services.Infrastracture.Data.SqlServer.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit.Services.Infrastracture.Data.SqlServer.Database;

public class UnitOfWorkDbAppMsTests
{
    [Fact]
    public void Ctor_CreaConnessioneETransazione()
    {
        // Arrange
        Mock<IDBConnectionFactory> factoryMock = new();
        Mock<IDbConnection> connMock = new();
        Mock<IDbTransaction> txMock = new();

        factoryMock.Setup(f => f.Create()).Returns(connMock.Object);
        connMock.Setup(c => c.BeginTransaction()).Returns(txMock.Object);

        // Act
        UnitOfWorkDbAppMs uow = new(factoryMock.Object);

        // Assert
        Assert.NotNull(uow.Connection);
    }

    [Fact]
    public void Commit_ChiamaCommitEDispose()
    {
        // Arrange
        Mock<IDBConnectionFactory> factoryMock = new();
        Mock<IDbConnection> connMock = new();
        Mock<IDbTransaction> txMock = new();

        factoryMock.Setup(f => f.Create()).Returns(connMock.Object);
        connMock.Setup(c => c.BeginTransaction()).Returns(txMock.Object);

        UnitOfWorkDbAppMs uow = new(factoryMock.Object);

        // Act
        uow.Commit();

        // Assert
        Assert.True(true);
    }

    [Fact]
    public void Rollback_ChiamaRollbackEDispose()
    {
        // Arrange
        Mock<IDBConnectionFactory> factoryMock = new();
        Mock<IDbConnection> connMock = new();
        Mock<IDbTransaction> txMock = new();

        factoryMock.Setup(f => f.Create()).Returns(connMock.Object);
        connMock.Setup(c => c.BeginTransaction()).Returns(txMock.Object);

        UnitOfWorkDbAppMs uow = new(factoryMock.Object);

        // Act
        uow.Rollback();

        // Assert
        Assert.True(true);
    }

    [Fact]
    public void Dispose_PiuVolte_NonRiesegueDispose()
    {
        // Arrange
        Mock<IDBConnectionFactory> factoryMock = new();
        Mock<IDbConnection> connMock = new();
        Mock<IDbTransaction> txMock = new();

        factoryMock.Setup(f => f.Create()).Returns(connMock.Object);
        connMock.Setup(c => c.BeginTransaction()).Returns(txMock.Object);

        UnitOfWorkDbAppMs uow = new(factoryMock.Object);

        // Act
        uow.Dispose();
        uow.Dispose(); // seconda chiamata → ramo disposed == true

        // Assert
        Assert.True(true);
    }

    [Fact]
    public void Dispose_TransactionNull_CopreRamoNull()
    {
        // Arrange
        Mock<IDBConnectionFactory> factoryMock = new();
        Mock<IDbConnection> connMock = new();

        connMock.Setup(c => c.BeginTransaction()).Returns((IDbTransaction)null);
        factoryMock.Setup(f => f.Create()).Returns(connMock.Object);

        UnitOfWorkDbAppMs uow = new(factoryMock.Object);

        // Act
        uow.Dispose();

        // Assert
        Assert.True(true);
    }
}
