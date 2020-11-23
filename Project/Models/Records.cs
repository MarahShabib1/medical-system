using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Records
    {
        public enum Status
        {
            Open,
            NewOffer,
            Approved,
            Denied
        }
        public int id { get; set; }
        public int Userid { get; set; }
        public int Doctorid { get; set; }
        public int? Employeeid { get; set; }
        public int? Prescriptionid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Case { get; set; }
        public string ExtraInfo { get; set; }
        public Status status { get; set; }
        public int ApprovedBy { get; set; }
        public User user { get; set; }
        public Doctor doctor { get; set; }
        public Employee employee { get; set; }
        public  Prescription prescription { get; set; }



    }
}
