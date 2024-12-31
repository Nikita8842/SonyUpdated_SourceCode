using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.MasterMaintenance
{
    public class GridVariables
    {
        public int Draw { get; set; }
        public int PageSize { get; set; }
        public int PageStart { get; set; }
        public int SortColumn { get; set; }
        public string SortDirection { get; set; }
        public string SearchValue { get; set; }
    }
}
