using AMBOModels.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.MasterMaintenance
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
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Please select at least one SFA.")]
        public List<Int64> SFAIds { get; set; }
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Please select at least one SFA.")]
        public List<Int64> SIDIds { get; set; }
        [Range(0, 2, ErrorMessage = "Invalid form submitted. Please contact your administrator.")]
        public Int64 Status { get; set; }
        public virtual Int64 BranchId { get; set; }
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
        public string CheckFileName { get; set; }
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
        public Int64 RoleNames { get; set; }

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
        public Int64 RoleIds { get; set; }
        public Int64 RoleNames { get; set; }
    }
}
