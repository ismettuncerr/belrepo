using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bel.DataLayer
{
    public class UserRepository
    {
        beldatabaseEntities beldatabaseEntities = new beldatabaseEntities();
        public List<User> GetUsers()
        {
            return beldatabaseEntities.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return beldatabaseEntities.Users.FirstOrDefault(x=>x.Id==id);
        }

    }
}
