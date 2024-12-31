using AmboLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.SFAMOBILEAPP
{
    public class TrainingSearch : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 SFAId { get; set; }
    }

    public class TrainingList
    {
        public Int64 TrainingId { get; set; }
        public string CourseName { get; set; }
        public string TrainerName { get; set; }
        public string ProductCategory { get; set; }
        public string ChannelName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public bool IsActive { get; set; }

    }

    public class TrainingRatingList
    {
        public Int64 SubTitleId { get; set; }
        public Int64 Rating { get; set; }
    }

    public class FeedbackForm
    {
        public List<FeedbackFormTitles> Titles { get; set; }
    }

    public class FeedbackFormSubTitles
    {
        public Int64 SubTitleId { get; set; }
        public string SubTitleName { get; set; }
    }

    public class FeedbackFormTitles
    {
        public Int64 TitleId { get; set; }
        public string TitleName { get; set; }
        public List<FeedbackFormSubTitles> SubTitles { get; set; }
    }

    public class SFAFeedbackData : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 SFAId { get; set; }
        public Int64 TrainingId { get; set; }
        public string Comment { get; set; }
        public List<TrainingRatingList> FeedbackRatings { get; set; }
    }
    public class SFAFeedbackDataInput
    {
        public Int64 TrainingId { get; set; }
    }
}
