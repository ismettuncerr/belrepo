﻿using Bel.DataLayer.Interfaces;
using Bel.DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bel.DataLayer.Repository
{
    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
    {
        private readonly beldatabaseEntities context;
        public ReservationRepository(beldatabaseEntities context)
        {
            this.context = context;
        }
        public List<ReservationModel> GetReservations()
        {
            var query = from r in context.Reservation
                        join u in context.User on r.RefUserId equals u.Id
                        join mc in context.MunicipalityClass on r.RefMunicipalityClassId equals mc.Id
                        join sc in context.SchoolClass on r.RefSchoolId equals sc.Id
                        join ch in context.ClassHour on r.RefClassHourId equals ch.Id
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

        public List<ReservationModel> GetActiveReservations(int? refUserId)
        {
            var query = from r in context.Reservation
                        join u in context.User on r.RefUserId equals u.Id
                        join mc in context.MunicipalityClass on r.RefMunicipalityClassId equals mc.Id
                        join sc in context.SchoolClass on r.RefSchoolId equals sc.Id
                        join ch in context.ClassHour on r.RefClassHourId equals ch.Id
                        where r.IsActive == true && r.ReservationDate >= DateTime.Now
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
            var activeReservations = query.ToList();
            if (refUserId != null)
                return activeReservations.Where(x => x.RefUserId == refUserId).ToList();
            else
                return activeReservations;
        }

        public List<ReservationModel> GetPastReservations(int? refUserId)
        {
            var query = from r in context.Reservation
                        join u in context.User on r.RefUserId equals u.Id
                        join mc in context.MunicipalityClass on r.RefMunicipalityClassId equals mc.Id
                        join sc in context.SchoolClass on r.RefSchoolId equals sc.Id
                        join ch in context.ClassHour on r.RefClassHourId equals ch.Id
                        where r.IsActive == true && r.ReservationDate < DateTime.Now
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
            var pastReservations = query.ToList();
            if (refUserId != null)
                return pastReservations.Where(x => x.RefUserId == refUserId).ToList();
            else
                return pastReservations;
        }

        public string saveReservation(Reservation reservation)
        {
            var query = from res in context.Reservation
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

            Reservation userDateControlQuery = context.Reservation.Where(u => u.RefUserId == reservation.RefUserId).OrderByDescending(x => x.Id).FirstOrDefault();
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
            Add(reservation);
            Save();
            return "Kayıt Başarılı";
        }

        public string DeleteReservation(int id)
        {
            var entity = Get(id);
            if (entity is null)
                return "Kayıt Bulunamadı!";
            else
            {
                entity.IsActive = false;
                Save();
                return "Kayıt Silindi!";
            }
        }
    }
}