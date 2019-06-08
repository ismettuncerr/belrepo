using Bel.DataLayer;
using Bel.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bel.Controllers
{
    public class HomeController : BaseController
    {
        //UserRepository userRepository = new UserRepository();
        public ActionResult Index()
        {

            ViewBag.Title = "Home Page";
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Title = "Contact";

            /*List<SelectListItem> users = new List<SelectListItem>();

            users.AddRange(userRepository.GetUsers().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }));*/
            List<User> users = new List<User>();
            users = dataClient.UserRepository.GetAll().ToList();
            return View(users);
        }
    }
}
