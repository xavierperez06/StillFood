using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StillFood.WEB.Strategy
{
    public class TarjetaCreditoStrategy : FormaPagoStrategy
    {
        private string mNombre;
        private string mNumeroTarjeta;
        private string mCVV;
        private string mFechaVencimiento;


        public TarjetaCreditoStrategy(string pNombre, string pNumeroTarjeta, string pCVV, string pFechaVencimiento)
        {
            mNombre = pNombre;
            mNumeroTarjeta = pNumeroTarjeta;
            mCVV = pCVV;
            mFechaVencimiento = pFechaVencimiento;
        }

        public override bool Pagar(decimal pMonto)
        {
            if (!ValidarNumero(mNumeroTarjeta) || !ValidarCVV(mCVV) || !ValidarVencimiento(mFechaVencimiento))
            {
                return false;
            }
            else
            {
                return true;
            }   
        }

        private bool ValidarVencimiento(string pFechaVencimiento)
        {
            bool wValido = true;

            char wSeparador = Convert.ToChar("/");
            string[] wFechas = pFechaVencimiento.Split(wSeparador);

            if (wFechas.Count() != 2)
            {
                wValido = false;
            }
            else
            {
                //El primero corresponde al Mes
                int wMes = Convert.ToInt32(wFechas[0]);
                int wAnio = Convert.ToInt32(String.Concat("20",wFechas[1]));

                if(wMes < 1 || wMes > 12)
                {
                    wValido = false;
                }

                if(wAnio < DateTime.Now.Year)
                {
                    wValido = false;
                }
            }

            return wValido;
        }

        private bool ValidarCVV(string pCVV)
        {
            bool wValido = true;

            if (string.IsNullOrWhiteSpace(pCVV))
            {
                wValido = false;
            }

            if (pCVV.Length > 3 || pCVV.Length < 3)
            {
                wValido = false;
            }

            return wValido;
        }


        private bool ValidarNumero(string pNumeroTarjeta)
        {
            bool wValido = true;

            if(string.IsNullOrWhiteSpace(pNumeroTarjeta))
            {
                wValido = false;
            }

            foreach (char wDigito in pNumeroTarjeta.Reverse())
            {
                if (wDigito < '0' || wDigito > '9')
                {
                    wValido = false;
                }
            }

            if (pNumeroTarjeta.Length > 16 || pNumeroTarjeta.Length < 16)
            {
                wValido = false;
            }

            int wSumaDigitos = pNumeroTarjeta.Where((e) => e >= '0' && e <= '9')
                    .Reverse()
                    .Select((e, i) => ((int)e - 48) * (i % 2 == 0 ? 1 : 2))
                    .Sum((e) => e / 10 + e % 10);


            // Si la suma final es divisible por 10, entonces la tarjeta es valida, de lo contrario no.
            if (wSumaDigitos % 10 != 0)
            {
                wValido = false;
            }

            return wValido;
        }
    }
}