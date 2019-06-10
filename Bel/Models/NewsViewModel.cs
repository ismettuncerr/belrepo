using Bel.DataLayer;
using Bel.DataLayer.Model;
using Bel.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bel.Models
{
    public class NewsViewModel : BaseClass
    {
        public List<News> News { get; set; }
        public News New { get; set; }
        public NewsViewModel()
        {
            News = new List<News>();
            News = newsRepository.GetAll().OrderByDescending(x=>x.Id).ToList();
        }
    }
}