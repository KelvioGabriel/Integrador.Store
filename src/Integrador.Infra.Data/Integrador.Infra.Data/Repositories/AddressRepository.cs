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
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(EShopDbContext context)
           : base(context) { }

        public Address Add(Address entity)
        {
            entity.Id = entity.Id;

            DbSet.Add(entity);
            return entity;
        }

        public Address GetById(Guid id)
        {
            var context = DbSet.AsQueryable();
            var address = context.FirstOrDefault(a => a.Id == id);
            return address;
        }

        public void Remove(Guid id)
        {
            var obj = GetById(id);
            if (obj != null)
            {
                DbSet.Remove(obj);
            }
        }

        public void Remove(Expression<Func<Address, bool>> predicate)
        {
            var context = DbSet.AsQueryable();
            var entities = context.Where(predicate);
            DbSet.RemoveRange(entities);
        }

        public IEnumerable<Address> Search(Expression<Func<Address, bool>> predicate)
        {
            var context = DbSet.AsQueryable();
            return context.Where(predicate).ToList();
        }

        public IEnumerable<Address> Search(Expression<Func<Address, bool>> predicate,
            int pageNumber, int pageSize)
        {
            var context = DbSet.AsQueryable();
            var result = context.Where(predicate).Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
            return result;
        }

        public Address Update(Address entity)
        {
            DbSet.Update(entity);
            return entity;
        }
    }
}
