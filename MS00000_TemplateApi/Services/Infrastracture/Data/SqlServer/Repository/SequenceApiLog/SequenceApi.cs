using MS00000_TemplateApi.Customizations.Consts;
using System.Data;

namespace MS00000_TemplateApi.Services.Infrastracture.Data.SqlServer.Repository.SequenceApiLog;

public class SequenceApi : RepositoryBase, ISequenceApi
{
    private readonly IDapperExecutor dapperExecutor;
    private readonly ISqlQueryLoader sqlQueryLoader;

    public SequenceApi([FromKeyedServices(DbConnectionRepository.DBAppMicorservizio)] IDBConnectionFactory factory, IDapperExecutor dapperExecutor, ISqlQueryLoader sqlQueryLoader) : base(factory)
    {
        this.dapperExecutor = dapperExecutor;
        this.sqlQueryLoader = sqlQueryLoader;
    }

    public async Task<long> GetNextValueApiAsync(CancellationToken ct)
    {
        using IDbConnection conn = Factory?.Create() ?? throw new InvalidOperationException("Nessuna connessione valida disponibile.");
        string sql = sqlQueryLoader.Load("Queries.SequenceApiLog.SelectNextValue.sql");
        long id = await dapperExecutor.QuerySingleAsync<long>(conn, sql, cancellationToken: ct);
        return id;
    }
}