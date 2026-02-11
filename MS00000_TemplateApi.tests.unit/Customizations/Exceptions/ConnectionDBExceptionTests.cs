using MS00000_TemplateApi.Customizations.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit.Customizations.Exceptions;

public class ConnectionDBExceptionTests
{
    [Fact]
    public void ConnectionDBException_EmptyCtor_ReturnModel()
    {
        //Arrange + Act
        ConnectionDBException connectionDBException = new();

        //Assert
        Assert.NotNull(connectionDBException);
    }
    [Fact]
    public void ConnectionDBException_WithMessageCtor_ReturnModel()
    {
        //Arrange + Act
        ConnectionDBException connectionDBException = new("Message");

        //Assert
        Assert.NotNull(connectionDBException);
    }
    [Fact]
    public void ConnectionDBException_WithMessageAndExceptionCtor_ReturnModel()
    {
        //Arrange + Act
        ConnectionDBException connectionDBException = new("Message", new Exception());

        //Assert
        Assert.NotNull(connectionDBException);
    }
}
