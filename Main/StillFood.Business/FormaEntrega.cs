using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Business
{
    public class FormaEntrega
    {
        public List<Entities.FormaEntrega> ObtenerFormasEntregasActivas()
        {
            DAL.FormasEntregas wFormasEntregasDAL = new DAL.FormasEntregas();
            return wFormasEntregasDAL.ObtenerFormasEntregasActivas();
        }

        public List<Entities.FormaEntrega> ObtenerFormasEntrega()
        {
            DAL.FormasEntregas wFormasEntregaDAL = new DAL.FormasEntregas();
            return wFormasEntregaDAL.ObtenerFormasEntrega();
        }

        public Entities.FormaEntrega ObtenerFormaEntrega(int pId)
        {
            DAL.FormasEntregas wFormasEntregaDAL = new DAL.FormasEntregas();
            return wFormasEntregaDAL.ObtenerFormaEntrega(pId);
        }

        public int Guardar(Entities.FormaEntrega pFormaEntrega)
        {
            DAL.FormasEntregas wFormasEntregaDAL = new DAL.FormasEntregas();
            int wId = 0;

            if (pFormaEntrega.Id == 0)
            {
                wId = wFormasEntregaDAL.Agregar(pFormaEntrega);
            }
            else
            {
                wId = wFormasEntregaDAL.Editar(pFormaEntrega);
            }

            return wId;
        }

        public void Eliminar(int pIdFormaEntrega)
        {
            DAL.FormasEntregas wFormasEntregaDAL = new DAL.FormasEntregas();
            wFormasEntregaDAL.Eliminar(pIdFormaEntrega);
        }
    }
}
