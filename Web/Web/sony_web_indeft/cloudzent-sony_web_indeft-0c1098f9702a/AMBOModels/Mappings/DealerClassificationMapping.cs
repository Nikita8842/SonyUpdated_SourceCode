using AMBOModels.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.Mappings
{
    public class DealerClassificationMapping : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override long UserId { get; set; }

        public Int64 ID { get; set; }
        public Int64 ProductCategoryId { get; set; }
        public Int64 BranchId { get; set; }
        public Int64 DealerId { get; set; }
        public Int64 ClassificationId { get; set; }
    }

    public class DealerClassificationMappingTable : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override long UserId { get; set; }

        public Int64 ProductCategoryId { get; set; }
        public Int64 BranchId { get; set; }
        public List<DealerClassificationMap> MappingTable { get; set; }
    }

    public class DealerClassificationMappingSearch : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override long UserId { get; set; }

        public Int64 ProductCategoryId { get; set; }
        public Int64 BranchId { get; set; }
    }

    public class DealerClassificationMap
    {
        public Int64 DealerId { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public string LocationName { get; set; }
        public Int64? ClassificationId { get; set; }
    }

    public class DealerClassificationMappingGridData : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override long UserId { get; set; }

        public Int64 ID { get; set; }
        public Int64 ProductCategoryId { get; set; }
        public Int64 BranchId { get; set; }
        public string ProductCategoryName { get; set; }
        public string BranchName { get; set; }
        public Int64 DealerId { get; set; }
        public string DealerName { get; set; }
        public Int64 ClassificationId { get; set; }
        public string ClassificationName { get; set; }
    }

    public class DealerProductMapping : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override long UserId { get; set; }

        public Int64 ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
        public Int64 BranchId { get; set; }
        public Int64 DealerId { get; set; }
        public Int64 ClassificationId { get; set; }
        public string ClassificationName { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }
        public string ContactPerson { get; set; }
        public string EmailID { get; set; }
        public List<ProductClassificationList> listProductClassification { get; set; }
    }

    public class ProductClassificationList
    {
        public Int64 ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
        public Int64 ClassificationId { get; set; }
        public string ClassificationName { get; set; }
    }
}
