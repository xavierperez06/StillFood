using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Services
{
    public class FormasPago
    {
        public List<Models.FormaPago> ObtenerFormasPagoActivas()
        {
            Business.FormaPago wFormaPago = new Business.FormaPago();

            return ORM.ListaFormasPagoEntitieAModel(wFormaPago.ObtenerFormasPagoActivas());
        }

        public List<Models.FormaPago> ObtenerFormasPago()
        {
            Business.FormaPago wFormaPago = new Business.FormaPago();

            return ORM.ListaFormasPagoEntitieAModel(wFormaPago.ObtenerFormasPago());
        }

        public Models.FormaPago ObtenerFormaPago(int pIdFormaPago)
        {
            Business.FormaPago wFormaPago = new Business.FormaPago();

            return ORM.FormaPagoEntitieToModel(wFormaPago.ObtenerFormaPago(pIdFormaPago));
        }

        public int Guardar(Models.FormaPago pFormaPago)
        {
            Business.FormaPago wFormaPago = new Business.FormaPago();

            return wFormaPago.Guardar(ORM.FormaPagoModelToEntitie(pFormaPago));
        }

        public void Eliminar(int pIdFormaPago)
        {
            Business.FormaPago wFormaPago = new Business.FormaPago();
            wFormaPago.Eliminar(pIdFormaPago);
        }

        public List<Models.FormaPago> ObtenerFormasPagoRestantes(Models.Comercio pComercio)
        {
            Business.FormaPago wFormaPago = new Business.FormaPago();
            return ORM.ListaFormasPagoEntitieAModel(wFormaPago.ObtenerFormasPagoRestantes(ORM.ComercioModelToEntitie(pComercio)));
        }
    }
}
