using AMBOModels.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.MasterMaintenance
{
    public class CompetitorProductMaster : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 ID { get; set; }
        public Int64 CompetitorID { get; set; }
        public string ProductName { get; set; }
        public bool Discontinued { get; set; }
        public bool Status { get; set; }
        public Int64 SonyProductCategory { get; set; }
        public bool TopModel { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime LastModificationDate { get; set; }
        public Int64 LastModifiedBy { get; set; }
    }
    public class CompetitorProductData : CompetitorProductMaster
    {
        public string CompetitorName { get; set; }
        public string ProductCategoryName { get; set; }
        public string ProductStatus { get; set; }
        public string TopModelString { get; set; }
    }
}
