using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
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

       public async void SignIn(HttpContext httpContext, User user)
        {

            var claims = new List<Claim>()
              {
                 new Claim(ClaimTypes.Name,user.FirstName),
                  new Claim(ClaimTypes.Email,user.email),
              };
            var idn = new ClaimsIdentity(claims, "User Claim");
            var userPrinciple = new ClaimsPrincipal(idn);
           await httpContext.SignInAsync(userPrinciple);
        }


        public async void Signout(HttpContext httpContext)
        {
            await httpContext.SignOutAsync();
        }
    }
}
