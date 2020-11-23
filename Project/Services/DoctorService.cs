using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.EntityFrameworkCore;
using NPOI.HSSF.Record.Chart;
using Project.Data;
using Project.Models;
using Project.Repositories.Interface;
using Project.Services.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IUserrRepository _userRep;
        private readonly IEmployeeRepository _employeeRep;
        private readonly IRoleRepository _RoleRep;
        private readonly IRecordsRepository _recordRep;
        private readonly IPrescriptionRepository _prescriptionRep;
        private readonly DataContext _context;
    


        public DoctorService(DataContext context,
            IUserrRepository userRep,
            IDoctorRepository drRep,
            IEmployeeRepository employeeRep,
            IRoleRepository RoleRep,
            IPrescriptionRepository prescriptionRep,
            IRecordsRepository  recordRep
           )
        {
            _userRep = userRep;
           _employeeRep = employeeRep;
            _RoleRep = RoleRep;
            _prescriptionRep = prescriptionRep;
            _recordRep = recordRep;
           _context = context;
        }

        public async Task<Employee> Create_Employee(Employee model)
        {
            var user = await _context.user1.FindAsync(model.Userid);
            if (user != null)
            {
                var employee = await _employeeRep.Create_Employee(model);
                Assign_EmployeeRole(model);
                return employee;
            }
            return null;
        }
        public void Assign_EmployeeRole(Employee model)
        {
            var user = _context.user1.Where(o => o.id == model.Userid).FirstOrDefault();
            var Employee_Roleid = _context.roles.Where(o => o.Name == "Employee").FirstOrDefault();
            user.user_role.Add(new Models.UserRole { Userid = model.id, Roleid = Employee_Roleid.id });

        }


        public async Task<List<Employee>> Get_AllEmployees()
        {
            if (_context.employee.Count() == 0)
            {
                return null;
            }
            var employees = await _employeeRep.Get_AllEmployees();

            return employees;
        }
        public async Task<Employee> Get_Employee_Info(int id)
        {
            var employee = await _employeeRep.Get_Employee_Info(id);
            return employee;
        }

        public bool EmployeeExists(int id)
        {
            var medicine = _employeeRep.EmployeeExists(id);
            return medicine;
        }
        public void Delete_Employee(int id)
        {
             _employeeRep.Delete_Employee(id);
        }

    public  async  Task<bool> Assign_Roles(int id, string role)
        {
            var employee = await _employeeRep.Get_Employee_Info(id);
            if (employee != null)
            {
                var Role = await _RoleRep.Get_Role(role);
                if (Role != null)
                {
                    _context.user_role.Add(new Models.UserRole { Userid = employee.Userid, Roleid = Role.id });
                    return true;
                }
            }
            return false;

        }

       public void Create_Prescription(Prescription model)
        {
            _prescriptionRep.Create_Prescription(model);
        }

        public async Task<Records> Get_Patient_Record(int Recordid)
        {
           var record=await _recordRep.Get_Record(Recordid);
            return record;
        }

        public async Task<object> Get_Patient_Info(int Patientid)
        {
            var Info = await _userRep.Get_User_Patient_Info(Patientid);
            return Info;
        }


        public void Generate_File(string name ,int Currentid)
        {
           //a3dl el database..
            var filep = name + ".csv";
          //  var filepath = "test.csv";
            using (StreamWriter writer = new StreamWriter(new FileStream(filep,
            FileMode.Create, FileAccess.Write)))
            {
                 
                writer.WriteLine("Users :");
                writer.WriteLine("Userid,FisrtName,LastName,Email,PhoneNumber");
                var users = _userRep.Get_All_User();
                 foreach(var user in users)
                {
                    
                    var _in =  _context.records.Where(o => o.Userid == user.id && o.Doctorid == 6).FirstOrDefault();
                    if(_in != null)
                    {
                        writer.WriteLine(user.id+","+user.FirstName+","+user.LastName+","+user.Email+","+user.phonenumber);
                        
                    }
                 
                 
                }
              //  writer.WriteLine("sep=,");
                writer.WriteLine("End!");
            }
        }
    }
}
