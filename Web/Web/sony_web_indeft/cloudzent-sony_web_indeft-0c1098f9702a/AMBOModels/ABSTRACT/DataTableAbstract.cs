using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMBOModels.Abstract;
using System.Data;

namespace AMBOModels.ABSTRACT
{
    public abstract class DataTableAbstract : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public abstract DataTable dtAsset { get; set; }
    }
}
