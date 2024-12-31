using AmboLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.MasterMaintenance
{
    public class LocationMaster : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 ID { get; set; }
        public string LocationName { get; set; }
        public Int64 CityId { get; set; }
        public bool IsActive { get; set; }
    }

    public class LocationData
    {
        public Int64 ID { get; set; }
        public string LocationName { get; set; }
    }

    public class LocationGridData : LocationMaster
    {
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string RegionName { get; set; }
        public string Status { get; set; }
        public Int64 StateId { get; set; }
        public Int64 RegionId { get; set; }
    }

    public class CreateLocationForm : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public string LocationName { get; set; }
        public Int64 CityId { get; set; }
        public bool IsActive { get; set; }
    }

    public class UpdateLocationForm : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 ID { get; set; }
        public string LocationName { get; set; }
        public Int64 CityId { get; set; }
        public bool IsActive { get; set; }
    }

    public class DeleteLocationForm : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 ID { get; set; }
    }
}
