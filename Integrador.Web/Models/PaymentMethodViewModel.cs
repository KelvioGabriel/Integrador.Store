using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Integrador.Web.Models
{
    public class PaymentMethodViewModel
    {
        [Required(ErrorMessage = "O campo Alias é obrigatório.")]
        [DisplayName("Apelido")]
        public string Alias { get; private set; }

        public string CardId { get; private set; }

        [Required(ErrorMessage = "O campo Last4 é obrigatório.")]
        [MaxLength(4, ErrorMessage = "O campo Last4 deve conter 4 caracteres")]
        [DisplayName("Últimos 4 dígitos")]
        public string Last4 { get; private set; }

        public ClientViewModel Client { get; private set; }
    }
}
