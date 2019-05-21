using Bel.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bel.Controllers
{
    public class LoginController : Controller
    {
        UserRepository userRepository = new UserRepository();
        // GET: Login
        public ActionResult Index()
        {
            ViewBag.Title = "Contact";
            /*List<SelectListItem> users = new List<SelectListItem>();

            users.AddRange(userRepository.GetUsers().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }));*/
            
            return View(userList());
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            if (user.Id > 0)
            {
                var userControl = userRepository.GetUserById(user.Id);
                if (userControl != null)
                {
                    if (userControl.Password == user.Password)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.PasswordControl = "Şifreniz Yanlış. Lütfen Tekrar Deneyin.";
                        return View(userList());
                    }
                }
            }
            ViewBag.PasswordControl= "Lütfen Okulunuzu Seçiniz.";
            return View(userList());
        }

        public List<User> userList()
        {
            List<User> users = new List<User>();
            return userRepository.GetUsers();
        }
    }
}