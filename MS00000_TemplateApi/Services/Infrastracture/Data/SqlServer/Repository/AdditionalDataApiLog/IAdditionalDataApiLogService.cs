using MS00000_TemplateApi.Model.Application.DTOs;

namespace MS00000_TemplateApi.Services.Infrastracture.Data.SqlServer.Repository.AdditionalDataApiLog;

public interface IAdditionalDataApiLogService
{
    Task SaveAdditionalDataAsync(AdditionalDataLogDto additionalDataLog, CancellationToken ct);
}