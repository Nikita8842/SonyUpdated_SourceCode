using AMBOModels.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.MasterMaintenance
{
    public class TargetTypeMaster : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override long UserId { get; set; }

        public Int64 Id { get; set; }
        public string TargetType { get; set; }
    }
}
