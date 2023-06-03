namespace OneSanofi.API.Infrastructure
{
    public class JWTOptions
    {
        public string Issuer { get; init; }
        public string Audience { get; init; }
        public string Secretkey { get; init; }
    }
}
