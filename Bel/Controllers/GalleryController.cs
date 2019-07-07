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
    public class GalleryController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            var galleries = new GalleryViewModel();
            return View(galleries);
        }


        public ActionResult Gallery()
        {
            var galleries = new GalleryViewModel();
            return View(galleries);
        }
        public ActionResult DeleteGallery(int id)
        {
            var fileDetail = dataClient.GalleryRepository.Get(id);
            var result = dataClient.GalleryRepository.Delete(id);
            if (result)
            {
                var filePath = Server.MapPath($"~/Content/gallery/" + fileDetail.PhotoName);
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);
                return RedirectToAction("Gallery");
            }
            else
            {
                TempData["message"] = "Kayıt Silinemedi..";
                return RedirectToAction("Gallery");
            }
        }

        public ActionResult GalleryCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GalleryCreate(IEnumerable<HttpPostedFileBase> ResimDosya)
        {

            if (ResimDosya != null)
            {

                foreach (var item in ResimDosya)//kaç adet resim seçildiyse, o kadar kez çalışacak
                {
                    string guid = Guid.NewGuid().ToString();
                    item.SaveAs(Server.MapPath($"~/Content/gallery/{guid + item.FileName}"));//resim klasörüne resimleri kaydetme
                    Gallery gallery = new Gallery();
                    gallery.PhotoName = guid + item.FileName;
                    dataClient.GalleryRepository.Add(gallery);
                    dataClient.GalleryRepository.Save();
                    //resim.Adi = item.FileName;
                    //db.Resim.Add(resim);
                }

                //db.SaveChanges();//veri tabanına kayıt işlemi

            }
            return RedirectToAction("Gallery", "Gallery");
        }

    }
}
