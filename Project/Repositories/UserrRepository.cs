
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using Project.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repositories
{
    public class UserrRepository : IUserrRepository
    {
        
        private readonly DataContext _context;
        private readonly IConfiguration _Configuration;

        public UserrRepository(DataContext context, IConfiguration configuration)
        {
            _Configuration = configuration;
            _context = context;
        }

        public async Task<User> Create_User(User model)
        {
            var user_exist = await Check_User(model._Login, model.email);
            if (user_exist)
            {
                return null;
             
            }
            else
            {
                model.pwd =Encrypte_User_Pass(model.pwd);
                _context.user1.Add(model);
                return model;
            }
        }

        public async Task<User> Update_User(User model)
        {
            var user_exist = await _context.user1.FindAsync(model.id);
            if(user_exist != null)
            {
                _context.Entry(model).State = EntityState.Modified;
                return model;
            }
            else
            {
                return null;
            }
        }

        public async Task<User> Delete_User(User model)
        {
            var user_exist = await _context.user1.FindAsync(model.id);
            if (user_exist != null)
            {
                _context.user1.Remove(user_exist);
                return model;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> Check_User(string login_name, string email)
        {

            var name_exist = await _context.user1.Where(o => o._Login == login_name).FirstOrDefaultAsync();
            var email_exist = await _context.user1.Where(o => o.email == email).FirstOrDefaultAsync();
            if (name_exist == null && email_exist == null)
                return false;
            else
                return true;
        }


        public string Encrypte_User_Pass(string password)
        {
            string key = _Configuration.GetValue<string>("Secret:key");
            password += key;
            var passbyte = Encoding.UTF8.GetBytes(password);
            string encode1 = Convert.ToBase64String(passbyte);
            return encode1;
        }
    }
}
