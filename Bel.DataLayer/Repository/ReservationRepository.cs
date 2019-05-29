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
                        select new ReservationModel
                        {
                            Id = r.Id
                            ,
                            IsActive = r.IsActive.Value
                            ,
                            AdvisorName = r.AdvisorName
                            ,
                            ReservationDate = r.ReservationDate
                            ,
                            RefUserId = r.RefUserId.Value
                            ,
                            UserName = u.Name
                            ,
                            RefMunicipalityClassId = r.RefMunicipalityClassId.Value
                            ,
                            MunicipalityClassName = mc.Name
                            ,
                            RefSchoolId = r.RefSchoolId.Value
                            ,
                            SchoolClassName = sc.Name
                            ,
                            RefClassHourId = r.RefClassHourId.Value
                            ,
                            Hour = ch.Hour
                        };
            var a = query.ToList();
            return a;
        }

        public List<ReservationModel> GetGuestActiveReservations(int refUserId)
        {
            var query = from r in beldatabaseEntities.Reservation
                        join u in beldatabaseEntities.User on r.RefUserId equals u.Id
                        join mc in beldatabaseEntities.MunicipalityClass on r.RefMunicipalityClassId equals mc.Id
                        join sc in beldatabaseEntities.SchoolClass on r.RefSchoolId equals sc.Id
                        join ch in beldatabaseEntities.ClassHour on r.RefClassHourId equals ch.Id
                        where r.RefUserId == refUserId && r.IsActive == true && r.ReservationDate >= DateTime.Now
                        select new ReservationModel
                        {
                            Id = r.Id
                            ,
                            IsActive = r.IsActive.Value
                            ,
                            AdvisorName = r.AdvisorName
                            ,
                            ReservationDate = r.ReservationDate
                            ,
                            RefUserId = r.RefUserId.Value
                            ,
                            UserName = u.Name
                            ,
                            RefMunicipalityClassId = r.RefMunicipalityClassId.Value
                            ,
                            MunicipalityClassName = mc.Name
                            ,
                            RefSchoolId = r.RefSchoolId.Value
                            ,
                            SchoolClassName = sc.Name
                            ,
                            RefClassHourId = r.RefClassHourId.Value
                            ,
                            Hour = ch.Hour
                        };
            var a = query.ToList();
            return a;
        }

        public List<ReservationModel> GetGuestPastReservations(int refUserId)
        {
            var query = from r in beldatabaseEntities.Reservation
                        join u in beldatabaseEntities.User on r.RefUserId equals u.Id
                        join mc in beldatabaseEntities.MunicipalityClass on r.RefMunicipalityClassId equals mc.Id
                        join sc in beldatabaseEntities.SchoolClass on r.RefSchoolId equals sc.Id
                        join ch in beldatabaseEntities.ClassHour on r.RefClassHourId equals ch.Id
                        where r.RefUserId == refUserId && r.IsActive == true && r.ReservationDate < DateTime.Now
                        select new ReservationModel
                        {
                            Id = r.Id
                            ,
                            IsActive = r.IsActive.Value
                            ,
                            AdvisorName = r.AdvisorName
                            ,
                            ReservationDate = r.ReservationDate
                            ,
                            RefUserId = r.RefUserId.Value
                            ,
                            UserName = u.Name
                            ,
                            RefMunicipalityClassId = r.RefMunicipalityClassId.Value
                            ,
                            MunicipalityClassName = mc.Name
                            ,
                            RefSchoolId = r.RefSchoolId.Value
                            ,
                            SchoolClassName = sc.Name
                            ,
                            RefClassHourId = r.RefClassHourId.Value
                            ,
                            Hour = ch.Hour
                        };
            var a = query.ToList();
            return a;
        }

        public string saveReservation(Reservation reservation)
        {
            var query = from res in beldatabaseEntities.Reservation
                        where res.RefMunicipalityClassId == reservation.RefMunicipalityClassId
                                                             && res.ReservationDate == reservation.ReservationDate
                                                             && res.RefClassHourId == reservation.RefClassHourId
                                                             && res.IsActive == true
                        select new ReservationModel
                        {
                            Id = res.Id
                        };

            /*var dateControlquery = from res in beldatabaseEntities.Reservation
                        where res
                        select new ReservationModel
                        {
                            Id = res.Id
                        };*/

            var a = query.ToList();

            Reservation userDateControlQuery = beldatabaseEntities.Reservation.Where(u => u.RefUserId == reservation.RefUserId).OrderByDescending(x => x.Id).FirstOrDefault();
            if (userDateControlQuery.ReservationDate.Value.AddDays(15) > reservation.ReservationDate)
            {
                return "15 gün içerisinde tekrar randevu alamazsınız.";
            }
            if (a.Count() > 0)
            {
                return "Aynı gün ve saatte rezervasyon bulunmaktır.";
            }
            if (DateTime.Now.AddDays(15) < reservation.ReservationDate)
            {
                return "15 günden daha ileri bir tarih seçemezsiniz.";
            }
            ReservationRepository reservationRepository = new ReservationRepository();
            reservationRepository.Add(reservation);
            reservationRepository.Save();
            return "Kayıt Başarılı";
        }
    }
}