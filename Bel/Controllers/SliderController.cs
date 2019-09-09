using Bel.DataLayer;
using Bel.DataLayer.Repository;
using Bel.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bel.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SliderController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            var sliders = new SliderViewModel();
            return View(sliders);
        }


        public ActionResult Slider()
        {
            var sliders = new SliderViewModel();
            return View(sliders);
        }

        public ActionResult DeleteSlider(int id)
        {
            var fileDetail = dataClient.SliderRepository.Get(id);
            var result = dataClient.SliderRepository.Delete(id);
            if (result)
            {
                var filePath = Server.MapPath($"~/Content/slider/" + fileDetail.PhotoName);
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);
                return RedirectToAction("Slider");
            }
            else
            {
                TempData["message"] = "Kayıt Silinemedi..";
                return RedirectToAction("Slider");
            }
        }

        public ActionResult SliderCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SliderCreate(SliderCustomViewModel sliderView)
        {

            if (sliderView.ResimDosya != null)
            {

                foreach (var item in sliderView.ResimDosya)//kaç adet resim seçildiyse, o kadar kez çalışacak
                {
                    string guid = Guid.NewGuid().ToString();
                    item.SaveAs(Server.MapPath($"~/Content/slider/{guid + item.FileName}"));//resim klasörüne resimleri kaydetme
                    Slider slider = new Slider();
                    slider.SortOrder = sliderView.SortOrder;
                    slider.PhotoName = guid + item.FileName;
                    dataClient.SliderRepository.Add(slider);
                    dataClient.SliderRepository.Save();
                    //resim.Adi = item.FileName;
                    //db.Resim.Add(resim);
                }

                //db.SaveChanges();//veri tabanına kayıt işlemi

            }
            return RedirectToAction("Slider", "Slider");
        }

    }
}
