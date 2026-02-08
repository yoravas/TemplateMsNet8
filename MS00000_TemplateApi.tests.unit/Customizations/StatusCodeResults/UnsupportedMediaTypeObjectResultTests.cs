using Microsoft.AspNetCore.Http;
using MS00000_TemplateApi.Customizations.StatusCodeResults;

namespace MS00000_TemplateApi.tests.unit.Customizations.StatusCodeResults;
public class UnsupportedMediaTypeObjectResultTests
{
    [Fact]
    public void Constructor_SetsStatusCodeAndValue()
    {
        // Arrange
        var value = new { error = "Unsupported media type" };

        // Act
        UnsupportedMediaTypeObjectResult result = new(value);

        // Assert
        Assert.Equal(StatusCodes.Status415UnsupportedMediaType, result.StatusCode);
        Assert.Equal(value, result.Value);
    }

    [Fact]
    public void Constructor_AllowsNullValue()
    {
        // Act
        UnsupportedMediaTypeObjectResult result = new(null);

        // Assert
        Assert.Equal(StatusCodes.Status415UnsupportedMediaType, result.StatusCode);
        Assert.Null(result.Value);
    }
}
