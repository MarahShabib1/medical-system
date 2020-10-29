using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Serialization;
using Project.Data;
using Project.Migrations;
using Project.Models;
using Project.Repositories.Interface;



namespace Project.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
       
        private readonly DataContext _context;
        private readonly IUserRepository userRep;
        private readonly IFileRepository fileRep;
        private readonly ISecurityManager securityManager;
     

        public AdminController(
            DataContext context , 
            IUserRepository UserRep ,
            ISecurityManager Securitymanager, 
            IFileRepository FileRep)
        {
             userRep = UserRep;
            securityManager = Securitymanager;
           _context = context;
            fileRep = FileRep;
        }

       // [Authorize(AuthenticationSchemes = "Admin")]
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value2" };
        }
       
        [HttpGet("index")]
        public ActionResult<IEnumerable<string>> index()
        {

            return new string[] { "logadmincont" };
        }

        [HttpGet("Messagelogin")]
        public ActionResult<IEnumerable<string>> Login_Message()
        {
            return new string[] { "You should login first :)" };
        }
      

        [HttpGet("login/{login}/{pass}")]   //Done
        public async Task<ActionResult<IEnumerable<string>>> login(string login, string pass)
        {
           var user = await userRep.Login(login, pass, "Admin");
           
            if (user !=null)
            {
            
                securityManager.SignIn(this.HttpContext, user);
            }
            else
            {
                return new string[] { "Login Failed" };
            }
         

            return new string[] {"Login Succ"};
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("logout")] //Done
        public ActionResult<IEnumerable<string>> logout()
        {
            securityManager.Signout(this.HttpContext);

            return new string[] { "Logout Succ"};
        }

      //  [Authorize(Roles = "Admin")]
        [HttpPost("User")] //Done
        public async Task<ActionResult<IEnumerable<string>>> Post(User model)
        {   
            try
            {
                var user = await userRep.CheckUser(model._Login);
               
                if (user == null)
                {
                    model.pwd = userRep.encrypte_pass(model.pwd);
                    var id = _context.user_role.Where(o => o.role.Name == "Doctor").FirstOrDefault(); // ta3deel
                    model.user_role.Add(new Models.UserRole { Userid = model.id, Roleid = id.Roleid });
                    _context.user1.Add(model);
                }
                else
                {
                    return BadRequest("Faild :_login name already exist" );
                }
             
                if (await _context.SaveChangesAsync() > 0)
                {
                    return Ok("Added successfully");
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest();
        } 


        [Authorize(Roles = "Admin")]
        [HttpPost("Doctor")] //Done
        public async Task< ActionResult<IEnumerable<string>>> Post(Doctor model)
        {   
            try
            {
                var user = await userRep.CheckUser(model.Userid);
                if (user == null)
                {
                    return NotFound($"Could not find user with Userid of {model.Userid}");
                }
                else
                {
                    var doctor = await userRep.CheckDoctor(model.Userid);
                    if (doctor == null)
                    {
                        _context.doctor.Add(model);
                    }
                    else
                    {
                        return BadRequest($"Failed : Doctor with Userid {model.Userid} is already exist");
                    }

                }
                
                if (await _context.SaveChangesAsync() > 0)
                {
                    return Ok("Doctor Added successfully");
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest();

        }

        [Authorize(Roles = "Admin")]
        [HttpGet("AllDoctors")] //Done
        public  async Task<IActionResult> GetDR()
        {
            try
            {
             
                if (_context.doctor.Count()==0)
                {
                    return NotFound($"There is no doctors in DB");
                }
                else
                {
                    var Get_All_Doctor = await userRep.GetAllDoctors();
                    return Ok(Get_All_Doctor);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get doctors");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("Doctor/{id}")] //Done
        public async Task<IActionResult> GetSDR(int id)
        {
            try
            {
               
                if (_context.doctor.Count() == 0)
                {
                    return NotFound($"There is no doctors in DB");
                }
                else
                {
                    var Get_Doctor = await userRep.GetDoctorInfo(id);
                    if (Get_Doctor == null)
                    {
                        return NotFound($"There is no doctors with Doctorid {id}");
                    }
                    return Ok(Get_Doctor);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get doctors");
            }
        }



        [Authorize(Roles = "Admin")]
        [HttpPut("Doctor/{id}")] //Done
        public async Task<ActionResult<IEnumerable<string>>> putdr([FromRoute] int id,Doctor model)
        { 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.id)
            {
                return BadRequest();
            }
      
           
            _context.Entry(model).State = EntityState.Modified;

            try
            {

                var doctor = await _context.doctor.Where(o => o.id == model.id && o.Userid == model.Userid).FirstOrDefaultAsync();
                if (doctor == null)
                {
                    return NotFound($"Could not find Doctor with Doctorid of {model.id} and Userid of {model.Userid} PS : You cant update Userid");
                }
                if (await _context.SaveChangesAsync() > 0)
                {
                    return Ok("Doctor Updated successfully");
                }
            }
            catch (Exception)
            {
              
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest();

        }

        [Authorize(Roles = "Admin")]
        [HttpPut("User/{id}")] //Done
        public async Task< ActionResult<IEnumerable<string>>> Put2(int id,User model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.id)
            {
                return BadRequest();
            }
            _context.Entry(model).State = EntityState.Modified;
            try
            {

               if( await _context.SaveChangesAsync() >0)
                    return Ok("User Updated successfully");
            }
            catch (Exception)
            {
                var user = await userRep.CheckUser(model.id);
                if (user == null)
                {
                    return NotFound($"Could not find user with Userid of {model.id}");
                }
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<string>>> delete(int id)
        {
            var find = await _context.doctor.FindAsync(id);
            if (find == null)
            {
                return NotFound($"Could not find Doctor with Doctorid of {id}");
            }
            else
            {    
                _context.doctor.Remove(find);
            } 
           if (await _context.SaveChangesAsync() > 0)
            {
                return Ok("Deleted Succesfully");
            }
            return BadRequest();          
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("import")] //Done
        public async Task<IActionResult> import(IFormFile file)
        {
            try
            {
                fileRep.CreateFile(file);
                var list = fileRep.read(file.FileName);
                  await  fileRep.Add(list);
                if (await _context.SaveChangesAsync() > 0)
                    return Ok(list);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("AllMedicine")] //Done
        public async Task<IActionResult> GetAllMed()
        {
            try
            {

                if (_context.medicine.Count() == 0)
                {
                    return NotFound($"There is no Medicines in DB");
                }
                else
                {
                    var query = await _context.medicine.Include(o => o.company).ToListAsync();
                    return Ok(query);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get Medicines");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("Medicine/{id}")] //Done
        public async Task<IActionResult> GetMed(int id)
        {
            try
            {

                if (_context.medicine.Count() == 0)
                {
                    return NotFound($"There is no Medicines in DB");
                }
                else
                {
                    var query = await _context.medicine.Where(o => o.id == id).Include(o => o.company).FirstOrDefaultAsync();
                    if (query != null) {
                        return Ok(query);
                    }
                    else
                    {
                        return NotFound($"Could not find Medicine with id of {id}");
                    }
                   
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get Medicines");
            }
        
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("AllCompanies")] //Done
        public async Task<IActionResult> GetAllCompanies()
        {
            try
            {

                if (_context.company.Count() == 0)
                {
                    return NotFound($"There is no Medicines in DB");
                }
                else
                {
                    var query = await _context.company.ToListAsync();
                    return Ok(query);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get Medicines");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Company/{id}")] //Done
        public async Task<IActionResult> Getcompany(int id)
        {
            try
            {

                if (_context.company.Count() == 0)
                {
                    return NotFound($"There is no Medicines in DB");
                }
                else
                {
                    var query = await _context.company.Where(o => o.id == id).FirstOrDefaultAsync();
                    if (query != null)
                    {
                        return Ok(query);
                    }
                    else
                    {
                        return NotFound($"Could not find Company with id of {id}");
                    }

                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get Companies");
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Medicine/{id}")] //Done
        public async Task<ActionResult<IEnumerable<string>>> PutMedicine(int id, Medicine model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.id)
            {
                return BadRequest();
            }
            _context.Entry(model).State = EntityState.Modified;
            try
            {

                if (await _context.SaveChangesAsync() > 0)
                    return Ok("Medicine Updated successfully");
            }
            catch (Exception)
            {
                var medicine = await _context.medicine.FindAsync(id);
                if (medicine == null)
                {
                    return NotFound($"Could not find medicine withid of {model.id}");
                }
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Company/{id}")] //Done
        public async Task<ActionResult<IEnumerable<string>>> Putcompany(int id, Company model)
        {    // on delete cascade when deleting comapny all its medicines will be deleted too.

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.id)
            {
                return BadRequest();
            }
            _context.Entry(model).State = EntityState.Modified;
            try
            {

                if (await _context.SaveChangesAsync() > 0)
                    return Ok("Company Updated successfully");
            }
            catch (Exception)
            {
                var Company = await _context.company.FindAsync(id);
                if (Company == null)
                {
                    return NotFound($"Could not find company with id of {model.id}");
                }
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Medicine/{id}")]
        public async Task<ActionResult<IEnumerable<string>>> delete_Medicine(int id)
        {

            var find = await _context.medicine.FindAsync(id);
            if (find == null)
            {
                return NotFound($"Could not find Medicine with id of {id}");
            }
            else
            {
                _context.medicine.Remove(find);
            }

            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok("Deleted Succesfully");
            }

            return BadRequest();

        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("company/{id}")]
        public async Task<ActionResult<IEnumerable<string>>> delete_Company(int id)
        {

            var find = await _context.company.FindAsync(id);
            if (find == null)
            {
                return NotFound($"Could not find Company with id of {id}");
            }
            else
            {
                _context.company.Remove(find);
            }

            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok("Deleted Succesfully");
            }

            return BadRequest();

        }


        [Authorize(Roles = "Admin")]
        [HttpGet("UserRole/{id}")] //Done
        public async Task<IActionResult> Get_UserRole(int id)
        {
            try
            {
                var find = await _context.user_role.Where(o => o.Userid == id).FirstOrDefaultAsync();
                if (find == null)
                {
                    return NotFound($"Could not find User-role for User with id of {id}");
                }
                else
                {
                    var roleu = await _context.user1.Where(o=>o.id==id).Select(o => new
                    {
                        user_role = o.user_role.Select(o => new
                        {
                            Roleid=o.role.id,
                            Name=o.role.Name
                        }
                        ).ToList()
              
                    }).FirstOrDefaultAsync();
            
                    return Ok(roleu);

                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get Companies");
            }

        }

    }
}
