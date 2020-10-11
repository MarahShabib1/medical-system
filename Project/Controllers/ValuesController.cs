using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Project.Data;

namespace Project.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IConfiguration _Configuration;
        private readonly DataContext _context;

        public ValuesController(IConfiguration configuration , DataContext context)
        {

            _Configuration = configuration;
            _context = context;
        }
        // GET api/values

        [Authorize(AuthenticationSchemes = "Management_Scheme")]
        //  [Authorize]
        [HttpGet("n")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value2" };
        }
    
        [HttpGet("index")]
        public ActionResult<IEnumerable<string>> index()
        {


            return new string[] { "neeeew" };
        }

        [Authorize(AuthenticationSchemes = "Admin")]
        //  [Authorize]
        [HttpGet("admin")]
        public ActionResult<IEnumerable<string>> admin()
        {


            return new string[] { "value admin" };
        }
        [HttpGet("login")]
        public ActionResult<IEnumerable<string>> login()
        {
            var claims = new List<Claim>()
            {
               new Claim(ClaimTypes.Name,"MM"),
                new Claim(ClaimTypes.Email,"MM@"),
            };
            var idn = new ClaimsIdentity(claims, "User Claim");
            var userPrinciple = new ClaimsPrincipal(new[] { idn });
            HttpContext.SignInAsync("Admin",userPrinciple);
            return RedirectToAction("index");

            //return new string[] { "not", "log22" };
        }

        [HttpGet("heey")]
        public IActionResult heey()
        {
            return RedirectToAction("index");

            //return new string[] { "not", "log22" };
        }

        [HttpGet("login1")]
        public ActionResult<IEnumerable<string>> login1()
        {
            var claims = new List<Claim>()
            {
               new Claim(ClaimTypes.Name,"MM"),
                new Claim(ClaimTypes.Email,"MM@"),
            };
            var idn = new ClaimsIdentity(claims, "User Claim");
            var userPrinciple = new ClaimsPrincipal(new[] { idn });
            HttpContext.SignInAsync("Management_Scheme", userPrinciple);
            return RedirectToAction("index1");

            //return new string[] { "not", "log22" };
        }

        [HttpGet("index1")]
        public ActionResult<IEnumerable<string>> index1()
        {


            return new string[] { "log111" };
        }


        [Authorize(AuthenticationSchemes = "Management_Scheme")]
    //  [Authorize]
        [HttpGet]
      /*  public ActionResult<IEnumerable<string>> Get()
        {

          string admin=  _Configuration.GetValue<string>("Admin:email");
            return new string[] { admin, "value2" };
        }

        public ActionResult<IEnumerable<string>> Get1()
        {

            string admin = _Configuration.GetValue<string>("Admin:email");
            return new string[] { admin, "value333" };
        }
        [HttpGet("login/{login}/{pass}")]
        public ActionResult<IEnumerable<string>> login(string login ,string pass)
        {
            var user = _context.user1.Where(c => c._Login == login && c.pwd == pass).ToList();
           var ss= user[0].FirstName;
            if(user.Count() ==0)
            {
                return new string[] { login, pass, "log" ,"Not Exist"};
            }

            var claims = new List<Claim>()
            {
               new Claim(ClaimTypes.Name,ss),
                new Claim(ClaimTypes.Email,user[0].email),
            };
            var idn = new ClaimsIdentity(claims, "User Claim");
            var userPrinciple = new ClaimsPrincipal(idn);
            HttpContext.SignInAsync(userPrinciple); 


            return new string[] { login,pass, "log","Done" };
        }
        [HttpGet("login")]
        public ActionResult<IEnumerable<string>> login()
        {
            var claims = new List<Claim>()
            {
               new Claim(ClaimTypes.Name,"MM"),
                new Claim(ClaimTypes.Email,"MM@"),
            };
            var idn = new ClaimsIdentity(claims, "User Claim");
            var userPrinciple = new ClaimsPrincipal(new[] { idn });
            HttpContext.SignInAsync(userPrinciple);
            return RedirectToAction("Get1");
            string admin = _Configuration.GetValue<string>("Admin:email");
            return new string[] { admin, "log22" };
            //return new string[] { "not", "log22" };
        }
        //    [HttpGet("login2")]
        public ActionResult<IEnumerable<string>> login2()
        {
            string admin = _Configuration.GetValue<string>("Admin:email");
            return new string[] { admin, "log22" };
           //return new string[] { "not", "log22" };
        }
        public ActionResult<IEnumerable<string>> logout()
        {
            return new string[] { "not", "log" };
        } */

        // GET api/values/5
        [HttpGet("{id}")]
      /*  public ActionResult<string> Get(int id)
        {
            return "value";
        }*/

        // POST api/values
        //[HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        //[HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        } 
    }
}
