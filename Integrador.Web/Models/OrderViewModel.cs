using Integrador.Core.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Integrador.Web.Models
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Desconto")]
        public decimal Discount { get; set; }
        public OrderStatus Status { get; set; }

        public ClientViewModel Client { get; set; }
        public VoucherViewModel? Voucher { get; set; }
        public AddressViewModel Address { get; set; }
        public PaymentMethodViewModel PaymentMethod { get; set; }

        [DisplayName("Valor total")]
        public decimal TotalValue { get; set; }

        [DisplayName("Valor do desconto")]
        public decimal DiscountValue { get; set; }
        [DisplayName("Valor Final")]
        public decimal FinalValue { get; set; }
        [MaxLength(5)]
        [DisplayName("Código da venda")]
        public string Code { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }

        [DisplayName("Data da venda")]
        public DateTime Date { get; set; } = DateTime.Now; //Aqui
    }

    public class OrderItemViewModel
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        [Required(ErrorMessage = "O campo Quantidade é obrigatório")]
        [DisplayName("Quantidade")]
        public int Amount { get; set; }
        public string ProductImage { get; set; }
        public ProductViewModel Product { get; set; }
    }
}
