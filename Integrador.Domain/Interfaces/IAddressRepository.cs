using Integrador.Domain.Entities;
using System.Linq.Expressions;

namespace Integrador.Domain.Interfaces
{
    public interface IAddressRepository
    {
        Address GetById(Guid id);
        IEnumerable<Address> Search(Expression<Func<Address, bool>> predicate);
        IEnumerable<Address> Search(Expression<Func<Address, bool>> predicate,
            int pageNumber,
            int pageSize);
        Address Add(Address entity);
        Address Update(Address entity);
        void Remove(Guid id);
        void Remove(Expression<Func<Address, bool>> predicate);
    }
}
