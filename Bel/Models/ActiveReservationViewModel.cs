using Bel.DataLayer;
using Bel.DataLayer.Model;
using Bel.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bel.Models
{
    
    public class ActiveReservationViewModel : BaseClass
    {
        public List<ReservationModel> Reservations { get; set; }

        public ActiveReservationViewModel(int? refUserId = null)
        {
            Reservations = new List<ReservationModel>();
            Reservations = dataClient.ReservationRepository.GetActiveReservations(refUserId);
        }
    }
}