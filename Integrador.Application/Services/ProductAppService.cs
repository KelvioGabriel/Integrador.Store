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
    public class ProductAppService : BaseService, IProductAppService
    {
        protected readonly IProductRepository _repository;
        protected readonly IMapper _mapper;

        public ProductAppService(IProductRepository repository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IMediator bus) : base(unitOfWork, bus)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ProductViewModel Add(ProductViewModel viewModel)
        {
            Product domain = _mapper.Map<Product>(viewModel);
            domain = _repository.Add(domain);
            Commit();

            ProductViewModel viewModelReturn = _mapper.Map<ProductViewModel>(domain);
            return viewModelReturn;
        }

        public ProductViewModel GetById(Guid id)
        {
            Product product = _repository.GetById(id);
            ProductViewModel productViewModel = _mapper.Map<ProductViewModel>(product);
            return productViewModel;
        }

        public void Remove(Guid id)
        {
            _repository.Remove(id);
            Commit();
        }

        public void Remove(Expression<Func<Product, bool>> expression)
        {
            _repository.Remove(expression);
            Commit();
        }

        public IEnumerable<ProductViewModel> Search(Expression<Func<Product, bool>> expression)
        {
            var products = _repository.Search(expression);
            var productsViewModel = _mapper.Map<IEnumerable<ProductViewModel>>(products);
            return productsViewModel;
        }

        public IEnumerable<ProductViewModel> Search(Expression<Func<Product, bool>> expression,
            int pageNumber,
            int pageSize)
        {
            var products = _repository.Search(expression, pageNumber, pageSize);
            var productsViewModel = _mapper.Map<IEnumerable<ProductViewModel>>(products);
            return productsViewModel;
        }


        public ProductViewModel Update(ProductViewModel viewModel)
        {
            var product = _mapper.Map<Product>(viewModel);
            product = _repository.Update(product);
            Commit();

            var productViewModel = _mapper.Map<ProductViewModel>(product);
            return productViewModel;
        }

        public void UpdateStock(Guid productId, int quantity)
        {
            var product = _repository.GetById(productId);

            if (quantity < 0 && (quantity * -1) > product.StockQuantity)
            {
                throw new Exception("Não é possível baixar o estoque para um valor negativo.");
            }

            product.SetAddRemoveStock(quantity);
            _repository.Update(product);
            Commit();
        }

        public int CheckQuantityStock(Guid productId)
        {
            var product = _repository.GetById(productId);
            return product.StockQuantity;
        }
    }
}
