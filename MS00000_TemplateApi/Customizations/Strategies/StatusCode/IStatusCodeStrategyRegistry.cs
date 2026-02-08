namespace MS00000_TemplateApi.Customizations.Strategies.StatusCode;
public interface IStatusCodeStrategyRegistry
{
    bool TryGet(int statusCode, out IStatusCodeStrategy strategy);
}
