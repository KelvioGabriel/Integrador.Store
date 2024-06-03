using Integrador.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.Application.ViewModel
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo ClientId é obrigatório")]
        public Guid ClientId { get; set; }
        public Guid? VoucherId { get; set; }
        public decimal Discount { get; set; }
        [Required(ErrorMessage = "O campo AddressId é obrigatório")]
        public Guid AddressId { get; set; }
        [Required(ErrorMessage = "O campo Status é obrigatório")]
        public OrderStatus Status { get; set; }

        public ClientViewModel Client { get; set; }
        public VoucherViewModel? Voucher { get; set; }
        public AddressViewModel Address { get; set; }
        [Required(ErrorMessage = "O campo PaymentMethodId é obrigatório")]
        public Guid PaymentMethodId { get; set; }
        public PaymentMethodViewModel PaymentMethod { get; set; }
        public decimal TotalValue { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal FinalValue { get; set; }
        [MaxLength(5)]
        public string Code { get; set; }
        public IEnumerable<OrderItemViewModel> OrderItems { get; set; }
    }
}
