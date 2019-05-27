using Bel.DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bel.DataLayer.Repository
{
    public class ReservationRepository : GenericRepository<Reservation>
    {
        beldatabaseEntities beldatabaseEntities = new beldatabaseEntities();
        public List<ReservationModel> GetReservations()
        {
            var query = from r in beldatabaseEntities.Reservation
                        join u in beldatabaseEntities.User on r.RefUserId equals u.Id
                        join mc in beldatabaseEntities.MunicipalityClass on r.RefMunicipalityClassId equals mc.Id
                        join sc in beldatabaseEntities.SchoolClass on r.RefSchoolId equals sc.Id
                        join ch in beldatabaseEntities.ClassHour on r.RefClassHourId equals ch.Id
                        select new { r.Id,r.IsActive,r.AdvisorName,r.ReservationDate
                        , r.RefUserId, UserName = u.Name 
                        , r.RefMunicipalityClassId, MunicipalityClassName = mc.Name
                        , r.RefSchoolId,SchoolName = sc.Name
                        , r.RefClassHourId, ch.Hour};
            var a =   query.ToList().Cast<ReservationModel>().ToList();
            return a;
        }
    }
}