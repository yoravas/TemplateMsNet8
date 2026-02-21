using Asp.Versioning;
using Flowify.Contracts;
using Microsoft.AspNetCore.Mvc;

using MS00000_TemplateApi.Model.Application;
using MS00000_TemplateApi.Model.Application.DTOs;
using MS00000_TemplateApi.Services.Application.ConfigApp.Queries.ConfigApp.GetAll;

namespace MS00000_TemplateApi.Controllers.V1;
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[ProducesResponseType(typeof(ApiResponse<ReturnDetails>), StatusCodes.Status406NotAcceptable)]
[ProducesResponseType(typeof(ApiResponse<ReturnDetails>), StatusCodes.Status404NotFound)]
[ProducesResponseType(typeof(ApiResponse<ReturnDetails>), StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(ApiResponse<ReturnDetails>), StatusCodes.Status415UnsupportedMediaType)]
[ProducesResponseType(typeof(ApiResponse<ReturnDetails>), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(ApiResponse<ReturnDetails>), StatusCodes.Status500InternalServerError)]
[ProducesResponseType(typeof(ApiResponse<ReturnDetails>), StatusCodes.Status502BadGateway)]
[ProducesResponseType(typeof(ApiResponse<ReturnDetails>), StatusCodes.Status503ServiceUnavailable)]
[ProducesResponseType(typeof(ApiResponse<ReturnDetails>), StatusCodes.Status406NotAcceptable)]
public class ConfigAppController(IMediator mediator, ILogger<ConfigAppController> logger) : ApiBaseController
{

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<ConfigAppDto>>), StatusCodes.Status200OK)]
    [Route("GetConfigApp")]
    [Produces("application/json")]
    public async Task<IActionResult> GetConfigAppAsync(CancellationToken cancellationToken)
    {
        logger.LogInformationCustom("GetConfigAppAsync called");
        List<ConfigAppDto> configList = await mediator.Send(new GetConfigAppAllQuery(), cancellationToken);

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

