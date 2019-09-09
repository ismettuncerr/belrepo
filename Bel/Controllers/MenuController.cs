using Bel.DataLayer;
using Bel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bel.Controllers
{
    public class MenuController : BaseController
    {
        // GET: Menu
        public ActionResult Index()
        {
            var menus = new MenuViewModel();
            return View(menus);
        }
        [AllowAnonymous]
        public ActionResult MenuDetail(int id)
        {
            return View(dataClient.MenuRepository.Get(id));
        }
        public ActionResult Menu()
        {
            if (TempData["message"] != null)
            {
                ViewBag.Message = TempData["message"].ToString();
            }
            var menus = new MenuViewModel();
            return View(menus);
        }

        [AllowAnonymous]
        public ActionResult EditMenu(int id)
        {
            var menu = dataClient.MenuRepository.Get(id);
            MenuCustomViewModel menuCustomViewModel = new MenuCustomViewModel();
            menuCustomViewModel.Detail = menu.Detail;
            menuCustomViewModel.Header = menu.Header;
            menuCustomViewModel.Id = menu.Id;
            menuCustomViewModel.Link = menu.Link;
            menuCustomViewModel.LinkType = menu.LinkType.Value;
            //menuCustomViewModel.Image = menu.Image;
            menuCustomViewModel.IsVisible = menu.IsVisible.Value;
            menuCustomViewModel.Row = menu.Row.Value;
            return View(menuCustomViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditMenu(MenuCustomViewModel menuCustomViewModel)
        {
            if (menuCustomViewModel.Image != null)
            {
                string guid = Guid.NewGuid().ToString();
                menuCustomViewModel.Image.SaveAs(Server.MapPath($"~/Content/menu/{guid + menuCustomViewModel.Image.FileName}"));//resim klasörüne resimleri kaydetme
                Menu menu = new Menu();
                menu.Id = menuCustomViewModel.Id;
                menu.Image = guid + menuCustomViewModel.Image.FileName;
                menu.IsVisible = menuCustomViewModel.IsVisible;
                menu.Header = menuCustomViewModel.Header;
                menu.Row = menuCustomViewModel.Row;
                menu.Detail = menuCustomViewModel.Detail;
                menu.Link = menuCustomViewModel.Link;
                menu.LinkType = menuCustomViewModel.LinkType;
                dataClient.MenuRepository.Edit(menu);
                return RedirectToAction("Menu");
            }
            else
            {
                Menu menu = new Menu();
                menu.Id = menuCustomViewModel.Id;
                menu.Image = dataClient.MenuRepository.Get(menuCustomViewModel.Id).Image;
                menu.Header = menuCustomViewModel.Header;
                menu.Row = menuCustomViewModel.Row;
                menu.IsVisible = menuCustomViewModel.IsVisible;
                menu.Detail = menuCustomViewModel.Detail;
                menu.Link = menuCustomViewModel.Link;
                menu.LinkType = menuCustomViewModel.LinkType;
                dataClient.MenuRepository.Edit(menu);
                return RedirectToAction("Menu");
            }
        }

        public ActionResult DeleteMenu(int id)
        {
            var fileDetail = dataClient.MenuRepository.Get(id);
            var result = dataClient.MenuRepository.Delete(id);
            if (result)
            {
                var filePath = Server.MapPath($"~/Content/menu/" + fileDetail.Image);
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);
                return RedirectToAction("Menu");
            }
            else
            {
                TempData["message"] = "Kayıt Silinemedi..";
                return RedirectToAction("Menu");
            }
        }

        public ActionResult MenuCreate()
        {
            var menuCustomViewModel = new MenuCustomViewModel();
            return View(menuCustomViewModel);
        }

        [HttpPost]
        public ActionResult MenuCreate(MenuCustomViewModel menuCustomViewModel)
        {
            if (menuCustomViewModel.Image != null)
            {
                string guid = Guid.NewGuid().ToString();
                menuCustomViewModel.Image.SaveAs(Server.MapPath($"~/Content/menu/{guid + menuCustomViewModel.Image.FileName}"));//resim klasörüne resimleri kaydetme
                Menu menu = new Menu();
                menu.Image = guid + menuCustomViewModel.Image.FileName;
                menu.IsVisible = menuCustomViewModel.IsVisible;
                menu.Header = menuCustomViewModel.Header;
                menu.Row = menuCustomViewModel.Row;
                menu.Detail = menuCustomViewModel.Detail;
                menu.Link = menuCustomViewModel.Link;
                menu.LinkType = menuCustomViewModel.LinkType;
                dataClient.MenuRepository.Add(menu);
                dataClient.MenuRepository.Save();
            }
            return RedirectToAction("Menu", "Menu");
        }

    }
}