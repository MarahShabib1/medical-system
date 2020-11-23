using Project.Data;
using Project.Models;
using Project.Repositories;
using Project.Repositories.Interface;
using Project.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRecordsRepository _recordRep; 
        private readonly DataContext _context;



        public EmployeeService(DataContext context,
           IRecordsRepository recordRep 
           )
        {
            _recordRep = recordRep;
            _context = context;
        }


      public async void Accept_Appointmnet(int Recordid, string status)
        {
            var record =await _recordRep.Get_Record(Recordid);
           
            if (record != null)
            {
                if (status == "Approved")
                {
                    record.status = Records.Status.Approved;
                    
                }
                if (status == "Denied")
                {
                    record.status = Records.Status.Denied;
                }

            }
        }
    }
}
