namespace MS00000_TemplateApi.Customizations.Strategies.StatusCode;

public sealed class BadGatewayStrategy : BaseJsonStatusCodeStrategy
{
    public override int StatusCode => StatusCodes.Status502BadGateway;
    protected override string Message => "Errore di comunicazione con il server.";
}

