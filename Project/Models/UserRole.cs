using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class UserRole
    {
        public int Userid { get; set; }
        public int Roleid { get; set; }

        public User user { get; set; }
        public Role role { get; set; }
      

    }
}
