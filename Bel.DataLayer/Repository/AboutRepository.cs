using Bel.DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bel.DataLayer.Repository
{
    public class AboutRepository : GenericRepository<About>, IAboutRepository
    {
        private readonly beldatabaseEntities context;
        public AboutRepository(beldatabaseEntities context)
        {
            this.context = context;
        }
        public void Edit(About about)
        {
            var result = context.Set<About>().Where(x=>x.ContentType==about.ContentType).FirstOrDefault();
            result.AboutDetail = about.AboutDetail;
            result.Header = about.Header;
            context.SaveChanges();
        }
        public About GetContent(int ContentType)
        {
            var result = context.Set<About>().Where(x => x.ContentType == ContentType).FirstOrDefault();
            return result;
        }
    }
}
