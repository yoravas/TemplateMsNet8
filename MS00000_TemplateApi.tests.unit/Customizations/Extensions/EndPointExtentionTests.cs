using Microsoft.AspNetCore.Http;
using MS00000_TemplateApi.Customizations.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit.Customizations.Extensions;

public class EndPointExtentionTests
{
    [Fact]
    public void EndPoint_WithValue_ReturnString()
    {
        //Arrange
        HostString host = new("HostValue");
        string schema = "Schema";
        string path = "Percorso";
        
        //Act
        string endpoint = EndPointExtention.EndPoint(host, schema, path);

        //Assert
        Assert.NotNull(endpoint);
    }
    [Fact]
    public void EndPoint_WithoutValue_ReturnEmptyString()
    {
        //Arrange
        HostString host = new();
        string schema = "Schema";
        string path = "Percorso";
        
        //Act
        string endpoint = EndPointExtention.EndPoint(host, schema, path);

        //Assert
        Assert.NotNull(endpoint);
    }
}
