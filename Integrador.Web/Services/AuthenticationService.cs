using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Integrador.Core.Communication;
using Integrador.Web.Extensions;
using Integrador.Web.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Integrador.Web.Services
{
    public interface IAuthService
    {
        Task<UserLoginResponse> Login(UserLogin userLogin);
        Task<UserLoginResponse> NewUSer(UserRegister userRegister);
        Task AccomplishLogin(UserLoginResponse response);
        Task Logout();
        bool ExpiredToken();
    }

    public class AuthenticationService : Service, IAuthService
    {
        private readonly HttpClient _httpClient;

        private readonly IUserService _user;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationService(HttpClient httpClient,
                                   IOptions<AppSettings> settings,
                                   IUserService user,
                                   IAuthenticationService authenticationService)
        {
            httpClient.BaseAddress = new Uri(settings.Value.Url);

            _httpClient = httpClient;
            _user = user;
            _authenticationService = authenticationService;
        }

        public async Task<UserLoginResponse> Login(UserLogin userLogin)
        {
            var loginContent = GetContent(userLogin);

            var response = await _httpClient.PostAsync("/api/v1/login", loginContent);

            if (!HandleErrosResponse(response))
            {
                return await DeserializeObjectResponse<UserLoginResponse>(response);
            }

            return await DeserializeObjectResponse<UserLoginResponse>(response);
        }

        public async Task<UserLoginResponse> NewUSer(UserRegister userRegister)
        {
            var contentRegister = GetContent(userRegister);

            var response = await _httpClient.PostAsync("/api/v1/register", 
                contentRegister);

            if (!HandleErrosResponse(response))
            {
                return await DeserializeObjectResponse<UserLoginResponse>(response);
            }

            return await DeserializeObjectResponse<UserLoginResponse>(response);
        }


        public async Task AccomplishLogin(UserLoginResponse response)
        {
            var token = GetTokenFormated(response.Token);

            var claims = new List<Claim>();
            claims.Add(new Claim("JWT", response.Token));
            claims.AddRange(token.Claims);

            var claimsIdentity = new ClaimsIdentity(claims, 
                CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8),
                IsPersistent = true
            };

            await _authenticationService.SignInAsync(
                _user.GetHttpContext(),
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        public async Task Logout()
        {
            await _authenticationService.SignOutAsync(
                _user.GetHttpContext(),
                CookieAuthenticationDefaults.AuthenticationScheme,
                null);
        }

        public static JwtSecurityToken GetTokenFormated(string jwtToken)
        {
            return new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken;
        }

        public bool ExpiredToken()
        {
            var jwt = _user.GetUserToken();
            if (jwt is null) return false;

            var token = GetTokenFormated(jwt);
            return token.ValidTo.ToLocalTime() < DateTime.Now;
        }
        
        

    }
}
