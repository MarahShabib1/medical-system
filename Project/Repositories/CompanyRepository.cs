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
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DataContext _context;


        public CompanyRepository(DataContext context)
        {

            _context = context;
        }
        public  void Create_Company(string name)
        {

            var find_company =  _context.company.Where(o => o.Name == name).FirstOrDefault();
            if (find_company == null)
            {
                var company = new Company { Name = name };
                _context.company.Add(company);
                _context.SaveChanges();
            }
        }

      public   Company Get_Company(string name)
        {
            var find_company = _context.company.Where(o => o.Name == name).FirstOrDefault();
            return find_company;

        }

        public async Task<List<Company>> Get_AllCompanies()
        {
            var companies = await _context.company.ToListAsync();
            return companies;
        }

        public Company Update_Company(Company model)
        {
         
            var company = _context.company.FindAsync(model.id);
            if (company != null)
            {
             _context.Entry(model).State = EntityState.Modified;
                return model;
            }
            else
            {
                return null;
            }
        }

       public bool CompanyExists(int id)
        {
            var company = _context.company.Where(o => o.id == id).FirstOrDefault();
            if (company != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Delete_Company(int id)
        {
            var company = _context.company.Where(o => o.id == id).FirstOrDefault();
            if (company != null)
            {
                _context.company.Remove(company);
            }
           
        }


    }
}
