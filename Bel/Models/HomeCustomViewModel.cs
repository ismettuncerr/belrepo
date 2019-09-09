using Bel.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bel.Models
{
    public class HomeCustomViewModel
    {
        public List<News> news { get; set; }
        public List<Slider> slider { get; set; }
    }
}