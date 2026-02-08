namespace MS00000_TemplateApi.Customizations.Strategies.StatusCode;
public interface IStatusCodeStrategy
{
    int StatusCode { get; }
    Task HandleAsync(HttpContext context, Stream originalBody, MemoryStream buffer, CancellationToken ct);
}
