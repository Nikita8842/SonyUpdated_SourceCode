using AMBOModels.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.MasterMaintenance
{
    public class SFALevelMasterData : MasterAbstract
    {
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }

        public Int64 Id { get; set; }

        [Required(ErrorMessage ="Please Enter Level.")]
        public string SFALevelName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime LastModificationDate { get; set; }
        public Int64 LastModifiedBy { get; set; }
    }

    public class SFALevelData
    {
        public Int64 SFALevelId { get; set; }
        public string SFALevelName { get; set; }
    }

    public class SFALevel : SFALevelMasterData
    {
        public string LevelCreationDate { get; set; }
    }
    public class SFALevelList
    {
        public List<SFALevel> SFALevel { get; set; }
        public SFALevelList()
        {
            SFALevel = new List<SFALevel>();
        }
    }

    public class SFALevelInput: MasterAbstract
    {
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }

        public Int64 SFALevelId { get; set; }
    }
}
