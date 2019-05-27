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
                        select new ReservationModel {
                            Id = r.Id
                            , IsActive = r.IsActive.Value
                            , AdvisorName = r.AdvisorName
                            , ReservationDate =  r.ReservationDate
                            , RefUserId = r.RefUserId.Value
                            , UserName = u.Name 
                            , RefMunicipalityClassId = r.RefMunicipalityClassId.Value
                            , MunicipalityClassName = mc.Name
                            , RefSchoolId = r.RefSchoolId.Value
                            , SchoolName = sc.Name
                            , RefClassHourId = r.RefClassHourId.Value
                            , Hour = ch.Hour};
            var a =   query.ToList();
            return a;
        }
    }
}