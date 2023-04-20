using Application.Common.Interfaces.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infratructure.Authentication
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly IConfiguration configuration;

        public JwtGenerator(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GenerateJwt(int UserId, string Name, string Email)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Name, Name),
                new Claim(JwtRegisteredClaimNames.Email, Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, UserId.ToString()),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"],
              claims, expires: DateTime.Now.AddDays(2),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
