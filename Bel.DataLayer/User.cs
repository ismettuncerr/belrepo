
namespace Bel.DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public Nullable<int> RefUserRoleId { get; set; }
    }
}
