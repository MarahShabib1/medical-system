using Microsoft.AspNetCore.Http;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repositories.Interface
{
  public  interface ISecurityManager
    {

         void SignIn(HttpContext httpContext, User user);
        void Signout(HttpContext httpContext);

    }
}
