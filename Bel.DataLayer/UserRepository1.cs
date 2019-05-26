using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bel.DataLayer
{
    public class UserRepository1
    {
        beldatabaseEntities beldatabaseEntities = new beldatabaseEntities();
        public List<User> GetUsers()
        {
            return beldatabaseEntities.User.ToList();
        }

        public User GetUserById(int id)
        {
            return beldatabaseEntities.User.FirstOrDefault(x=>x.Id==id);
        }
        public String GetUserRoleById(int id)
        {
            var query = from user in beldatabaseEntities.User
                        join userRole in beldatabaseEntities.UserRole on user.RefUserRoleId equals userRole.Id
                        where user.Id == id
                        select new { Role = userRole.Name }; 
            return query.FirstOrDefault().Role;
        }

    }
}
