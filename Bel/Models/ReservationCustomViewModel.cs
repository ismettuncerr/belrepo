using Bel.DataLayer;
using Bel.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bel.Models
{
    public class ReservationCustomViewModel
    {
        public int municipalityClassId { get; set; }
        public int schoolClassId { get; set; }
        public string datepicker { get; set; }
        public string hour { get; set; }
        public string consultant { get; set; }
        public HttpPostedFileBase studentList { get; set; }

    }
}