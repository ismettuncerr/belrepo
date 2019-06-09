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
    public class HomeController : BaseController
    {
        SiteManagementViewModel siteManagementViewModel = new SiteManagementViewModel();
        NewsRepository newsRepository = new NewsRepository();
        //UserRepository userRepository = new UserRepository();
        public ActionResult Index()
        {
            var news = newsRepository.GetAll().OrderByDescending(x=> x.Id).Take(8).ToList();
            return View(news);
        }
        public ActionResult Contact()
        {
            siteManagementViewModel.contact = dataClient.ContactRepository.GetAll().FirstOrDefault();
            return View(siteManagementViewModel);
        }

        public ActionResult About()
        {
            siteManagementViewModel.about = dataClient.AboutRepository.GetAll().FirstOrDefault();
            ViewData["myInnerHtml"] = siteManagementViewModel.about.AboutDetail;
            return View(siteManagementViewModel);
        }
    }
}
