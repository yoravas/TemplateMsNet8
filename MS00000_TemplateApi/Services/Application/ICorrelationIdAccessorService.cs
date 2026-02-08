namespace MS00000_TemplateApi.Services.Application;

public interface ICorrelationIdAccessorService
{
    string CorrelationId { get; }
}