using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LepackOne.Extension.Models.Interface
{
    public interface IUploadFiles
    {
        List<ReportDataFile> Files { get; set; }
    }
}
