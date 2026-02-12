namespace MS00000_TemplateApi.Services.Infrastracture.Data;

public sealed class SqlQueryLoader : ISqlQueryLoader
{
    public string Load(string resourceName)
    {
        string sql = SqlLoader.Load(resourceName);
        return sql;
    }
}
