using LepackOne.Extension.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LepackOne.Extension.Models
{
    public class AttainmentUploadReport : IUploadFiles
    {
        public List<ReportDataFile> Files { get; set; }
    }
}
