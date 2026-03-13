namespace MS00000_TemplateApi.Model.Application.DTOs;

public class AdditionalDataLogDto
{
    public long AdditionalDataLogID { get; set; }
    public string? RequestPath { get; set; }
    public string? FilePath { get; set; }
    public string? AdditionalData { get; set; }
    public string? Exception { get; set; }
}