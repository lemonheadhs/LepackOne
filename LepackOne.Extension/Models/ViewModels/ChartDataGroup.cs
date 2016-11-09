using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LepackOne.Extension.Models.ViewModels
{
    public class ChartDataGroup<T>
    {
        public string Name { get; set; }
        public List<T> Data { get; set; }
    }
}
