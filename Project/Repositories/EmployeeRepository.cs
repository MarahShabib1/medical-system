using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using Project.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _context;
        public EmployeeRepository(DataContext context)
        {
            _context = context;
        }
        public async  Task<Employee> Create_Employee(Employee model)
        {
            var employee_exist = await _context.employee.Where(o => o.Userid == model.Userid).FirstOrDefaultAsync();
            if (employee_exist != null)
            {
                return null;
            }
            else
            {
                _context.employee.Add(model);
                return model;
            }
        }

      public async  Task<List<Employee>> Get_AllEmployees()
        {
            var employees = await _context.employee.Include(o => o.user).ToListAsync();
            return employees;

        }
       public async Task<Employee> Get_Employee_Info(int id)
        {
            var employee = await _context.employee.Where(o => o.id == id).Include(o => o.user).FirstOrDefaultAsync();
            return employee;
        }

        public bool EmployeeExists(int id)
        {
            var employee = _context.employee.Where(o => o.id == id).FirstOrDefault();
            if (employee != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Delete_Employee(int id)
        {
            var employee = _context.employee.Where(o => o.id == id).FirstOrDefault();
            if (employee != null)
            {
                _context.employee.Remove(employee);
            }


        }
    }
}
