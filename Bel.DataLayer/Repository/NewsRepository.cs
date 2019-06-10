using Bel.DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bel.DataLayer.Repository
{
    public class NewsRepository : GenericRepository<News>, INewsRepository
    {
        private readonly beldatabaseEntities context;
        public NewsRepository(beldatabaseEntities context)
        {
            this.context = context;
        }
        public void Edit(News news)
        {
            var result = context.Set<News>().Find(news.Id);
            result.NewsBrief = news.NewsBrief;
            result.NewsContent = news.NewsContent;
            result.NewsDate = news.NewsDate;
            result.NewsHeadline = news.NewsHeadline;
            result.NewsImage = news.NewsImage;
            context.SaveChanges();
        }
    }
}
