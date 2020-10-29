using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Services.Interface
{
  public  interface IAdminService
    {

        Task<User> Create_User(User model);
        Task<Doctor> Create_Doctor(Doctor model);
        void Assign_DoctorRole(Doctor model);
        Task<object> Get_AllDoctors();
        Task<object> Get_Doctor_Info(int id);
        Task<Doctor> Update_Doctor(Doctor model);
        Task<Doctor> Delete_Doctor(int id);
        void import(IFormFile file);
        Task<List<Medicine>> Get_AllMedicines();
        Task<Medicine> Get_Medicine(string name);
        Company Get_Company(string name);
        Task<List<Company>> Get_AllCompanies();
       Company Update_Company(Company model);
        bool CompanyExists(int id);
        bool MedicineExists(int id);
            Task<Medicine> Update_Medicine(Medicine model);
        void Delete_Medicine(int id);
        void Delete_Company(int id);
        Task<object> Get_UserRole(int id);

    }
}
