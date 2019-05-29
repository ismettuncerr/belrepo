using Bel.DataLayer.Model;
using Bel.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bel.Models
{
    public class GuestPastReservationViewModel
    {
        ReservationRepository reservationRepository = new ReservationRepository();
        public List<ReservationModel> Reservations { get; set; }

        public GuestPastReservationViewModel(int refUserId)
        {
            Reservations = new List<ReservationModel>();
            Reservations = reservationRepository.GetGuestPastReservations(refUserId);
        }
    }
}