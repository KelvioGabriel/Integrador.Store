using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.Application.ViewModel
{
    public class BasketViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo Cliente Id é obrigatório.")]
        public Guid ClientId { get; set; }
        public ClientViewModel? Client { get; set; }
        public List<BasketItemViewModel> Items { get; set; }
    }
}
