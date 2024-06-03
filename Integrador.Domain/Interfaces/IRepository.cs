using Integrador.Core.DomainObjects;
using System.Linq.Expressions;

namespace Integrador.Domain.Interfaces
{
    public interface IRepository<T> : IDisposable where T : Entity
    {
        long Count();
        long Count(Expression<Func<T, bool>> predicate);
    }
}
