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
            return null;
        }
    }
}