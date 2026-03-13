namespace MS00000_TemplateApi.Services.Infrastracture.Data.SqlServer.Repository.SequenceApiLog;

public interface ISequenceApi
{
    Task<long> GetNextValueApiAsync(CancellationToken ct);
}