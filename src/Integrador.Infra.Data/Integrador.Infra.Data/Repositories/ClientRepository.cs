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
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(EShopDbContext context) : base(context)
        { }

        public Client Add(Client entity)
        {
            entity.SetAddress(null);

            DbSet.Add(entity);
            return entity;
        }

        public Client GetById(Guid id)
        {
            var context = DbSet.AsQueryable();
            var client = context.FirstOrDefault(c => c.Id == id);
            return client;
        }

        public void Remove(Guid id)
        {
            var obj = GetById(id);
            if (obj != null)
            {
                DbSet.Remove(obj);
            }
        }

        public void Remove(Expression<Func<Client, bool>> predicate)
        {
            var context = DbSet.AsQueryable();
            var entities = context.Where(predicate);
            DbSet.RemoveRange(entities);
        }

        public IEnumerable<Client> Search(Expression<Func<Client, bool>> predicate)
        {
            var context = DbSet.AsQueryable();
            return context.Where(predicate).ToList();
        }

        public IEnumerable<Client> Search(Expression<Func<Client, bool>> predicate,
            int pageNumber,
            int pageSize)
        {
            var context = DbSet.AsQueryable();
            var result = context.Where(predicate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
            return result;
        }

        public Client Update(Client entity)
        {
            DbSet.Update(entity);
            return entity;
        }
    }
}
