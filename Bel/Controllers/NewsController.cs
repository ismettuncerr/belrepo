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
    public class NewsController : Controller
    {
        // GET: News
        NewsRepository newsRepository = new NewsRepository();
        [AllowAnonymous]
        public ActionResult Index()
        {
            var news = new NewsViewModel();
            return View(news);
        }
        [AllowAnonymous]
        public ActionResult NewsDetail(int id)
        {            
            return View(newsRepository.Get(id));
        }

        public ActionResult News()
        {
            var news = new NewsViewModel();
            return View(news);
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
                    newsRepository.Add(news);
                    newsRepository.Save();
                }
            }
            return RedirectToAction("News", "News");
        }
    }
}