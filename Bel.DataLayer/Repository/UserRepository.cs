using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bel.DataLayer.Repository
{
    public class UserRepository : GenericRepository<User>
    {
        beldatabaseEntities dbContext = new beldatabaseEntities();
        public void Edit(User user)
        {
            var result = dbContext.Set<User>().Find(user.Id);
            result.Name = user.Name;
            result.EMail = user.EMail;
            result.Password = user.Password;
            dbContext.SaveChanges();
        }
    }
}
