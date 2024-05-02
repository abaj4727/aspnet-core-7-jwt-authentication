using JWTImplimentation.Models;

namespace JWTImplimentation.Interfaces
{
    public interface IAuthService
    {
        User AddUser(User user);
        string Login(LoginRequest request);
    }
}
