namespace MS00000_TemplateApi.Customizations.Helpers;

public sealed class CombinedDisposableHelper : IDisposable
{
    private readonly IDisposable[] disposables;

    public CombinedDisposableHelper(params IDisposable[] disposables)
    {
        this.disposables = disposables;
    }

    public void Dispose()
    {
        foreach (IDisposable d in disposables)
            d.Dispose();
    }
}