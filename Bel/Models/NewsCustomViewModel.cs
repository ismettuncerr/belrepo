using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bel.Models
{
    public class NewsCustomViewModel
    {
        public string NewsHeadline { get; set; }
        public IEnumerable<HttpPostedFileBase> NewsImage { get; set; }
        [AllowHtml]
        public string NewsBrief { get; set; }
        [AllowHtml]
        public string NewsContent { get; set; }
    }
}