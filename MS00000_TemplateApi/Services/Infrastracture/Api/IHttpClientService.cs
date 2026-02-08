namespace MS00000_TemplateApi.Services.Infrastracture.Api;
public interface IHttpClientService
{
    Task<HttpResponseMessage> ExecuteAsync(HttpRequestMessage request, CancellationToken ct = default);
}
