using MS00000_TemplateApi.Customizations.Consts;
using System.Data;

namespace MS00000_TemplateApi.Services.Infrastracture.Data.SqlServer.Database;
public class UnitOfWorkDbAppMs : IUnitOfWork
{
    public IDbConnection Connection { get; }
    public IDbTransaction Transaction { get; }

    private bool disposed;

    public UnitOfWorkDbAppMs([FromKeyedServices(DbConnectionRepository.DBAppMicorservizio)] IDBConnectionFactory factory)
    {
        Connection = factory.Create();
        Connection.Open();
        Transaction = Connection.BeginTransaction();
    }

    public void Commit()
    {
        Transaction.Commit();
        Dispose();
    }

    public void Rollback()
    {
        Transaction.Rollback();
        Dispose();
    }

    public void Dispose()
    {
        if (disposed)
            return;

        Transaction?.Dispose();
        Connection?.Dispose();
        disposed = true;
    }
}
