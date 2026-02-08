using System.Data;

namespace MS00000_TemplateApi.Services.Infrastracture.Data;
public abstract class RepositoryBase
{
    protected readonly IDBConnectionFactory Factory;
    protected readonly IUnitOfWork? Uow;

    protected RepositoryBase(IDBConnectionFactory factory) => Factory = factory;

    protected RepositoryBase(IUnitOfWork uow) => Uow = uow;

    protected IDbConnection GetConnection() => Uow?.Connection ?? Factory.Create();

    protected IDbTransaction? GetTransaction() => Uow?.Transaction;
}
