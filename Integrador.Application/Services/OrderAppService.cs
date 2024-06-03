using AutoMapper;
using Integrador.Application.Interfaces;
using Integrador.Application.ViewModel;
using Integrador.Core.Enums;
using Integrador.Domain.Entities;
using Integrador.Domain.Interfaces;
using Integrador.Domain.Shared.Transaction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.Application.Services
{
    public class OrderAppService : BaseService, IOrderAppService
    {
        protected readonly IOrderRepository _repository;
        protected readonly IOrderItemRepository _itemRepository;
        protected readonly IMapper _mapper;
        protected readonly IAddressRepository _addressRepository;
        protected readonly IVoucherRepository _voucherRepository;

        public OrderAppService(IOrderRepository repository,
            IOrderItemRepository itemRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IMediator bus,
            IAddressRepository addressRepository,
            IVoucherRepository voucherRepository) : base(unitOfWork, bus)
        {
            _repository = repository;
            _itemRepository = itemRepository;
            _mapper = mapper;
            _addressRepository = addressRepository;
            _voucherRepository = voucherRepository;
        }



        public IEnumerable<OrderItemViewModel> DeleteItemInOrder(Guid orderItemId, Guid orderId)
        {
            _itemRepository.Remove(orderItemId);
            Commit();

            var order = _repository.GetById(orderId);
            var orderViewModel = _mapper.Map<IEnumerable<OrderItemViewModel>>(order.OrderItems);
            return orderViewModel;
        }

        public OrderViewModel SetAddressDelivery(Guid orderId, AddressViewModel addresViewModel)
        {
            var address = _mapper.Map<Address>(addresViewModel);
            address = _addressRepository.Add(address);
            Commit();

            var order = _repository.GetById(orderId);
            order.SetAddress(address);
            order = _repository.Update(order);
            Commit();

            var orderViewModel = _mapper.Map<OrderViewModel>(order);
            return orderViewModel;
        }

        public OrderViewModel SetApplyVoucher(Guid orderId, string code)
        {
            var vouchers = _voucherRepository.Search(v => v.Code == code &&
            v.Active);
            if (!vouchers.Any())
            {
                throw new Exception("Voucher enviado é inválido.");
            }

            var voucher = vouchers.FirstOrDefault();

            if (voucher.ExpirationDate < DateTime.Now)
            {
                throw new Exception("Voucher vencido.");
            }

            var order = _repository.GetById(orderId);
            decimal discountValue = 0;
            if (voucher.DiscountType == DiscountTypeVoucher.Percentual)
            {
                discountValue = order.TotalValue * (voucher.Value / 100);
            }
            else
            {
                discountValue = voucher.Value;
            }

            if ((order.TotalValue - discountValue) < 0)
            {
                order.SetFinalValue(0);
                order.SetDiscountValue(order.TotalValue);
            }
            else
            {
                order.SetFinalValue(order.TotalValue - discountValue);
                order.SetDiscountValue(discountValue);
            }

            order = _repository.Update(order);
            Commit();

            voucher.DebitAmount();
            _voucherRepository.Update(voucher);
            Commit();

            var orderViewModel = _mapper.Map<OrderViewModel>(order);
            return orderViewModel;
        }

        public OrderViewModel SetCreateNewOrder(OrderViewModel viewModel)
        {
            var order = _mapper.Map<Order>(viewModel);
            order = _repository.Add(order);
            Commit();

            var orderViewModel = _mapper.Map<OrderViewModel>(order);
            return orderViewModel;

        }

        public IEnumerable<OrderItemViewModel> SetInsertNewItem(OrderItemViewModel model, Guid orderId)
        {
            var item = _mapper.Map<OrderItem>(model);
            _ = _itemRepository.Add(item);

            Commit();

            var orderItems = _itemRepository.Search(oi => oi.OrderId == orderId);
            return _mapper.Map<IEnumerable<OrderItemViewModel>>(orderItems);
        }

        public void UpdateQuantityItemInOrder(Guid orderItemId, int newQuantity)
        {
            if (newQuantity == 0)
            {
                _itemRepository.Remove(orderItemId);
            }
            else
            {
                var orderItem = _itemRepository.GetById(orderItemId);
                orderItem.SetAddAmount(newQuantity);
                _ = _itemRepository.Update(orderItem);
                Commit();
            }
        }

        public OrderViewModel UpdateStatusOrder(Guid orderId, OrderStatus newStatus)
        {
            var order = _repository.GetById(orderId);
            switch (newStatus)
            {
                case OrderStatus.Em_aprovação:
                    order.AuthorizedOrder();
                    break;
                case OrderStatus.Cancelado:
                    order.CanceledOrder();
                    break;
                case OrderStatus.Entregue:
                    order.DeliveredOrder();
                    break;
                case OrderStatus.Aprovado:
                    order.ProcessedOrder();
                    break;
            }
            order = _repository.Update(order);
            Commit();
            var orderViewModel = _mapper.Map<OrderViewModel>(order);
            return orderViewModel;
        }

        public OrderViewModel GetById(Guid orderId)
        {
            var order = _repository.GetById(orderId);
            var orderViewModel = _mapper.Map<OrderViewModel>(order.OrderItems);
            return orderViewModel;
        }

        public OrderViewModel GetLastOrderByClient(Guid clientId)
        {
            var orders = _repository.Search(o => o.ClientId == clientId);

            if (orders.Any())
            {
                var order = orders.OrderByDescending(o => o.CreatedAt).FirstOrDefault();
                return _mapper.Map<OrderViewModel>(order);
            }
            else
            {
                throw new Exception("Pedido não encontrado.");
            }
        }

        public IEnumerable<OrderViewModel> GetOrdersByClient(Guid clientId)
        {
            var orders = _repository.Search(o => o.ClientId == clientId);
            return _mapper.Map<IEnumerable<OrderViewModel>>(orders);
        }
    }
}
