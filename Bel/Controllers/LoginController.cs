﻿using Bel.DataLayer;
using Bel.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Bel.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Login
        //[OutputCache(Duration = 1000)]
        public ActionResult Index()
        {
            FormsAuthentication.SignOut();
            ViewBag.Title = "Login";
            /*List<SelectListItem> users = new List<SelectListItem>();

            users.AddRange(userRepository.GetUsers().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }));*/
            
            return View(userList());
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            if (user.Id > 0)
            {
                var userControl = dataClient.UserRepository.Get(user.Id);
                if (userControl != null)
                {
                    if (userControl.Password == user.Password)
                    {
                        FormsAuthentication.SetAuthCookie(user.Id.ToString(),false);
                        if(dataClient.UserRoleRepository.GetUserRoleById(user.Id)=="Guest")
                            return RedirectToAction("Appointment", "Appointment");
                        else
                            return RedirectToAction("Index", "Site");
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

        [Authorize(Roles = "Guest")]
        public ActionResult Appointment()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        
        public List<User> userList()
        {
            return dataClient.UserRepository.GetAll().ToList();
        }

    }
}