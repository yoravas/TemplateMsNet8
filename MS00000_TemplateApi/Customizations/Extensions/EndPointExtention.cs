namespace MS00000_TemplateApi.Customizations.Extensions;
public static class EndPointExtention
{
    public static string EndPoint(this HostString host, string schema, string path)
    {
        if (host.HasValue)
        {
            return string.Concat(schema, "://", host.Value, path);
        }
        else
        {
            return string.Empty;
        }
    }
}
