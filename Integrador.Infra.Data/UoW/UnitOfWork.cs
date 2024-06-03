using Integrador.Domain.Shared.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context.EShopDbContext _context;

        public UnitOfWork(Context.EShopDbContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
