using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace MS00000_TemplateApi.Customizations.StatusCodeResults;
public class UnsupportedMediaTypeObjectResult : ObjectResult
{
    private const int DefaultStatusCode = StatusCodes.Status415UnsupportedMediaType;
    public UnsupportedMediaTypeObjectResult([ActionResultObjectValue] object? value) : base(value)
    {
        StatusCode = DefaultStatusCode;
    }
}
