using Microsoft.AspNetCore.Mvc;

namespace MS00000_TemplateApi.Customizations.StatusCodeResults;
public class NotAcceptableResult : StatusCodeResult
{
    private const int DefaultStatusCode = StatusCodes.Status406NotAcceptable;
    public NotAcceptableResult() : base(DefaultStatusCode)
    {
    }
}
