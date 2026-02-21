using System.Text.Json.Serialization;

namespace MS00000_TemplateApi.Model.Application;
public record ApiResponse<T> where T : class
{
    [JsonConstructor]
    public ApiResponse([property: JsonPropertyName("Response")] T response)
    {
        Response = response;
    }

    public T Response { get; set; }
}