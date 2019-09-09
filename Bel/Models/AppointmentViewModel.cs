using Bel.DataLayer;
using Bel.DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bel.Models
{
    public class AppointmentViewModel
    {
        public List<MunicipalityClass> municipalities { get; set; }
        public List<SchoolClassModel> schoolClasses { get; set; }
        public List<User> Users { get; set; }
    }
}