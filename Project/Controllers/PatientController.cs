using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http2.HPack;
using Project.Data;
using Project.Services.Interface;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IPatientService _patientService;
        private readonly IAccountService _accountService;
        public PatientController(DataContext context, IPatientService patientService, IAccountService accountService)
        {
            _patientService = patientService;
            _context = context;
            _accountService = accountService;
        }

        [HttpGet("Reserve/{Doctorid}/{date}")] //Done when try it post
        public async Task<ActionResult<IEnumerable<string>>> Post(int Doctorid, DateTime date)
        { //make sure that doctor id avalable
            var userid = _accountService.Get_Current_Userid();

            _patientService.New_Appointmnet(Doctorid, date ,userid);

            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok("Record Added successfully");
            }
            return BadRequest("Baad");
        }
       



    }
}
