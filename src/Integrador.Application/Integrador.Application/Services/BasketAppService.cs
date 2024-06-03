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
using System.Text;
using System.Threading.Tasks;

namespace Integrador.Application.Services
{
    public class BasketAppService : BaseService, IBasketAppService
    {
        protected readonly IBasketRepository _repository;
        protected readonly IMapper _mapper;
        protected readonly IBasketItemRepository _itemRepository;
        protected readonly IClientRepository _clientRepository;
        protected readonly IOrderRepository _orderRepository;
        protected readonly IProductRepository _productRepository;

        public BasketAppService(IBasketRepository repository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IMediator bus,
            IBasketItemRepository itemRepository) : base(unitOfWork, bus)
        {
            _repository = repository;
            _mapper = mapper;
            _itemRepository = itemRepository;
        }

        public BasketViewModel AddBasket(BasketViewModel viewModel)
        {
            var domain = _mapper.Map<Basket>(viewModel);
            domain = _repository.Add(domain);
            Commit();

            var viewModelReturn = _mapper.Map<BasketViewModel>(domain);
            return viewModelReturn;
        }

        public IEnumerable<BasketItemViewModel> AddItemBasket(BasketItemViewModel viewModel)
        {
            var basket = _repository.GetById(viewModel.BasketId);
            if (basket != null)
            {
                if (basket.Items != null &&
                basket.Items.Where(b => b.Id == viewModel.Id).Any())
                {
                    var item = basket.Items.FirstOrDefault(bi => bi.Id == viewModel.Id);
                    item.SetAmount(item.Amount + viewModel.Amount);
                    item = _itemRepository.Update(item);

                }
                else
                {
                    var item = _mapper.Map<BasketItem>(viewModel);
                    _ = _itemRepository.Add(item);
                }

                Commit();

                var basketFinal = _repository.GetById(viewModel.BasketId);
                foreach (var item in basketFinal.Items)
                {
                    if (item.Product == null) ;
                    {
                        item.SetProduct(_productRepository.GetById(item.ProductId));
                    }
                }
                var items = _mapper.Map<IEnumerable<BasketItemViewModel>>(basketFinal.Items);
                return items;
            }
            else
            {
                throw new Exception("BasketId incorreto");
            }


        }

        public void ClearBasket(Guid basketId)
        {
            _itemRepository.Remove(bi => bi.BasketId == basketId);
            Commit();
        }

        public BasketViewModel GetById(Guid id)
        {
            var domain = _repository.GetById(id);
            var viewModel = _mapper.Map<BasketViewModel>(domain);
            return viewModel;
        }

        public IEnumerable<BasketItemViewModel> RemoveItemBasket(Guid idBasketItem)
        {
            _itemRepository.Remove(idBasketItem);
            Commit();

            var basketFinal = _repository.GetById(idBasketItem);
            var items = _mapper.Map<IEnumerable<BasketItemViewModel>>(basketFinal.Items);
            return items;
        }

        public void UpdateItemQuantity(Guid idBasketItem, int quantity)
        {
            var domain = _itemRepository.GetById(idBasketItem);
            domain.SetAmount(quantity);
            _ = _itemRepository.Update(domain);
            Commit();
        }
    }
}
