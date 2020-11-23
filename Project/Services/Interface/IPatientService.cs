using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Services.Interface
{
  public  interface IPatientService
    {

        void New_Appointmnet(int Doctorid, DateTime date,int userid);


    }
}
