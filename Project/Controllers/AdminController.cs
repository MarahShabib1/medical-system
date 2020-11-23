using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.DTOs;
using Project.Models;
using Project.Services.Interface;
using Project.Validetors;

namespace Project.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class Admin2Controller : ControllerBase
    {

        private readonly DataContext _context;
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;

        public Admin2Controller(  DataContext context,IAdminService adminService, IMapper mapper)
        {
            _adminService = adminService;
            _mapper = mapper;
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("User")] 
        public async Task Post(User model)
        {
            await _adminService.Create_User(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Doctor")] 
        public async Task Post(Doctor model)
        {
                await _adminService.Create_Doctor(model);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("AllDoctors")] 
        public async Task<IActionResult> Get_All_Doctor()
        {    
                var Get_All_Doctor = await _adminService.Get_AllDoctors(); 
                  return Ok(Get_All_Doctor);     
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Doctor/{id}")] 
        public async Task<IActionResult> Get_DR(int id)
        {
            
                var Get_Doctor = await _adminService.Get_Doctor_Info(id);
                    if (Get_Doctor == null)
                    {
                        return NotFound($"There is no doctors with Doctorid {id}");
                    }
                    return Ok(Get_Doctor);   
        }



        [Authorize(Roles = "Admin")] // doctor validation
        [HttpPut("Doctor/{id}")] //Done
        public async Task<ActionResult<IEnumerable<string>>> put_dr( int id, Doctor model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != model.id)
            {
                return BadRequest();
            }
            try
            {
                var doctor = await _adminService.Update_Doctor(model);

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
        [HttpDelete("Doctor/{id}")] // warning it its in the employee table !!
        public async Task<ActionResult<IEnumerable<string>>> delete_dr(int id)
        {
          
                var doctor = await _adminService.Delete_Doctor(id);
                if (doctor == null)
                {
                    return NotFound($"Could not find Doctor with Doctorid of {id}");
                }

                if (await _context.SaveChangesAsync() > 0)
                {
                    return Ok("Deleted Succesfully");
                }
            
            return BadRequest();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("import")]
        public IActionResult import(IFormFile file)
        {
            try
            {
                _adminService.import(file);

                if (_context.SaveChanges() > 0)
                {
                    return Ok("Added");
                }
                

            }   catch (Exception)
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
                var medicines = await _adminService.Get_AllMedicines();
                if (medicines == null)
                {
                    return BadRequest("No Medicines in the DB");
                }
                else
                {
                    return Ok(medicines);
                }
                    
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get Medicines");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Medicine/{name}")] //Done
        public async Task<IActionResult> GetMed(string name )
        {
            try
            {
                var medicine = await _adminService.Get_Medicine(name);
                if (medicine == null)
                {
                    return NotFound("No Medicine with this name in the the DB");
                }
                else
                {
                    return Ok(medicine);
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
                var companies = await _adminService.Get_AllCompanies();
                if (companies == null)
                {
                    return BadRequest("No companies in the DB");
                }
                else
                {
                    return Ok(companies);
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get companies");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Company/{name}")] //Done
        public IActionResult GetCompany(string name)
        {
            try
            {
                var company =  _adminService.Get_Company(name);
                if (company == null)
                {
                    return NotFound("No Medicine with this name in the the DB");
                }
                else
                {
                    return Ok(company);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get Medicines");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Medicine/{id}")] //Done
        public async Task<ActionResult<IEnumerable<string>>> put_Medicine(int id, Medicine model)
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
                    return Ok("Company Updated successfully");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(_adminService.MedicineExists(id)))
                {
                    return NotFound($"Could not find company with id {model.id}");
                }

            }

            return BadRequest();
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("Company/{id}")] //Done
        public async Task<ActionResult<IEnumerable<string>>> put_Company(int id, Company model)
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
                    return Ok("Company Updated successfully");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(_adminService.CompanyExists(id)))
                {
                    return NotFound($"Could not find company with id {model.id}");
                } 
            }
            return BadRequest();
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("Medicine/{id}")]
        public async Task<ActionResult<IEnumerable<string>>> delete_Medicine(int id)
        {
            if (!(_adminService.MedicineExists(id)))
            {
                return NotFound($"Could not find medicine with id {id}");
            }
            else
            {
                _adminService.Delete_Medicine(id);
            }

            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok("Medicine Deleted Succesfully");
            }

            return BadRequest();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Company/{id}")]
        public async Task<ActionResult<IEnumerable<string>>> delete_Company(int id)
        {
            if (!(_adminService.CompanyExists(id)))
            {
                return NotFound($"Could not find company with id {id}");
            }
            else
            {
                _adminService.Delete_Company(id);
            }

            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok("Company Deleted Succesfully");
            }
            return BadRequest();
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("UserRole/{id}")] //Done
        public async Task<IActionResult> Get_UserRole(int id)
        {
            var result = await _adminService.Get_UserRole(id);
            if (result == null)
            {
                return NotFound($"Could not find User-role for User with id of {id}");
            }
            return Ok(result);
        }
    }
}
