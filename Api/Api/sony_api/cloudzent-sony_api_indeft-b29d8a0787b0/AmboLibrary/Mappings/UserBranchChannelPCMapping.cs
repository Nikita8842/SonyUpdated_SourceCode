using AmboLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.Mappings
{
    public class UserBranchChannelPCMapping : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 ID { get; set; }
        public Int64 UserIdForMapping { get; set; }
        public Int64 RoleId { get; set; }
        public string Role { get; set; }
        public Int64[] BranchIds { get; set; }
        public Int64[] ChannelIds { get; set; }
        public Int64[] ProdCatIds { get; set; }
        public Int64[] DivisionIds { get; set; }
        public bool isUpdate { get; set; }
        public string SubmitMessage { get; set; }
    }
    public class UserBranchChannelPCMappingGridData : UserBranchChannelPCMapping
    {
        public string UserFullName { get; set; }
        public string UserRoleName { get; set; }
    }
}
