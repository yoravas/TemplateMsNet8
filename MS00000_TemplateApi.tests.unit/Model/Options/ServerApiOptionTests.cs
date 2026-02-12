using AutoBogus;
using MS00000_TemplateApi.Model.Options;
using MS00000_TemplateApi.tests.unit.SupportoTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit.Model.Options;

public class ServerApiOptionTests
{
    [Fact]
    public void ServerApiOption_ReturnExpectedModel()
    {
        ServerApiOption dto = new AutoFaker<ServerApiOption>("it")
            .Configure(x => x.WithOverride(new DateOnlyAndDateTimeGeneratorOverride()))
            .Generate();
        Assert.NotNull(dto);
    }
}