using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repositories.Interface
{
  public  interface IEmployeeRepository
    {
        Task<Employee> Create_Employee(Employee model);
        Task<List<Employee>> Get_AllEmployees();
        Task<Employee> Get_Employee_Info(int id);
        bool EmployeeExists(int id);
        void Delete_Employee(int id);

    }
}
