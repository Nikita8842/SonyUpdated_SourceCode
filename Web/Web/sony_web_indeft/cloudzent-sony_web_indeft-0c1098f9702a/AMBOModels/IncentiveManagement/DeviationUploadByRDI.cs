using AMBOModels.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.IncentiveManagement
{
    public class DeviationUploadByRDISearch : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public string IncentiveMonth { get; set; }
        public int FestivalSchemeId { get; set; }
        public int BranchId { get; set; }
    }

    public class DeviationUploadByRDISaveReasons : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public List<SaveReasonsData> SaveReasonsDataList { get; set; }
    }

    public class SaveReasonsData
    {
        public Int64 RecordId { get; set; }
        public Int64 ReasonId { get; set; }
        public string Remark1 { get; set; }
        public string Remark2 { get; set; }
    }


    public class DeviationUploadByRDIExcel //: MasterAbstract
    {
        public string EncryptKey { get; set; }
        public string RoleName { get; set; }

        public string Month { get; set; }
        public int FestivalSchemeId { get; set; }
        public Int64 UserId { get; set; }
        public int Flag { get; set; }

        public DataTable ExcelData { get; set; }
    }

    public class DeviationReasons
    {
        public Int64 Id { get; set; }
        public string ReasonName { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
    }
}
