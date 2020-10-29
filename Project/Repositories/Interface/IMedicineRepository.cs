using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repositories.Interface
{
  public  interface IMedicineRepository
    {
        void Create_Medicine(string name, Company company);
        Task <List<Medicine>> Get_AllMedicines();
       Task< Medicine> Get_Medicine(string name);
        Task<Medicine> Update_Medicine(Medicine model);
        void Delete_Medicine(int id);

        bool MedicineExists(int id);

    }
}
