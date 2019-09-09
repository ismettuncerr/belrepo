using Bel.DataLayer;
using Bel.DataLayer.Model;
using Bel.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bel.Models
{
    public class MenuViewModel : BaseClass
    {
        public List<Menu> Menus { get; set; }
        public Menu Menu { get; set; }
        public MenuViewModel()
        {
            Menus = new List<Menu>();
            Menus = dataClient.MenuRepository.GetAll().OrderByDescending(x => x.Id).ToList();
        }

    }
}