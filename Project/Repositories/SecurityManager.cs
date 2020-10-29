using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Project.Data;
using Project.Models;
using Project.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Project.Repositories
{
    public class SecurityManager: ISecurityManager
    {
        private readonly DataContext _context;
        public SecurityManager()
        {

        }
        public SecurityManager(DataContext context)
        {
            _context = context;
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
           await httpContext.SignInAsync("User_Cookie",userPrinciple);
        }


        public async void Signout(HttpContext httpContext)
        {
            await httpContext.SignOutAsync();
        }
    }
}
