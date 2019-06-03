using Bel.DataLayer;
using Bel.DataLayer.Model;
using Bel.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bel.Models
{
    public class UserViewModel
    {
        UserRepository userRepository = new UserRepository();
        public List<User> Users { get; set; }
        public User User { get; set; }

        public UserViewModel()
        {
            Users = new List<User>();
            Users = userRepository.GetAll().Where(x => x.RefUserRoleId == 2).ToList();
        }
    }
}