using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LepackOne.Extension.Models
{
    public class ReportRecord
    {
        public decimal Fice { get; set; }
        public string UniversityName { get; set; }
        public string Region { get; set; }
        public int Year { get; set; }
        public string Gender { get; set; }
        public string EthnicityGroup { get; set; }
        public int Enrollment { get; set; }
    }
}