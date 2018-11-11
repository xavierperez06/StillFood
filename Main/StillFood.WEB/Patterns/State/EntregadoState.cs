using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StillFood.WEB.Patterns.State
{
    public class EntregadoState : State
    {
        public override Common.Enums.eResultadoAccion Accion(Models.NotaPedido pNotaPedido)
        {
            Common.Enums.eResultadoAccion wResultado;

            try
            {
                pNotaPedido.IdEstado = Convert.ToInt32(Common.Enums.eEstadosNotasPedido.Entregado);
                Services.Compras wComprasService = new Services.Compras();
                //Actualizo el estado de la nota de pedido
                wComprasService.GuardarNotaPedido(pNotaPedido);
                //Guardo el log de la transacción
                Services.Logs wLogService = new Services.Logs();
                wLogService.Agregar(pNotaPedido.IdUsuario, "Pedido Preparado", "Pedido Entregado", "NotasPedidos");
                wResultado = Common.Enums.eResultadoAccion.Ok;
            }
            catch
            {
                wResultado = Common.Enums.eResultadoAccion.Error;
            }

            return wResultado;
        }
    }
}