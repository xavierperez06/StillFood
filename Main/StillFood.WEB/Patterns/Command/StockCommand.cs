using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StillFood.WEB.Patterns.Command
{
    public class StockCommand : Command
    {
        List<object> mLista;

        public StockCommand(Reporte pReporte) : base(pReporte)
        {

        }

        public override void Ejecutar()
        {
            mLista = new List<object>();
            mLista = _Reporte.ReporteStock();
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