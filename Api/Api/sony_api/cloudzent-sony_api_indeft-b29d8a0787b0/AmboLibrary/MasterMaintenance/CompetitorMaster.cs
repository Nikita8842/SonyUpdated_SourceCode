using AmboLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.MasterMaintenance
{
    public class CompetitorMaster : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 ID { get; set; }
        public string CompetitorCode { get; set; }
        public string CompetitorName { get; set; }
        public bool Discontinued { get; set; }
        public bool Status { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime LastModificationDate { get; set; }
        public Int64 LastModifiedBy { get; set; }
    }
    public class CompetitorMasterData: CompetitorMaster
    {
        public string CompetitorStatus { get; set; }
    }

    public class CompetitorMasterInput: MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 ID { get; set; }
    }

    public class BrandList
    {
        public Int64 BrandId { get; set; }
        public string BrandName { get; set; }
    }

    public class CompetitionCountType
    {
        public Int64 Id { get; set; }
        public string Type { get; set; }
    }
    public class CompetitorCountTypeInput
    {
        public Int64 UserId { get; set; }
        public Int64 CompetitorId { get; set; }
    }
}
