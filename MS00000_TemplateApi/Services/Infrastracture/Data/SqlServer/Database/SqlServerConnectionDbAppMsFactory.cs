using Microsoft.Data.SqlClient;
using System.Data;

namespace MS00000_TemplateApi.Services.Infrastracture.Data.SqlServer.Database;
public class SqlServerConnectionDbAppMsFactory : IDBConnectionFactory
{
    private readonly string connectionString;
    public SqlServerConnectionDbAppMsFactory(IConfiguration config)
    {
        connectionString = config.GetSection("ConnectionStrings").GetSection("DBAppMs").GetValue<string>("Connection") ??
            throw new InvalidOperationException("Missing SqlServer connection string");
    }
    public IDbConnection Create() => new SqlConnection(connectionString);

}
