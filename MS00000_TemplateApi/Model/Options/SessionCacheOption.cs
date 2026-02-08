namespace MS00000_TemplateApi.Model.Options;

#nullable disable
public class SessionCacheOption
{
    public string ConnectionString { get; set; }
    public int MaxRetryCount { get; set; }
    public int TimeSessionExpiration { get; set; }
}
