using MS00000_TemplateApi.Model.Application;
using System.Text.Json;

namespace MS00000_TemplateApi.Customizations.Helpers;
public static class ResponseBodyHelper
{
    public static MemoryStream OriginalBodyStream(HttpContext context, out Stream originalBody)
    {
        originalBody = context.Response.Body;
        MemoryStream buffer = new();
        context.Response.Body = buffer;
        return buffer;
    }

    public static async Task FlushBuffer(HttpContext context, MemoryStream buffer, ApiResponse<ReturnDetails> payload)
    {
        await JsonSerializer.SerializeAsync(buffer, payload, cancellationToken: context.RequestAborted);
        await buffer.FlushAsync(context.RequestAborted);
    }
}
