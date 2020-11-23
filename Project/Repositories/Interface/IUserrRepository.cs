using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repositories.Interface
{
  public  interface IUserrRepository
    {
        Task<User> Create_User(User model);
        Task<User> Update_User(User model);
        Task<User> Delete_User(User model);
        Task<bool> Check_User(string login_name, string email);
        Task<User> Check_User_ByEmail(string email);
        Task<User> Check_User_ByName(string login_name);
        Task<User> Check_User_ById(int id);
        public string Encrypte_User_Pass(string password);
        Task<object> Get_User_Patient_Info(int Userid);
        List<User> Get_All_User();

    }
}
