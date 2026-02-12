using AutoBogus;
using MS00000_TemplateApi.Model.Application.DTOs;
using MS00000_TemplateApi.tests.unit.SupportoTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit.Model.Application.DTOs;

public class ConfigAppDtoTests
{
    [Fact]
    public void ConfigAppDto_ReturnExpectedModel()
    {
        ConfigAppDto dto = new AutoFaker<ConfigAppDto>("it")
            .Configure(x => x.WithOverride(new DateOnlyAndDateTimeGeneratorOverride()))
            .Generate();
        Assert.NotNull(dto);
    }
}