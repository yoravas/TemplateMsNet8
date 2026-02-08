using Dapper;
using MS00000_TemplateApi.Customizations.Consts;
using MS00000_TemplateApi.Model.Application.DTOs;
using System.Data;

namespace MS00000_TemplateApi.Services.Infrastracture.Data.SqlServer.Repository.ConfigApp;
public class ConfigAppRepository : RepositoryBase, IConfigAppRepository
{
    public ConfigAppRepository([FromKeyedServices(DbConnectionRepository.DBAppMicorservizio)] IDBConnectionFactory factory) : base(factory)
    {

    }

    public async Task<IEnumerable<ConfigAppDto>> GetAllAsync()
    {
        using IDbConnection conn = Factory.Create();
        string sql = SqlLoader.Load("Queries.ConfigApp.SelectAll.sql");

        IEnumerable<ConfigAppDto> configApp = await conn.QueryAsync<ConfigAppDto>(sql);

        return configApp;
    }
}
