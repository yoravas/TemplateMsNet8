using MS00000_TemplateApi.Model.Application;

namespace MS00000_TemplateApi.Customizations.Strategies.StatusCode;
public abstract class BaseJsonStatusCodeStrategy : IStatusCodeStrategy
{
    public abstract int StatusCode { get; }
    protected abstract string Message { get; }

    public async Task HandleAsync(HttpContext context, Stream originalBody, MemoryStream buffer, CancellationToken ct)
    {
        // Svuota qualsiasi output già bufferizzato
        buffer.SetLength(0);

        // Ripristina lo stream reale prima di scrivere
        context.Response.Body = originalBody;

        // Pulisce intestazioni potenzialmente già impostate
        context.Response.Headers.ContentLength = null;
        context.Response.ContentLength = null;
        context.Response.ContentType = null;

        // Forza JSON e status
        context.Response.ContentType = "application/json; charset=utf-8";
        context.Response.StatusCode = StatusCode;

        ApiResponse<ReturnDetails> payload = new(new ReturnDetails
        {
            Message = Message,
            StatusCode = StatusCode,
            DescStatusCode = StatusCode.ToString()
        });

        await context.Response.WriteAsJsonAsync(payload, cancellationToken: ct);
        await context.Response.Body.FlushAsync(ct);
    }
}

