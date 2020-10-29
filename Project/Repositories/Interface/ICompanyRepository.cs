using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repositories.Interface
{
   public interface ICompanyRepository
    {
        void Create_Company(string name);
        Company Get_Company(string name);
        Task<List<Company>> Get_AllCompanies();
        Company Update_Company(Company model);
        bool CompanyExists(int id);
        void Delete_Company(int id);
    }
}
