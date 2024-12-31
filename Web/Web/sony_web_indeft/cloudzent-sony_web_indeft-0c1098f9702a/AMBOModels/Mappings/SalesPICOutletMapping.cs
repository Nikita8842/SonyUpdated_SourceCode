using AMBOModels.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.Mappings
{
    public class SalesPICOutletMappingGridData : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override long UserId { get; set; }

        public Int64 MappingId { get; set; }
        public Int64 BranchId { get; set; }
        public string BranchName { get; set; }
        public Int64 SalesPICId { get; set; }
        public string SalesPICName { get; set; }
    }

    public class ManageSalesPICOutletMappingForm : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override long UserId { get; set; }

        public Int64 BranchId { get; set; }
        public Int64 SalesPICId { get; set; }
        public List<string> NonSFAMasterCodes { get; set; }
        public List<Int64> DealerIds { get; set; }
    }

    public class DeleteSalesPICOutletMappingForm : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override long UserId { get; set; }

        public Int64 SalesPICId { get; set; }
    }

    public class SalesPIC
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
    }
}
