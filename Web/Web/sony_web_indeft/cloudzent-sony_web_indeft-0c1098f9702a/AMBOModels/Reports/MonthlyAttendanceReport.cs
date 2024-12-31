using AMBOModels.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.Reports
{
    public class MonthlyAttendanceReportInput
    {
        public Int64 BranchId { get; set; }
        public Int64 SFAId { get; set; }
        public string StartDate { get; set; }
        public string Month { get; set; }
        public string EndDate { get; set; }
        public Int64 SFATypeId { get; set; }
        public Int64 UserId { get; set; }
        public Int64[] BranchIds { get; set; }
        public Int64 SystemId { get; set; } //nikita 29/12/2024

    }

    public class ApprovalModel
    {
        public MonthlyAttendanceReportInput MonthlyAttendanceReportInput { get; set; }
        public ApprovalData ApprovalData { get; set; }
    }

    public class UpdateMonthlyReport : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override long UserId { get; set; }
        public string SFACode { get; set; }
        public string Month { get; set; }
        public string AttendanceDate { get; set;
        }

        public string Remarks { get; set; }
        public Int64 NewAttendanceTypeId { get; set; }
        public string OLD_ATT_TYPE { get; set; }
        //public Int32 IsApproved { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public Int64 Id { get; set; }
        public Int64 SFAId { get; set; }
        public Int64 BranchId { get; set; }
        public Int64? AttendanceTypeId { get; set; }
        public string NewAttendanceType { get; set; }
        public Int64 DealerCode { get; set; } //nikita 30/12/2024
    }

    public class ApprovalData
    {
        public DataTable Dt { get; set; }
        public List<ApprovalGrid> ApprovalGrid { get; set; }
        public ApprovalData()
        {
            ApprovalGrid = new List<ApprovalGrid>();
        }
    }

    public class ApprovalGrid
    {
        public Int64 SFAIds { get; set; }
        public string NewAttendanceType { get; set; }
        public string OldAttendanceType { get; set; }
        
        public string AttendanceDates { get; set; }
        public string Approval { get; set; }
        public Int64 DealerCode { get; set; } //nikita 30/12/2024
    }
}
