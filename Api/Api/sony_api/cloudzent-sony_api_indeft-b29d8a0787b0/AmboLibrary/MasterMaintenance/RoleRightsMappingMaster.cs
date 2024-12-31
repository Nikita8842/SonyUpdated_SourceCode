using AmboLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.MasterMaintenance
{
    public class RoleRightsMappingMaster : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 RoleRightsId { get; set; }
        public Int64 RoleId { get; set; }
        public Int64[] SubModuleIds { get; set; }
        public bool ViewPermission { get; set; }
        public bool CreatePermission { get; set; }
        public bool EditPermission { get; set; }
        public bool DeletePermission { get; set; }
    }


    public class RoleRightsMappingGet
    {
        public Int64 RoleRightsId { get; set; }
        public string RoleName { get; set; }
        public string ModuleName { get; set; }
        public string SubModuleName { get; set; }
        public string ViewPermission { get; set; }
        public string CreatePermission { get; set; }
        public string EditPermission { get; set; }
        public string DeletePermission { get; set; }
    }

    public class RoleRightsMappingId
    {
        public Int64 RoleRightsId { get; set; }
    }
}
