using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MS00000_TemplateApi.Customizations.StatusCodeResults;

namespace MS00000_TemplateApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class ApiBaseController : ControllerBase
{
    [NonAction]
    public virtual NotAcceptableResult NotAcceptable()
        => new();

    [NonAction]
    public virtual NotAcceptableObjectResult NotAcceptable([ActionResultObjectValue] object? value)
        => new(value);
}