using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StillFood.WEB.Patterns.State
{
    public class RealizadoState : State
    {
        public RealizadoState()
        {
        }

        public override Common.Enums.eResultadoAccion Accion(Models.NotaPedido pNotaPedido)
        {
            return Common.Enums.eResultadoAccion.Ok;
        }
    }
}