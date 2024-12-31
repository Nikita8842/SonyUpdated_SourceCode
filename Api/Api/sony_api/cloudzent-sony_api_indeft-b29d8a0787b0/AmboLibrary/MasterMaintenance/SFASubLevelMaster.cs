using AmboLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.MasterMaintenance
{
    public class SFASubLevelMaster : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 Id { get; set; }
        public string SFASubLevelName { get; set; }
        public string Description { get; set; }
        public Int64 SFALevelId { get; set; }
        public string SFALevelName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime LastModificationDate { get; set; }
        public Int64 LastModifiedBy { get; set; }
    }

    public class SFASubLevel : SFASubLevelMaster
    {
        public string LevelCreationDate { get; set; }
        public string SFALevelName { get; set; }
    }
    public class SFASubLevelList
    {
        public List<SFASubLevel> SFASubLevel { get; set; }
        public SFASubLevelList()
        {
            SFASubLevel = new List<SFASubLevel>();
        }
    }

    public class SFASubLevelInput : MasterAbstract
    {
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }

        public Int64 SFASubLevelId { get; set; }
    }
}
