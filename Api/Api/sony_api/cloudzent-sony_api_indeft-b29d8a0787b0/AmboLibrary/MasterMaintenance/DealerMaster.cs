using AmboLibrary.Abstract;
using System;
using System.Collections.Generic;

namespace AmboLibrary.MasterMaintenance
{
    public class DealerMaster : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 ID { get; set; }
        public string DealerCode { get; set; }
        public string SAPCode { get; set; }//used in grid
        public string MasterCode1 { get; set; }
        public string MasterCode2 { get; set; }
        public string PayerCode { get; set; }
        public string PayerName { get; set; }//used in grid
        public string StoreCode { get; set; }
        public string CustomerId { get; set; }
        public string DealerName { get; set; }//used in grid
        public Int64 ChannelId { get; set; }
        public Int64 BranchId { get; set; }
        public Int64 RegionId { get; set; }
        public Int64 StateId { get; set; }
        public Int64 CityId { get; set; }
        public Int64 LocationId { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string MobileNumber { get; set; }
        public string ContactPerson { get; set; }
        public string EmailID { get; set; }
        public string TIN { get; set; }
        public string PAN { get; set; }
        public bool PSIOutlet1 { get; set; }
        public bool PSIOutlet2 { get; set; }
        public string OpeningDate { get; set; }
        public string ClosingDate { get; set; }
        public string OutletType { get; set; }//used in grid
        public bool Status { get; set; }
        public bool Discontinued { get; set; }
        public DateTime CreationDate { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime LastModificationDate { get; set; }
        public Int64 LastModifiedBy { get; set; }        
    }

    public class DealerMasterCode
    {
        public string MasterCode { get; set; }
    }

    public class DealerList
    {
        public Int64 DealerId { get; set; }
        public string DealerName { get; set; }
    }

    public class NonSFADealer
    {
        public Int64 DealerId { get; set; }
        public string DealerName { get; set; }
    }

    public class NonSFAMasterCodeList : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public List<DealerMasterCode> MasterCodes { get; set; }
    }

    public class DealerGridData : DealerMaster
    {
        public string BranchName { get; set; }
        public string LocationName { get; set; }
        public string ChannelName { get; set; }
        public string ActiveStatus { get; set; }
        public string PSIOutlet1Data { get; set; }
        public string PSIOutlet2Data { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
    }

    public class PayerDetails
    {
        public string SAPCode { get; set; }
        public string DealerCode { get; set; }
        public string PayerCode { get; set; }
        public string PayerName { get; set; }//used in grid
    }
}
