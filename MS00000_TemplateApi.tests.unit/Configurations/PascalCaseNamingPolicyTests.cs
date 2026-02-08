using MS00000_TemplateApi.Configurations;

namespace MS00000_TemplateApi.tests.unit.Configurations;
public class PascalCaseNamingPolicyTests
{
    [Theory]
    [InlineData("test", "Test")]
    [InlineData("Test", "Test")]
    [InlineData("t", "T")]
    [InlineData("T", "T")]
    [InlineData("", "")]
    [InlineData("testName", "TestName")]
    [InlineData("1test", "1test")]
    [InlineData(null, null)]
    public void ConvertName_ReturnsExpectedResult(string input, string expected)
    {
        // Arrange
        PascalCaseNamingPolicy policy = new();
        // Act
        string result = policy.ConvertName(input);
        // Assert
        Assert.Equal(expected, result);
    }
}
