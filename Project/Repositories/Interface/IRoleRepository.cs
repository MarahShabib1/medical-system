using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repositories.Interface
{
   public interface IRoleRepository
    {
        Task<Role> Get_Role(string name);
        Task<int> Get_RoleId(string RoleName);
    }
}
