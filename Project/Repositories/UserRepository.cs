using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using NPOI.SS.Formula.Functions;
using Project.Data;
using Project.Migrations;
using Project.Models;
using Project.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;

using System.Threading.Tasks;


namespace Project.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _Configuration;
     
        public UserRepository(DataContext context , IConfiguration configuration)
        {
            _Configuration = configuration;
            _context = context;
        } 

        public async Task<User> Login(string login, string pass, string role)
        {
           
            string encode1 = encrypte_pass(pass);

            var user = await _context.user1.Where(c => c._Login == login && c.pwd == encode1).FirstOrDefaultAsync();
           
            if (user == null)
            {
                return null;
            }

            var userCheck = await _context.user1.Where(c => c._Login == login && c.pwd == encode1)
                .Include(one => one.user_role)
                    .ThenInclude(team => team.role).FirstOrDefaultAsync(o => o.id == user.id);
            // var b = userCheck.user_role.Find(o => o.Roleid == 1); //zabtt
            var RoleCheck = userCheck.user_role.Find(o => o.role.Name == role);//zabtt ll admin wl boss
            if (RoleCheck == null)
            {
                return null;
            }

            return userCheck;
        }

     public async Task<User> CheckUser(string login_name)
        {
            var user = await _context.user1.Where(o => o._Login == login_name).FirstOrDefaultAsync();
            return user;

        }

        public async Task<User> CheckUser(int id)
        {
            var user = await _context.user1.Where(o => o.id == id).FirstOrDefaultAsync();
            return user;

        }
        public async Task<Doctor> CheckDoctor(int id)
        {
            var doctor = await _context.doctor.Where(o => o.Userid == id).FirstOrDefaultAsync();  
            return doctor;
        }
        public string encrypte_pass(string pass)
        {
           
            string key = _Configuration.GetValue<string>("Secret:key");
            pass += key;
            var passbyte = Encoding.UTF8.GetBytes(pass);
            string encode1 = Convert.ToBase64String(passbyte);
            return encode1;

        }
       public async Task<object> GetAllDoctors()
        {   //user with user role
          /*  var query1 = _context.user1.Where(t => t.id == 1).Select(o => new
            {
                // o.id,
                Role = o.user_role.Select(ot => ot.Roleid).ToList()
            }).ToList();*/
            var query = await _context.doctor.Select(o => new
            {
                o.id,
                o.Userid,
                o.Address1,
                o.Address2,
                o.user,
                user_role = o.user.user_role.Select(ot => ot.Roleid).ToList()
            }).ToListAsync();
            return query;
        }
       public async Task<object> GetDoctorInfo(int id)
        {
            var query = await _context.doctor.Where(o => o.id == id).Select(o => new
            {
                o.id,
                o.Userid,
                o.Address1,
                o.Address2,
                o.user,
                user_role = o.user.user_role.Select(ot => ot.Roleid).ToList()
            }).FirstOrDefaultAsync();
            return query;
        }
    }
}
