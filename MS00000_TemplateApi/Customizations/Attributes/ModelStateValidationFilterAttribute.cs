using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MS00000_TemplateApi.Model.Application;
using MS00000_TemplateApi.Services.Application.Logger;

namespace MS00000_TemplateApi.Customizations.Attributes;

public class ModelStateValidationFilterAttribute(IApplicationLogger logger) : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    { }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            string msg = $"Il modello dati non è valido.";

            logger.Warning(msg, context.ModelState.SerializeErrors());

            context.Result = new UnprocessableEntityObjectResult(new ApiResponse<Dictionary<string, object>>(context.ModelState.SerializeErrors()));
        }

        logger.Information("Il modello dati è valido.");
    }
}