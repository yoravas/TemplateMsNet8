namespace MS00000_TemplateApi.Model.Application;
public record ReturnDetails
{
    public int StatusCode { get; set; }
    public string? DescStatusCode { get; set; }
    public string? Message { get; set; }
}
