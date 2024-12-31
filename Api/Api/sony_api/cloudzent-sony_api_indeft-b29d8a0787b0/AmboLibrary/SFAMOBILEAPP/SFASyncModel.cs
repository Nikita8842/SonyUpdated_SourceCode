using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.SFAMobileApp
{
    public class SyncInput
    {
        public Int64 SFAId { get; set; }
    }

    public class SyncDefault
    {
        public Int64 ID { get; set; }
        public string Value { get; set; }
        public string Code { get; set; }
    }

    public class SFASyncModel
    {
        public string SplashImage { get; set; }
        public List<SyncAttendance> Attendance { get; set; }
        public List<SyncProductCat> ProductCategory { get; set; }
        public List<SyncDealer> Dealer { get; set; }
        public List<SyncOutletData> SyncOutletData { get; set; }
        public List<SyncSonyProductCategory> SonyProductCategory { get; set; }
        public SFASyncModel()
        {
            Attendance = new List<SyncAttendance>();
            ProductCategory = new List<SyncProductCat>();
            Dealer = new List<SyncDealer>();
            SonyProductCategory = new List<SyncSonyProductCategory>();
            SyncOutletData = new List<SyncOutletData>();
        }
    }

    // For Attendance Sync
    public class SyncAttendance
    {
        public Int64 ID { get; set; }
        public string Value { get; set; }
    }

    //For Sony Sync
    public class SyncMaterial
    {
        public Int64 ID { get; set; }
        public string Value { get; set; }
        public string Code { get; set; }
    }
    public class SyncProductSubCat
    {
        public Int64 ID { get; set; }
        public string Value { get; set; }
        public List<SyncMaterial> Material { get; set; }
        public SyncProductSubCat()
        {
            Material = new List<SyncMaterial>();
        }
    }
    public class SyncProductCat
    {
        public Int64 ID { get; set; }
        public string Value { get; set; }
        public List<SyncProductSubCat> ProductSubCategory { get; set; }
        public SyncProductCat()
        {
            ProductSubCategory = new List<SyncProductSubCat>();
        }
    }

    //For Dealer Sync
    public class SyncDealer
    {
        public Int64 ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    //For Competitor Sync
    public class SyncCompany
    {
        public Int64 ID { get; set; }
        public string Value { get; set; }
        public string Code { get; set; }
        public List<SyncProductSubCat> ProductCategory { get; set; }
        public SyncCompany()
        {
            ProductCategory = new List<SyncProductSubCat>();
        }
    }
    public class SyncSonyProductCategory
    {
        public Int64 ID { get; set; }
        public string Value { get; set; }
        public List<SyncCompany> Company { get; set; }
        public SyncSonyProductCategory()
        {
            Company = new List<SyncCompany>();
        }
    }

    //Extras
    public class ProductSyncData
    {
        public Int64 ProductCatId { get; set; }
        public string CategoryName { get; set; }
        public Int64 ProductSubCatId { get; set; }
        public string SubCategoryName { get; set; }
        public Int64 MaterialId { get; set; }
        public string MaterialName { get; set; }
        public string MaterialCode { get; set; }
    }

    public class CompProductSyncData
    {
        public Int64 SonyProductCatId { get; set; }
        public string SonyCategoryName { get; set; }
        public Int64 CompetitorId { get; set; }
        public string CompetitorName { get; set; }
        public string CompetitorCode { get; set; }
        public Int64 ProductCatId { get; set; }
        public string CategoryName { get; set; }
        public Int64 MaterialId { get; set; }
        public string MaterialName { get; set; }
    }
    public class SyncOutletData
    {
        public Int64 OutletId { get; set; }
        public string OutletName { get; set; }
    }
}
