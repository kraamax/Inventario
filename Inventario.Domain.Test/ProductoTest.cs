using NUnit.Framework;
using Inventario.Domain;
using System.Collections.Generic;

namespace Inventario.Domain.Test
{
    public class ProductoTests
    {
        [SetUp]
        public void Setup()
        {
        }
        /*
        COMO USUARIO QUIERO REGISTRAR LA ENTRADA PRODUCTOS 
       CRITERIOS DE ACEPTACI�N
       1. La cantidad de la entrada de debe ser mayor a 0
       Dado un producto simple con id=123a, nombre=lechuga, cantidad=2, costo=1000, precio=1500
       cuando se van a agregas 0 unidades mas 
       entonces mostrara el mensaje La cantidad de la entrada de debe ser mayor a 0
            */
        [Test]
        public void NoPuedeRegitrarEntradaDeProductosConCantidadMenorOIgualACero()
        {
            var producto = new ProductoSimple("123a", "lechuga", 1, 1000,1500 );
            var resultado = producto.RegistrarEntradaProducto(0);
            Assert.AreEqual("La cantidad de la entrada de debe ser mayor a 0", resultado);
        }
        /*
         COMO USUARIO QUIERO REGISTRAR LA ENTRADA PRODUCTOS 
        CRITERIOS DE ACEPTACI�N
           2. La cantidad de la entrada se le aumentar� a la cantidad existente del producto
        Dado un producto simple con id=123a, nombre=lechuga, cantidad=2, costo=1000, precio=1500
        cuando se va a registrar la entrada de 2 unidades mas
        entonces mostrara el mensaje la cantidad del producto lechuga es 4
             */
        [Test]
        public void PuedoSumarCantidadAUnProducto()
        {
            var producto = new ProductoSimple("123a", "lechuga", 2, 1000,1500 );

            var resultado = producto.RegistrarEntradaProducto(2);
            Assert.AreEqual("La cantidad del producto lechuga es 4", resultado);
        }
        /* COMO USUARIO QUIERO REGISTRAR LA SALIDA PRODUCTOS
         CRITERIOS DE ACEPTACI�N
         La cantidad de la salida de debe ser mayor a 0
         Dado un producto simple con id=123a, nombre=lechuga, cantidad=4, costo=1000, precio=1500
         cuando se va a registrar la salida de 0 unidades 
         entonces mostrara el mensaje La cantidad de la salida de debe ser mayor a 0
        */
        [Test]
        public void NoPuedoRegistrarUnaSalidaDeProductoConCantidadMenorOIgualACero()
        {
            var producto = new ProductoSimple("123a", "lechuga", 1, 1000,1500 );
            var resultado = producto.RegistrarSalidaProducto(0);
            Assert.AreEqual("La cantidad de la salida de debe ser mayor a 0", resultado);
        }
        /* COMO USUARIO QUIERO REGISTRAR LA SALIDA PRODUCTOS
         CRITERIOS DE ACEPTACI�N
         2. En caso de un producto sencillo la cantidad de la salida se le disminuir� a la cantidad existente del producto.
         Dado un producto simple con id=123a, nombre=lechuga, cantidad=4, costo=1000, precio=1500
         cuando se va a registrar la salida de 2 unidades 
         entonces mostrara el mensaje la cantidad del producto lechuga es 2
        */
        [Test]
        public void TengoQueDisminuirLaCantidadDelProductoExistenteCuandoRegistroLaSalida()
        {
            var producto = new ProductoSimple("123a", "lechuga", 4, 1000,1500 );
            var resultado = producto.RegistrarSalidaProducto(2);
            Assert.AreEqual("La cantidad del producto lechuga es 2", resultado);
        }
        //producto compuesto
        /*  HU1.SALIDA DE PRODUCTO(3.5)
      COMO USUARIO QUIERO REGISTRAR LA SALIDA PRODUCTOS
      CRITERIOS DE ACEPTACI�N
      1. La cantidad de la salida de debe ser mayor a 0
      2. En caso de un producto sencillo la cantidad de la salida se le disminuir� a la cantidad existente del producto.
      3. En caso de un producto compuesto la cantidad de la salida se le disminuir� a la cantidad existente de cada uno de su ingrediente
      Dado un producto compuesto llamado la ensalada conformado por "123a", 1500, 1000, 4, "lechuga" y "123b", 1500, 1000, 4, "tomate"
      cuando se va a registrar la salida de una ensalada
      entonces -La cantidad del producto lechuga es 3-La cantidad del producto tomate es 3-La cantidad del producto Gaseosa es 19La cantidad del producto lechuga es 2 -La cantidad del producto tomate es 2
   */
        [Test]
        public void TengoQueDismuirLaCantidadDeIngredientesDeCadaProducto()
        {
            List<Producto> IngredientesEnsalada = new List<Producto>();
            var tomate = new ProductoSimple("123a", "tomate", 1, 1300,1500 );
            var lechuga = new ProductoSimple("123b", "lechuga", 1, 1000, 1500);
            var gaseosa = new ProductoSimple("123c", "Gaseosa", 1, 1000, 1500);
            IngredientesEnsalada.Add(tomate);
            IngredientesEnsalada.Add(lechuga);
            var ensalada = new ProductoCompuesto("123d", "Ensalada", 1,4000 , IngredientesEnsalada);
            List<Producto> ensaladaConGaseosaComposicicion = new List<Producto>();
            ensaladaConGaseosaComposicicion.Add(ensalada);
            ensaladaConGaseosaComposicicion.Add(gaseosa);
            var ensaladaConGaseosaCombo = new ProductoCompuesto("123d", "EnsaladaConGaseosa", 1,4000 , ensaladaConGaseosaComposicicion);
            //inventario.RegistrarSalidaProductoCompuesto(combo);
            var resultado = ensaladaConGaseosaCombo.RegistrarSalidaProducto(2);
            Assert.AreEqual("Salida registrada EnsaladaConGaseosa, cantidad 2 precio 8000", resultado);
        }
        /*5. El costo de los productos compuestos corresponden al costo de sus ingredientes por la cantidad de estos.*/
        [Test]
        public void ElCostoDeLosProductosCorrespondeAlCostodeSusIngredientes()
        {

            List<Producto> IngredientesEnsalada = new List<Producto>();
            var tomate = new ProductoSimple("123a", "tomate", 1, 1300,1500 );
            var lechuga = new ProductoSimple("123b", "lechuga", 1, 1000, 1500);
            var gaseosa = new ProductoSimple("123c", "Gaseosa", 1, 1000, 1500);
            IngredientesEnsalada.Add(tomate);
            IngredientesEnsalada.Add(lechuga);
            var ensalada = new ProductoCompuesto("123d", "Ensalada", 1,4000 , IngredientesEnsalada);
            List<Producto> ensaladaConGaseosaComposicicion = new List<Producto>();
            ensaladaConGaseosaComposicicion.Add(ensalada);
            ensaladaConGaseosaComposicicion.Add(gaseosa);
            var ensaladaConGaseosaCombo = new ProductoCompuesto("123d", "EnsaladaConGaseosa", 1,4000 , ensaladaConGaseosaComposicicion);
            var resultado = "El costo es: $" + ensaladaConGaseosaCombo.CalcularCosto();
            Assert.AreEqual("El costo es: $3300", resultado);
        }
    }
}