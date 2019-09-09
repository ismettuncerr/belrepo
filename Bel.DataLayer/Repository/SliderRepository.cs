using Bel.DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bel.DataLayer.Repository
{
    public class SliderRepository : GenericRepository<Slider>, ISliderRepository
    {
        private readonly beldatabaseEntities context;
        public SliderRepository(beldatabaseEntities context)
        {
            this.context = context;
        }
    }
}
