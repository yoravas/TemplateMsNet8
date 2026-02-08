namespace MS00000_TemplateApi.Customizations.Exceptions;
public class ConnectionDBException : Exception
{
    public ConnectionDBException() : base() { }
    public ConnectionDBException(string message) : base(message) { }
    public ConnectionDBException(string message, Exception ex) : base(message, ex) { }
}
