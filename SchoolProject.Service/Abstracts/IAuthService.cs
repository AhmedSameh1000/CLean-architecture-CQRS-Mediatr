using JWTApi.Models;
using TestApiJWT.Models;

namespace JWTApi.Services
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel registerModel);

        Task<AuthModel> GetToken(LogInModel model);
    }
}