namespace MS00000_TemplateApi.Customizations.Strategies.StatusCode;
public class StatusCodeStrategyRegistry : IStatusCodeStrategyRegistry
{
    private readonly IReadOnlyDictionary<int, IStatusCodeStrategy> map;

    public StatusCodeStrategyRegistry(IEnumerable<IStatusCodeStrategy> strategies)
    {
        map = strategies.ToDictionary(s => s.StatusCode);
    }

    public bool TryGet(int statusCode, out IStatusCodeStrategy strategy) =>
        map.TryGetValue(statusCode, out strategy!);
}
