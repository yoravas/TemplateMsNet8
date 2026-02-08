namespace MS00000_TemplateApi.Configurations;
public static class AddControllersExtension
{
    public static void ToAddControllers(this IServiceCollection services)
    {
        services.AddControllers(config =>
        {
            config.RespectBrowserAcceptHeader = true;
            config.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            config.ReturnHttpNotAcceptable = true;
            config.AllowEmptyInputInBodyModelBinding = true;

        })
       .AddJsonOptions(opt =>
       {
           opt.JsonSerializerOptions.PropertyNamingPolicy = new PascalCaseNamingPolicy();
       });
    }
}
