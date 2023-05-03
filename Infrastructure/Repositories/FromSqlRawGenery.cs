using Application.Common.Interfaces.Persitence;
using Application.Services.WorkExperience.Commands;
using Application.Tools;
using Domain.Shared;
using Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Data;

namespace Infrastructure.Repositories
{
    public class FromSqlRawGenery : IFromSqlRawGeneric
    {
        private readonly PortfolioDbContext dbContext;
        private IDbContextTransaction transaction;

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

        public async Task<int> ExecuteSqlRawAsync<T>(string SQL, T Type, CancellationToken cancellationToken)
        {
            List<SqlParameter> parameters = new()
            {
                new SqlParameter("@Identity", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            foreach (var Property in Type.GetType().GetProperties())
            {
                var value = Property.GetValue(Type, null);
                var name = Property.Name;
                var type = Property.PropertyType;

                SqlDbType? sqlDbType = Utility.GetSqlDbType(type);

                if (sqlDbType.HasValue)
                {
                    parameters.Add(new SqlParameter("@" + name, value ?? DBNull.Value)
                    {
                        SqlDbType = (SqlDbType) sqlDbType
                    });
                }
            }
            _ = await this.dbContext.Database.ExecuteSqlRawAsync(SQL, parameters, cancellationToken);
            return (int)parameters.FirstOrDefault().Value;
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToke)
        {
            IDbContextTransaction transaction = await this.dbContext.Database.BeginTransactionAsync(cancellationToke);
            this.transaction = transaction;
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToke)
        {
            await this.transaction.CommitAsync(cancellationToke);
            await this.transaction.DisposeAsync();
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToke)
        {
            await this.transaction.RollbackAsync(cancellationToke);
            await this.transaction.DisposeAsync();
        }
      
    }
}
