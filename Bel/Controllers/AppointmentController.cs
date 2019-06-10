using Bel.DataLayer;
using Bel.DataLayer.Model;
using Bel.DataLayer.Repository;
using Bel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace Bel.Controllers
{
    public class AppointmentController : BaseController
    {
        // GET: Randevu
        //MunicipalityClassRepository municipalityClassRepository = new MunicipalityClassRepository();
        //SchoolClassRepository schoolClassRepository = new SchoolClassRepository();
        //ClassHourRepository classHourRepository = new ClassHourRepository();
        AppointmentViewModel appointmentViewModel = new AppointmentViewModel();
        //ReservationRepository reservationRepository = new ReservationRepository();



        public FormsAuthenticationTicket ticket()
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            return FormsAuthentication.Decrypt(authCookie.Value);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            //var reservations = new ReservationViewModel();
            //return View(reservations);
            return RedirectToAction("ActiveReservationForAdmin");
        }
        [Authorize(Roles = "Guest")]
        public ActionResult Appointment()
        {

            appointmentViewModel.municipalities = userList();
            appointmentViewModel.schoolClasses = schoolClass(Convert.ToInt32(ticket().Name));
            if (TempData["shortMessage"] != null)
            {
                ViewBag.Message = TempData["shortMessage"].ToString();
            }
            return View(appointmentViewModel);
        }

        public ActionResult DeleteReservation(int id)
        {
            var result = dataClient.ReservationRepository.DeleteReservation(id);
            return RedirectToAction("Index");
        }
        public ActionResult DetailReservation(int id)
        {
            StudentListsViewModel studentListsViewModel = new StudentListsViewModel();
            var studentJson = dataClient.ReservationRepository.Get(id).StudentsJson;
            if (studentJson != null)
            {
                var jsonSerialiser = new JavaScriptSerializer();
                studentListsViewModel.StudentList = jsonSerialiser.Deserialize<List<StudentListViewModel>>(studentJson);
            }
            else
            {
                ViewBag.StudentsList = "Öğrenci Listesi Boş..";
            }

            return View(studentListsViewModel);
        }


        public ActionResult ActiveAppointment()
        {
            var activeReservations = new ActiveReservationViewModel(Convert.ToInt32(ticket().Name));
            return View(activeReservations);
        }
        public ActionResult PastAppointment()
        {
            var pastReservations = new PastReservationViewModel(Convert.ToInt32(ticket().Name));
            return View(pastReservations);
        }
        public ActionResult ActiveReservationForAdmin()
        {
            var activeReservations = new ActiveReservationViewModel();
            return View(activeReservations);
        }
        public ActionResult PastReservationForAdmin()
        {
            var pastReservations = new PastReservationViewModel();
            return View(pastReservations);
        }



        /*[HttpPost]
        public ActionResult Appointment(FormCollection collection)
        {
            AppointmentViewModel appointmentViewModel = new AppointmentViewModel();
            DateTime? date = string.IsNullOrEmpty(collection["Date"]) ? default(DateTime?) : DateTime.Parse(collection["Date"]);
            return View(appointmentViewModel);
        }*/
        public List<MunicipalityClass> userList()
        {
            return dataClient.MunicipalityClassRepository.GetAll().ToList();
        }
        public List<SchoolClassModel> schoolClass(int refUserId)
        {
            return dataClient.SchoolClassRepository.GetSchoolClass(refUserId).ToList();
        }

        [HttpPost]
        public string GetDate(string municipalityClassId, DateTime date)
        {
            if (DateTime.Now.AddDays(15) < date)
            {
                return null;
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(dataClient.ClassHourRepository.GetUserByClasHourId(Convert.ToInt32(municipalityClassId)));
            return json;
        }
        [HttpPost]
        public ActionResult GetAppointment(ReservationCustomViewModel reservationCustomViewModel)
        {
            string studentJson = "";
            if (reservationCustomViewModel.studentList.FileName.EndsWith("xls") || reservationCustomViewModel.studentList.FileName.EndsWith("xlsx") || reservationCustomViewModel.studentList.FileName.EndsWith("XLS") || reservationCustomViewModel.studentList.FileName.EndsWith("XLSX"))
            {
                //Seçilen dosyanın nereye kaydedileceği belirtiliyor.
                string path = Server.MapPath("~/Content/" + reservationCustomViewModel.studentList.FileName);

                //Dosya kontrol edilir, varsa silinir.
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                //Excel path altına kaydedilir.
                reservationCustomViewModel.studentList.SaveAs(path);

                //+Exceli açıyoruz
                Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook workbook = application.Workbooks.Open(path);
                Microsoft.Office.Interop.Excel.Worksheet worksheet = workbook.ActiveSheet;
                Microsoft.Office.Interop.Excel.Range range = worksheet.UsedRange;
                //-

                //Verileri listeye atıp listele viewında göstereceğim, o yüzden modelimin
                //tipinde liste değişkeni tanımlıyorum.
                List<StudentListViewModel> localList = new List<StudentListViewModel>();

                //tüm verilerde dönüp ilgili veriyi ilgili modele atıyorum. sonrasında modeli
                //listeye atıyorum.
                for (int i = 1; i <= range.Rows.Count; i++)
                {
                    //ilk önce verileri modele ekleyeceğim bunun için
                    //model değişkenimi tanımlıyorum.
                    //Her seferinde yeni kayıtlarımı atacağım için for içinde tanımılıyorum.
                    //for dışında tanımlarsanız daima son aldığı değerleri listede gösterir.
                    StudentListViewModel lm = new StudentListViewModel();

                    lm.schooluNumber = ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 1]).Text;
                    lm.name = ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 2]).Text;
                    lm.surName = ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, 3]).Text;
                    if (lm.schooluNumber != "")
                    {
                        localList.Add(lm);
                    }
                }
                var jsonSerialiser = new JavaScriptSerializer();
                studentJson = jsonSerialiser.Serialize(localList);
                application.Quit();
            }
            Reservation reservation = new Reservation();
            reservation.AdvisorName = reservationCustomViewModel.consultant;
            reservation.IsActive = true;
            reservation.RefClassHourId = Convert.ToInt32(reservationCustomViewModel.hour);
            reservation.RefMunicipalityClassId = Convert.ToInt32(reservationCustomViewModel.municipalityClassId);
            reservation.StudentsJson = studentJson;
            reservation.RefUserId = Convert.ToInt32(ticket().Name);
            reservation.RefSchoolId = reservationCustomViewModel.schoolClassId;
            reservation.ReservationDate = DateTime.Parse(reservationCustomViewModel.datepicker);
            TempData["shortMessage"] = dataClient.ReservationRepository.saveReservation(reservation);
            return RedirectToAction("Appointment", "Appointment");

        }
    }
}