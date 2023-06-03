using EFCore.Sample.API.DataModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OneSanofi.API.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OneSanofi.API.Services
{
    public class JWTTokenService : IJWTTokenService
    {
        private readonly JWTOptions options;

        public JWTTokenService(IOptions<JWTOptions> options)
        {
            this.options = options.Value;
        }

        public string GenerateAccessToken(User user)
        {
            var calims = new Claim[] { 
              new Claim(JwtRegisteredClaimNames.Sub, user.EmployeeId),
              new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };
            var signingKey = new SigningCredentials( new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Secretkey)), SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                options.Issuer,
                options.Audience,
                calims,
                null,
                DateTime.UtcNow.AddHours(1),
                signingKey);
            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenValue;
        }
    }
}
