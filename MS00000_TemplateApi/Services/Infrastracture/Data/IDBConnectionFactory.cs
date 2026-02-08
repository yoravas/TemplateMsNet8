using System.Data;

namespace MS00000_TemplateApi.Services.Infrastracture.Data;
public interface IDBConnectionFactory
{
    IDbConnection Create();
}
