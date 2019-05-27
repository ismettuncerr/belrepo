﻿using Bel.DataLayer;
using Bel.DataLayer.Model;
using Bel.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bel.Models
{
    public class ReservationViewModel
    {
        ReservationRepository reservationRepository = new ReservationRepository();
        public List<ReservationModel> Reservations { get; set; }

        public ReservationViewModel()
        {
            Reservations = new List<ReservationModel>();
            Reservations = reservationRepository.GetReservations();
        }
    }
}