using Integrador.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Order GetById(Guid id);
        IEnumerable<Order> Search(Expression<Func<Order, bool>> predicate);
        IEnumerable<Order> Search(Expression<Func<Order, bool>> predicate,
            int pageNumber,
            int pageSize);
        Order Add(Order entity);
        Order Update(Order entity);
        void Remove(Guid id);
        void Remove(Expression<Func<Order, bool>> predicate);
    }
}
