using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.SFAMobileApp
{
    public class WeeklyStoreStockModel
    {
        public Int64 ProductCategoryId { get; set; }
        public Int64 ProductSubCategoryId { get; set; }
        public Int64 SFAId { get; set; }
        public List<WeeklyStoreStockDetailsModel> WeeklyStoreStockDetails { get; set; }
        public WeeklyStoreStockModel()
        {
            WeeklyStoreStockDetails = new List<WeeklyStoreStockDetailsModel>();
        }
    }
    public class WeeklyStoreStockDetailsModel
    {
        public Int64 MaterialId { get; set; }
        public Int64 ClosingStockId { get; set; }
        public Int64 Quantity { get; set; }
    }
    public class WeeklyStoreStockData
    {
        public Int64 Id { get; set; }
        public string ProductCategory { get; set; }
        public string ProductSubCategory { get; set; }
        public string MaterialName { get; set; }
        public Int64 Quantity { get; set; }
    }
    public class WeeklyStoreStockDataList
    {
        public List<WeeklyStoreStockData> WeeklyStoreStockData { get; set; }
        public WeeklyStoreStockDataList()
        {
            WeeklyStoreStockData = new List<WeeklyStoreStockData>();
        }
    }
    public class WeeklyStoreStockDataInput
    {
        public Int64 SFAId { get; set; }
    }
}
