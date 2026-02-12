using Microsoft.AspNetCore.Http;
using MS00000_TemplateApi.Customizations.Strategies.StatusCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit.Customizations.Strategies.StatusCode;

public class BadGatewayStrategyTests
{

    [Fact]
    public async Task HandleAsync_ProduceJsonNelBody()
    {
        // Arrange
        BadGatewayStrategy strategy = new();
        DefaultHttpContext httpContext = new();
        MemoryStream original = new();
        MemoryStream buffer = new();
        httpContext.Response.Body = original;

        // Act
        await strategy.HandleAsync(httpContext, original, buffer, CancellationToken.None);

        // Assert
        Assert.True(original.Length > 0);
    }    
}
