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
        CommandDefinition cmd = new(
            sql,
            param,
            transaction,
            commandTimeout, 
            commandType,
            cancellationToken: cancellationToken);

        IEnumerable<T> rows = await connection.QueryAsync<T>(cmd);
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
        CommandDefinition cmd = new(
            sql,
            param,
            transaction,
            commandTimeout,
            commandType,
            cancellationToken: cancellationToken);

        T? row = await connection.QuerySingleOrDefaultAsync<T>(cmd);
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
        CommandDefinition cmd = new(
            sql,
            param,
            transaction,
            commandTimeout,
            commandType,
            cancellationToken: cancellationToken);

        int affected = await connection.ExecuteAsync(cmd);
        return affected;
    }
}