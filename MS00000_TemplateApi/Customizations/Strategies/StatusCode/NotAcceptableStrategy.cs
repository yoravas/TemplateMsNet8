namespace MS00000_TemplateApi.Customizations.Strategies.StatusCode;

public sealed class NotAcceptableStrategy : BaseJsonStatusCodeStrategy
{
    public override int StatusCode => StatusCodes.Status406NotAcceptable;
    protected override string Message => "Media type non supportato.";
}

