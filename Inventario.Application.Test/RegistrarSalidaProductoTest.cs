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
    public class RegistrarSalidaProductoTest
    {
        private InventarioContext _dbContext;
        private RegistrarSalidaProductoService _registrarSalidaProductoService;

        private IProductoRepository _productoRepository;

        [SetUp]
        public void Setup()
        {
            var optionsSqlite = new DbContextOptionsBuilder<InventarioContext>()
                .UseSqlite(@"Data Source=C:\\sqlite\\InventarioDBTest.db")
                .Options;

            _dbContext = new InventarioContext(optionsSqlite);
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
            _productoRepository = new ProductoRepository(_dbContext);

           
            _registrarSalidaProductoService = new RegistrarSalidaProductoService(
                new UnitOfWork(_dbContext),
                _productoRepository
            );
          
        }

        [Test]
        public void PuedoRegistrarLaSalidaDeProductoCompuesto()
        {
            var ensaladaConG=ProductoCompuestoMother.CrearProductoCompuestoComplejo();
            _productoRepository.Add(ensaladaConG);
            _productoRepository.AddRange(ensaladaConG.ProductosEnInventario);
            _dbContext.SaveChanges();
            var request = new SalidaProductoRequest("Compuesto",ensaladaConG.Codigo,1);
            var resultado = _registrarSalidaProductoService.Handle(request);
            Assert.AreEqual("se registro la salida de 1 Ensalada Con Gaseosa", resultado);
        }
        [Test]
        public void PuedoRegistrarLaSalidaDeProductoSimple()
        {
            var gaseosaInv=new ProductoSimple("sadad", "gaseosa", 5, 100, 200);
            _productoRepository.Add(gaseosaInv);
            _dbContext.SaveChanges();
            var request = new SalidaProductoRequest("Simple",gaseosaInv.Codigo,1);
            var resultado = _registrarSalidaProductoService.Handle(request);
            Assert.AreEqual("La nueva cantidad del producto gaseosa es 4", resultado);
        }
        [Test]
        public void NoPuedoRegistrarLaSalidaDeProductoSimpleSiLaCantidadExcedeLaDisponible()
        {
            var gaseosaInv=new ProductoSimple("sadad", "gaseosa", 5, 100, 200);
            _productoRepository.Add(gaseosaInv);
            _dbContext.SaveChanges();
            var request = new SalidaProductoRequest("Simple",gaseosaInv.Codigo,6);
            var resultado = _registrarSalidaProductoService.Handle(request);
            Assert.AreEqual("Error gaseosa: La cantidad de la salida no puede ser mayor a la disponible", resultado);
        }

        [Test]
        public void NoPuedoRegistrarLaSalidaDeProductoCompuestoSiNoHayProductosParaHacerlo()
        {
            var ensalada = ProductoCompuestoMother.CrearProducto();
            _productoRepository.Add(ensalada);
            _productoRepository.AddRange(ensalada.ProductosEnInventario);
            _dbContext.SaveChanges();
            var request = new SalidaProductoRequest("Simple",ensalada.Codigo,6);
            var resultado = _registrarSalidaProductoService.Handle(request);
            Assert.AreEqual("Error tomate: La cantidad de la salida no puede ser mayor a la disponible", resultado);

        }
        
    }
}