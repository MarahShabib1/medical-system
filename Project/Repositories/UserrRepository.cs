
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
using NPOI.HSSF.UserModel;

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
          
                model.pwd =Encrypte_User_Pass(model.pwd);
                _context.user1.Add(model);
                await _context.SaveChangesAsync();
                return model;
            
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
            var email_exist = await _context.user1.Where(o => o.Email == email).FirstOrDefaultAsync();
            if (name_exist == null && email_exist == null)
                return false;
            else
                return true;
        }


        public async Task<User> Check_User_ByEmail(string email)
        {
            var user = await _context.user1.Where(o => o.Email == email).FirstOrDefaultAsync();
            return user;
        }
       public async Task<User> Check_User_ByName(string login_name)
        {
            var user= await _context.user1.Where(o => o._Login == login_name).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> Check_User_ById(int id)
        {
            var user_exist = await _context.user1.Where(o => o.id ==id).FirstOrDefaultAsync();
            return user_exist;
        }

        public string Encrypte_User_Pass(string password)
        {
            string key = _Configuration.GetValue<string>("Secret:key");
            password += key;
            var passbyte = Encoding.UTF8.GetBytes(password);
            string encode1 = Convert.ToBase64String(passbyte);
            return encode1;
        }


        public List<User> Get_All_User()
        {
            var users = _context.user1.ToList();
            return users;
        }


        public async Task<object> Get_User_Patient_Info(int Userid) // delete or not ? 
        {

            var query = await _context.user1.Select(o => new
            {
                o.id,
                o.FirstName,
                o.LastName,
                o.Email,
                o.phonenumber
            }).FirstOrDefaultAsync();
     //  query.
            return query;        
        }

        public async void gg()
        {
            var hh = await Get_User_Patient_Info(1);
          //  hh.
        }
   
     
    }
}
