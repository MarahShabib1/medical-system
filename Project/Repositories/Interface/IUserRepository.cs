using Microsoft.AspNetCore.Http;
using NPOI.SS.Formula.Functions;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repositories.Interface
{
   public interface IUserRepository
    {

         Task<User> Login(string login, string pass,string role);
        //void Logout();
        Task<User> CheckUser(string login_name);
        Task<User> CheckUser(int id);
        Task<Doctor> CheckDoctor(int id);
        Task<object> GetAllDoctors();
        Task<object> GetDoctorInfo(int id);
       
        string encrypte_pass(string pass);



    }
}
