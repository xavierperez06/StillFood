using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Services
{
    public class Logs
    {
        public void Agregar(int pIdUsuario, string pEstadoAnterior, string pEstadoPosterior, string pTabla)
        {
            Models.Log wLog = new Models.Log();

            wLog.IdUsuario = pIdUsuario;
            wLog.EstadoAnterior = pEstadoAnterior;
            wLog.EstadoPosterior = pEstadoPosterior;
            wLog.Tabla = pTabla;
            wLog.Fecha = DateTime.Now;

            Business.Log wBusinessLog = new Business.Log();

            wBusinessLog.Agregar(ORM.LogModelToEntitie(wLog));
        }
    }
}
