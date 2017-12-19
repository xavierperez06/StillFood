using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace StillFood.Test
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void ObtenerMontoTotal()
        {
            decimal wMontoTotal = 278.40M;

            Models.Producto wProducto1 = new Models.Producto();
            wProducto1.PrecioDescuento = 52.30M;
            wProducto1.Cantidad = 2;

            Models.Producto wProducto2 = new Models.Producto();
            wProducto2.PrecioDescuento = 43.45M;
            wProducto2.Cantidad = 4;

            List<Models.Producto> wListaProductos = new List<Models.Producto>();
            wListaProductos.Add(wProducto1);
            wListaProductos.Add(wProducto2);

            Assert.AreEqual(wMontoTotal, Common.Utils.ObtenerMontoTotal(wListaProductos));
        }

        [TestMethod]
        public void IniciarSesion()
        {
            WEB.Facade.FacadeSecurity wFacade = new WEB.Facade.FacadeSecurity();
            string wEmail = "prueba@hotmail.com";
            string wContraseña = "1234";
            bool wRecuerdame = false;

            Common.Enums.eResultadoLogin wResultado = Common.Enums.eResultadoLogin.Logueado;


            Assert.AreEqual(wResultado, wFacade.Autenticar(wEmail, wContraseña, wRecuerdame));
        }
    }
}
