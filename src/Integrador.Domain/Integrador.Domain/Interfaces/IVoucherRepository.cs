using Integrador.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.Domain.Interfaces
{
    public interface IVoucherRepository
    {
        Voucher GetById(Guid id);
        IEnumerable<Voucher> Search(Expression<Func<Voucher, bool>> predicate);
        IEnumerable<Voucher> Search(Expression<Func<Voucher, bool>> predicate,
            int pageNumber,
            int pageSize);
        Voucher Add(Voucher entity);
        Voucher Update(Voucher entity);
        void Remove(Guid id);
        void Remove(Expression<Func<Voucher, bool>> predicate);
    }
}
