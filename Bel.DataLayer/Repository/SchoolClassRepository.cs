using Bel.DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bel.DataLayer.Repository
{
    public class SchoolClassRepository : GenericRepository<SchoolClass>
    {
        beldatabaseEntities beldatabaseEntities = new beldatabaseEntities();
        public List<SchoolClassModel> GetSchoolClass(int refUserId)
        {
            /*var query = from school in beldatabaseEntities.SchoolClass
                        where school.RefUserId == refUserId
                        select new { schoolClass = school.Name };
            return query.ToList();*/

            var query = from r in beldatabaseEntities.SchoolClass
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
