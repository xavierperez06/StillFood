using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Business
{
    public class Log
    {
        public void Agregar(Entities.Log pLog)
        {
            DAL.Logs wLogsDAL = new DAL.Logs();

            wLogsDAL.Agregar(pLog);
        }
    }
}
