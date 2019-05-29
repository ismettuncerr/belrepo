using Bel.DataLayer.Model;
using Bel.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bel.Models
{
    
    public class GuestActiveReservationViewModel
    {
        ReservationRepository reservationRepository = new ReservationRepository();
        public List<ReservationModel> Reservations { get; set; }

        public GuestActiveReservationViewModel(int refUserId)
        {
            Reservations = new List<ReservationModel>();
            Reservations = reservationRepository.GetGuestActiveReservations(refUserId);
        }
    }
}