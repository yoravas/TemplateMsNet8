using Dapper;
using MS00000_TemplateApi.Customizations.Consts;
using MS00000_TemplateApi.Model.Application.DTOs;
using System.Data;

namespace MS00000_TemplateApi.Services.Infrastracture.Data.SqlServer.Repository.ConfigApp;

public class ConfigAppRepository : RepositoryBase, IConfigAppRepository
{
    private readonly IDapperExecutor dapperExecutor;
    private readonly ISqlQueryLoader sqlQueryLoader;

    public ConfigAppRepository(
        [FromKeyedServices(DbConnectionRepository.DBAppMicorservizio)] IDBConnectionFactory factory,
        IDapperExecutor dapperExecutor,
        ISqlQueryLoader sqlQueryLoader) : base(factory)
    {
        this.dapperExecutor = dapperExecutor;
        this.sqlQueryLoader = sqlQueryLoader;
    }

    public async Task<IEnumerable<ConfigAppDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        using IDbConnection conn = Factory.Create();
        string sql = sqlQueryLoader.Load("Queries.ConfigApp.SelectAll.sql");
        IEnumerable<ConfigAppDto> rows = await dapperExecutor.QueryAsync<ConfigAppDto>(conn, sql, cancellationToken: cancellationToken);
        return rows;
    }
}

