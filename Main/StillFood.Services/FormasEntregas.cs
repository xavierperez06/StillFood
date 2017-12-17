using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Services
{
    public class FormasEntregas
    {
        public List<Models.FormaEntrega> ObtenerFormasEntregasActivas()
        {
            Business.FormaEntrega wFormaEntrega = new Business.FormaEntrega();

            return ORM.ListaFormasEntregasEntitieAModel(wFormaEntrega.ObtenerFormasEntregasActivas());
        }

        public List<Models.FormaEntrega> ObtenerFormasEntrega()
        {
            Business.FormaEntrega wFormaEntrega = new Business.FormaEntrega();

            return ORM.ListaFormasEntregasEntitieAModel(wFormaEntrega.ObtenerFormasEntrega());
        }

        public Models.FormaEntrega ObtenerFormaEntrega(int pIdFormaEntrega)
        {
            Business.FormaEntrega wFormaEntrega = new Business.FormaEntrega();

            return ORM.FormaEntregaEntitieToModel(wFormaEntrega.ObtenerFormaEntrega(pIdFormaEntrega));
        }

        public int Guardar(Models.FormaEntrega pFormaEntrega)
        {
            Business.FormaEntrega wFormaEntrega = new Business.FormaEntrega();

            return wFormaEntrega.Guardar(ORM.FormaEntregaModelToEntitie(pFormaEntrega));
        }

        public void Eliminar(int pIdFormaEntrega)
        {
            Business.FormaEntrega wFormaEntrega = new Business.FormaEntrega();
            wFormaEntrega.Eliminar(pIdFormaEntrega);
        }
    }
}
