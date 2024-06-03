using Integrador.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.Domain.Interfaces
{
    public interface IBasketRepository
    {
        Basket GetById(Guid id);
        IEnumerable<Basket> Search(Expression<Func<Basket, bool>> predicate);
        IEnumerable<Basket> Search(Expression<Func<Basket, bool>> predicate,
            int pageNumber,
            int pageSize);
        Basket Add(Basket entity);
        Basket Update(Basket entity);
        void Remove(Guid id);
        void Remove(Expression<Func<Basket, bool>> predicate);
    }
}
