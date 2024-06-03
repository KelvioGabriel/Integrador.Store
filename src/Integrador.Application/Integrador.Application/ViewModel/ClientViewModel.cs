using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.Application.ViewModel
{
    public class ClientViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        [MaxLength(14, ErrorMessage = "O campo CPF tem mais caracteres que o permitido.")]
        public string Cpf { get; set; }
        public bool Active { get; set; }
        [Required(ErrorMessage = "O campo Data de Nascimento é obrigatório.")]
        public DateTime Birth { get; set; }
        [Required(ErrorMessage = "O campo AddressId é obrigatório.")]
        public Guid AddressId { get; set; }
        public AddressViewModel? AddressClient { get; set; }
        public IEnumerable<PaymentMethodViewModel>? PaymentMethods { get; set; }
    }
}
