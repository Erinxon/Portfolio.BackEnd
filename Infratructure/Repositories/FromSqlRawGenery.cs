using Application.Common.Interfaces.Persitence;
using Domain.Shared;
using Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infratructure.Repositories
{
    public class FromSqlRawGenery : IFromSqlRawGeneric
    {
        private readonly PortfolioDbContext dbContext;

        public FromSqlRawGenery(PortfolioDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<TEntity>> GetAllFromSql<TEntity>(FromSqlRawParams fromSqlParams, CancellationToken cancellationToken) where TEntity : class
        {
            return await this.dbContext.Set<TEntity>().FromSqlRaw(fromSqlParams.Sql, fromSqlParams.Parameters).ToListAsync(cancellationToken);
        }

        public async Task<TEntity> GetSingleFromSql<TEntity>(FromSqlRawParams fromSqlParams, CancellationToken cancellationToken) where TEntity : class
        {
            var result = await this.dbContext.Set<TEntity>().FromSqlRaw(fromSqlParams.Sql, fromSqlParams.Parameters).ToListAsync(cancellationToken);
            return result.FirstOrDefault();
        }

        public async Task<int> ExecuteSqlRawAsync(string sql, CancellationToken cancellationToken)
        {
            List<SqlParameter> parameters = new()
            {
                new SqlParameter("@Identity", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            _ = await this.dbContext.Database.ExecuteSqlRawAsync(sql, parameters, cancellationToken);
            return (int) parameters.FirstOrDefault().Value;

        }

    }
}
