using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StillFood.WEB.Strategy
{
    public class EfectivoStrategy : FormaPagoStrategy
    {
        private decimal mMontoPaga;

       public EfectivoStrategy(decimal pMontoPaga)
        {
            mMontoPaga = pMontoPaga;
        }

        public override bool Pagar(decimal pMonto)
        {
            return true;
        }
    }
}