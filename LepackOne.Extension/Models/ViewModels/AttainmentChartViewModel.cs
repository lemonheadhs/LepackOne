using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LepackOne.Extension.Models.ViewModels
{
    public class AttainmentChartViewModel
    {
        public IEnumerable<int> NonTargetCategories { get; set; }
        public IEnumerable<int> TargetCategories { get; set; }
        public List<ChartDataGroup<decimal>> Groups { get; set; }
    }
}
