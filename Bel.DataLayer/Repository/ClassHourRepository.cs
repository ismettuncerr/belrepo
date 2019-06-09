using Bel.DataLayer.Interfaces;
using Bel.DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public List<ReservationClassHour> GetUserByClasHourId(int municipalityClassId, DateTime dateTime)
        {
            var query = from ch in context.ClassHour
                        join res in context.Reservation on ch.Id equals res.RefClassHourId into reservations
                        where ch.Id == municipalityClassId
                        select new { Id = ch.Id, Name = ch.Name };

            var result = context.Database.SqlQuery<ReservationClassHour>(@"select ch.Id,ch.Hour from ClassHour ch 
                                                                            left join Reservation r on r.RefClassHourId=ch.Id and r.ReservationDate='" + dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + @"'
                                                                            where ch.RefMunicipalityClassId=" + municipalityClassId + " and r.Id is null").ToList();

            return result;
        }
    }
}
