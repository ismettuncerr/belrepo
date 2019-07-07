using Bel.DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bel.DataLayer.Repository
{
    public class GalleryRepository : GenericRepository<Gallery>, IGalleryRepository
    {
        private readonly beldatabaseEntities context;
        public GalleryRepository(beldatabaseEntities context)
        {
            this.context = context;
        }
    }
}
