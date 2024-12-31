using AmboLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.IncentiveManagement
{
    public class DeviationApprovalSearch : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 BranchId { get; set; }
        public string IncentiveMonth { get; set; }
    }

    public class DeviationApprovalExcel //: MasterAbstract
    {
        public string EncryptKey { get; set; }
        public string RoleName { get; set; }
        public string Month { get; set; }
        public Int64 UserId { get; set; }
        public int Flag { get; set; }

        public DataTable ExcelData { get; set; }
    }

    //public class DeviationApprovalExcel : MasterAbstract
    //{
    //    public override string EncryptKey { get; set; }
    //    public override string RoleName { get; set; }
    //    public override Int64 UserId { get; set; }

    //    public DataTable ExcelData { get; set; }
    //}
}
