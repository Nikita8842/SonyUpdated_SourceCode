using AmboLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.MasterMaintenance
{
    public class FeedbackGridData
    {
        public Int64 FormId { get; set; }
        public string FormName { get; set; }
        public bool Status { get; set; }
    }

    public class CreateFeedbackForm : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override long UserId { get; set; }

        public string FormName { get; set; }
        public List<FeedbackTitles> Titles { get; set; }
    }

    public class DeleteFeedbackForm : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override long UserId { get; set; }

        public Int64 FormId { get; set; }
    }

    public class CreateTrainingForm : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override long UserId { get; set; }
        public Int64 Id { get; set; }
        public string CourseName { get; set; }
        public string TrainerName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public Int64[] ProdCatId { get; set; }
        public Int64[] ChannelId { get; set; }
        public Int64 FormId { get; set; }
        public Int64[] BranchIds { get; set; }
    }

    public class ViewFeedbackForm : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override long UserId { get; set; }

        public Int64 FormId { get; set; }
    }

    public class FeedbackTitles
    {
        public Int64 TitleId { get; set; }
        public string TitleName { get; set; }
        public List<string> SubTitles { get; set; }
    }

    public class GetFeedbackForm
    {
        public Int64 FormId { get; set; }
        public string FormName { get; set; }
    }
}
