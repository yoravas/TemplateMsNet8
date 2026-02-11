using AutoBogus;
using Microsoft.AspNetCore.Http;
using MS00000_TemplateApi.Customizations.Helpers;
using MS00000_TemplateApi.Model.Application;
using MS00000_TemplateApi.tests.unit.SupportoTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit.Customizations.Helpers;

public class ResponseBodyHelperTests
{
    [Fact]
    public void OriginalBodyStream_CambiaBody_E_RestituisceBuffer()
    {
        // Arrange
        DefaultHttpContext context = new();
        Stream initialBody = new MemoryStream();

        context.Response.Body = initialBody;

        // Act
        Stream result = ResponseBodyHelper.OriginalBodyStream(context, out Stream originalBody);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task FlushBuffer_SerializeEFlush_Coperto()
    {
        // Arrange
        DefaultHttpContext context = new()
        {
            RequestAborted = CancellationToken.None
        };

        MemoryStream buffer = new();
        ApiResponse<ReturnDetails> payload = new AutoFaker<ApiResponse<ReturnDetails>>("it")
            .Configure(x => x.WithOverride(new DateOnlyAndDateTimeGeneratorOverride()))
            .Generate();

        // Act
        await ResponseBodyHelper.FlushBuffer(context, buffer, payload);

        // Assert
        Assert.True(buffer.Length > 0);
    }    
}
