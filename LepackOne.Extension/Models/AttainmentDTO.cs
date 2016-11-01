using LINQtoCSV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LepackOne.Extension.Models
{
    public class AttainmentDTO
    {
        [CsvColumn(FieldIndex = 1, CanBeNull = false)]
        public int Year { get; set; }
        [CsvColumn(FieldIndex = 2, CanBeNull = false)]
        public string IsTarget { get; set; }
        [CsvColumn(FieldIndex = 3)]
        public string DegreeLevel { get; set; }
        [CsvColumn(FieldIndex = 4, Name = "Percentage(%)")]
        public float Percentage { get; set; }
        [CsvColumn(FieldIndex = 5)]
        public string Region { get; set; }
    }
}
