using Bel.DataLayer;
using System.Collections.Generic;
using System.Linq;

namespace Bel.Models
{
    public class UserViewModel : BaseClass
    {
        public List<User> Users { get; set; }
        public User User { get; set; }

        public UserViewModel()
        {
            Users = new List<User>();
            Users = dataClient.UserRepository.GetAll().Where(x => x.RefUserRoleId == 2).ToList();
        }
    }
}