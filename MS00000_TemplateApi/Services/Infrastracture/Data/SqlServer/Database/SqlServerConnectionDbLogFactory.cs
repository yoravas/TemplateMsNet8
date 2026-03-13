using Microsoft.Data.SqlClient;
using System.Data;

namespace MS00000_TemplateApi.Services.Infrastracture.Data.SqlServer.Database;

public class SqlServerConnectionDbLogFactory : IDBConnectionFactory
{
    private readonly string connectionString;

    public SqlServerConnectionDbLogFactory(IConfiguration config)
    {
        connectionString = config.GetValue<string>("ConnectionStrings:LogDatabase:Connection") ??
            throw new InvalidOperationException("Missing SqlServer connection string for log.");
    }

    public IDbConnection Create() => new SqlConnection(connectionString);
}