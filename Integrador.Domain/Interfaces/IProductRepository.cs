using Integrador.Domain.Entities;
using System.Linq.Expressions;

namespace Integrador.Domain.Interfaces
{
    public interface IProductRepository
    {
        Product GetById(Guid id);
        IEnumerable<Product> Search(Expression<Func<Product, bool>> predicate);
        IEnumerable<Product> Search(Expression<Func<Product, bool>> predicate,
            int pageNumber,
            int pageSize);
        Product Add(Product entity);
        Product Update(Product entity);
        void Remove(Guid id);
        void Remove(Expression<Func<Product, bool>> predicate);
    }
}
