using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.DAL
{
    public class Logs
    {
        public void Agregar(Entities.Log pLog)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.Log.Add(pLog);
                wContext.SaveChanges();
            }
        }
    }
}
