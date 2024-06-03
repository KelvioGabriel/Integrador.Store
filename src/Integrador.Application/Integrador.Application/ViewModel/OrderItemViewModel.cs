using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.Application.ViewModel
{
    public class OrderItemViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo OrderId é obrigatório")]
        public Guid OrderId { get; set; }
        [Required(ErrorMessage = "O campo Product é obrigatório")]
        public Guid ProductId { get; set; }
        [Required(ErrorMessage = "O campo Nome do produto é obrigatório")]
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        [Required(ErrorMessage = "O campo Quantidade é obrigatório")]
        public int Amount { get; set; }
        public string ProductImage { get; set; }
        public OrderViewModel Order { get; set; }
        public ProductViewModel Product { get; set; }
    }
}
