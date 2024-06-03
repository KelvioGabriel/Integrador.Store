using Integrador.Domain.Shared.Transaction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.Application.Services
{
    public abstract class BaseService
    {
        protected readonly IUnitOfWork UoW;
        protected readonly IMediator Bus;

        protected BaseService(IUnitOfWork uoW, IMediator bus)
        {
            UoW = uoW;
            Bus = bus;
        }

        protected bool Commit()
        {
            return UoW.Commit();
        }
    }
}
