using Project.Data;
using Project.Models;
using Project.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repositories
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly DataContext _context;
        public PrescriptionRepository(DataContext context)
        {
            _context = context;
        }


      public  void Create_Prescription(Prescription model)
        {
            _context.prescription.Add(model);
        }
    }
}
