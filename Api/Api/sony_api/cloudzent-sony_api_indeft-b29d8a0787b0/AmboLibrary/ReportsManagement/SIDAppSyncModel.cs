using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.ReportsManagement
{
    public class SIDAppSyncModel
    {
        public List<SIDSyncBranches> SIDSyncBranches { get; set; }
        public List<SIDSyncProducts> SIDSyncProducts { get; set; }
        public List<SIDSyncProductGroups> SIDSyncProductGroups { get; set; }
        public List<SIDSyncSubProduct> SIDSyncSubProduct { get; set; }
        public List<SIDSyncSubDealers> SIDSyncSubDealers { get; set; }
        public List<SIDSyncServiceEntityAccount> SIDSyncServiceEntityAccount { get; set; }
        public List<SIDSyncServiceEntityChannel> SIDSyncServiceEntityChannel { get; set; }
        public List<SIDSyncServiceEntityModel> SIDSyncServiceEntityModel { get; set; }
        public List<SIDSyncFestivalData> SIDSyncFestivalData { get; set; }
        public SIDAppSyncModel()
        {
            SIDSyncBranches = new List<SIDSyncBranches>();
            SIDSyncProducts = new List<SIDSyncProducts>();
            SIDSyncProductGroups = new List<SIDSyncProductGroups>();
            SIDSyncSubProduct = new List<SIDSyncSubProduct>();
            SIDSyncSubDealers = new List<SIDSyncSubDealers>();
            SIDSyncServiceEntityAccount = new List<SIDSyncServiceEntityAccount>();
            SIDSyncServiceEntityChannel = new List<SIDSyncServiceEntityChannel>();
            SIDSyncServiceEntityModel = new List<SIDSyncServiceEntityModel>();
            SIDSyncFestivalData = new List<SIDSyncFestivalData>();
        }
    }

    public class SIDSyncBranches
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
    }

    public class SIDSyncProducts
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
    }

    public class SIDSyncProductGroups
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
    }

    public class SIDSyncSubProduct
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public Int64 ProductCategoryId { get; set; }
    }

    public class SIDSyncSubDealers
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public Int64 BranchId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    public class SIDSyncServiceEntityAccount
    {
        public string MasterCode { get; set; }
        public string GlobalDealerName { get; set; }
        public Int64 ChannelId { get; set; }
        public Int64 BranchId { get; set; }
    }

    public class SIDSyncServiceEntityChannel
    {
        public Int64 ChannelId { get; set; }
        public string Channel { get; set; }
    }

    public class SIDSyncServiceEntityModel
    {
        public Int64 ModelId { get; set; }
        public string Model { get; set; }
        public Int64 ProductCategoryId { get; set; }
        public Int64 ProductSubCategoryId { get; set; }
    }

    public class SIDSyncInput
    {
        public Int64 UserId { get; set; }
    }

    public class SIDSyncFestivalData
    {
        public Int64 Id { get; set; }
        public string SchemeName { get; set; }
    }
}
