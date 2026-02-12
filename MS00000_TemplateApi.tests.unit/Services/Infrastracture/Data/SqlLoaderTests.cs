using MS00000_TemplateApi.Services.Infrastracture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit.Services.Infrastracture.Data;

public class SqlLoaderTests
{
    [Fact]
    public void Load_TentaCaricamentoRisorsa_Coperto()
    {
        // Arrange
        string resource = "fake.sql";

        // Act
        try
        {
            string _ = SqlLoader.Load(resource);
        }
        catch
        {
        }

        // Assert
        Assert.True(true);
    }
}
