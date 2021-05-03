using Inventario.Domain.Contracts;
using Inventario.Infrastructure.Data.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventario.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContext _context;
        public UnitOfWork(IDbContext context) => _context = context;

        public void Commit()
        {
            _context.SaveChanges();
        }

        
    }
}
