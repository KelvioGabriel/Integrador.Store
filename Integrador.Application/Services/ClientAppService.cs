using AutoMapper;
using Integrador.Application.Interfaces;
using Integrador.Application.ViewModel;
using Integrador.Domain.Entities;
using Integrador.Domain.Interfaces;
using Integrador.Domain.Shared.Transaction;
using MediatR;
using System.Linq.Expressions;

namespace Integrador.Application.Services
{
    public class ClientAppService : BaseService, IClientAppService
    {
        protected readonly IClientRepository _repository;
        protected readonly IAddressAppService _addressAppService;
        protected readonly IMapper _mapper;

        public ClientAppService(IClientRepository repository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IMediator bus,
            IAddressAppService addressAppService) : base(unitOfWork, bus)
        {
            _repository = repository;
            _mapper = mapper;
            _addressAppService = addressAppService;
        }

        public ClientViewModel Add(ClientViewModel viewModel)
        {
            viewModel.AddressClient = _addressAppService.GetById(viewModel.AddressId);

            Client domain = _mapper.Map<Client>(viewModel);
            domain = _repository.Add(domain);
            Commit();

            ClientViewModel viewModelReturn = _mapper.Map<ClientViewModel>(domain);
            return viewModelReturn;
        }

        public ClientViewModel GetById(Guid id)
        {
            Client client = _repository.GetById(id);
            ClientViewModel clientViewModel = _mapper.Map<ClientViewModel>(client);
            return clientViewModel;
        }

        public void Remove(Guid id)
        {
            _repository.Remove(id);
            Commit();
        }

        public void Remove(Expression<Func<Client, bool>> expression)
        {
            _repository.Remove(expression);
            Commit();
        }

        public IEnumerable<ClientViewModel> Search(Expression<Func<Client, bool>> expression)
        {
            var clients = _repository.Search(expression);
            var clientsViewModel = _mapper.Map<IEnumerable<ClientViewModel>>(clients);
            return clientsViewModel;
        }

        public IEnumerable<ClientViewModel> Search(Expression<Func<Client, bool>> expression,
            int pageNumber,
            int pageSize)
        {
            var clients = _repository.Search(expression, pageNumber, pageSize);
            var clientsViewModel = _mapper.Map<IEnumerable<ClientViewModel>>(clients);
            return clientsViewModel;
        }


        public ClientViewModel Update(ClientViewModel viewModel)
        {
            var client = _mapper.Map<Client>(viewModel);
            client = _repository.Update(client);
            Commit();

            var clientViewModel = _mapper.Map<ClientViewModel>(client);
            return clientViewModel;
        }

        /// <summary>
        /// Recebe um Address e um client, cria o Address no banco e salva essa informação em client
        /// </summary>
        /// <param name="clientId">Client Id</param>
        /// <param name="addressViewModel">Objeto Address ViewModel</param>
        /// <returns></returns>
        public ClientViewModel SetAddAddressClient(Guid clientId, AddressViewModel addressViewModel)
        {
            addressViewModel = _addressAppService.Add(addressViewModel);
            Commit();

            var address = _mapper.Map<Address>(addressViewModel);

            var client = _repository.GetById(clientId);
            client.SetAddress(address);

            client = _repository.Update(client);
            Commit();

            return _mapper.Map<ClientViewModel>(client);
        }
    }
}
