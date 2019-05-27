using Bel.DataLayer;
using Bel.DataLayer.Repository;
using Bel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Bel.Controllers
{
    public class AppointmentController : Controller
    {
        // GET: Randevu
        MunicipalityClassRepository municipalityClassRepository = new MunicipalityClassRepository();
        ClassHourRepository classHourRepository = new ClassHourRepository();

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var reservations = new ReservationViewModel();
            return View(reservations);
        }
        [Authorize(Roles = "Guest")]
        public ActionResult Appointment()
        {
            return View(userList());
        }
        [HttpPost]
        public ActionResult Appointment(FormCollection collection)
        {
            AppointmentViewModel appointmentViewModel = new AppointmentViewModel();
            DateTime? date = string.IsNullOrEmpty(collection["Date"]) ? default(DateTime?) : DateTime.Parse(collection["Date"]);
            return View(appointmentViewModel);
        }
        public List<MunicipalityClass> userList()
        {
            return municipalityClassRepository.GetAll().ToList();
        }

        [HttpPost]
        public string GetDate(string municipalityClassId,DateTime date) {
            if (DateTime.Now.AddDays(15) < date)
            {
                return null;
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(classHourRepository.GetUserByClasHourId(Convert.ToInt32(municipalityClassId)));
            return json;
        }
    }
}