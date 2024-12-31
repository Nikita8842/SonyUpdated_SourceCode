using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.SFAMobileApp
{
    // To insert SFA Demo Ranging Stock Data
    public class SFADemoStockRangingModel
    {
        [Required (ErrorMessage ="SFA Id is missing.")]
        [Range(0, int.MaxValue, ErrorMessage = "Please provide valid integer SFAId.")]
        public Int64 SFAId { get; set; }
        [Required(ErrorMessage = "Product Category Id is missing.")]
        [Range(0, int.MaxValue, ErrorMessage = "Please provide valid integer ProductCategoryId.")]
        public Int64 ProductCategoryId { get; set; }
        [Required(ErrorMessage = "Product Sub Category Id is missing.")]
        [Range(0, int.MaxValue, ErrorMessage = "Please provide valid integer ProductSubCategoryId.")]
        public Int64 ProductSubCategoryId { get; set; }
        [Required(ErrorMessage = "Demo Stock Ranging Model Data is missing.")]
        public List<DemoStockRangingModelType> DemoStockRangingModel { get; set; }
        public SFADemoStockRangingModel()
        {
            DemoStockRangingModel = new List<DemoStockRangingModelType>();
        }
    }
    public class DemoStockRangingModelType
    {
        public Int64 ModelId { get; set; }
        public Int64 Quantity { get; set; }
    }

    // To get SFA Demo Ranging Sock Data
    public class DemoRangingStockData
    {
        public string MaterialCode { get; set; }
        public string PlannedQuantity { get; set; }
        public string DisplayQuantity { get; set; }
        public string ActualDate { get; set; }
        public string Week { get; set; }
    }
    public class DemoRangingStockDataList
    {
        public List<DemoRangingStockData> DemoRangingStockData { get; set; }
        public DemoRangingStockDataList()
        {
            DemoRangingStockData = new List<DemoRangingStockData>();
        }
    }
    public class DemoRagingStockInput
    {
        public Int64 SFAId { get; set; }
        public Int64 ProductCategoryId { get; set; }
    }
}