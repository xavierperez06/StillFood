using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StillFood.WEB.Patterns.Command
{
    public class IngresosMensualesCommand : Command
    {
        List<object> mLista;

        public IngresosMensualesCommand(Reporte pReporte) : base(pReporte)
        {

        }

        public override void Ejecutar()
        {
            mLista = new List<object>();
            mLista = _Reporte.ReporteMensual ();
        }

        public override List<object> Resultado
        {
            get
            {
                return mLista;
            }

        }
    }
}