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
            var result = context.Set<About>().FirstOrDefault();
            result.AboutDetail = about.AboutDetail;
            context.SaveChanges();
        }
    }
}
