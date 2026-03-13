using MS00000_TemplateApi.Model.Application.DTOs;
using MS00000_TemplateApi.Services.Application.Enqueue;
using MS00000_TemplateApi.Services.Infrastracture.Data.SqlServer.Repository.AdditionalDataApiLog;

namespace MS00000_TemplateApi.Services.Workers;

public class AdditionalDataLogWorker : BackgroundService
{
    private readonly IServiceScopeFactory serviceScopeFactory;
    private readonly IChannelService<AdditionalDataLogDto> channelService;

    public AdditionalDataLogWorker(IServiceScopeFactory serviceScopeFactory, IChannelService<AdditionalDataLogDto> channelService)
    {
        this.serviceScopeFactory = serviceScopeFactory;
        this.channelService = channelService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (AdditionalDataLogDto item in channelService.Reader.ReadAllAsync(stoppingToken))
        {
            try
            {
                await WorkAdditionalDataAsync(item, stoppingToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore durante l'inserimento dei dati aggiuntivi: {ex.ToString()}");
            }
        }
    }

    private async Task WorkAdditionalDataAsync(AdditionalDataLogDto item, CancellationToken ct)
    {
        await InsertAdditionalDataAsync(item, ct);
    }

    private async Task InsertAdditionalDataAsync(AdditionalDataLogDto item, CancellationToken ct)
    {
        using IServiceScope scope = serviceScopeFactory.CreateScope();
        IAdditionalDataApiLogService additionalDataApiLogService = scope.ServiceProvider.GetRequiredService<IAdditionalDataApiLogService>();
        await additionalDataApiLogService.SaveAdditionalDataAsync(item, ct);
    }
}