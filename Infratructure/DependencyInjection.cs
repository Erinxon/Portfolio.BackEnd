using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persitence;
using Application.Specifications;
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
                option.UseSqlServer(configuration.GetConnectionString(ConstSetting.ConnectionString));
            });
            services.AddScoped<IFromSqlRawGeneric, FromSqlRawGenery>();
            services.Configure<JwtAuthSetting>(configuration.GetSection(ConstSetting.JwtAuthSection)); 
            services.AddSingleton<IJwtGenerator, JwtGenerator>();
            services.AddJwt(configuration);
            return services;
        }

        private static IServiceCollection AddJwt(this IServiceCollection services, ConfigurationManager configuration)
        {
            var JwtAuthSettings = new JwtAuthSetting();
            configuration.Bind(ConstSetting.JwtAuthSection, JwtAuthSettings);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options => {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = JwtAuthSettings.Issuer,
                       ValidAudience = JwtAuthSettings.Audience,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtAuthSettings.Key))
                   };
               });
            return services;
        }
    }
}
