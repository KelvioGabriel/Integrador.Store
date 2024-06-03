using Integrador.Application.DTOs.Request;
using Integrador.Application.DTOs.Response;

namespace Integrador.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<UserRegisteredResponse> RegisterUser(UserRegisteredRequest request);
        Task<UserLoginResponse> Login(UserLoginRequest request);
    }
}
