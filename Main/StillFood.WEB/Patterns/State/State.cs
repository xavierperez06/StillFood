using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StillFood.WEB.Patterns.State
{
    public abstract class State
    {
        public abstract Common.Enums.eResultadoAccion Accion(Models.NotaPedido pNotaPedido);
    }
}