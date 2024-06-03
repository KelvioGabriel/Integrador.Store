using Integrador.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.Domain.Interfaces
{
    public interface IPaymentMethodRepository
    {
        PaymentMethod GetById(Guid id);
        IEnumerable<PaymentMethod> Search(Expression<Func<PaymentMethod, bool>> predicate);
        IEnumerable<PaymentMethod> Search(Expression<Func<PaymentMethod, bool>> predicate,
            int pageNumber,
            int pageSize);
        PaymentMethod Add(PaymentMethod entity);
        PaymentMethod Update(PaymentMethod entity);
        void Remove(Guid id);
        void Remove(Expression<Func<PaymentMethod, bool>> predicate);
    }
}
