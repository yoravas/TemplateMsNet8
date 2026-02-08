using System.Data;

namespace MS00000_TemplateApi.Services.Infrastracture.Data;
public interface IUnitOfWork : IDisposable
{
    IDbConnection Connection { get; }
    IDbTransaction Transaction { get; }

    void Commit();
    void Rollback();
}
