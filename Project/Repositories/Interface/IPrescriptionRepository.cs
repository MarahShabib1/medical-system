using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repositories.Interface
{
   public interface IPrescriptionRepository
    {
        void Create_Prescription(Prescription model);
    }
}
