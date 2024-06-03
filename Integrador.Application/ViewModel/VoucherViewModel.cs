using Integrador.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.Application.ViewModel
{
    public class VoucherViewModel
    {
        public Guid Id { get; set; }
        [MaxLength(5, ErrorMessage = "O campo Código só pode ter 5 caracteres.")]
        public string Code { get; set; }
        [Required(ErrorMessage = "O campo Tipo do desconto é obrigatório.")]
        public DiscountTypeVoucher DiscountType { get; set; }
        [Required(ErrorMessage = "O campo Quantidade é obrigatório.")]
        public int Amount { get; set; }
        public int AmountUsed { get; set; }
        [Required(ErrorMessage = "O campo Data de expiração é obrigatório.")]
        public DateTime ExpirationDate { get; set; }
        public bool Active { get; set; }
        [Required(ErrorMessage = "O campo Valor é obrigatório.")]
        public decimal Value { get; set; }
    }
}
