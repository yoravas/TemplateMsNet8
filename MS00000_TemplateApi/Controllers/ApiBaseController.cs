using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MS00000_TemplateApi.Customizations.StatusCodeResults;


namespace MS00000_TemplateApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public abstract class ApiBaseController : ControllerBase
{
    /// <summary>
    /// Creates an <see cref="NotAcceptableResult"/> that produces a
    /// <see cref="StatusCodes.Status406NotAcceptable/> response.
    /// </summary>
    /// <returns>The created <see cref="NotAcceptableResult"/> for the
    ///     response.</returns>
    [NonAction]
    public virtual NotAcceptableResult NotAcceptable()
        => new NotAcceptableResult();

    /// <summary>
    /// Creates an <see cref="NotAcceptableObjectResult"/> that produces a
    /// <see cref="StatusCodes.Status406NotAcceptable"/> response.
    /// </summary>
    /// <returns>The created <see cref="NotAcceptableObjectResult"/> for the
    ///     response.</returns>
    [NonAction]
    public virtual NotAcceptableObjectResult NotAcceptable([ActionResultObjectValue] object? value)
        => new NotAcceptableObjectResult(value);
}
