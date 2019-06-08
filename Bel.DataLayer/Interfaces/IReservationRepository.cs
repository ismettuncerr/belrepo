using Bel.DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bel.DataLayer.Interfaces
{
    public interface IReservationRepository : IGenericRepository<Reservation>
    {
        List<ReservationModel> GetReservations();
        List<ReservationModel> GetActiveReservations(int? refUserId);
        List<ReservationModel> GetPastReservations(int? refUserId);
        string saveReservation(Reservation reservation);
        string DeleteReservation(int id);
    }
}
