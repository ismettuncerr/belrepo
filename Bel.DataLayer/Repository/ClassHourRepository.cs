using Bel.DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bel.DataLayer.Repository
{
    public class ClassHourRepository : GenericRepository<ClassHour>, IClassHourRepository
    {
        private readonly beldatabaseEntities context;
        public ClassHourRepository(beldatabaseEntities context)
        {
            this.context = context;
        }
        public List<ClassHour> GetUserByClasHourId(int municipalityClassId)
        {
            return context.ClassHour.Where(x => x.RefMunicipalityClassId == municipalityClassId).ToList();
        }
    }
}
