using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StillFood.WEB.Patterns.Command
{
    //Command
    public abstract class Command
    {
        protected Reporte _Reporte = null;

        public Command(Reporte pReporte)
        {
           _Reporte = pReporte;
        }

        public abstract void Ejecutar();
        public abstract List<object> Resultado { get; }
    }
}