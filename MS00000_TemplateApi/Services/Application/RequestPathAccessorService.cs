using MS00000_TemplateApi.Customizations.Consts;

namespace MS00000_TemplateApi.Services.Application;
public class RequestPathAccessorService : IRequestPathAccessorService
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public RequestPathAccessorService(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }
    public string RequestPath
    {
        get
        {
            return httpContextAccessor?.HttpContext?.Items[ContextItems.RequestUrl]?.ToString() ?? string.Empty;
        }
    }
}

