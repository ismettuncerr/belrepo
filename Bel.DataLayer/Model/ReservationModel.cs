using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bel.DataLayer.Model
{
    public class ReservationModel
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string AdvisorName { get; set; }
        public DateTime? ReservationDate { get; set; }
        public int RefUserId { get; set; }
        public string UserName { get; set; }
        public int RefMunicipalityClassId { get; set; }
        public string MunicipalityClassName { get; set; }
        public int RefSchoolId { get; set; }
        public string SchoolName { get; set; }
        public int RefClassHourId { get; set; }
        public string Hour { get; set; }
    }
}
