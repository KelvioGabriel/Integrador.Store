using Integrador.Domain.Entities;
using Integrador.Domain.Interfaces;
using Integrador.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.Infra.Data.Repositories
{
    public class BasketItemRepository : Repository<BasketItem>, IBasketItemRepository
    {
        public BasketItemRepository(EShopDbContext context) : base(context)
        { }

        public BasketItem Add(BasketItem entity)
        {
            DbSet.Add(entity);
            return entity;
        }

        public BasketItem GetById(Guid id)
        {
            var context = DbSet.AsQueryable();
            var basketItem = context.FirstOrDefault(c => c.Id == id);
            return basketItem;
        }

        public void Remove(Guid id)
        {
            var obj = GetById(id);
            if (obj != null)
            {
                DbSet.Remove(obj);
            }
        }

        public void Remove(Expression<Func<BasketItem, bool>> predicate)
        {
            var context = DbSet.AsQueryable();
            var entities = context.Where(predicate);
            DbSet.RemoveRange(entities);
        }

        public IEnumerable<BasketItem> Search(Expression<Func<BasketItem, bool>> predicate)
        {
            var context = DbSet.AsQueryable();
            return context.Where(predicate).ToList();
        }

        public IEnumerable<BasketItem> Search(Expression<Func<BasketItem, bool>> predicate,
            int pageNumber,
            int pageSize)
        {
            var context = DbSet.AsQueryable();
            var result = context.Where(predicate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
            return result;
        }

        public BasketItem Update(BasketItem entity)
        {
            DbSet.Update(entity);
            return entity;
        }
    }
}
