using Bel.DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bel.DataLayer.Repository
{
    public class MenuRepository : GenericRepository<Menu>, IMenuRepository
    {
        private readonly beldatabaseEntities context;
        public MenuRepository(beldatabaseEntities context)
        {
            this.context = context;
        }
        public void Edit(Menu menu)
        {
            var result = context.Set<Menu>().Find(menu.Id);
            result.Detail = menu.Detail;
            result.Header = menu.Header;
            result.Image = menu.Image;
            result.IsVisible = menu.IsVisible;
            result.Row = menu.Row;
            result.LinkType = menu.LinkType;
            result.Link = menu.Link;
            context.SaveChanges();
        }
    }
}
