using Inventario.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Application.Test.Dobles
{
    class UnitOfWorkFake : IUnitOfWork
    {
        public void Commit()
        {
            Console.WriteLine("Se confirman cambios en la base de datos");
        }

      
    }
}
