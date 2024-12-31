using AmboLibrary.Abstract;
using AmboLibrary.MasterMaintenance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.Mappings
{
    public class EmployeeStructureMapping : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override long UserId { get; set; }

        public Int64 ID { get; set; }
        public Int64 RDIId { get; set; }
        public Int64 BranchId { get; set; }
        public Int64 DivisionId { get; set; }
        public Int64 SFATypeId { get; set; }
        public Int64[] SFAIds { get; set; }
    }

    public class EmployeeStructureMapGridFilters : ModelGrid
    {
        public List<Int64> BranchIds { get; set; }
        public string RDIName { get; set; }
    }

    public class EmployeeStructureMappingGridData : EmployeeStructureMapping
    {
        public string RDIName { get; set; }
        public string RDIBranchName { get; set; }
        public string SFAName { get; set; }
        public Int64 SFAId { get; set; }
    }

    public class UserBranchChannelPCMappingFilter : ModelGrid
    {
        public string LoginId { get; set; }
        public string EmployeeName { get; set; }
        public Int64[] BranchIds { get; set; }
    }
}
