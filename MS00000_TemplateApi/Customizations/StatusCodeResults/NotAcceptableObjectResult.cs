using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace MS00000_TemplateApi.Customizations.StatusCodeResults;
public class NotAcceptableObjectResult : ObjectResult
{
    private const int DefaultStatusCode = StatusCodes.Status406NotAcceptable;
    public NotAcceptableObjectResult([ActionResultObjectValue] object? value) : base(value)
    {
        StatusCode = DefaultStatusCode;
    }
}
