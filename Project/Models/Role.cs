using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Role
    {
        public Role()
        {
            user_role = new List<UserRole>();
        }
        public int id { get; set; }
        public string Name { get; set; }
        public List<UserRole> user_role { get; set; }
    }
}
