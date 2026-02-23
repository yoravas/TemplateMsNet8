using AutoBogus;
using MS00000_TemplateApi.Model.ValueObjects;
using MS00000_TemplateApi.tests.unit.SupportoTests;

namespace MS00000_TemplateApi.tests.unit.Model.ValueObjects;

public class SqlTests
{
    [Fact]
    public void Sql_ToString_ShouldReturnValue()
    {
        // Arrange
        string fakeValue = new AutoFaker<string>("it")
            .Configure(x => x.WithOverride(new DateOnlyAndDateTimeGeneratorOverride()))
            .Generate();
        Sql sql = new(fakeValue);

        // Act
        string result = sql.ToString();

        // Assert
        Assert.Equal(fakeValue, result);
    }
}