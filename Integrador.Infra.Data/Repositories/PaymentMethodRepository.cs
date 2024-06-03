using Integrador.Domain.Entities;
using Integrador.Domain.Interfaces;
using Integrador.Infra.Data.Context;
using System.Linq.Expressions;

namespace Integrador.Infra.Data.Repositories
{
    public class PaymentMethodRepository : Repository<PaymentMethod>, IPaymentMethodRepository
    {
        public PaymentMethodRepository(EShopDbContext context) : base(context)
        { }

        public PaymentMethod Add(PaymentMethod entity)
        {
            DbSet.Add(entity);
            return entity;
        }

        public PaymentMethod GetById(Guid id)
        {
            var context = DbSet.AsQueryable();
            var paymentMethod = context.FirstOrDefault(c => c.Id == id);
            return paymentMethod;
        }

        public void Remove(Guid id)
        {
            var obj = GetById(id);
            if (obj != null)
            {
                DbSet.Remove(obj);
            }
        }

        public void Remove(Expression<Func<PaymentMethod, bool>> predicate)
        {
            var context = DbSet.AsQueryable();
            var entities = context.Where(predicate);
            DbSet.RemoveRange(entities);
        }

        public IEnumerable<PaymentMethod> Search(Expression<Func<PaymentMethod, bool>> predicate)
        {
            var context = DbSet.AsQueryable();
            return context.Where(predicate).ToList();
        }

        public IEnumerable<PaymentMethod> Search(Expression<Func<PaymentMethod, bool>> predicate,
            int pageNumber,
            int pageSize)
        {
            var context = DbSet.AsQueryable();
            var result = context.Where(predicate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
            return result;
        }

        public PaymentMethod Update(PaymentMethod entity)
        {
            DbSet.Update(entity);
            return entity;
        }
    }
}
