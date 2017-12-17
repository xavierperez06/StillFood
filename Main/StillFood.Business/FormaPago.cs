using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Business
{
    public class FormaPago
    {
        public List<Entities.FormaPago> ObtenerFormasPagoActivas()
        {
            DAL.FormasPago wFormasPagoDAL = new DAL.FormasPago();
            return wFormasPagoDAL.ObtenerFormasPagoActivas();
        }

        public List<Entities.FormaPago> ObtenerFormasPago()
        {
            DAL.FormasPago wFormasPagoDAL = new DAL.FormasPago();
            return wFormasPagoDAL.ObtenerFormasPago();
        }

        public Entities.FormaPago ObtenerFormaPago(int pId)
        {
            DAL.FormasPago wFormasPagoDAL = new DAL.FormasPago();
            return wFormasPagoDAL.ObtenerFormaPago(pId);
        }

        public int Guardar(Entities.FormaPago pFormaPago)
        {
            DAL.FormasPago wFormasPagoDAL = new DAL.FormasPago();
            int wId = 0;

            if (pFormaPago.Id == 0)
            {
                wId = wFormasPagoDAL.Agregar(pFormaPago);
            }
            else
            {
                wId = wFormasPagoDAL.Editar(pFormaPago);
            }

            return wId;
        }

        public void Eliminar(int pIdFormaPago)
        {
            DAL.FormasPago wFormasPagoDAL = new DAL.FormasPago();
            wFormasPagoDAL.Eliminar(pIdFormaPago);
        }

        public List<Entities.FormaPago> ObtenerFormasPagoRestantes(Entities.Comercio pComercio)
        {
            DAL.FormasPago wFormasPagoDAL = new DAL.FormasPago();
            return wFormasPagoDAL.ObtenerFormasPagoRestantes(pComercio);
        }
    }
}
