using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StillFood.WEB.Strategy
{
    public abstract class FormaPagoStrategy
    {
        public abstract bool Pagar(decimal pMonto);
    }
}