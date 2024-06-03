using Integrador.Application.ViewModel;
using Integrador.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.Application.Interfaces
{
    public interface IProductAppService
    {
        ProductViewModel GetById(Guid id);
        IEnumerable<ProductViewModel> Search(Expression<Func<Product, bool>> expression);
        IEnumerable<ProductViewModel> Search(Expression<Func<Product, bool>> expression,
            int pageNumber,
            int pageSize);
        ProductViewModel Add(ProductViewModel viewModel);
        ProductViewModel Update(ProductViewModel viewModel);
        void Remove(Guid id);
        void Remove(Expression<Func<Product, bool>> expression);
        /// <summary>
        /// Dado um valor passado ele aumenta o reduz o estoque
        /// </summary>
        /// <param name="productId">Id do produto</param>
        /// <param name="quantity">Quantidade</param>
        void UpdateStock(Guid productId, int quantity);
        /// <summary>
        /// Checar a quantiade de itens no estoque
        /// </summary>
        /// <param name="productId">Id do produto</param>
        /// <returns>A quantidade de itens de estoque</returns>
        int CheckQuantityStock(Guid productId);
    }
}
