using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Moq;
using MS00000_TemplateApi.Services.Infrastracture.Data.SqlServer.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit.Services.Infrastracture.Data.SqlServer.Database;

public class SqlServerConnectionDbAppMsFactoryTests
{
    [Fact]
    public void Ctor_ConfigValida_IstanzaCreata()
    {
        // Arrange
        IConfiguration config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                    { "ConnectionStrings:DBAppMs:Connection", "Server=.;Database=TestDb;Trusted_Connection=True;" }
            })
            .Build();

        // Act
        SqlServerConnectionDbAppMsFactory factory = new(config);

        // Assert
        Assert.NotNull(factory);
    }

    [Fact]
    public void Ctor_ConfigAssente_LanciaInvalidOperationException()
    {
        // Arrange
        IConfiguration config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>()) // niente connection
            .Build();

        // Act + Assert
        Assert.Throws<InvalidOperationException>(() =>
        {
            _ = new SqlServerConnectionDbAppMsFactory(config);
        });
    }

    [Fact]
    public void Create_ConfigValida_CreaSqlConnection()
    {
        // Arrange
        IConfiguration config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                    { "ConnectionStrings:DBAppMs:Connection", "Server=.;Database=TestDb;Trusted_Connection=True;" }
            })
            .Build();

        SqlServerConnectionDbAppMsFactory factory = new(config);

        // Act
        IDbConnection conn = factory.Create();

        // Assert
        Assert.IsType<SqlConnection>(conn);
    }
}
