using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace LepackOne.Extension.Models
{
    [TableName("LeRep_Attainment")]
    [PrimaryKey("Id", autoIncrement = true)]
    public class Attainment
    {
        public const string TableName = "LeRep_Attainment";

        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        public int Year { get; set; }
        public bool IsTarget { get; set; }
        public string DegreeLevel { get; set; }
        public decimal Percentage { get; set; }
        public string Region { get; set; }
    }
}
