using Integrador.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.Application.ViewModel
{
    public class ProductViewModel
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "O campo Ativo é obrigatório.")]
        public bool Active { get; set; }
        [Required(ErrorMessage = "O campo Preço é obrigatório.")]
        public decimal Price { get; set; }
        public string Image { get; set; }
        [Required(ErrorMessage = "O campo Quantidade em estoque é obrigatório.")]
        public int StockQuantity { get; set; }
        [Required(ErrorMessage = "O campo Tipo é obrigatório.")]
        public ProductType Tipo {  get; set; }
    }
}
