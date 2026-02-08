namespace MS00000_TemplateApi.Customizations.Strategies.StatusCode;

public sealed class UnauthorizedStrategy : BaseJsonStatusCodeStrategy
{
    public override int StatusCode => StatusCodes.Status401Unauthorized;
    protected override string Message => "Impossibile accedere se non si è autorizzati.";
}

