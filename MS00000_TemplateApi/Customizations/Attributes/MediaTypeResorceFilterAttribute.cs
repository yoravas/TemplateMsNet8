using Microsoft.AspNetCore.Mvc.Filters;
using MS00000_TemplateApi.Customizations.StatusCodeResults;
using MS00000_TemplateApi.Model.Application;
using System.Net.Http.Headers;
using System.Net.Mime;

namespace MS00000_TemplateApi.Customizations.Attributes;
public class MediaTypeResorceFilterAttribute(ILogger<MediaTypeResorceFilterAttribute> logger) : IResourceFilter
{
    public void OnResourceExecuted(ResourceExecutedContext context)
    { }
    public void OnResourceExecuting(ResourceExecutingContext context)
    {

        logger.LogInformationCustom("Inizio esecuzione del metodo per la validazione del media type.");

        string message = string.Empty;

        if (string.IsNullOrEmpty(context.HttpContext.Request.ContentType))
        {
            message = "Non risulta inserito il media type json.";
            logger.LogWarningCustom(message);

            context.Result = new UnsupportedMediaTypeObjectResult(new ApiResponse<ReturnDetails>(new ReturnDetails
            {
                StatusCode = StatusCodes.Status415UnsupportedMediaType,
                Message = $"È accettato il Media Type: '{MediaTypeNames.Application.Json}."
            }));
            return;
        }

        if (!string.Equals(MediaTypeHeaderValue.Parse(context.HttpContext.Request.ContentType).MediaType, MediaTypeNames.Application.Json, StringComparison.OrdinalIgnoreCase))
        {

            logger.LogWarningCustom("Media type non supportato.");

            context.Result = new UnsupportedMediaTypeObjectResult(new ApiResponse<ReturnDetails>(new ReturnDetails
            {
                StatusCode = StatusCodes.Status415UnsupportedMediaType,
                Message = $"È accettato il Media Type: '{MediaTypeNames.Application.Json}."
            }));
        }


        logger.LogInformationCustom("Fine esecuzione del metodo per la validazione del media type.");

    }
}
