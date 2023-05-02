using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Persitence
{
    public interface IFromSqlRawGeneric
    {
        Task<IEnumerable<TEntity>> GetAllFromSql<TEntity>(FromSqlRawParams fromSqlParams, CancellationToken cancellationToken) where TEntity : class;
        Task<TEntity> GetSingleFromSql<TEntity>(FromSqlRawParams fromSqlParams, CancellationToken cancellationToken) where TEntity : class;
        Task<int> ExecuteSqlRawAsync(string sql, CancellationToken cancellationToken);
        Task<int> ExecuteSqlRawAsync<T>(string sql, T Type, CancellationToken cancellationToken);
        Task BeginTransactionAsync(CancellationToken cancellationToke);
        Task CommitTransactionAsync(CancellationToken cancellationToke);
        Task RollbackTransactionAsync(CancellationToken cancellationToke);
    }

}