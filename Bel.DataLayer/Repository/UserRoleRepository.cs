using Bel.DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bel.DataLayer.Repository
{
    public class UserRoleRepository : GenericRepository<UserRole>, IUserRoleRepository
    {
        private readonly beldatabaseEntities context;
        public UserRoleRepository(beldatabaseEntities context)
        {
            this.context = context;
        }
        public String GetUserRoleById(int id)
        {
            var query = from user in context.User
                        join userRole in context.UserRole on user.RefUserRoleId equals userRole.Id
                        where user.Id == id
                        select new { Role = userRole.Name };
            return query.FirstOrDefault().Role;
        }
    }
}
