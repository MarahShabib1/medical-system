using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Data;
using Project.Services.Interface;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly DataContext _context;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(DataContext context, IEmployeeService employeeService)
        {
            _employeeService = employeeService;
            _context = context;
        }


        [HttpPost("Appointmnet/{Recordid}/{status}")] //Done
        public async Task<ActionResult<IEnumerable<string>>> Dr_Appointmnet(int Recordid,string status)
        {
            try
            {   // the current employee id 
                _employeeService.Accept_Appointmnet(Recordid, status);

                if (await _context.SaveChangesAsync() > 0)
                {
                    return Ok("Records Updated");
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
