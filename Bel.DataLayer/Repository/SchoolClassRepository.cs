using Bel.DataLayer.Interfaces;
using Bel.DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bel.DataLayer.Repository
{
    public class SchoolClassRepository : GenericRepository<SchoolClass>, ISchoolClassRepository
    {
        private readonly beldatabaseEntities context;
        public SchoolClassRepository(beldatabaseEntities context)
        {
            this.context = context;
        }
        public List<SchoolClassModel> GetSchoolClass(int refUserId)
        {
            /*var query = from school in beldatabaseEntities.SchoolClass
                        where school.RefUserId == refUserId
                        select new { schoolClass = school.Name };
            return query.ToList();*/

            var query = from r in context.SchoolClass
                        where r.RefUserId==refUserId
                        select new SchoolClassModel
                        {
                            Id=r.Id,
                            Name=r.Name
                        };
            var a = query.ToList();
            return a;
        }
    }
}
