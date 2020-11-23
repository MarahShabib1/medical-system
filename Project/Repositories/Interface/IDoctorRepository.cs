using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repositories.Interface
{
  public  interface IDoctorRepository
    {

        Task<Doctor> Create_Doctor(Doctor model);  
        Task<object> Get_AllDoctors();
        Task<Doctor> Get_Doctor_ByUserid(int Userid);
        Task<object> Get_Doctor_Info(int id);
        Task<Doctor> Update_Doctor(Doctor model);
        Task<Doctor> Delete_Doctor(int id);
        Task<Doctor> Get_Doctor_Byid(int id);




    }
}
