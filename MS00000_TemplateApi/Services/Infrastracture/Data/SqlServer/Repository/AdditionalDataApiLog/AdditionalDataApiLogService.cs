using MS00000_TemplateApi.Customizations.Consts;
using MS00000_TemplateApi.Model.Application.DTOs;
using System.Data;

namespace MS00000_TemplateApi.Services.Infrastracture.Data.SqlServer.Repository.AdditionalDataApiLog;

public class AdditionalDataApiLogService : RepositoryBase, IAdditionalDataApiLogService
{
    private readonly IDapperExecutor dapperExecutor;
    private readonly ISqlQueryLoader sqlQueryLoader;

    public AdditionalDataApiLogService([FromKeyedServices(DbConnectionRepository.DBLog)] IDBConnectionFactory factory, IDapperExecutor dapperExecutor, ISqlQueryLoader sqlQueryLoader) : base(factory)
    {
        this.dapperExecutor = dapperExecutor;
        this.sqlQueryLoader = sqlQueryLoader;
    }

    public async Task SaveAdditionalDataAsync(AdditionalDataLogDto additionalDataLog, CancellationToken ct)
    {
        using IDbConnection conn = Factory?.Create() ?? throw new InvalidOperationException("Nessuna connessione valida disponibile.");
        string sql = sqlQueryLoader.Load("Queries.AdditonalDataApiLog.InsertAdditionalData.sql");
        var parameters = new
        {
            @AdditionalDataLogID = additionalDataLog.AdditionalDataLogID,
            RequestPath = additionalDataLog.RequestPath,
            FilePath = additionalDataLog.FilePath,
            AdditionalData = additionalDataLog.AdditionalData,
            Exception = additionalDataLog.Exception
        };

        await dapperExecutor.ExecuteAsync(conn, sql, parameters, cancellationToken: ct);
    }
}