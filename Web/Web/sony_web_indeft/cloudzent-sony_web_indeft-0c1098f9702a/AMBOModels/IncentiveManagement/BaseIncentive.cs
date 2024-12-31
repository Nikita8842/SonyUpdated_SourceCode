using AMBOModels.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.IncentiveManagement
{
    public class BaseIncentiveGridData
    {
        public Int64 TargetCategoryId { get; set; }
        public string TargetCategory { get; set; }
        public string Status { get; set; }
    }

    public class CreateBaseIncentiveForm : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override long UserId { get; set; }

        public Int64 TargetCategoryId { get; set; }
        public bool Status { get; set; }
        public List<BaseIncentiveDefinition> objDefinitionData { get; set; }

    }

    public class GetBaseIncentive : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override long UserId { get; set; }

        public Int64 TargetCategoryId { get; set; }

    }

    public class DeleteBaseIncentiveForm : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override long UserId { get; set; }

        public Int64 TargetCategoryId { get; set; }
    }

    public class BaseIncentiveDefinition
    {
        public Int64 Id { get; set; }
        public Int64 TargetTypeId { get; set; }
        public string TargetType { get; set; }
        public decimal Minimum { get; set; }
        public decimal Maximum { get; set; }
        public List<SFALevelAmountMap> objAmountMappings { get; set; }
    }

    public class SFALevelAmountMap
    {
        public Int64 SFALevelId { get; set; }
        public Int64 Amount { get; set; }
    }
}
