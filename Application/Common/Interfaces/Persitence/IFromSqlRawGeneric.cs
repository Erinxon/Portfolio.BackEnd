﻿using Domain.Shared;
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
    }

}