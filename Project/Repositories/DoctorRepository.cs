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
    public class DoctorRepository : IDoctorRepository
    {
        private readonly DataContext _context;
     

        public DoctorRepository(DataContext context)
        {
         
           _context = context;
        }
        public async Task<Doctor> Create_Doctor(Doctor model)
        {
            var doctor_exist = await _context.doctor.Where(o => o.Userid == model.Userid).FirstOrDefaultAsync();
            if (doctor_exist !=null)
            {
                return null;
            }
            else
            {
                _context.doctor.Add(model);
                return model;
            }
        }

        public async Task<object> Get_AllDoctors()
        {
            var query = await _context.doctor.Select(o => new
            {
                o.id,
                o.Userid,
                o.Address1,
                o.Address2,
                o.user,
                user_role = o.user.user_role.Select(ot => ot.role.Name).ToList()
            }).ToListAsync();
            return query;
        }

        public async Task<object> Get_Doctor_Info(int id)
        {

            var query = await _context.doctor.Where(o => o.id == id).Select(o => new
            {
                o.id,
                o.Userid,
                o.Address1,
                o.Address2,
                o.user,
                user_role = o.user.user_role.Select(ot => ot.role.Name).ToList()
            }).FirstOrDefaultAsync();
            return query;
        }


        public  async Task<Doctor> Update_Doctor(Doctor model)
        {
            _context.Entry(model).State = EntityState.Modified;
            var doctor_exist =  await _context.doctor.Where(o => o.id == model.id && o.Userid==model.Userid).FirstOrDefaultAsync();
            if (doctor_exist != null)
                 {
                return model;
                 }
                 else
                 {
                     return null;
                 }
           
        }

        public async Task<Doctor> Delete_Doctor(int id)
        {
            var doctor_exist = await _context.doctor.FindAsync(id);
            if (doctor_exist != null){
                _context.doctor.Remove(doctor_exist);
            }
            return doctor_exist;
        }




    }
}
