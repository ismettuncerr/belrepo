using Bel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bel.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            var userViewModel = new UserViewModel();
            return View(userViewModel);
        }
    }
}