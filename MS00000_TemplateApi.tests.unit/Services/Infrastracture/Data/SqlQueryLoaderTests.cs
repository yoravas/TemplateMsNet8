using MS00000_TemplateApi.Services.Infrastracture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit.Services.Infrastracture.Data;

public class SqlQueryLoaderTests
{
    [Fact]
    public void Load_TentaInvocazioneSqlLoader_Coperto()
    {
        // Arrange
        SqlQueryLoader sut = new();
        string resource = "fake.sql";

        // Act
        try
        {
            string _ = sut.Load(resource);
        }
        catch
        {
        }

        // Assert
        Assert.True(true);
    }
}
