using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repositories.Interface
{
  public  interface IRecordsRepository
    {
        Records Create_Record(Records model);
        Task<Records> Get_Record(int Recordid);
        
    }
}
