using AmboLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.WebReports
{
    public class MonthlyAttendanceReportInput
    {
        public Int64 BranchId { get; set; }
        public Int64 SFAId { get; set; }
        public Int64 Month { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public Int64 SFATypeId { get; set; }
        public Int64 UserId { get; set; }
    }

    public class UpdateMonthlyReport : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override long UserId { get; set; }
        public string SFACode { get; set; }
        public string AttendanceDate { get; set; }
        public string Remarks { get; set; }
        public Int64 NewAttendanceTypeId { get; set; }
        public string OLD_ATT_TYPE { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
        //public Int32 IsApproved { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<string> AttendanceDates { get; set; }
        public Int64 Id { get; set; }
        public Int64 SFAId { get; set; }
        public Int64 BranchId { get; set; }
        public Int64? AttendanceTypeId { get; set; }
        public string NewAttendanceType { get; set; }
        public Int64 DealerCode { get; set; } //nikita 30/12/2024

    }

    public class ApprovalGrid
    {
        public Int64 SFAIds { get; set; }
        public string NewAttendanceType { get; set; }
        public string OldAttendanceType { get; set; }
        public string AttendanceDates { get; set; }
        public string Approval { get; set; }
        public long DealerCode { get; set; }
    }
}
