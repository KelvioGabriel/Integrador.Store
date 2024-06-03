using Integrador.Application.ViewModel;
using Integrador.Domain.Entities;
using System.Linq.Expressions;

namespace Integrador.Application.Interfaces
{
    public interface IClientAppService
    {
        ClientViewModel GetById(Guid id);
        IEnumerable<ClientViewModel> Search(Expression<Func<Client, bool>> expression);
        IEnumerable<ClientViewModel> Search(Expression<Func<Client, bool>> expression,
            int pageNumber,
            int pageSize);
        ClientViewModel Add(ClientViewModel viewModel);
        ClientViewModel Update(ClientViewModel viewModel);
        void Remove(Guid id);
        void Remove(Expression<Func<Client, bool>> expression);

        /// <summary>
        /// Recebe um Address e um client, cria o Address no banco e salva essa informação em client
        /// </summary>
        /// <param name="clientId">Client Id</param>
        /// <param name="addressViewModel">Objeto Address ViewModel</param>
        /// <returns></returns>
        ClientViewModel SetAddAddressClient(Guid clientId, AddressViewModel addressViewModel);
    }
}
