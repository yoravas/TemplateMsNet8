using MS00000_TemplateApi.Customizations.Consts;

namespace MS00000_TemplateApi.Services.Application;
public class CorrelationIdAccessorService : ICorrelationIdAccessorService
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public CorrelationIdAccessorService(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public string CorrelationId
    {
        get
        {
            return httpContextAccessor?.HttpContext?.Items[ContextItems.CorrelationID]?.ToString() ?? Ulid.NewUlid().ToString();
        }
    }
}
