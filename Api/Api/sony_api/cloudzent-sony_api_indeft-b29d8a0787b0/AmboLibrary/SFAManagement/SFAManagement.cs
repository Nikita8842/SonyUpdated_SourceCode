using System;
using AmboLibrary.Abstract;

namespace AmboLibrary.SFAManagement
{
    /// <summary>
    /// class to validate SFA Management
    /// </summary>
    public class SFAManagement
    {
    }

    public class Master : MasterAbstract
    {
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
    }

    /// <summary>
    /// Get SFA Profile Info
    /// </summary>
    public class GetSFAProfile
    {
        public string SFARole { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime? DOJ { get; set; }
        public string Mobile { get; set; }
        public string SFALevel { get; set; }
        public string TimeIn { get; set; }
        public string AssociateDealer { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
    }

    public class SFAProfileUpdateInput
    {
        public Int64 UserId { get; set; }
        public string Mobile { get; set; }
        public string EmailId { get; set; }
    }
}
