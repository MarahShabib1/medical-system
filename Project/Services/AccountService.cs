using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using Project.Repositories.Interface;
using Project.Services.Interface;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Project.Services
{
    public class AccountService : IAccountService
    {

        private readonly IUserrRepository _userRep;
        private readonly DataContext _context;
      
        public AccountService(DataContext context,
            IUserrRepository userRep
            
            )
        {
            _userRep = userRep;
            _context = context;
        }

        public async Task<User> Login(string login, string pass)
        {
            string encode1 = _userRep.Encrypte_User_Pass(pass);

            var user = await _context.user1.Where(c => c._Login == login && c.pwd == encode1).FirstOrDefaultAsync();

            if (user == null)
            {
                return null;
            }
            var userCheck = await _context.user1.Where(c => c._Login == login && c.pwd == encode1)
                .Include(one => one.user_role)
                    .ThenInclude(team => team.role).FirstOrDefaultAsync(o => o.id == user.id);
            // var b = userCheck.user_role.Find(o => o.Roleid == 1); //zabtt
            /*    var RoleCheck = userCheck.user_role.Find(o => o.role.Name == role);//zabtt ll admin wl boss
                if (RoleCheck == null)
                {
                    return null;
                }*/
        

            return user;
        }

        public async void SignIn(HttpContext httpContext, User user)
        {

            var claims = new List<Claim>()
              {
                 new Claim(ClaimTypes.Name,user.FirstName),
                  new Claim(ClaimTypes.Email,user.email),
                 // new Claim( ClaimTypes.Role,role),
              };

            var roles = _context.user_role.Where(o => o.Userid == user.id).ToList();
            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item.role.Name));
            }



            var idn = new ClaimsIdentity(claims, "User Claim");
            var userPrinciple = new ClaimsPrincipal(idn);
            await httpContext.SignInAsync("User_Cookie", userPrinciple);
        }


        public async void Signout(HttpContext httpContext)
        {
            await httpContext.SignOutAsync();
        }






    }
}
