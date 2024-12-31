using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboLibrary.Abstract;

namespace AmboLibrary.MasterMaintenance
{
    public class SalaryMaster : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64? Id { get; set; }
        public Int64 LoginId { get; set; }
        public string Basic { get; set; }
        public string HRA { get; set; }
        public string Med { get; set; }
        public string Conv { get; set; }
        public string Airtime { get; set; }
        public string Other { get; set; }
        public string Insurance { get; set; }
        public string OtherAllowance1 { get; set; }
        public string OtherAllowance2 { get; set; }
        public bool IsActive { get; set; }
        
    }

    public class SFASalaryMasterGrid : SalaryMaster
    {
        public string SFACode { get; set; }
        public string SFAName { get; set; }
        public string SFALevel { get; set; }
        public string Region { get; set; }
        public string Branch { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Division { get; set; }
    }

    public class SFASalaryMasterDelete : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 Id { get; set; }
    }

    public class SFASalaryMasterUpload
    {
        public string SFACode { get; set; }
        public string SFAName { get; set; }
        public string Branch { get; set; }
        public string City { get; set; }
        public string Division { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
    }

    public class SFASalaryMasterDownload
    {
        public Int64[] BranchIds { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
