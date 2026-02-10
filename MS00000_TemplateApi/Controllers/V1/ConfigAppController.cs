using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MS00000_TemplateApi.Customizations.Attributes;

using MS00000_TemplateApi.Model.Application;
using MS00000_TemplateApi.Model.Application.DTOs;
using MS00000_TemplateApi.Services.Application.ConfigApp.Queries.ConfigApp.GetAll;

namespace MS00000_TemplateApi.Controllers.V1;
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class ConfigAppController(IMediator mediator, ILogger<ConfigAppController> logger) : ApiBaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<ConfigAppDto>>), StatusCodes.Status200OK)]
    [ResponseOpenApi]
    [Route("GetConfigApp")]
    [Produces("application/json")]
    public async Task<IActionResult> GetConfigAppAsync()
    {
        logger.LogInformationCustom("GetConfigAppAsync called");
        List<ConfigAppDto> configList = await mediator.Send(new GetConfigAppAllQuery());

        logger.LogDebugCustom($"Lista configurazione recuperata: {configList.Count} items", additionalData: configList);
        if (configList.Any())
        {
            return Ok(new ApiResponse<List<ConfigAppDto>>(configList));
        }
        else
        {
            logger.LogInformationCustom("Nessuna configurazione trovata, restituisco NoContent");

            return NoContent();
        }

    }
}
