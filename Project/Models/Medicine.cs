using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Medicine
    {
        public int id { get; set; }
        public int Companyid { get; set; }
        public string Name { get; set; }
        public Company company { get; set; }
        
    }
}
