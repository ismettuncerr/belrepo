using Bel.DataLayer;
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
    public class AppointmentController : Controller
    {
        // GET: Randevu
        MunicipalityClassRepository municipalityClassRepository = new MunicipalityClassRepository();
        ClassHourRepository classHourRepository = new ClassHourRepository();

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
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
        [HttpPost]
        public ActionResult GetAppointment(AppointmentViewModel appointmentViewModel)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            string studentJson="";
            if (appointmentViewModel.studentList.FileName.EndsWith("xls") || appointmentViewModel.studentList.FileName.EndsWith("xlsx") || appointmentViewModel.studentList.FileName.EndsWith("XLS") || appointmentViewModel.studentList.FileName.EndsWith("XLSX"))
            {
                //Seçilen dosyanın nereye kaydedileceği belirtiliyor.
                string path = Server.MapPath("~/Content/" + appointmentViewModel.studentList.FileName);

                //Dosya kontrol edilir, varsa silinir.
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                //Excel path altına kaydedilir.
                appointmentViewModel.studentList.SaveAs(path);

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
                //listeyi bu sayfaya taşımak için bu viewBag içine alıyorum.
            }
            Reservation reservation = new Reservation();
            reservation.AdvisorName = appointmentViewModel.consultant;
            reservation.IsActive = true;
            reservation.RefClassHourId =Convert.ToInt32(appointmentViewModel.hour);
            reservation.RefMunicipalityClassId = Convert.ToInt32(appointmentViewModel.municipalityClassId);
            reservation.StudentsJson = studentJson;
            reservation.RefUserId =Convert.ToInt32(ticket.Name);
            reservation.RefSchoolId =Convert.ToInt32(ticket.Name);
            reservation.ReservationDate = new DateTime();
            reservation.ReservationDate = DateTime.Parse(appointmentViewModel.datepicker);
            ReservationRepository reservationRepository = new ReservationRepository();
            reservationRepository.Add(reservation);
            reservationRepository.Save();


            //listele viewına döndürüyorum.
            return RedirectToAction("Appointment", "Appointment");

        }
    }
}