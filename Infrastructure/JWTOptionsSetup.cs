using Microsoft.Extensions.Options;

namespace OneSanofi.API.Infrastructure
{
    public class JWTOptionsSetup : IConfigureOptions<JWTOptions>
    {
        private readonly IConfiguration configuration;

        public JWTOptionsSetup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void Configure(JWTOptions options)
        {
            this.configuration.GetSection("JWT").Bind(options);
        }
    }
}
