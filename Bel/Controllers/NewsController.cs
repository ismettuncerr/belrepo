using Bel.DataLayer;
using Bel.DataLayer.Repository;
using Bel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bel.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NewsController : BaseController
    {
        // GET: News
        //NewsRepository newsRepository = new NewsRepository();
        [AllowAnonymous]
        public ActionResult Index()
        {
            var news = new NewsViewModel();
            return View(news);
        }
        [AllowAnonymous]
        public ActionResult NewsDetail(int id)
        {
            return View(dataClient.NewsRepository.Get(id));
        }

        public ActionResult News()
        {
            if (TempData["message"] != null)
            {
                ViewBag.Message = TempData["message"].ToString();
            }
            var news = new NewsViewModel();
            return View(news);
        }
        [AllowAnonymous]
        public ActionResult EditNew(int id)
        {
            var news = new NewsViewModel();
            news.New = dataClient.NewsRepository.Get(id);
            return View(news.New);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditNew(NewsCustomViewModel newsCustomViewModel)
        {
            if (newsCustomViewModel.NewsImage.FirstOrDefault() != null)
            {
                string guid = Guid.NewGuid().ToString();
                newsCustomViewModel.NewsImage.FirstOrDefault().SaveAs(Server.MapPath($"~/Content/news/{guid + newsCustomViewModel.NewsImage.FirstOrDefault().FileName}"));//resim klasörüne resimleri kaydetme
                News news = new News();
                news.Id = newsCustomViewModel.Id;
                news.NewsImage = guid + newsCustomViewModel.NewsImage.FirstOrDefault().FileName;
                news.NewsBrief = newsCustomViewModel.NewsBrief;
                news.NewsContent = newsCustomViewModel.NewsContent;
                news.NewsHeadline = newsCustomViewModel.NewsHeadline;
                news.NewsDate = DateTime.Now;
                dataClient.NewsRepository.Edit(news);
                return RedirectToAction("News");
            }
            else
            {
                News news = new News();
                news.Id = newsCustomViewModel.Id;
                news.NewsImage = dataClient.NewsRepository.Get(newsCustomViewModel.Id).NewsImage;
                news.NewsBrief = newsCustomViewModel.NewsBrief;
                news.NewsContent = newsCustomViewModel.NewsContent;
                news.NewsHeadline = newsCustomViewModel.NewsHeadline;
                news.NewsDate = DateTime.Now;
                dataClient.NewsRepository.Edit(news);
                return RedirectToAction("News");
            }
        }
        public ActionResult DeleteNew(int id)
        {
            var fileDetail = dataClient.NewsRepository.Get(id);
            var result = dataClient.NewsRepository.Delete(id);
            if (result)
            {
                var filePath = Server.MapPath($"~/Content/news/" + fileDetail.NewsImage);
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);
                return RedirectToAction("News");
            }
            else
            {
                TempData["message"] = "Kayıt Silinemedi..";
                return RedirectToAction("News");
            }
        }


        public ActionResult NewsCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewsCreate(NewsCustomViewModel newsCustomViewModel)
        {
            if (newsCustomViewModel.NewsImage != null)
            {

                foreach (var item in newsCustomViewModel.NewsImage)//kaç adet resim seçildiyse, o kadar kez çalışacak
                {
                    string guid = Guid.NewGuid().ToString();
                    item.SaveAs(Server.MapPath($"~/Content/news/{guid + item.FileName}"));//resim klasörüne resimleri kaydetme
                    News news = new News();
                    news.NewsImage = guid + item.FileName;
                    news.NewsBrief = newsCustomViewModel.NewsBrief;
                    news.NewsContent = newsCustomViewModel.NewsContent;
                    news.NewsHeadline = newsCustomViewModel.NewsHeadline;
                    news.NewsDate = DateTime.Now;
                    dataClient.NewsRepository.Add(news);
                    dataClient.NewsRepository.Save();
                }
            }
            return View();
        }
    }
}