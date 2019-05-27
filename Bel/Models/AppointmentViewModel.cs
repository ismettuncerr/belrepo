using Bel.DataLayer;
using Bel.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bel.Models
{
    public class AppointmentViewModel
    {
        MunicipalityClassRepository municipalityClassRepository = new MunicipalityClassRepository();
        public List<MunicipalityClass> MunicipalityClass { get; set; }

        public List<SelectListItem> MunicipalityClasses { get; set; }

        public DateTime?  Date { get; set; }

        public int MunicipalityClassId { get; set; }

        public AppointmentViewModel()
        {
            MunicipalityClasses = new List<SelectListItem>();
            MunicipalityClass = municipalityClassRepository.GetAll().ToList();

            MunicipalityClasses.AddRange(MunicipalityClass.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }));
        }

    }
}