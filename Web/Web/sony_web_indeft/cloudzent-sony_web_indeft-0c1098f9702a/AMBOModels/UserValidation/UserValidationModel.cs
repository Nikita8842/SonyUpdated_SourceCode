using System;
using System.Collections.Generic;

namespace AMBOModels.UserValidation
{
    public class UserValidationModel
    {
        public int LoginMode { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string EncryptKey { get; set; }
        public DateTime SessionTime { get; set; }
        public string IMEI { get; set; }
    }

    public class UserDetailsModel
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public Int64 UserId { get; set; }
        public string Name { get; set; }
        public Int64 RoleId { get; set; }
        public string RoleName { get; set; }
        public DateTime LastLoginDate { get; set; }
        public List<RoleRights> listRoleRights { get; set; }
    }

    public class RoleRights
    {
        public Int64 RoleId { get; set; }
        public Int64 ModuleId { get; set; }
        public Int64 SubModuleId { get; set; }
        public bool ViewPermission { get; set; }
        public bool CreatePermission { get; set; }
        public bool EditPermission { get; set; }
        public bool DeletePermission { get; set; }
    }
}
