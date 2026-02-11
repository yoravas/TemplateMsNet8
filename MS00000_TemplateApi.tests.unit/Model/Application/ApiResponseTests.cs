using AutoBogus;
using MS00000_TemplateApi.Model.Application;
using MS00000_TemplateApi.Model.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit.Model.Application;

public class ApiResponseTests
{
    [Fact]
    public void ApiResponse_ReturnExpected()
    {
        object dto = new
        {
            Nome = "Mario"
        };
        ApiResponse<object> response = new(dto);
            
        Assert.NotNull(response.Response);
    }
}