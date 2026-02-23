using System.Text.Json.Serialization;

namespace MS00000_TemplateApi.Model.Application;
public record ApiResponse<T> where T : class
{
    [JsonConstructor]
    public ApiResponse(T response)
    {
        Response = response ?? throw new ArgumentNullException(nameof(response));
    }

    [property: JsonPropertyName("Response")]
    public T Response { get; init; }
}