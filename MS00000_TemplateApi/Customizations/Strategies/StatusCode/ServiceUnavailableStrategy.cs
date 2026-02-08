namespace MS00000_TemplateApi.Customizations.Strategies.StatusCode;

public sealed class ServiceUnavailableStrategy : BaseJsonStatusCodeStrategy
{
    public override int StatusCode => StatusCodes.Status503ServiceUnavailable;
    protected override string Message => "Servizio non disponibile.";
}

