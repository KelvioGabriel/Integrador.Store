using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Integrador.Infra.Identity.Configurations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Integrador.Application.Interfaces;
using Integrador.Application.DTOs.Response;
using Integrador.Application.DTOs.Request;

namespace Integrador.Infra.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtOptions _jwtOptions;

        public IdentityService(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IOptions<JwtOptions> jwtOptions)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<UserRegisteredResponse> RegisterUser(UserRegisteredRequest request)
        {
            //Criamos um objeto do tipo IdentityUser (Vem da biblioteca Identity)
            var identityUser = new IdentityUser
            {
                UserName = request.Email,
                Email = request.Email,
                EmailConfirmed = true
            };

            //Fazemos a solicitação de criação de um usuário
            var result = await _userManager.CreateAsync(identityUser, request.Password);

            //Caso tudo esteja correto:
            if(result.Succeeded)
            {
                //Vou mandar desbloquear o Usuário
                await _userManager.SetLockoutEnabledAsync(identityUser, false);
            }

            //Vou criar um objeto de retorno com o Success = true
            var userRegisteredResponse = new UserRegisteredResponse(result.Succeeded);

            //Se ele não tiver tido sucesso ao criar o usuário e tiver um ou mais erros na lista de erros
            if(!result.Succeeded && result.Errors.Count() > 0)
            {
                //Adiciono o(s) erro(s) na lista
                userRegisteredResponse.AddErrors(result.Errors.Select(e => e.Description));
            }

            return userRegisteredResponse;

        }

        public async Task<UserLoginResponse> Login (UserLoginRequest request)
        {
            //Método que tenta fazer login
            var result = await _signInManager.PasswordSignInAsync(userName: request.Email,
                password: request.Password,
                isPersistent: false,
                lockoutOnFailure: true);

            //Verifico se retornou successed, se retornou vamos gerar um Token para enviar.
            if(result.Succeeded )
            {
                return await SetToken(request.Email);
            }

            var userLoginResponse = new UserLoginResponse(result.Succeeded);

            if(!result.Succeeded)
            {
                //Verificar se o usuário está bloqueado:
                if (result.IsLockedOut)
                {
                    userLoginResponse.AddError("Esta conta está bloqueada.");
                }
                else if (result.IsNotAllowed)
                {
                    //Caso a conta não tenha acesso a fazer login
                    userLoginResponse.AddError("Esta conta não tem permissão para fazer login.");
                }
                else if (result.RequiresTwoFactor)
                {
                    //Caso esteja com o duplo fator de autenticação ativado e a conta ainda não
                    //tenha sido ativada no segundo fator de autenticação
                    userLoginResponse.AddError("É necessário confirmar o login no seu segundo fato de " +
                        "autenticação.");
                }
                else
                {
                    userLoginResponse.AddError("Usuário ou senha estão incorretos.");
                }
            }

            return userLoginResponse;

        }

        private async Task<IList<Claim>> GetClaims(IdentityUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            //Notbefore (não antes de: xxxxxx)
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()));
            //Ussued at time (Emitido no momento xxxxxx)
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()));

            foreach(var role in roles)
            {
                claims.Add(new Claim("role", role));
            }

            return claims;
        }

        private async Task<UserLoginResponse> SetToken(string email)
        {
            //Buscando o usuário pelo e-mail
            var user = await _userManager.FindByEmailAsync(email);
            var tokenClaims = await GetClaims(user);

            var expirationDate = DateTime.Now.AddSeconds(_jwtOptions.Expiration);

            //Crio meu objeto de token
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: tokenClaims,
                notBefore: DateTime.Now,
                expires: expirationDate,
                signingCredentials: _jwtOptions.SigningCredentials);

            //Aqui ele efetivamente escreve o Token
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            //Retorno uma resposta de login de usuário
            return new UserLoginResponse(
                success: true,
                token: token,
                expirationDate: expirationDate);

        }
    }

    
}
