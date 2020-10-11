using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Employee
    {
        public int id { get; set; }
        public int Userid { get; set; }
        public int Doctorid { get; set; }
        public string Address { get; set; }
        public User user { get; set; }
        public Doctor doctor { get; set; }
    }
}
