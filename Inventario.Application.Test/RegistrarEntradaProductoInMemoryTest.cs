using System.Collections.Generic;
using Inventario.Application.Test.Dobles;
using Inventario.Domain;
using Inventario.Domain.Contracts;
using Inventario.Infrastructure.Data;
using Inventario.Infrastructure.Data.ObjectMother;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace Inventario.Application.Test
{
    public class RegistrarEntradaProductoInMemoryTest
    {
        private InventarioContext _dbContext;
        private RegistrarEntradaProductoService _registrarEntadaProductoService;

        private IProductoRepository _productoRepository;

        [SetUp]
        public void Setup()
        {
            var optionsSqlite = new DbContextOptionsBuilder<InventarioContext>()
                .UseSqlite(SqlLiteDatabaseInMemory.CreateConnection())
                .Options;

            _dbContext = new InventarioContext(optionsSqlite);
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
            _productoRepository = new ProductoRepository(_dbContext);

            var mockEmailServer = new Mock<IMailServer>();
            _registrarEntadaProductoService = new RegistrarEntradaProductoService(
                new UnitOfWork(_dbContext),
                _productoRepository
            );
         
        }

        [Test]
        public void PuedoRegistrarEntradaDeUnProductonNuevo()
        {
            var producto = new EntradaProductoRequest("Simple","123a", "Lechuga", 1, 2,1,null );
            var resultado = _registrarEntadaProductoService.Handle(producto);
            Assert.AreEqual("Se registro la entrada del producto Lechuga con una cantidad de 1", resultado);
        }
        [Test]
        public void NoPuedoRegistrarEntradaDeUnProductoNuevoConCantidad0()
        {
            var producto = new EntradaProductoRequest("Simple","123a", "Lechuga", 0, 2,1,null );
            var resultado = _registrarEntadaProductoService.Handle(producto);
            Assert.AreEqual("Error: La cantidad de la entrada de debe ser mayor a 0", resultado);
        }

        [Test]
        public void PuedoRegistrarEntradaDeUnProductoExistente()
        {
            var producto = new EntradaProductoRequest("Simple","123a", "Lechuga", 1, 2,1,null);
            _registrarEntadaProductoService.Handle(producto);
            var resultado = _registrarEntadaProductoService.Handle(producto);
            Assert.AreEqual("La nueva cantidad del producto Lechuga es 2", resultado);
        }
       
       
    }
}