﻿using Integrador.Domain.Entities;
using Integrador.Domain.Interfaces;
using Integrador.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.Infra.Data.Repositories
{
    public class VoucherRepository : Repository<Voucher>, IVoucherRepository 
    {
        public VoucherRepository(EShopDbContext context) : base(context)
        { }

        public Voucher Add(Voucher entity)
        {
            DbSet.Add(entity);
            return entity;
        }

        public Voucher GetById(Guid id)
        {
            var context = DbSet.AsQueryable();
            var voucher = context.FirstOrDefault(c => c.Id == id);
            return voucher;
        }

        public void Remove(Guid id)
        {
            var obj = GetById(id);
            if (obj != null)
            {
                DbSet.Remove(obj);
            }
        }

        public void Remove(Expression<Func<Voucher, bool>> predicate)
        {
            var context = DbSet.AsQueryable();
            var entities = context.Where(predicate);
            DbSet.RemoveRange(entities);
        }

        public IEnumerable<Voucher> Search(Expression<Func<Voucher, bool>> predicate)
        {
            var context = DbSet.AsQueryable();
            return context.Where(predicate).ToList();
        }

        public IEnumerable<Voucher> Search(Expression<Func<Voucher, bool>> predicate,
            int pageNumber,
            int pageSize)
        {
            var context = DbSet.AsQueryable();
            var result = context.Where(predicate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
            return result;
        }

        public Voucher Update(Voucher entity)
        {
            DbSet.Update(entity);
            return entity;
        }
    }
}
