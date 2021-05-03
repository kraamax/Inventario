using System.Collections.Generic;
using Inventario.Domain;
using Inventario.Domain.Contracts;
using Inventario.Infrastructure.Data;
using Inventario.Infrastructure.Data.ObjectMother;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace Inventario.Application.Test
{
    public class RegistrarSalidaProductoInMemoryTest
    {
        private InventarioContext _dbContext;
        private RegistrarSalidaProductoService _registrarSalidaProductoService;
        private RegistrarSalidaProductoService2 _registrarSalidaProductoService2;

        private IProductoRepository _productoRepository;

        [SetUp]
        public void Setup()
        {
            var optionsSqlite = new DbContextOptionsBuilder<InventarioContext>()
                .UseInMemoryDatabase(@"InventarioInmemory")
                .Options;

            _dbContext = new InventarioContext(optionsSqlite);
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
            _productoRepository = new ProductoRepository(_dbContext);

           
            _registrarSalidaProductoService = new RegistrarSalidaProductoService(
                new UnitOfWork(_dbContext),
                _productoRepository
            );
            _registrarSalidaProductoService2 = new RegistrarSalidaProductoService2(
                new UnitOfWork(_dbContext),
                _productoRepository
            );
        }

        [Test]
        public void PuedoRegistrarLaSalidaDeProducto()
        {
            var ensalada=ProductoCompuestoMother.CrearProducto();
            var gaseosa = new ProductoSimple("sadad", "gaseosa", 1, 100, 200);
            var gaseosaInv=new ProductoSimple("sadad", "gaseosa", 5, 100, 200);
            var ing = new List<Producto>();
            ing.Add(ensalada);
            ing.Add(gaseosa);
            var ensaladaConG = new ProductoCompuesto("dasd", "ensaladaconG", 1, 2, ing, ensalada.ProductosEnInventario);
            _productoRepository.Add(ensaladaConG);
            _productoRepository.AddRange(ensalada.ProductosEnInventario);
            _productoRepository.Add(gaseosaInv);
            _dbContext.SaveChanges();
            var request = new SalidaProductoRequest("Compuesto",ensaladaConG.Ingredientes,ensaladaConG.Codigo,1);
            var resultado = _registrarSalidaProductoService.Handle(request);
            Assert.AreEqual("se registro la salida", resultado);
        }
        
    }
}