using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StillFood.WEB.Patterns.Command
{
    public class IngresosDiariosCommand : Command
    {
        List<object> mLista;

        public IngresosDiariosCommand(Reporte pReporte) : base (pReporte)
        {

        }

        public override void Ejecutar()
        {
            mLista = new List<object>();
            mLista = _Reporte.ReporteDiario();
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