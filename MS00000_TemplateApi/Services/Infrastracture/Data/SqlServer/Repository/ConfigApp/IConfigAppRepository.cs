using MS00000_TemplateApi.Model.Application.DTOs;

namespace MS00000_TemplateApi.Services.Infrastracture.Data.SqlServer.Repository.ConfigApp;
public interface IConfigAppRepository
{
    Task<IEnumerable<ConfigAppDto>> GetAllAsync(CancellationToken cancellationToken);
}