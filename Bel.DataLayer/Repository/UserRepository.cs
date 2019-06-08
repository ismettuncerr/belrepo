using Bel.DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bel.DataLayer.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly beldatabaseEntities context;
        public UserRepository(beldatabaseEntities context)
        {
            this.context = context;
        }
        public void Edit(User user)
        {
            var result = context.Set<User>().Find(user.Id);
            result.Name = user.Name;
            result.EMail = user.EMail;
            result.Password = user.Password;
            context.SaveChanges();
        }
    }
}
