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
    public class UserController : Controller
    {
        UserRepository repository = new UserRepository();
        public ActionResult Index()
        {
            var userViewModel = new UserViewModel();
            return View(userViewModel);
        }
        [HttpPost]
        public ActionResult Index(string searchString)
        {
            var userViewModel = new UserViewModel();
            if (!string.IsNullOrEmpty(searchString))
                userViewModel.Users = userViewModel.Users.Where(x => x.Name.Contains(searchString)).ToList();
            return View(userViewModel);
        }
        public ActionResult EditUser(int id)
        {
            var userViewModel = new UserViewModel();
            userViewModel.User = repository.Get(id);
            return View(userViewModel);
        }
        [HttpPost]
        public ActionResult EditUser(User user)
        {
            repository.Edit(user);
            return RedirectToAction("Index");
        }

    }
}