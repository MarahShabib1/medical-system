using FluentValidation;
using Project.Data;
using Project.Models;
using Project.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Validetors
{
    public class DoctorValidator :AbstractValidator<Doctor>
    {
        private readonly IUserrRepository _userRep;
        private readonly IDoctorRepository _doctorRep;
        public DoctorValidator(IUserrRepository userRep , IDoctorRepository doctorRep)
        {
            _userRep = userRep;
            _doctorRep = doctorRep;

            RuleFor(x => x.id).Null().Must(r =>
            {
                var result = Task.Run(async () => await _doctorRep.Get_Doctor_Byid(r)).Result;
                return result != null;
            }).WithMessage("This id is not Exist!");

            RuleFor(x => x.Userid).NotEmpty().WithMessage("Userid is required").NotNull().Must(r =>
            {
                var result = Task.Run(async () => await _userRep.Check_User_ById(r)).Result;
                return result != null;
            }).WithMessage("This Userid is not Exist!").Must(r =>
            {
                var result = Task.Run(async () => await _doctorRep.Get_Doctor_ByUserid(r)).Result;
                return result == null;
            }).WithMessage("This Userid is already a Doctor!");


            RuleFor(x => x.Address1).NotEmpty().WithMessage("Address1 is required").NotNull();
            RuleFor(x => x.Address2).NotEmpty().WithMessage("Address2 is required").NotNull();
        }

     


    }
}
