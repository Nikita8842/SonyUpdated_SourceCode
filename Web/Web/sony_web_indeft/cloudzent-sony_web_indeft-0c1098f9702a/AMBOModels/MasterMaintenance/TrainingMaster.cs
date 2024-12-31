using AMBOModels.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.MasterMaintenance
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
        public List<string> CategoryName { get; set; }
        public List<string> BranchName { get; set; }
        public List<string> ChannelName { get; set; }
        
    }
}
