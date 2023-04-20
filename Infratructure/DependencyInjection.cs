using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persitence;
using Infrastructure.Persistence;
using Infratructure.Authentication;
using Infratructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfratructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<PortfolioDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IFromSqlRawGeneric, FromSqlRawGenery>();
            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<IJwtGenerator, JwtGenerator>();
            services.AddJwt(configuration);
            return services;
        }

        private static IServiceCollection AddJwt(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options => {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = configuration["Jwt:Issuer"],
                       ValidAudience = configuration["Jwt:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                   };
               });


            return services;
        }
    }
}
