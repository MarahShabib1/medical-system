using Microsoft.AspNetCore.Http;
using NPOI.HSSF.Record;
using Project.Data;
using Project.Models;
using Project.Repositories.Interface;
using Project.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Project.Services
{
    public class PatientService : IPatientService
    {
        private readonly DataContext _context;
        private readonly IRecordsRepository _RecordsRep;
        private readonly IHttpContextAccessor _httpContextAccessor;
      
        public PatientService(DataContext context,
             IRecordsRepository RecordsRep,
             IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _RecordsRep = RecordsRep;
            _httpContextAccessor = httpContextAccessor;
        }
      public  void New_Appointmnet(int Doctorid, DateTime date ,int userid)
        {
          
              // UserRole { Userid = model.id, Roleid = DR_Roleid.id }
              Records new_record = new Records { Doctorid = Doctorid,Userid=userid, StartDate = date  ,status=Records.Status.Open};
              _RecordsRep.Create_Record(new_record);
        }
    }
}
