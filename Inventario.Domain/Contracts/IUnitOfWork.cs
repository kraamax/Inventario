using System;
using System.Collections.Generic;
using System.Text;

namespace Inventario.Domain.Contracts
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
