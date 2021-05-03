using Inventario.Domain;
using Inventario.Domain.Contracts;
using Inventario.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace Inventario.Application.Test
{
    [Ignore("Test no implementados")]
    public class Tests
    {
        private InventarioContext _dbContext;
        private RegistrarEntradaProductoService _registrarEntadaProductoService;

        [SetUp]
        public void Setup()
        {
            var optionsSqlite = new DbContextOptionsBuilder<InventarioContext>()
                .UseSqlite(@"Data Source=InventarioBaseTest.db")
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
        public void PuedoRegistrarEntradaDeUnProducto()
        {
            var producto = new ProductoRequest("123a", "lechuga", 1, 2,1 );
            var resultado = _registrarEntadaProductoService.Handle(producto);
            Assert.AreEqual("La cantidad del producto lechuga es 4", resultado);
        }
    }
}