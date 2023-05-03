using Application.Common.Interfaces.Authentication;
using Application.Specifications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authentication
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly JwtAuthSetting JwtAuthSetting;

        public JwtGenerator(IOptions<JwtAuthSetting> JwtAuthSetting)
        {
            this.JwtAuthSetting = JwtAuthSetting.Value;
        }   

        public string GenerateJwt(int UserId, string Name, string Email)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Name, Name),
                new Claim(JwtRegisteredClaimNames.Email, Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, UserId.ToString()),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtAuthSetting.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(JwtAuthSetting.Issuer, JwtAuthSetting.Audience,
              claims, expires: DateTime.Now.AddDays(JwtAuthSetting.ExpiryTime),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
