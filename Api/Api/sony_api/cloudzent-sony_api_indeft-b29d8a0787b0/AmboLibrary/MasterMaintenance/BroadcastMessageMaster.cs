using AmboLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.MasterMaintenance
{
    public class CreateBroadcastMessageForm : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public virtual Int64 Id { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
        public string FileName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<Int64> DivisionIds { get; set; }
        public List<Int64> BranchIds { get; set; }
        public List<Int64> SFAIds { get; set; }
        public List<Int64> SIDIds { get; set; }
        public Int64 Status { get; set; }
        public List<Int64> RoleIds { get; set; }
        public string PICTUREDATA { get; set; }
        public byte[] PictureAsBytes { get; set; }

    }

    public class CreateBroadcastMessageFormOutput
    {
        public Int64 MessageId { get; set; }
        public List<FCMIDListWithRole> FCMIds { get; set; }
    }

    public class FCMIDListWithRole
    {
        public string FCMId { get; set; }
        public string RoleName { get; set; }
    }

    public class UpdateBroadcastMessageForm : CreateBroadcastMessageForm
    {
        public override Int64 Id { get; set; }
    }

    public class BroadcastMessageGridData : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 Id { get; set; }
        public string Message { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }
        public string Subject { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }


    }


    public class BroadcastMessageEDITData : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }
        public Int64 Id { get; set; }
        public List<Int64> DivisionIds { get; set; }
        public List<Int64> BranchIds { get; set; }
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Please select at least one SFA.")]
        public List<Int64> SFAIds { get; set; }
        public List<Int64> SIDID { get; set; }
        public List<string> DivisionName { get; set; }
        public List<string> SFANAME { get; set; }
        public List<string> BRANCHNAME { get; set; }
        public List<String> SIDNAME { get; set; }
        public List<Int64> RoleIds { get; set; }
        public List<string> RoleNames { get; set; }

       public BroadcastMessageEDITData()
        {
            SFAIds = new List<Int64>();
            SIDID = new List<Int64>();
            DivisionIds = new List<Int64>();
            BranchIds = new List<Int64>();

            DivisionName = new List<string>();
            SFANAME = new List<string>();
            SIDNAME = new List<String>();
            BRANCHNAME = new List<String>();
            RoleIds = new List<Int64>();
            RoleNames = new List<string>();

        }
    }

    public class BMessageModel
    {
        public Int64 Id { get; set; }
        public Int64 DivisionIds { get; set; }
        public Int64 BranchIds { get; set; }
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Please select at least one SFA.")]
        public Int64 SFAId { get; set; }
        public Int64 SIDID { get; set; }
        public string DivisionName { get; set; }
        public string SFANAME { get; set; }
        public string BRANCHNAME { get; set; }
        public string SIDNAME { get; set; }
        public Int64 RoleIds { get; set; }
        public string RoleNames { get; set; }

    }
    public class BMessageInputModel
    {
        public Int64 Id { get; set; }
        public Int64 DivisionIds { get; set; }
        public Int64 BranchIds { get; set; }
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Please select at least one SFA.")]
        public Int64 SFAId { get; set; }
        public Int64 SIDID { get; set; }
        public Int64 MessageId { get; set; }
    }
}
