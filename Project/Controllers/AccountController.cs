using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Data;
using Project.Repositories;
using Project.Repositories.Interface;
using Project.Services.Interface;

namespace Project.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
       // private ISecurityManager _securityManager = new SecurityManager();
        private readonly IAccountService _accountService;

        public AccountController(   IAccountService accountService
          )
        {
           _accountService = accountService;
        }


        [HttpGet("login/{login}/{pass}")]   
        public async Task<ActionResult<IEnumerable<string>>> login(string login, string pass)
        {
            var user = await _accountService.Login(login, pass);

            if (user != null)
            {
                _accountService.SignIn(this.HttpContext,user);
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
            _accountService.Signout(this.HttpContext);
            return new string[] { "Logout Succ" };
        }









    }
}
