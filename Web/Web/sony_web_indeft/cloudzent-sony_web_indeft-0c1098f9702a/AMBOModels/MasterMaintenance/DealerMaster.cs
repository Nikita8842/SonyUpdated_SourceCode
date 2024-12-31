using AMBOModels.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.MasterMaintenance
{
    public class DealerMaster : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 ID { get; set; }
        
        public string DealerCode { get; set; }
        [Required (ErrorMessage = "SAP Code is required")]
        [MaxLength(25, ErrorMessage ="Length cannot be greater than 25 characters")]
        public string SAPCode { get; set; }
        public string MasterCode1 { get; set; }
        public string MasterCode2 { get; set; }
        [Required(ErrorMessage = "Payer Code is required")]
        public string PayerCode { get; set; }
        [Required(ErrorMessage = "Payer Name is required")]
        public string PayerName { get; set; }
        //[Required(ErrorMessage = "Required")]
        public string StoreCode { get; set; }
        //[Required(ErrorMessage = "Required")]
        public string CustomerId { get; set; }
        [Required(ErrorMessage = "Dealer name is required")]
        public string DealerName { get; set; }
        [Required(ErrorMessage = "Channel is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Channel is required")]
        public Int64 ChannelId { get; set; }
        [Required(ErrorMessage = "Branch is required")]
        [Range (1,int.MaxValue,ErrorMessage ="Branch is required")]
        public Int64 BranchId { get; set; }
        [Required(ErrorMessage = "Region is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Region is required")]
        public Int64 RegionId { get; set; }
        [Required(ErrorMessage = "State is required")]
        [Range(1, int.MaxValue, ErrorMessage = "State is required")]
        public Int64 StateId { get; set; }
        [Required(ErrorMessage = "City is required")]
        [Range(1, int.MaxValue, ErrorMessage = "City is required")]
        public Int64 CityId { get; set; }
        [Required(ErrorMessage = "Location is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Location is required")]
        public Int64 LocationId { get; set; }
        //[Required(ErrorMessage = "Required")]
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        [Required(ErrorMessage = "Mobile number is required")]
        [StringLength(10, ErrorMessage = "Enter 10 digit number"), MinLength(10, ErrorMessage ="Enter 10 digit number")]
        public string MobileNumber { get; set; }
        //[Required(ErrorMessage = "Required")]
        public string ContactPerson { get; set; }
        //[Required(ErrorMessage = "Required")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Invalid Email Id")]
        public string EmailID { get; set; }
        public string TIN { get; set; }
        public string PAN { get; set; }
        public bool PSIOutlet1 { get; set; }
        public bool PSIOutlet2 { get; set; }
        [Required(ErrorMessage = "Opening date is required")]
        public string OpeningDate { get; set; }
        public string ClosingDate { get; set; }
        [Required(ErrorMessage = "Outlet type is required")]
        public string OutletType { get; set; }//used in grid
        public bool Status { get; set; }
        public bool Discontinued { get; set; }
        public DateTime CreationDate { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime LastModificationDate { get; set; }
        public Int64 LastModifiedBy { get; set; }        
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
    public class Outlettype
    {
        public int Id { get; set; }
        public string OutletTypeId { get; set; }
        public string OutletType { get; set; }
    }

    public class PayerDetails
    {
        public string SAPCode { get; set; }
        public string DealerCode { get; set; }
        public string PayerCode { get; set; }
        public string PayerName { get; set; }//used in grid
    }
}
