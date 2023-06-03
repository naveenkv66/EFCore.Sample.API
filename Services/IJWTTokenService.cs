using EFCore.Sample.API.DataModels;

namespace OneSanofi.API.Services
{
    public interface IJWTTokenService
    {
        string GenerateAccessToken(User user);
    }
}
