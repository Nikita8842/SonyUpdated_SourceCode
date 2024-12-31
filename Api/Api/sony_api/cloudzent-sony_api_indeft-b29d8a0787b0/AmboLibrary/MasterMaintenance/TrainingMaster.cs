using AmboLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.MasterMaintenance
{
    public class TrainingGridData
    {
        public Int64 TrainingId { get; set; }
        public string CourseName { get; set; }
        public string TrainerName { get; set; }
        public string ProductCategory { get; set; }
        public string Channel { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public bool Status { get; set; }
        public List<Int64> ProductCategoryIds { get; set; }
        public List<string> ProductCategoryNames { get; set; }
        public List<Int64> ChannelIds { get; set; }
        public List<string> ChannelName { get; set; }
        public List<Int64> BranchIds { get; set; }
        public List<string> BranchNames { get; set; }
        public Int64 ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
        public Int64 FORMId { get; set; }
    }

    public class TrainingDropdown
    {
        public Int64 Id { get; set; }
        public string CourseName { get; set; }
    }


    public class TInputModel
    {
        public Int64 Id { get; set; }
        public Int64 TrainingId { get; set; }
        public string CourseName { get; set; }
        public string TrainerName { get; set; }
        public string ProductCategory { get; set; }
        public string Channel { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public bool Status { get; set; }
        public Int64 ProductCategoryid { get; set; }
        public string CategoryName { get; set; }
        public Int64 BranchIds { get; set; }
        public string BranchName { get; set; }
        public Int64 Channelid { get; set; }
        public string ChannelName { get; set; }
    }

    public class TMessageModel
    {
        public Int64 Id { get; set; }
        public Int64 ProductCategoryid { get; set; }
        public string CategoryName { get; set; }
        public Int64 BranchIds { get; set; }
        public string BranchName { get; set; }
        public Int64 Channelid { get; set; }
        public string ChannelName { get; set; }

    }


    public class TrainingMasterEditData : MasterAbstract
    {


        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }
        public Int64 TrainingId { get; set; }
        public Int64 Id { get; set; }
        public List<Int64> ProductCategoryid { get; set; }
        public List<Int64> BranchIds { get; set; }
        public List<Int64> Channelid { get; set; }
        public List<string> CategoryNames { get; set; }
        public List<string> BranchNames { get; set; }
        public List<string> ChannelNames { get; set; }

    }

}
