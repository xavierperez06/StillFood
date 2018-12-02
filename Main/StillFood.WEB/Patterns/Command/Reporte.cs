using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace StillFood.WEB.Patterns.Command
{
    //Receiver
    public class Reporte
    {
        public List<object> ReporteDiario()
        {
            Services.Comercios wComerciosServices = new Services.Comercios();
            Facade.FacadeSecurity wFacade = new Facade.FacadeSecurity();

            Models.Usuario wUsuario = wFacade.ObtenerUsuario();

            if (wUsuario == null)
                return null;

            List<Models.NotaPedido> wNotasPedidos = wComerciosServices.ReporteIngresosDiarios(wUsuario.IdComercio.Value);

            List<object> wDatos = new List<object>();
            DataTable wDt = new DataTable();

            wDt.Columns.Add("Dia", Type.GetType("System.String"));
            wDt.Columns.Add("Monto", Type.GetType("System.Decimal"));

            DataRow wDr = wDt.NewRow();

            decimal[] wMontos = new decimal[7]; //0-Lunes - 6 Domingo
            

            foreach (Models.NotaPedido wNota in wNotasPedidos)
            {
                var wDia = wNota.FechaAlta.DayOfWeek.ToString();
                decimal wMonto = 0;

                foreach(Models.NotaPedidoDetalle wDetalle in wNota.Detalles)
                {
                    wMonto = wMonto + (wDetalle.Precio * wDetalle.Cantidad);
                }


                switch (wDia)
                {
                    case "Monday":
                        wMontos[0] = wMontos[0] + wMonto;
                        break;
                    case "Tueday":
                        wMontos[1] = wMontos[1] + wMonto;
                        break;
                    case "Wednesday":
                        wMontos[2] = wMontos[2] + wMonto;
                        break;
                    case "Thuesday":
                        wMontos[3] = wMontos[3] + wMonto;
                        break;
                    case "Friday":
                        wMontos[4] = wMontos[4] + wMonto;
                        break;
                    case "Saturday":
                        wMontos[5] = wMontos[5] + wMonto;
                        break;
                    case "Sunday":
                        wMontos[6] = wMontos[6] + wMonto;
                        break;
                }
            }

            wDr["Dia"] = "Lunes";
            wDr["Monto"] = wMontos[0];
            wDt.Rows.Add(wDr);

            wDr = wDt.NewRow();
            wDr["Dia"] = "Martes";
            wDr["Monto"] = wMontos[1];
            wDt.Rows.Add(wDr);

            wDr = wDt.NewRow();
            wDr["Dia"] = "Miercoles";
            wDr["Monto"] = wMontos[2];
            wDt.Rows.Add(wDr);

            wDr = wDt.NewRow();
            wDr["Dia"] = "Jueves";
            wDr["Monto"] = wMontos[3];
            wDt.Rows.Add(wDr);

            wDr = wDt.NewRow();
            wDr["Dia"] = "Viernes";
            wDr["Monto"] = wMontos[4];
            wDt.Rows.Add(wDr);

            wDr = wDt.NewRow();
            wDr["Dia"] = "Sábado";
            wDr["Monto"] = wMontos[5];
            wDt.Rows.Add(wDr);

            wDr = wDt.NewRow();
            wDr["Dia"] = "Domingo";
            wDr["Monto"] = wMontos[6];
            wDt.Rows.Add(wDr);


            foreach (DataColumn wDc in wDt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in wDt.Rows select drr[wDc.ColumnName]).ToList();
                wDatos.Add(x);
            }

            return wDatos;
        }

        public List<object> ReporteMensual()
        {
            Services.Comercios wComerciosServices = new Services.Comercios();
            Facade.FacadeSecurity wFacade = new Facade.FacadeSecurity();

            Models.Usuario wUsuario = wFacade.ObtenerUsuario();

            if (wUsuario == null)
                return null;

            List<Models.NotaPedido> wNotasPedidos = wComerciosServices.ReporteIngresosMensuales(wUsuario.IdComercio.Value);

            List<object> wDatos = new List<object>();
            DataTable wDt = new DataTable();

            wDt.Columns.Add("Mes", Type.GetType("System.String"));
            wDt.Columns.Add("Monto", Type.GetType("System.Decimal"));

            DataRow wDr = wDt.NewRow();

            decimal[] wMontos = new decimal[12]; 


            foreach (Models.NotaPedido wNota in wNotasPedidos)
            {
                var wMes = wNota.FechaAlta.Month;
                decimal wMonto = 0;

                foreach (Models.NotaPedidoDetalle wDetalle in wNota.Detalles)
                {
                    wMonto = wMonto + (wDetalle.Precio * wDetalle.Cantidad);
                }

                switch(wMes)
                {
                    case 1:
                        wMontos[0] = wMontos[0] + wMonto;
                        break;
                    case 2:
                        wMontos[1] = wMontos[1] + wMonto;
                        break;
                    case 3:
                        wMontos[2] = wMontos[2] + wMonto;
                        break;
                    case 4:
                        wMontos[3] = wMontos[3] + wMonto;
                        break;
                    case 5:
                        wMontos[4] = wMontos[4] + wMonto;
                        break;
                    case 6:
                        wMontos[5] = wMontos[5] + wMonto;
                        break;
                    case 7:
                        wMontos[6] = wMontos[6] + wMonto;
                        break;
                    case 8:
                        wMontos[7] = wMontos[7] + wMonto;
                        break;
                    case 9:
                        wMontos[8] = wMontos[8] + wMonto;
                        break;
                    case 10:
                        wMontos[9] = wMontos[9] + wMonto;
                        break;
                    case 11:
                        wMontos[10] = wMontos[10] + wMonto;
                        break;
                    case 12:
                        wMontos[11] = wMontos[11] + wMonto;
                        break;
                }
            }


            wDr["Mes"] = "Enero";
            wDr["Monto"] = wMontos[0];
            wDt.Rows.Add(wDr);

            wDr = wDt.NewRow();
            wDr["Mes"] = "Febrero";
            wDr["Monto"] = wMontos[1];
            wDt.Rows.Add(wDr);

            wDr = wDt.NewRow();
            wDr["Mes"] = "Marzo";
            wDr["Monto"] = wMontos[2];
            wDt.Rows.Add(wDr);

            wDr = wDt.NewRow();
            wDr["Mes"] = "Abril";
            wDr["Monto"] = wMontos[3];
            wDt.Rows.Add(wDr);

            wDr = wDt.NewRow();
            wDr["Mes"] = "Mayo";
            wDr["Monto"] = wMontos[4];
            wDt.Rows.Add(wDr);

            wDr = wDt.NewRow();
            wDr["Mes"] = "Junio";
            wDr["Monto"] = wMontos[5];
            wDt.Rows.Add(wDr);

            wDr = wDt.NewRow();
            wDr["Mes"] = "Julio";
            wDr["Monto"] = wMontos[6];
            wDt.Rows.Add(wDr);

            wDr = wDt.NewRow();
            wDr["Mes"] = "Agosto";
            wDr["Monto"] = wMontos[7];
            wDt.Rows.Add(wDr);

            wDr = wDt.NewRow();
            wDr["Mes"] = "Septiembre";
            wDr["Monto"] = wMontos[8];
            wDt.Rows.Add(wDr);

            wDr = wDt.NewRow();
            wDr["Mes"] = "Octubre";
            wDr["Monto"] = wMontos[9];
            wDt.Rows.Add(wDr);

            wDr = wDt.NewRow();
            wDr["Mes"] = "Noviembre";
            wDr["Monto"] = wMontos[10];
            wDt.Rows.Add(wDr);

            wDr = wDt.NewRow();
            wDr["Mes"] = "Diciembre";
            wDr["Monto"] = wMontos[11];
            wDt.Rows.Add(wDr);

            foreach (DataColumn wDc in wDt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in wDt.Rows select drr[wDc.ColumnName]).ToList();
                wDatos.Add(x);
            }

            return wDatos;
        }


        public List<object> ReporteProductosVendidos()
        {
            Services.Comercios wComerciosServices = new Services.Comercios();
            Facade.FacadeSecurity wFacade = new Facade.FacadeSecurity();

            Models.Usuario wUsuario = wFacade.ObtenerUsuario();

            if (wUsuario == null)
                return null;

            List<Models.NotaPedido> wNotasPedido = wComerciosServices.ReporteProductosVendidos(wUsuario.IdComercio.Value);

            List<object> wDatos = new List<object>();
            DataTable wDt = new DataTable();

            wDt.Columns.Add("Categoria", Type.GetType("System.String"));
            wDt.Columns.Add("Cantidad", Type.GetType("System.Int32"));

            DataRow wDr = wDt.NewRow();

            List<ReporteItem> wReporte = new List<ReporteItem>();

            //Obtengo las distintas categorias de los productos vendidos
            foreach(Models.NotaPedido wNotaPedido in wNotasPedido)
            {
                foreach(Models.NotaPedidoDetalle wDetalle in wNotaPedido.Detalles)
                {
                    bool wContiene = wReporte.Any(x => x.Id == wDetalle.Producto.Categoria.Nombre);

                    if (!wContiene)
                    {
                        ReporteItem wItem = new ReporteItem();

                        wItem.Id = wDetalle.Producto.Categoria.Nombre;

                        wReporte.Add(wItem);
                    }
                }
            }

            foreach (Models.NotaPedido wNotaPedido in wNotasPedido)
            {
                foreach (Models.NotaPedidoDetalle wDetalle in wNotaPedido.Detalles)
                {
                    foreach(ReporteItem wRep in wReporte)
                    {
                        if(wRep.Id == wDetalle.Producto.Categoria.Nombre)
                        {
                            wRep.Cantidad = wRep.Cantidad + wDetalle.Cantidad;
                        }
                    }
                }
            }

            foreach(ReporteItem wRep in wReporte.OrderBy(r => r.Id))
            {
                wDr = wDt.NewRow();
                wDr["Categoria"] = wRep.Id;
                wDr["Cantidad"] = wRep.Cantidad;
                wDt.Rows.Add(wDr);
            }

            foreach (DataColumn wDc in wDt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in wDt.Rows select drr[wDc.ColumnName]).ToList();
                wDatos.Add(x);
            }

            return wDatos;
        }

        public List<object> ReporteStock()
        {
            Services.Productos wProductosServices = new Services.Productos();
            Facade.FacadeSecurity wFacade = new Facade.FacadeSecurity();

            Models.Usuario wUsuario = wFacade.ObtenerUsuario();

            if (wUsuario == null)
                return null;

            List<Models.Producto> wProductos = wProductosServices.ReporteStock(wUsuario.IdComercio.Value);

            List<ReporteItem> wReporte = new List<ReporteItem>();

            List<object> wDatos = new List<object>();
            DataTable wDt = new DataTable();

            wDt.Columns.Add("Producto", Type.GetType("System.String"));
            wDt.Columns.Add("Cantidad", Type.GetType("System.Int32"));

            DataRow wDr = wDt.NewRow();

            foreach (Models.Producto wProducto in wProductos)
            {
                bool wContiene = wReporte.Any(x => x.Id == wProducto.Nombre);

                if (!wContiene)
                {
                    ReporteItem wItem = new ReporteItem();
                    wItem.Id = wProducto.Nombre;
                    if (wProducto.Stock.HasValue)
                        wItem.Cantidad = wProducto.Stock.Value;
                    else
                        wItem.Cantidad = 0;

                    wReporte.Add(wItem);
                }
            }

            foreach (ReporteItem wRep in wReporte.OrderBy(r => r.Id))
            {
                wDr = wDt.NewRow();
                wDr["Producto"] = wRep.Id;
                wDr["Cantidad"] = wRep.Cantidad;
                wDt.Rows.Add(wDr);
            }

            foreach (DataColumn wDc in wDt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in wDt.Rows select drr[wDc.ColumnName]).ToList();
                wDatos.Add(x);
            }

            return wDatos;
        }
    }
}

public class ReporteItem
{
    public string Id { get; set; }
    public int Cantidad { get; set; }
}