using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1;
using Project.Data;
using Project.Models;
using Project.Repositories.Interface;
using Project.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUserrRepository _userRep;
        private readonly IDoctorRepository _drRep;
        private readonly DataContext _context;
        private readonly ICompanyRepository _companyRep;
        private readonly IMedicineRepository _medicineRep;
        private readonly IFileRepository _fileRep;


        public AdminService(DataContext context,
            IUserrRepository userRep,
            IDoctorRepository drRep,
            ICompanyRepository companyRep,
            IMedicineRepository medicineRep,
            IFileRepository fileRep)
        {
            _userRep = userRep;
            _drRep = drRep;
            _companyRep = companyRep;
            _medicineRep = medicineRep;
            _fileRep = fileRep;
            _context = context;
        }
        public async Task<User> Create_User(User model)
        {
            var user = await _userRep.Create_User(model);
            return user;

        }

        public async Task<Doctor> Create_Doctor(Doctor model)
        {
            var user = await _context.user1.FindAsync(model.Userid);
            if (user != null)
            {
                var doctor = await _drRep.Create_Doctor(model);
                Assign_DoctorRole(model);
                return doctor;
            }
            return null;
        }

        public void Assign_DoctorRole(Doctor model)
        {
            var user = _context.user1.Where(o => o.id == model.Userid).FirstOrDefault();
            var DR_Roleid = _context.roles.Where(o => o.Name == "Doctor").FirstOrDefault();
            user.user_role.Add(new Models.UserRole { Userid = model.id, Roleid = DR_Roleid.id });
        }

        public async Task<object> Get_AllDoctors()
        {
            if (_context.doctor.Count() == 0)
            {
                return null;
            }
            var All_Doctors = await _drRep.Get_AllDoctors();

            return All_Doctors;
        }

        public async Task<object> Get_Doctor_Info(int id)
        {
            var doctor = await _drRep.Get_Doctor_Info(id);
            return doctor;
        }

        public async Task<Doctor> Update_Doctor(Doctor model)
        {
            var doctor = await _drRep.Update_Doctor(model);
            return doctor;

        }
        public async Task<Doctor> Delete_Doctor(int id)
        {
            var doctor = await _drRep.Delete_Doctor(id);
            return doctor;
        }

        public void import(IFormFile file)
        {
            _fileRep.CreateFile(file);
            var list = _fileRep.read(file.FileName);
            foreach (var icon in list)
            {
                _companyRep.Create_Company(icon.companyName);
                var company = _companyRep.Get_Company(icon.companyName);
                _medicineRep.Create_Medicine(icon.medicineName, company);
            }
        }


        public async Task<List<Medicine>> Get_AllMedicines()
        {
            var Medicines = await _medicineRep.Get_AllMedicines();
            return Medicines;
        }

        public async Task<Medicine> Get_Medicine(string name)
        {
            var medicine = await _medicineRep.Get_Medicine(name);
            return medicine;
        }

        public Company Get_Company(string name)
        {
            var find_company = _companyRep.Get_Company(name);
            return find_company;

        }


        public async Task<List<Company>> Get_AllCompanies()
        {
            var companies = await _companyRep.Get_AllCompanies();
            return companies;
        }

        public async Task<Medicine> Update_Medicine(Medicine model)
        {
            var medicine = await _medicineRep.Update_Medicine(model);
            return medicine;
        }
        public Company Update_Company(Company model)
        {
            var company = _companyRep.Update_Company(model);
            return company;
        }
        public bool CompanyExists(int id)
        {
            var company = _companyRep.CompanyExists(id);
            return company;
        }
        public bool MedicineExists(int id)
        {
            var medicine = _medicineRep.MedicineExists(id);
            return medicine;

        }
        public void Delete_Medicine(int id)
        {
            _medicineRep.Delete_Medicine(id);
        }
        public void Delete_Company(int id)
        {

            _companyRep.Delete_Company(id);
        }

        public async Task<object> Get_UserRole(int id)
        {
            var find = await _context.user_role.Where(o => o.Userid == id).FirstOrDefaultAsync();
            if (find != null)
            {
                var roleu = await _context.user1.Where(o => o.id == id).Select(o => new
                {
                    user_role = o.user_role.Select(o => new
                    {
                        Roleid = o.role.id,
                        Name = o.role.Name
                    }
                        ).ToList()
                }).FirstOrDefaultAsync();
                return roleu;
            }
            return null;
        }
    }
}
