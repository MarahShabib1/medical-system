using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Doctor
    {

        public int id { get; set; }
        public int Userid { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public User user { get; set; }
       
       
    }
}
