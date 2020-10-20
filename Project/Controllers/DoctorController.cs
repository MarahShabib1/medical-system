using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using Project.Repositories.Interface;

namespace Project.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUserRepository userRep;
        private readonly IFileRepository fileRep;
        private readonly ISecurityManager securityManager;

        public DoctorController(
            DataContext context,
            IUserRepository UserRep,
            ISecurityManager Securitymanager,
            IFileRepository FileRep)
        {
            userRep = UserRep;
            securityManager = Securitymanager;
            _context = context;
            fileRep = FileRep;
        }


        [Authorize(Roles = "Doctor")]
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Doctor Page" };
        }

        [HttpGet("Messagelogin")]
        public ActionResult<IEnumerable<string>> Login_Message()
        {
            return new string[] { "You should login first :)" };
        }

        //  [Authorize(Roles = "Doctor")]
        [HttpPost("User")] //Done
        public async Task<ActionResult<IEnumerable<string>>> Post(User model)
        {
            try
            {
                var user = await userRep.CheckUser(model._Login);

                if (user == null)
                {
                    model.pwd = userRep.encrypte_pass(model.pwd);
                    var id = await _context.roles.Where(o => o.Name == "Employee").FirstOrDefaultAsync(); // ta3deel
                    model.user_role.Add(new Models.UserRole { Userid = model.id, Roleid = id.id });
                    _context.user1.Add(model);
                }
                else
                {
                    return BadRequest("Faild :_login name already exist");
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


        //[Authorize(Roles = "Doctor")]
        [HttpPost("Employee")] //Done
        public async Task<ActionResult<IEnumerable<string>>> Post(Employee model)
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
                    var doctor = await _context.employee.Where(o => o.Userid == model.Userid).FirstOrDefaultAsync();
                    if (doctor == null)
                    {
                        _context.employee.Add(model);
                    }
                    else
                    {
                        return BadRequest($"Failed : Employee with Userid {model.Userid} is already exist");
                    }

                }

                if (await _context.SaveChangesAsync() > 0)
                {
                    return Ok("Employee Added successfully");
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest();

        }


        // [Authorize(Roles = "Doctor")]
        [HttpGet("AllEmployees")] //Done
        public async Task<IActionResult> Get_Employees()
        {
            try
            {

                if (_context.employee.Count() == 0)
                {
                    return NotFound($"There is no Employee in DB");
                }
                else
                {
                    var Get_All_Employee = await _context.employee.Include(o => o.doctor).Include(y => y.user).ToListAsync();
                    return Ok(Get_All_Employee);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get Employees");
            }
        }


        // [Authorize(Roles = "Doctor")]
        [HttpGet("Employee/{id}")] //Done
        public async Task<IActionResult> Get_Employee(int id)
        {
            try
            {

                if (_context.employee.Count() == 0)
                {
                    return NotFound($"There is no Employees in DB");
                }
                else
                {
                    var Get_Employee = await _context.employee.Where(p => p.id == id).Include(o => o.doctor).Include(y => y.user).FirstOrDefaultAsync();
                    if (Get_Employee == null)
                    {
                        return NotFound($"There is no Employee with EmployeeId {id}");
                    }
                    return Ok(Get_Employee);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get Employee");
            }
        }


       // [Authorize(Roles = "Doctor")]
        [HttpPut("Employee/{id}")] //Done
        public async Task<ActionResult<IEnumerable<string>>> putdr([FromRoute] int id, Employee model)
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
                var doctor = await _context.employee.Where(o => o.id == model.id).FirstOrDefaultAsync();
                if (doctor == null)
                {
                    return NotFound($"Could not find Employee with EmployeeId of {model.id}");
                }
                if (await _context.SaveChangesAsync() > 0)
                {
                    return Ok("Employee Updated successfully");
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest();
        }


        //[Authorize(Roles = "Doctor")]
        [HttpDelete("Employee/{id}")]
        public async Task<ActionResult<IEnumerable<string>>> Delete_Employee(int id)
        {

            var find = await _context.employee.FindAsync(id);
            if (find == null)
            {
                return NotFound($"Could not find Employee with EmployeeId of {id}");
            }
            else
            {
              var rolee=  _context.user_role.Where(o => o.Userid == find.Userid && o.Roleid == 5).FirstOrDefault(); //t3deel
                _context.user_role.Remove(rolee);
                _context.employee.Remove(find);
            }

            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok("Deleted Succesfully");
            }

            return BadRequest();
        }



        //[Authorize(Roles = "Doctor")]
        [HttpPost("Employee/{id}/{role}")] //Done
        public async Task<ActionResult<IEnumerable<string>>> Assign_Role(int id , string role)
        {
            try
            {
                var find = await _context.employee.FindAsync(id);
                if (find == null)
                {
                    return NotFound($"Could not find Employee with EmployeeId of {id}");
                }
                else
                {
                    // _context.user_role.Add(new Models.UserRole { Userid = model.id, Roleid = id.Roleid });
                    var RoleId1 = await _context.roles.Where(o => o.Name == role).FirstOrDefaultAsync();
                    //   find.user.user_role.Add(new Models.UserRole { Userid = find.Userid, Roleid = RoleId.id}); 
                    
                    _context.user_role.Add(new Models.UserRole { Userid = find.Userid, Roleid = RoleId1.id });
                }

                if (await _context.SaveChangesAsync() > 0)
                {
                    return Ok("Assigned Succesfully");
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest();
        }






    }
}
