using System.ComponentModel.DataAnnotations;

namespace Integrador.Application.DTOs.Request
{
    public class UserLoginRequest
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo {0} é inválido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Password { get; set; }
    }
}
