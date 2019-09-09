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
        //UserRepository userRepository = new UserRepository();
        public ActionResult Index()
        {
            var news = dataClient.NewsRepository.GetAll().OrderByDescending(x=> x.Id).Take(8).ToList();
            var slider = dataClient.SliderRepository.GetAll().OrderByDescending(x=> x.SortOrder).Take(3).ToList();
            siteManagementViewModel.home.news = dataClient.NewsRepository.GetAll().OrderByDescending(x => x.Id).Take(8).ToList();
            siteManagementViewModel.home.slider = dataClient.SliderRepository.GetAll().OrderByDescending(x => x.SortOrder).Take(3).ToList();
            return View(siteManagementViewModel);
        }
        public ActionResult Contact()
        {
            siteManagementViewModel.contact = dataClient.ContactRepository.GetAll().FirstOrDefault();
            return View(siteManagementViewModel);
        }

        public ActionResult About(int id)
        {
            var contentType = id;
            siteManagementViewModel.about = dataClient.AboutRepository.GetAll().Where(x=>x.ContentType==contentType).FirstOrDefault();
            ViewData["myInnerHtml"] = siteManagementViewModel.about.AboutDetail;
            return View(siteManagementViewModel);
        }
    }
}
