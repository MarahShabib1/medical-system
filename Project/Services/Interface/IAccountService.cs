using Microsoft.AspNetCore.Http;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Services.Interface
{
  public  interface IAccountService
    {
        Task<User> Login(string login, string pass);
        void SignIn(HttpContext httpContext, User user);
        void Signout(HttpContext httpContext);

    }
}
