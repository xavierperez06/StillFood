using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace StillFood.WEB.Patterns.State
{
    public class CanceladoState : State
    {
        public override Common.Enums.eResultadoAccion Accion(Models.NotaPedido pNotaPedido)
        {
            Common.Enums.eResultadoAccion wResultado = Common.Enums.eResultadoAccion.Ok;

            //Busco el usuario, para realizar en envío de email
            Services.Usuarios wUsuarioService = new Services.Usuarios();
            Models.Usuario wUsuario = wUsuarioService.ObtenerUsuario(pNotaPedido.IdUsuario);
            //Busco el comercio
            Services.Comercios wComercioService = new Services.Comercios();
            Models.Comercio wComercio = wComercioService.ObtenerComercio(pNotaPedido.IdComercio);

            if (wUsuario != null)
            {
                string wAsunto = "Pedido cancelado";
                string wTo = wUsuario.Email + ";" + wComercio.Email;
                string wComunicacion = string.IsNullOrWhiteSpace(wComercio.Telefono) ? wComercio.Email : wComercio.Telefono;
                string wMensaje = string.Format("<div style='width: 70%;margin: 0 auto;'><div><img src='' /></div><div style='font-size: 35px;color: #A3BD31;'> Hola, {0} </div> <br/> lamentamos informarle que su pedido n° {1} ha sido cancelado. <br/> Motivo cancelación: {2}. <br/> Puede comunicarse con {3} mediante: {4} para obtener más información. <br/> Atentamente, el equipo de StillFood. </div>", wUsuario.NombreApellido, pNotaPedido.Numero, pNotaPedido.MotivoCancelacion, wComercio.Nombre, wComunicacion);
                var wRecurso = new LinkedResource(HttpContext.Current.Server.MapPath("~/Content/Img/Header-Mail.jpg"));

                Common.ServicioEmail wServicio = new Common.ServicioEmail();

                Common.Enums.eResultadoEnvio wResultadoEmail = wServicio.Enviar(wTo, wAsunto, wMensaje, true, wRecurso);

                if (wResultadoEmail.Equals(Convert.ToInt32(Common.Enums.eResultadoEnvio.Error)))
                {
                    wResultado = Common.Enums.eResultadoAccion.Error;
                }
                else
                {
                    string wEstadoPrevio = Common.Utils.GetEnumDescription((Common.Enums.eEstadosNotasPedido)pNotaPedido.IdEstado);
                    pNotaPedido.IdEstado = Convert.ToInt32(Common.Enums.eEstadosNotasPedido.Cancelado);
                    Services.Compras wComprasService = new Services.Compras();
                    //Actualizo el estado de la nota de pedido
                    wComprasService.GuardarNotaPedido(pNotaPedido);
                    //Guardo el log de la transacción
                    Services.Logs wLogService = new Services.Logs();
                    wLogService.Agregar(wUsuario.Id, wEstadoPrevio, "Cancelado", "NotasPedidos");
                    wResultado = Common.Enums.eResultadoAccion.Ok;
                }
            }

            return wResultado;
        }
    }
}