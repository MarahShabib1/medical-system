using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Services.Interface
{
   public interface IDoctorService
    {
        Task<Employee> Create_Employee(Employee model);
        void Assign_EmployeeRole(Employee model);
        Task<List<Employee>> Get_AllEmployees();
        Task<Employee> Get_Employee_Info(int id);
        bool EmployeeExists(int id);
        void Delete_Employee(int id);

        Task<bool> Assign_Roles(int id, string role);
        void Create_Prescription(Prescription model);
        Task<Records> Get_Patient_Record(int Recordid);
        Task<object> Get_Patient_Info(int Patientid);
        void Generate_File(string name, int Currentid);





    }
}
