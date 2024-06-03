
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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(EShopDbContext context) : base(context)
        { }

        public Product Add(Product entity)
        {
            DbSet.Add(entity);
            return entity;
        }

        public Product GetById(Guid id)
        {
            var context = DbSet.AsQueryable();
            var product = context.FirstOrDefault(c => c.Id == id);
            return product;
        }

        public void Remove(Guid id)
        {
            var obj = GetById(id);
            if (obj != null)
            {
                DbSet.Remove(obj);
            }
        }

        public void Remove(Expression<Func<Product, bool>> predicate)
        {
            var context = DbSet.AsQueryable();
            var entities = context.Where(predicate);
            DbSet.RemoveRange(entities);
        }

        public IEnumerable<Product> Search(Expression<Func<Product, bool>> predicate)
        {
            var context = DbSet.AsQueryable();
            return context.Where(predicate).ToList();
        }

        public IEnumerable<Product> Search(Expression<Func<Product, bool>> predicate,
            int pageNumber,
            int pageSize)
        {
            var context = DbSet.AsQueryable();
            var result = context.Where(predicate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
            return result;
        }

        public Product Update(Product entity)
        {
            DbSet.Update(entity);
            return entity;
        }
    }
}
