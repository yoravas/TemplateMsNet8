namespace MS00000_TemplateApi.Services.Infrastracture.Data;

using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

public sealed class DapperExecutor : IDapperExecutor
{
    public async Task<IEnumerable<T>> QueryAsync<T>(
        IDbConnection connection,
        string sql,
        object? param = null,
        IDbTransaction? transaction = null,
        int? commandTimeout = null,
        CommandType? commandType = null,
        CancellationToken cancellationToken = default)
    {
        IEnumerable<T> rows = await connection.QueryAsync<T>(
            sql: sql,
            param: param,
            transaction: transaction,
            commandTimeout: commandTimeout,
            commandType: commandType);
        return rows;
    }

    public async Task<T?> QuerySingleAsync<T>(
        IDbConnection connection,
        string sql,
        object? param = null,
        IDbTransaction? transaction = null,
        int? commandTimeout = null,
        CommandType? commandType = null,
        CancellationToken cancellationToken = default)
    {
        T? row = await connection.QuerySingleOrDefaultAsync<T>(
            sql: sql,
            param: param,
            transaction: transaction,
            commandTimeout: commandTimeout,
            commandType: commandType);
        return row;
    }

    public async Task<int> ExecuteAsync(
        IDbConnection connection,
        string sql,
        object? param = null,
        IDbTransaction? transaction = null,
        int? commandTimeout = null,
        CommandType? commandType = null,
        CancellationToken cancellationToken = default)
    {
        int affected = await connection.ExecuteAsync(
            sql: sql,
            param: param,
            transaction: transaction,
            commandTimeout: commandTimeout,
            commandType: commandType);
        return affected;
    }
}