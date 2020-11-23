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
    public class UserValidator : AbstractValidator<User>
    {
        
        private readonly IUserrRepository _userRep;
        public  UserValidator(IUserrRepository userRep)
        {
           
            _userRep = userRep;

            RuleFor(x => x.FirstName).NotEmpty().WithMessage("FisrtName is required").NotNull().MaximumLength(15).WithMessage("Name Length cant be more than 15").Matches("^[a-zA-Z]*$").WithMessage("FirstName should only contain alphanumeric characters");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required").NotNull().Matches("^[a-zA-Z]*$").WithMessage("LastName should only contain alphanumeric characters");
           
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").NotNull().EmailAddress().Must(r => {
               var result = Task.Run(async () => await _userRep.Check_User_ByEmail(r)).Result;
                return result == null;
            }).WithMessage("This Email is already Exist!");

            RuleFor(x => x._Login).NotEmpty().WithMessage("Login_Name is required").NotNull().Must(r => {
                var result = Task.Run(async () => await _userRep.Check_User_ByName(r)).Result;
                return result == null;
            }).WithMessage("This Login_Name is already Exist!");

            RuleFor(x => x.phonenumber).NotEmpty().NotNull().WithMessage("Phone is required").Matches(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$").WithMessage("Not a valid phone number");
           
            RuleFor(x => x.pwd).NotEmpty().NotNull().WithMessage("Password is required").NotNull().MaximumLength(100).MinimumLength(8)
                .WithMessage("The password should at least contain 8 Charachters").Matches("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$").WithMessage("Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)");


        }
    }
}
