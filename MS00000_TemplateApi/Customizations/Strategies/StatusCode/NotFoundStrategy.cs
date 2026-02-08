namespace MS00000_TemplateApi.Customizations.Strategies.StatusCode;

public sealed class NotFoundStrategy : BaseJsonStatusCodeStrategy
{
    public override int StatusCode => StatusCodes.Status404NotFound;
    protected override string Message => "Endpoint inesistente.";
}
