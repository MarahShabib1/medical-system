using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Prescription
    {
        public int id { get; set; }
        public string LabTest { get; set; }
        public string ExtraInfo { get; set; }
        public List<Medicine> medicines { get; set; }

    }
}
