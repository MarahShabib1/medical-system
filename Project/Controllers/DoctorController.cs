using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using Project.Services.Interface;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Doctor2Controller : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IDoctorService _doctorService;
        private readonly IAccountService _accountService;

        public Doctor2Controller(DataContext context, IDoctorService doctorService, IAccountService accountService)
        {
            _doctorService = doctorService;
            _context = context;
            _accountService = accountService;
        }

        [HttpPost("Employee")] //Done
        public async Task<ActionResult<IEnumerable<string>>> Post(Employee model)
        {
            try
            {
                var employee = await _doctorService.Create_Employee(model);
                if (employee == null)
                {
                    return BadRequest("Faild :employee is already exist or Userid not Correct");
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

        [HttpGet("AllEmployees")] //Done
        public async Task<IActionResult> Get_All_Employees()
        {
            try
            {
                var Get_All_Employees = await _doctorService.Get_AllEmployees();
                if (Get_All_Employees == null)
                {
                    return NotFound($"There is no employees in DB");
                }
                else
                {
                    return Ok(Get_All_Employees);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get doctors");
            }
        }

        [HttpGet("Employee/{id}")] //Done
        public async Task<IActionResult> Get_Employee(int id)
        {
            try
            {
                var Get_Employee = await _doctorService.Get_Employee_Info(id);
                if (Get_Employee == null)
                {
                    return NotFound($"There is no Employee with Employeeid {id}");
                }
                return Ok(Get_Employee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get doctors");
            }
        }

        [HttpPut("Employee/{id}")] //Done
        public async Task<ActionResult<IEnumerable<string>>> put_Medicine(int id, Employee model)
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
                {
                    return Ok("Employee Updated successfully");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(_doctorService.EmployeeExists(id)))
                {
                    return NotFound($"Could not find employee with id {model.id}");
                }

            }

            return BadRequest();
        }

        [HttpDelete("Employee/{id}")]
        public async Task<ActionResult<IEnumerable<string>>> delete_Medicine(int id)
        {
            if (!(_doctorService.EmployeeExists(id)))
            {
                return NotFound($"Could not find employee with id {id}");
            }
            else
            {
                _doctorService.Delete_Employee(id);
            }

            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok("Employee Deleted Succesfully");
            }

            return BadRequest();
        }


        [HttpPost("Employee/{id}/{role}")] //Done
        public async Task<ActionResult<IEnumerable<string>>> Assign_Role(int id, string role)
        {
            try
            {
                var Add = await _doctorService.Assign_Roles(id, role);
                if (!Add)
                {
                    return NotFound($"Error : Employee id or Role not Exist");
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

        [HttpPost("Prescription")] //Done
        public async Task<ActionResult<IEnumerable<string>>> Create_Prescription(Prescription model)
        {
            try
            {
                _doctorService.Create_Prescription(model);
              
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

        [HttpGet("PatientInfo/{id}")] //Done
        public async Task<IActionResult> Get_Patient_Info(int id)
        {
            try
            {
                var Patient_Info = await _doctorService.Get_Patient_Info(id);
                if (Patient_Info == null)
                {
                    return NotFound($"There is no Patient with Userid {id}");
                }
                return Ok(Patient_Info);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get Patient Info");
            }
        }

        [HttpGet("PatientRecord/{id}")] //Done
        public async Task<IActionResult> Get_Patient_Record(int id)
        {
            try
            {
                var Patient_Record = await _doctorService.Get_Patient_Record(id);
                if (Patient_Record == null)
                {
                    return NotFound($"There is no Patient with Userid {id}");
                }
                return Ok(Patient_Record);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get Patient Record");
            }
        }

        [HttpGet("Generate/{fileName}")] //Done
        public async Task<ActionResult<IEnumerable<string>>> Genereta_file(string fileName)
        {
            try
            {
                //get all users 
                // generate and add information to the file 
                //get the Doctorid from the cookie 
                //get the Doctorid from the Userid
                //try all in one function to ensure that its working
                //function will be in the Doctorservice. 

                //var Currentid = _accountService.Get_Current_Userid();
                _doctorService.Generate_File(fileName, 1);
                //Al_Student

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



    }
}
