using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.MPRIntegration
{
    public class GetBranchDivisionWise_SFACountInput
    {
        public string LoginId { get; set; }
        public string Password { get; set; }
    }

    public class GetBranchDivisionWise_SFACountOutput
    {
        public int BranchId { get; set; }
        public string Branch { get; set; }
        public int DivisionId { get; set; }
        public string Division { get; set; }
        public Int64 SFACount { get; set; }
        public string Status { get; set; }
    }
}
