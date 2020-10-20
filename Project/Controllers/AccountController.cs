using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Data;
using Project.Repositories.Interface;

namespace Project.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUserRepository userRep;
      
        private readonly ISecurityManager securityManager;


        public AccountController(
            DataContext context,
            IUserRepository UserRep,
            ISecurityManager Securitymanager
           )
        {
            userRep = UserRep;
            securityManager = Securitymanager;
            _context = context;   
        }


        [HttpGet("login/{login}/{pass}/{role}")]   
        public async Task<ActionResult<IEnumerable<string>>> login(string login, string pass ,string role)
        {
            var user = await userRep.Login(login, pass, role);

            if (user != null)
            {

                securityManager.SignIn(this.HttpContext, user, role);
            }
            else
            {
                return new string[] { "Login Failed" };
            }


            return new string[] { "Login Succ" };
        }



        [Authorize]
        [HttpGet("logout")]
        public ActionResult<IEnumerable<string>> logout()
        {
            securityManager.Signout(this.HttpContext);

            return new string[] { "Logout Succ" };
        }









    }
}
