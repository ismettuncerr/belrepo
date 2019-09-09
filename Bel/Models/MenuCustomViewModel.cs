using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bel.Models
{
    public class MenuCustomViewModel
    {
        public int Id { get; set; }
        public bool IsVisible { get; set; }
        [AllowHtml]
        public string Header { get; set; }
        public string Link { get; set; }
        public int LinkType { get; set; }
        public int Row { get; set; }
        
        public HttpPostedFileBase Image { get; set; }
        [AllowHtml]
        public string Detail { get; set; }
        public List<SelectListItem> IsVisibles { get; set; }
        public List<SelectListItem> LinkTypes { get; set; }

        public MenuCustomViewModel()
        {
            IsVisibles = new List<SelectListItem>();
            IsVisibles.Add(new SelectListItem { Text = "True", Value = "true" });
            IsVisibles.Add(new SelectListItem { Text = "False", Value = "false" });
            LinkTypes = new List<SelectListItem>();
            LinkTypes.Add(new SelectListItem { Text = "İç Link", Value = "0" });
            LinkTypes.Add(new SelectListItem { Text = "Dış Link", Value = "1" });
        }
           

    }
}