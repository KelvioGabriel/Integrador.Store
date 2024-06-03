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
    public class VoucherAppService : BaseService, IVoucherAppService
    {
        protected readonly IVoucherRepository _repository;
        protected readonly IMapper _mapper;

        public VoucherAppService(IVoucherRepository repository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IMediator bus) : base(unitOfWork, bus)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public VoucherViewModel Add(VoucherViewModel viewModel)
        {
            Voucher domain = _mapper.Map<Voucher>(viewModel);
            domain = _repository.Add(domain);
            Commit();

            VoucherViewModel viewModelReturn = _mapper.Map<VoucherViewModel>(domain);
            return viewModelReturn;
        }

        public VoucherViewModel GetById(Guid id)
        {
            Voucher Voucher = _repository.GetById(id);
            VoucherViewModel VoucherViewModel = _mapper.Map<VoucherViewModel>(Voucher);
            return VoucherViewModel;
        }

        public void Remove(Guid id)
        {
            _repository.Remove(id);
            Commit();
        }

        public void Remove(Expression<Func<Voucher, bool>> expression)
        {
            _repository.Remove(expression);
            Commit();
        }

        public IEnumerable<VoucherViewModel> Search(Expression<Func<Voucher, bool>> expression)
        {
            var vouchers = _repository.Search(expression);
            var vouchersViewModel = _mapper.Map<IEnumerable<VoucherViewModel>>(vouchers);
            return vouchersViewModel;
        }

        public IEnumerable<VoucherViewModel> Search(Expression<Func<Voucher, bool>> expression,
            int pageNumber,
            int pageSize)
        {
            var vouchers = _repository.Search(expression, pageNumber, pageSize);
            var vouchersViewModel = _mapper.Map<IEnumerable<VoucherViewModel>>(vouchers);
            return vouchersViewModel;
        }


        public VoucherViewModel Update(VoucherViewModel viewModel)
        {
            var voucher = _mapper.Map<Voucher>(viewModel);
            voucher = _repository.Update(voucher);
            Commit();

            var voucherViewModel = _mapper.Map<VoucherViewModel>(voucher);
            return voucherViewModel;
        }
    }
}
