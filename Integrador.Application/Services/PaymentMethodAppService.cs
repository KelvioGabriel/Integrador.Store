using AutoMapper;
using Integrador.Application.Interfaces;
using Integrador.Application.ViewModel;
using Integrador.Domain.Entities;
using Integrador.Domain.Interfaces;
using Integrador.Domain.Shared.Transaction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.Application.Services
{
    public class PaymentMethodAppService : BaseService, IPaymentMethodAppService
    {
        protected readonly IPaymentMethodRepository _repository;
        protected readonly IMapper _mapper;

        public PaymentMethodAppService(IPaymentMethodRepository repository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IMediator bus) : base(unitOfWork, bus)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public PaymentMethodViewModel Add(PaymentMethodViewModel viewModel)
        {
            PaymentMethod domain = _mapper.Map<PaymentMethod>(viewModel);
            domain = _repository.Add(domain);
            Commit();

            PaymentMethodViewModel viewModelReturn = _mapper.Map<PaymentMethodViewModel>(domain);
            return viewModelReturn;
        }

        public PaymentMethodViewModel GetById(Guid id)
        {
            PaymentMethod paymentMethod = _repository.GetById(id);
            PaymentMethodViewModel paymentMethodViewModel = _mapper.Map<PaymentMethodViewModel>(paymentMethod);
            return paymentMethodViewModel;
        }

        public void Remove(Guid id)
        {
            _repository.Remove(id);
            Commit();
        }

        public void Remove(Expression<Func<PaymentMethod, bool>> expression)
        {
            _repository.Remove(expression);
            Commit();
        }

        public IEnumerable<PaymentMethodViewModel> Search(Expression<Func<PaymentMethod, bool>> expression)
        {
            var paymentMethods = _repository.Search(expression);
            var paymentMethodsViewModel = _mapper.Map<IEnumerable<PaymentMethodViewModel>>(paymentMethods);
            return paymentMethodsViewModel;
        }

        public IEnumerable<PaymentMethodViewModel> Search(Expression<Func<PaymentMethod, bool>> expression,
            int pageNumber,
            int pageSize)
        {
            var paymentMethods = _repository.Search(expression, pageNumber, pageSize);
            var paymentMethodsViewModel = _mapper.Map<IEnumerable<PaymentMethodViewModel>>(paymentMethods);
            return paymentMethodsViewModel;
        }


        public PaymentMethodViewModel Update(PaymentMethodViewModel viewModel)
        {
            var paymentMethod = _mapper.Map<PaymentMethod>(viewModel);
            paymentMethod = _repository.Update(paymentMethod);
            Commit();

            var paymentMethodViewModel = _mapper.Map<PaymentMethodViewModel>(paymentMethod);
            return paymentMethodViewModel;
        }
    }
}
