namespace MS00000_TemplateApi.Model.Application;
public record ApiResponse<T> where T : class
{
    public ApiResponse(T obj)
    {
        Response = obj;

    }

    public T Response { get; set; }

}
