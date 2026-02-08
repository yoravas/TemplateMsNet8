using Microsoft.AspNetCore.Http;

namespace MS00000_TemplateApi.tests.unit.SupportoTests
{
    public class TestServiceProvider : IServiceProvider
    {
        private readonly IHttpContextAccessor _accessor;
        public TestServiceProvider(IHttpContextAccessor accessor) => _accessor = accessor;
        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(IHttpContextAccessor))
                return _accessor;
            throw new NotImplementedException();
        }
    }
}
