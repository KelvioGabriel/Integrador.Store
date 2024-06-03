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
    public interface IPaymentMethodAppService
    {
        PaymentMethodViewModel GetById(Guid id);
        IEnumerable<PaymentMethodViewModel> Search(Expression<Func<PaymentMethod, bool>> expression);
        IEnumerable<PaymentMethodViewModel> Search(Expression<Func<PaymentMethod, bool>> expression,
            int pageNumber,
            int pageSize);
        PaymentMethodViewModel Add(PaymentMethodViewModel viewModel);
        PaymentMethodViewModel Update(PaymentMethodViewModel viewModel);
        void Remove(Guid id);
        void Remove(Expression<Func<PaymentMethod, bool>> expression);
    }
}
