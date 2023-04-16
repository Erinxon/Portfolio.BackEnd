using Application.Common.Interfaces.Persitence;
using Infrastructure.Persistence;
using Infratructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfratructure(this IServiceCollection services, string ConnectionString)
        {
            services.AddDbContext<PortfolioDbContext>(option =>
            {
                option.UseSqlServer(ConnectionString);
            });
            services.AddScoped<IFromSqlRawGeneric, FromSqlRawGenery>();
            return services;
        }
    }
}
