using AutoBogus;
using MS00000_TemplateApi.Model.ValueObjects;
using MS00000_TemplateApi.tests.unit.SupportoTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    [Fact]
    public void Sql_EmptyConstructor_ShouldAllowSettingValue()
    {
        // Arrange
        Sql sql = new();
        string fakeValue = new AutoFaker<string>("it")
            .Configure(x => x.WithOverride(new DateOnlyAndDateTimeGeneratorOverride()))
            .Generate();

        // Act
        sql.Value = fakeValue;

        // Assert
        Assert.Equal(fakeValue, sql.Value);
    }
}