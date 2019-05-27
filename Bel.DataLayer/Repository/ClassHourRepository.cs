using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bel.DataLayer.Repository
{
    public class ClassHourRepository : GenericRepository<ClassHour>
    {
        beldatabaseEntities beldatabaseEntities = new beldatabaseEntities();
        public List<ClassHour> GetUserByClasHourId(int municipalityClassId)
        {
            return beldatabaseEntities.ClassHour.Where(x => x.RefMunicipalityClassId == municipalityClassId).ToList();
        }
    }
}
