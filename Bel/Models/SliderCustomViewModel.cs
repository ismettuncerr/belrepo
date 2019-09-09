using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bel.Models
{
    public class SliderCustomViewModel
    {
        public int SortOrder { get; set; }
        public IEnumerable<HttpPostedFileBase> ResimDosya { get; set; }
    }
}