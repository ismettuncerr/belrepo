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
    public class GalleryController : Controller
    {
        // GET: Gallery
        GalleryRepository galleryRepository = new GalleryRepository();


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
                    galleryRepository.Add(gallery);
                    galleryRepository.Save();
                    //resim.Adi = item.FileName;
                    //db.Resim.Add(resim);
                }

                //db.SaveChanges();//veri tabanına kayıt işlemi

            }
            return RedirectToAction("Gallery", "Gallery");
        }

    }
}
