using AmboLibrary.Abstract;
using AmboLibrary.MasterMaintenance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.Mappings
{
    public class ProductCategorySFAMapping : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override long UserId { get; set; }

        public Int64 ID { get; set; }
        public Int64 BranchId { get; set; }
        public Int64 DealerId { get; set; }
        public Int64 EmployeeId { get; set; }
        public Int64[] ProductCategoryIds { get; set; }

    }

    public class ProdCatSFAMapGridSearchFilters : ModelGrid
    {
        public List<Int64> BranchIds { get; set; }
        public List<Int64> CityIds { get; set; }
        public List<Int64> LocationIds { get; set; }
        public List<Int64> DealerIds { get; set; }
        public string SFAIds { get; set; }
        public List<Int64> ProductCategoryIds { get; set; }
    }

    public class ProductCategorySFAMappingGridData : ProductCategorySFAMapping
    {
        public string BranchName { get; set; }
        public string DealerName { get; set; }
        public string SFAName { get; set; }
        public Int64 ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
        public string SFACode { get; set; }
        public string DealerChannel { get; set; }
        public string City { get; set; }
        public string Location { get; set; }
    }

    public class NavigationIncentiveCategorySFAMapping : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override long UserId { get; set; }

        public Int64 ID { get; set; }
        public Int64 BranchId { get; set; }
        public Int64 DealerId { get; set; }
        public Int64 EmployeeId { get; set; }
        public Int64 IncentiveCategoryId { get; set; }
    }

    public class NavigationProductCategorySFAMapping : NavigationIncentiveCategorySFAMapping
    {
        public Int64[] ProductCategoryIds { get; set; }
    }
}
