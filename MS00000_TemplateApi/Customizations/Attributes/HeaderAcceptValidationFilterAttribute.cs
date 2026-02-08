using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using MS00000_TemplateApi.Customizations.Enums;
using MS00000_TemplateApi.Customizations.StatusCodeResults;
using MS00000_TemplateApi.Model.Application;
using System.Net.Mime;

namespace MS00000_TemplateApi.Customizations.Attributes;
public class HeaderAcceptValidationFilterAttribute(ILogger<HeaderAcceptValidationFilterAttribute> logger) : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    { }
    public void OnActionExecuting(ActionExecutingContext context)
    {
        string endPoint = context.HttpContext.Request.Host.EndPoint(context.HttpContext.Request.Scheme, context.HttpContext.Request.Path);

        string msg = $"Inizio verifica header accept, end point: {endPoint}";

        logger.LogInformationCustom(msg);

        if (!context.HttpContext.Request.Headers.IsReadOnly)
        {
            if (context.HttpContext.Request.Headers.TryGetValue("Accept", out StringValues accept))
            {
                if (accept.ToString().Equals(MediaTypeNames.Application.Json, StringComparison.OrdinalIgnoreCase))
                {
                    logger.LogInformationCustom($"Header accept verificato correttamente: {accept}");
                }
                else
                {
                    context.Result = new NotAcceptableObjectResult(new ApiResponse<ReturnDetails>(new ReturnDetails
                    {
                        StatusCode = StatusCodes.Status406NotAcceptable,
                        Message = $"È accettato il Media Type: '{MediaTypeNames.Application.Json}.",
                        DescStatusCode = nameof(StatusCodes.Status406NotAcceptable)
                    }));



                    logger.LogWarningCustom($"Il media type ricevuto non è tra i tipi accettati: {accept}. End point: {endPoint}");
                }
            }
            else
            {
                string msg2 = $"Il media type non è presente pertanto viene forzato come default: {MediaTypeNames.Application.Json}. End point: {endPoint}";
                logger.LogInformationCustom(msg2);
                context.HttpContext.Request.Headers.Append(nameof(Headers.Accept), MediaTypeNames.Application.Json);
            }
        }
    }
}
