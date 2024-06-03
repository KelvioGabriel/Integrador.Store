using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Integrador.Web.Models
{
    public class AddressViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo Rua é obrigatório.")]
        [DisplayName("Rua")]
        public string Street { get; set; }

        [DisplayName("Número")]
        public string Number { get; set; }

        [DisplayName("Complemento")]
        public string Complement { get; set; }

        [Required(ErrorMessage = "O campo Bairro é obrigatório.")]
        [DisplayName("Bairro")]
        public string Neighborhood { get; set; }

        [DisplayName("CEP")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "O campo Cidade é obrigatório.")]
        [DisplayName("Cidade")]
        public string City { get; set; }

        [Required(ErrorMessage = "O campo Estado é obrigatório.")]
        [DisplayName("Estado")]
        public string State { get; set; }

        public override string ToString()
        {
            return $"{Street}, {Number}, {Complement} - {Neighborhood} - {City} - {State}";
        }
    }
}
