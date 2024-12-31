using AmboLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.SFAManagement
{
    public class SFAAttendanceMaster : MasterAbstract
    {
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public Int64 AttendanceTypeId  {get;set;}
        public int IsParent { get; set; }
        public string IMEI { get; set; }
        public Int64 DealerId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set;}
        public string Location { get; set; }
        public string Image { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Remarks { get; set; }
    }
    public class LeaveType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
    }
    public class LeaveTypeList
    {
        public List<LeaveType> LeaveType { get; set; }
        public LeaveTypeList()
        {
            LeaveType = new List<LeaveType>();
        }
    }
    #region MTD Attendance Report
    public class SFAMTDAttendance : MasterAbstract
    {
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
    }
    public class SFAMTDAttendanceList
    {
        public List<SFAMTDAttendance> SFAMTDAttendance { get; set; }
        public SFAMTDAttendanceList()
        {
            SFAMTDAttendance = new List<SFAMTDAttendance>();
        }
    }
    public class SFAMTDAttendanceInput
    {
        public Int64 SFAId { get; set; }
    }
    #endregion
}
