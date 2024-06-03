using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Integrador.Core.Communication;

namespace Integrador.Web.Models
{
    public class ClientViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        [MaxLength(14, ErrorMessage = "O campo CPF tem mais caracteres que o permitido.")]
        [DisplayName("CPF")]
        public string Cpf { get; set; }

        [DisplayName("Ativo")]
        public bool Active { get; set; }

        [Required(ErrorMessage = "O campo Data de Nascimento é obrigatório.")]
        [DisplayName("Data de Nascimento")]
        public DateTime Birth { get; set; }

        public AddressViewModel? AddressClient { get; set; } = new AddressViewModel();

    }

    public class UserLogin
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo {0} é inválido.")]
        [DisplayName("E-mail")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DisplayName("Senha")]
        public string Password { get; set; }
    }

    public class UserLoginResponse
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public List<string> Errors { get; set; }
        public ResponseResult ResponseResult { get; set; }
    }

    public class UserRegister
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo {0} é inválido.")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(50, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres", MinimumLength = 6)]
        [DisplayName("Senha")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "As senhas devem ser iguais.")]
        [DisplayName("Confirme sua Senha")]
        public string PasswordConfirm { get; set; }

        public ClientViewModel Client { get; set; } = new ClientViewModel();
    }

    public class UserRegisteredResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserToken UsuarioToken { get; set; }
        public ResponseResult ResponseResult { get; set; }
    }
    
    public class UserToken
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserClaim> Claims { get; set; }
    }

    public class UserClaim
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }
}
