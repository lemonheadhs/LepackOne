﻿using LepackOne.Extension.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LepackOne.Extension.Models
{
    public class Report : IUploadFiles
    {
        public List<ReportDataFile> Files { get; set; }
    }
}
