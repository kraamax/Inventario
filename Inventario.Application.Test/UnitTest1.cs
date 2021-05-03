using Inventario.Domain;
using Inventario.Domain.Contracts;
using Inventario.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace Inventario.Application.Test
{
    public class Tests
    {
        private InventarioContext _dbContext;
        private RegistrarEntradaProductoService _registrarEntadaProductoService;

        [SetUp]
        public void Setup()
        {
            var optionsSqlite = new DbContextOptionsBuilder<InventarioContext>()
                .UseSqlite(@"Data Source=C:\\sqlite\\InventarioDBTest.db")
                .Options;

            _dbContext = new InventarioContext(optionsSqlite);
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
            var mockEmailServer = new Mock<IMailServer>();
            _registrarEntadaProductoService = new RegistrarEntradaProductoService(
                new UnitOfWork(_dbContext),
                new ProductoRepository(_dbContext),
                mockEmailServer.Object
            );
        }

        [Test]
        public void PuedoRegistrarEntradaDeUnProductonNuevo()
        {
            var producto = new ProductoRequest(null,"123a", "Lechuga", 1, 2,1 );
            var resultado = _registrarEntadaProductoService.Handle(producto);
            Assert.AreEqual("Se registro la entrada del producto Lechuga con una cantidad de 1", resultado);
        }
        [Test]
        public void PuedoRegistrarEntradaDeUnProductoExistente()
        {
            var producto = new ProductoRequest(null,"123a", "Lechuga", 1, 2,1 );
            _registrarEntadaProductoService.Handle(producto);
            var resultado = _registrarEntadaProductoService.Handle(producto);
            Assert.AreEqual("La nueva cantidad del producto Lechuga es 2", resultado);
        }
    }
}