using AutoBogus;
using MS00000_TemplateApi.Model.Application;
using MS00000_TemplateApi.Model.Application.DTOs;
using MS00000_TemplateApi.tests.unit.SupportoTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit.Model.Application;

public class ReturnDetailsTests
{
    [Fact]
    public void ReturnDetails_ReturnExpectedModel()
    {
        ReturnDetails dto = new AutoFaker<ReturnDetails>("it")
            .Configure(x => x.WithOverride(new DateOnlyAndDateTimeGeneratorOverride()))
            .Generate();
        Assert.NotNull(dto);
    }
}
