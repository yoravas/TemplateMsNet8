using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MS00000_TemplateApi.Model.Application;

namespace MS00000_TemplateApi.Customizations.Attributes;
public class ModelStateValidationFilterAttribute(ILogger<ModelStateValidationFilterAttribute> logger) : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    { }
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            string msg = $"Il modello dati non è valido.";

            logger.LogWarningCustom(msg, context.ModelState.SerializeErrors());

            context.Result = new UnprocessableEntityObjectResult(new ApiResponse<Dictionary<string, object>>(context.ModelState.SerializeErrors()));

        }

        logger.LogInformationCustom("Il modello dati è valido.");
    }
}
