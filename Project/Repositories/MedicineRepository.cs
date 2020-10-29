using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using Project.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repositories
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly DataContext _context;
        public MedicineRepository(DataContext context)
        {
            _context = context;
        }
        public  void Create_Medicine(string name, Company company)
        {
            var find_medicine =  _context.medicine.Where(o => o.Name == name).FirstOrDefault();
            if (find_medicine == null)
            {
                var medicine = new Medicine { Name = name, company = company };
                _context.medicine.Add(medicine);
            }
        }

        public async Task<List<Medicine>> Get_AllMedicines()
        {

            var Medicines= await _context.medicine.Include(o => o.company).ToListAsync();
            return Medicines;

        }

        public async Task<Medicine> Get_Medicine(string name)
        {
            var medicine = await _context.medicine.Where(o => o.Name == name).Include(o => o.company).FirstOrDefaultAsync();
            return medicine;
        }


        public async Task<Medicine> Update_Medicine(Medicine model)
        {
           
            var medicine = await _context.medicine.Where(o => o.id == model.id).FirstOrDefaultAsync();
            if (medicine != null)
            {
                _context.Entry(model).State = EntityState.Modified;
                return model;
            }
            else
            {
                return null;
            }
        }

      public  bool MedicineExists(int id)
        {
            var medicine = _context.medicine.Where(o => o.id == id).FirstOrDefault();
            if (medicine!= null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Delete_Medicine(int id)
        {
            var medicine = _context.medicine.Where(o => o.id == id).FirstOrDefault();
            if (medicine != null)
            {
                _context.medicine.Remove(medicine);
            }
          

        }


    }
}
