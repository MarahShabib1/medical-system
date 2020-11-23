using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using Project.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;


        public RoleRepository(DataContext context)
        {

            _context = context;
        }
        public async Task<Role> Get_Role(string name)
        {
            var Role = await _context.roles.Where(o => o.Name == name).FirstOrDefaultAsync();
            return Role;
        }

        public async Task<int> Get_RoleId(string RoleName)
        {
            var Role= await _context.roles.Where(o => o.Name == RoleName).FirstOrDefaultAsync();
            return Role.id;
        }
    }
}
