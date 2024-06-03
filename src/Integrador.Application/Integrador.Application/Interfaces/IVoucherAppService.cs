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
    public interface IVoucherAppService
    {
        VoucherViewModel GetById(Guid id);
        IEnumerable<VoucherViewModel> Search(Expression<Func<Voucher, bool>> expression);
        IEnumerable<VoucherViewModel> Search(Expression<Func<Voucher, bool>> expression,
            int pageNumber,
            int pageSize);
        VoucherViewModel Add(VoucherViewModel viewModel);
        VoucherViewModel Update(VoucherViewModel viewModel);
        void Remove(Guid id);
        void Remove(Expression<Func<Voucher, bool>> expression);
    }
}
