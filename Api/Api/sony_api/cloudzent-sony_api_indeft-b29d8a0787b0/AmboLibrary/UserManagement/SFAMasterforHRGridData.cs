using AmboLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.MasterMaintenance
{
    public class SFAMasterforHRGridData
    { 
        public Int64 Id { get; set; }
        public string SFAName { get; set; }
        public string SFACode { get; set; }
        public string Branch { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        public string SourceName { get; set; }
        public string AgencyRef { get; set; }
        public string SFAType { get; set; }
        public string Education { get; set; }
        public string Experience { get; set; }

        public string DOB { get; set; }
        public string DOJ { get; set; }
        public string AssetIssued { get; set; }
        public string DOL { get; set; }
        public string FatherName { get; set; }
        public string LevelofEducation { get; set; }
        public string BankName { get; set; }
        public string BankAccountNo { get; set; }
        public Int64 RegionId { get; set; }
        public Int64 BranchId { get; set; }
        public Int64 StateId { get; set; }
        public Int64 CityId { get; set; }
        public Int64 DivisionId { get; set; }
        public string Region { get; set; }
        public string Division { get; set; }
        public string SFALevel { get; set; }
        public string ESIAccountNo { get; set; }
        public string PFAccountNo { get; set; }
        public string MedicalInsuranceNo { get; set; }
        public string MICoverageFrom { get; set; }
        public string MICoverageTo { get; set; }
        public string PersonalInsuranceNo { get; set; }
        public string PICoverageFrom { get; set; }
        public string PICoverageTo { get; set; }
        public string Address { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }
        public string CityType { get; set; }
        public string Location { get; set; }
        public string DealerName { get; set; }
        public string DealerCode { get; set; }
        public string Channel { get; set; }
        public string Agency { get; set; }
        public string SFASubLevel { get; set; }
        public string SourceCode { get; set; }
    }
}
