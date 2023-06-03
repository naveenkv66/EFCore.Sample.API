using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace OneSanofi.API.Infrastructure
{
    public class JWTBearerOptionSetup : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly JWTOptions jwtOptions;

        public JWTBearerOptionSetup(IOptions<JWTOptions> options)
        {
            this.jwtOptions = options.Value;
        }
  

        public void Configure(string? name, JwtBearerOptions options)
        {
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = this.jwtOptions.Issuer,
                ValidAudience = this.jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.jwtOptions.Secretkey))
            };
        }

        public void Configure(JwtBearerOptions options)
        {
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = this.jwtOptions.Issuer,
                ValidAudience = this.jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.jwtOptions.Secretkey))
            };
        }
    }
}
