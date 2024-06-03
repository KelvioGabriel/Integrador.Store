using Integrador.Core.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Integrador.Web.Models
{
    public class OrderTransactionViewModel
    {
        #region Pedido

        public decimal TotalValue { get; set; }
        public decimal Discount { get; set; }
        public string VoucherCode { get; set; }
        public bool VoucherUsed { get; set; }

        //TODO: Alterar aqui
        public List<OrderItemViewModel> Itens { get; set; } = new List<OrderItemViewModel>();

        #endregion

        #region Endereco

        public AddressViewModel Address { get; set; }

        #endregion

        #region Cartão

        [Required(ErrorMessage = "Informe o número do cartão")]
        [DisplayName("Número do Cartão")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Informe o nome do portador do cartão")]
        [DisplayName("Nome do Portador")]
        public string CardName { get; set; }

        [RegularExpression(@"(0[1-9]|1[0-2])\/[0-9]{2}",
            ErrorMessage = "O vencimento deve estar no padrão MM/AA")]
        [CardExpiration(ErrorMessage = "Cartão Expirado")]
        [Required(ErrorMessage = "Informe o vencimento")]
        [DisplayName("Data de Vencimento MM/AA")]
        public string ExpirationCard { get; set; }

        [Required(ErrorMessage = "Informe o código de segurança")]
        [DisplayName("Código de Segurança")]
        public string CvvCard { get; set; }

        #endregion
    }
}
