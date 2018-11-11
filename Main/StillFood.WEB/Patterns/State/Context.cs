using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StillFood.WEB.Patterns.State
{
    public class Context 
    {
        private State mState;

        public Context() 
        {
            mState = null;
        }

        public void SetearEstado(State pState)
        {
            this.mState = pState;
        }

        public State ObtenerEstado()
        {
            return this.mState;
        }

        public Common.Enums.eResultadoAccion EjecutarAccion(Models.NotaPedido pNotaPedido)
        {
           return this.mState.Accion(pNotaPedido);
        }
    }
}