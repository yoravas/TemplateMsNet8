using Microsoft.Extensions.Options;
using MS00000_TemplateApi.Model.Options;

namespace MS00000_TemplateApi.Services.Infrastracture.Api;
public class HttpClientService : IHttpClientService
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly ServerApiOption serverApiOption;
    public HttpClientService(IHttpClientFactory httpClientFactory, IOptionsMonitor<ServerApiOption> serverApiOption)
    {
        this.httpClientFactory = httpClientFactory;
        this.serverApiOption = serverApiOption.CurrentValue;
    }

    public async Task<HttpResponseMessage> ExecuteAsync(HttpRequestMessage request, CancellationToken ct = default)
    {
        HttpClient client = httpClientFactory.CreateClient("ServerApi");

        HttpResponseMessage response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct);

        return response;
    }


    // Esempio GET che utilizza ExecuteAsync per fare una chiamata specifica
    //public async Task<string> GetResourceAsync(string id, CancellationToken ct)
    //{
    //    using var req = new HttpRequestMessage(HttpMethod.Get, $"/resource/{id}");
    //    var resp = await httpClientService.ExecuteAsync(req, ct);

    //    // Se vuoi gestire qui gli errori
    //    if (!resp.IsSuccessStatusCode)
    //    {
    //        var body = await resp.Content.ReadAsStringAsync(ct);
    //        // logging e gestione dominio...
    //        throw new HttpRequestException($"Upstream error {(int)resp.StatusCode}: {body}");
    //    }

    //    return await resp.Content.ReadAsStringAsync(ct);
    //}

    // Esempio POST che utilizza ExecuteAsync per fare una chiamata specifica
    //public async Task<HttpResponseMessage> CreateAsync(object payload, CancellationToken ct)
    //{
    //    using var req = new HttpRequestMessage(HttpMethod.Post, "/resource");
    //    req.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

    //    // Per operazioni “write”, la pipeline standard non farà retry (grazie a DisableForUnsafeHttpMethods)
    //    return await httpClientService.ExecuteAsync(req, ct);
    //}

}


