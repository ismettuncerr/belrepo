using Bel.DataLayer;
using Bel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bel.Controllers
{
    public class SiteController : BaseController
    {
        // GET: Site

        [AllowAnonymous]
        public ActionResult Index()
        {
            SiteManagementViewModel siteManagementViewModel = new SiteManagementViewModel();
            siteManagementViewModel.about = dataClient.AboutRepository.GetAll().FirstOrDefault();
            siteManagementViewModel.contact = dataClient.ContactRepository.GetAll().FirstOrDefault();
            return View(siteManagementViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveAbout(About about)
        {
            dataClient.AboutRepository.Edit(about);
            return RedirectToAction("Index", "Site");
        }

        public ActionResult SaveContact(Contact contact)
        {
            dataClient.ContactRepository.Edit(contact);
            return RedirectToAction("Index", "Site");
        }
    }
}