using MediatR;
using MS00000_TemplateApi.Model.Application.DTOs;
using MS00000_TemplateApi.Services.Infrastracture.Data.SqlServer.Repository.ConfigApp;
using System.Text.Json;

namespace MS00000_TemplateApi.Services.Application.ConfigApp.Queries.ConfigApp.GetAll;
public class GetConfigAppAllQueryHandler : IRequestHandler<GetConfigAppAllQuery, List<ConfigAppDto>>
{
    private readonly IConfigAppRepository configAppRepository;
    private readonly IMediator mediator;
    private readonly ILogger<GetConfigAppAllQueryHandler> logger;

    public GetConfigAppAllQueryHandler(IConfigAppRepository configAppRepository, IMediator mediator, ILogger<GetConfigAppAllQueryHandler> logger)
    {
        this.configAppRepository = configAppRepository;
        this.mediator = mediator;
        this.logger = logger;
    }
    public async Task<List<ConfigAppDto>> Handle(GetConfigAppAllQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<ConfigAppDto> result = await configAppRepository.GetAllAsync();


        if (result is null)
            return new List<ConfigAppDto>();

        string jsonResult = JsonSerializer.Serialize(result);
        logger.LogDebugCustom($"GetConfigAppAllQueryHandler {jsonResult}", additionalData: result);
        return result.ToList();

    }
}
