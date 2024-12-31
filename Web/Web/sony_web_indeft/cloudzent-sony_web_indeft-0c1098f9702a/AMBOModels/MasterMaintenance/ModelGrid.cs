using AMBOModels.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.MasterMaintenance
{
    public class ModelGrid
    {
        // properties are not capital due to json mapping
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public List<Column> columns { get; set; }
        public Search search { get; set; }
        public List<Order> order { get; set; }
    }
    public class Column
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public Search search { get; set; }
    }

    public class Search
    {
        public string value { get; set; }
        public string regex { get; set; }
    }

    public class Order
    {
        public int column { get; set; }
        public string dir { get; set; }
    }

    public class ProCatMaster : ModelGrid
    {
        public Int64[] DvisionIds { get; set; }
        public Int64[] ProCatIds { get; set; }
        public string status { get; set; }
    }

    public class CompetitorFilter
    {
        public Int64[] CompetitorCodeIds { get; set; }
        public Int64[] CompetitorNames { get; set; }
        public string Status { get; set; }
    }

    public class SFALevelFilter
    {
        public Int64[] SFALevelIds { get; set; }
    }
        
    public class SFASubLevelFilter
    {
        public Int64[] SFALevelIds { get; set; }
        public Int64[] SFASubLevelIds { get; set; }
    }

    public class RegionFilter
    {
        public Int64[] RegionIds { get; set; }
        public string Status { get; set; }
    }

    public class StateFilter
    {
        public Int64[] RegionIds { get; set; }
        public Int64[] StateIds { get; set; }
        public string Status { get; set; }
    }

    public class CityFilter
    {
        public Int64[] RegionIds { get; set; }
        public Int64[] StateIds { get; set; }
        public Int64[] CityTypeIds { get; set; }
        public Int64[] CityIds { get; set; }
        public string Status { get; set; }
    }

    public class LocationFilter : ReportsGrid
    {
        public Int64[] RegionIds { get; set; }
        public Int64[] StateIds { get; set; }
        public Int64[] CityIds { get; set; }
        public Int64[] LocationIds { get; set; }

        public string Status { get; set; }
    }

    public class BranchFilter
    {
        public Int64[] BranchIds { get; set; }
        public Int64[] BranchCodeIds { get; set; }
        public string Status { get; set; }
    }

    public class ProductCategoryFilter
    {
        public Int64[] DivisionIds { get; set; }
        public Int64[] ProductCatIds { get; set; }
        public string Status { get; set; }
    }

    public class SubProductCategoryFilter
    {
        public Int64[] SubProCatIds { get; set; }
        public Int64[] ProductCatIds { get; set; }
        public string Status { get; set; }
    }

    public class MaterialFilter : ReportsGrid
    {
        public Int64[] MaterialIds { get; set; }
        public Int64[] SubProCatIds { get; set; }
        public Int64[] ProductCatIds { get; set; }
        public string Status { get; set; }        
    }

    public class CompetitorProductFilter
    {
        public Int64[] CompetitorIds { get; set; }
        public Int64[] ProductCatIds { get; set; }
        public string Status { get; set; }
    }

    public class CompetitorModelFilter
    {
        public Int64[] CompetitorIds { get; set; }
        public Int64[] ProductCatIds { get; set; }
        public Int64[] ModelIds { get; set; }
        public Int64 SonyProCatId { get; set; }
        public Int64[] SonyProSubCatIds { get; set; }
        public string Status { get; set; }
    }

    public class ProCatGroupFilter
    {
        public Int64[] GroupIds { get; set; }
        //public string Status { get; set; }
    }

    public class ChannelFilter
    {
        public Int64[] ChannelCodeIds { get; set; }
        public Int64[] ChannelNameIds { get; set; }
    }

    public class DealerFilter : ReportsGrid
    {
        public string DealerName { get; set; }
        public string DealerCode { get; set; }

        public Int64[] DealerIds { get; set; }
        public string[] MasterCodeIds { get; set; }
        public Int64[] BranchIds { get; set; }
        public Int64[] LocationIds { get; set; }

        public string Status { get; set; }
    }

    public class DealerSFAFilter : ReportsGrid
    {
        public Int64[] DealerIds { get; set; }
        //public Int64[] SFAIds { get; set; }
        public string SFAIds { get; set; }
        public Int64[] BranchIds { get; set; }
        public Int64[] StateIds { get; set; }
        public Int64[] CityIds { get; set; }
        public Int64[] LocationIds { get; set; }
    }

    public class SFAMasterforHRFilter : ReportsGrid
    {
        //public Int64[] LoginIds { get; set; }
        //public Int64[] EmployeeIds { get; set; }
        public string LoginIds { get; set; }
        public string EmployeeIds { get; set; }
        public Int64[] BranchIds { get; set; }
        public Int64[] StateIds { get; set; }
        public Int64[] CityIds { get; set; }
        public string Status { get; set; }
        public Int64 SFATypeId { get; set; }
    }

    public class SFASalaryMasterFilter : ReportsGrid
    {
        public string LoginIds { get; set; }
        public string EmployeeIds { get; set; }
        public Int64[] BranchIds { get; set; }
        public Int64[] StateIds { get; set; }
        public Int64[] CityIds { get; set; }
        public string Status { get; set; }
    }

    public class UserBranchChannelPCMappingFilter : ModelGrid
    {
        public string LoginId { get; set; }
        public string EmployeeName { get; set; }
        public Int64[] BranchIds { get; set; }
    }
}
