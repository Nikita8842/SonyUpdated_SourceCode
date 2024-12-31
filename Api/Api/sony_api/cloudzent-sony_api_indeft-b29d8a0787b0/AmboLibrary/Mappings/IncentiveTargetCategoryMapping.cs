using AmboLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.Mappings
{
    public class IncentiveTargetCategoryMapping : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override long UserId { get; set; }

        public Int64 IncentiveCategoryId { get; set; }
        public string IncentiveCategoryName { get; set; }
        public Int64 TargetCategoryId { get; set; }
        public Int64 Weightage { get; set; }
        public Int64 TargetTypeId { get; set; }
        public List<TargetCategoryList> TargetCategoryMappings { get; set; }
        public bool Status { get; set; }
        public string SubmitMessage { get; set; }
        public bool isUpdate { get; set; }
    }

    public class TargetCategoryList
    {
        public Int64 TargetCategoryId { get; set; }
        public Int64 Weightage { get; set; }
        public Int64 TargetTypeId { get; set; }
        public string TargetCategoryName { get; set; }
        public string WeightageValue { get; set; }
        public string TargetTypeName { get; set; }
    }

    public class IncentiveTargetCategoryMappingGridData : IncentiveTargetCategoryMapping
    {
        public string StatusName { get; set; }
    }
}
