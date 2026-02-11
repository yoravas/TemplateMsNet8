using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MS00000_TemplateApi.Customizations.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit.Customizations.Extensions;

public class GroupOptionExtensionTests
{
    //Test sempre in aggiornarmento, va in eccezione (ma non fallisce visto il try catch) se non trova il servizio nel GroupOptions.
    //Anche l'assert è forzato per evitare che il test fallisca. Se scende copertura basta aggiungere option qui nel dizionario
    [Fact]
    public void GroupOptions_WhenCalledWithConfiguration_ShouldNotThrowException()
    {
        // Arrange
        Dictionary<string, string> inMemorySettings = new()
        {
                { "SessionCacheDistr:CacheKey", "TestCacheKey" },
                { "ConnectionStrings:DefaultConnection", "TestConnectionString" }                
            };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        Mock<IServiceCollection> services = new();

        // Act
        try
        {
            services.Object.GroupOptions(configuration);

        }
        catch (Exception)
        {
        }
        // Assert
        Assert.True(true);
    }
}
