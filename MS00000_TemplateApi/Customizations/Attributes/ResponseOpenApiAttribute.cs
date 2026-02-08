using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using MS00000_TemplateApi.Model.Application;

namespace MS00000_TemplateApi.Customizations.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class ResponseOpenApiAttribute : Attribute
{
    public static void Apply(ActionDescriptor actionDescriptor)
    {
        IList<object> filters = actionDescriptor.EndpointMetadata;

        void AddIfNotExists(Type responseType, int statusCode)
        {
            bool alreadyExists = filters.OfType<ProducesResponseTypeAttribute>()
                .Any(attr => attr.StatusCode == statusCode && attr.Type == responseType);

            if (!alreadyExists)
            {
                filters.Add(new ProducesResponseTypeAttribute(responseType, statusCode));
            }
        }

        AddIfNotExists(typeof(ApiResponse<ReturnDetails>), StatusCodes.Status406NotAcceptable);
        AddIfNotExists(typeof(ApiResponse<ReturnDetails>), StatusCodes.Status404NotFound);
        AddIfNotExists(typeof(ApiResponse<ReturnDetails>), StatusCodes.Status401Unauthorized);
        AddIfNotExists(typeof(ApiResponse<ReturnDetails>), StatusCodes.Status415UnsupportedMediaType);
        AddIfNotExists(typeof(ApiResponse<ReturnDetails>), StatusCodes.Status400BadRequest);
        AddIfNotExists(typeof(ApiResponse<ReturnDetails>), StatusCodes.Status500InternalServerError);
        AddIfNotExists(typeof(ApiResponse<ReturnDetails>), StatusCodes.Status502BadGateway);
        AddIfNotExists(typeof(ApiResponse<ReturnDetails>), StatusCodes.Status503ServiceUnavailable);
        AddIfNotExists(typeof(ApiResponse<ReturnDetails>), StatusCodes.Status204NoContent);

    }
}
