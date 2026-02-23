using System.Data;

namespace MS00000_TemplateApi.Services.Infrastracture.Data;

public abstract class RepositoryBase
{
    protected readonly IDBConnectionFactory? Factory;
    protected readonly IUnitOfWork? Uow;

    protected RepositoryBase(IDBConnectionFactory factory) => Factory = factory ?? throw new ArgumentNullException(nameof(factory));

    protected RepositoryBase(IUnitOfWork uow) => Uow = uow ?? throw new ArgumentNullException(nameof(uow));

    protected IDbConnection GetConnection() => Uow?.Connection ?? Factory?.Create() ?? throw new InvalidOperationException("Nessuna connessione valida disponibile.");

    protected IDbTransaction? GetTransaction() => Uow?.Transaction;
}