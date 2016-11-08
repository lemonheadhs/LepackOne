using nuPickers.Shared.EnumDataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LepackOne.Extension.Enums
{
    public enum LeReportTypeEnum
    {
        [EnumDataSource(Label = "Chart1 - Attainment")]
        Attainment,

        [EnumDataSource(Label = "Chart2 - Completion")]
        Completion
    }
}
