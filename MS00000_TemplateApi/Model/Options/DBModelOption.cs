namespace MS00000_TemplateApi.Model.Options;
public class DBModelOption
{
    public string Connection { get; set; } = string.Empty;
    public int NumberRetries { get; set; } = 5;
}
