using AmboLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.MasterMaintenance
{
    public class CityMaster : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 ID { get; set; }
        public string CityName { get; set; }
        public bool IsActive { get; set; }
        public Int64 StateId { get; set; }
        public Int64 CityTypeId { get; set; }
        public Int64 RegionId { get; set; }
        public string CityType { get; set; }
        public string State { get; set; }
        public string Region { get; set; }
    }

    public class CityData
    {
        public Int64 CityId { get; set; }
        public string CityName { get; set; }
    }

    public class CityGridData : CityMaster
    {
        public string StateName { get; set; }
        public string RegionName { get; set; }
        public string CityTypeName { get; set; }
        public string Status { get; set; }
    }

    public class CreateCityForm : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public string CityName { get; set; }
        public Int64 StateId { get; set; }
        public Int64 CityTypeId { get; set; }
        public bool IsActive { get; set; }
    }

    public class UpdateCityForm : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 ID { get; set; }
        public string CityName { get; set; }
        public Int64 StateId { get; set; }
        public Int64 CityTypeId { get; set; }
        public bool IsActive { get; set; }
    }

    public class DeleteCityForm : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 ID { get; set; }
    }
}
