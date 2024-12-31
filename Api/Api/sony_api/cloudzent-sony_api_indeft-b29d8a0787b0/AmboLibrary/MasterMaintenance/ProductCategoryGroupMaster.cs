using AmboLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.MasterMaintenance
{
    public class ProductGroupCategoryMaster : MasterAbstract
    {
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }

        public Int64 GroupId { get; set; }
        public string GroupName { get; set; }
        public int DisplayOrder { get; set; }
        public string CategoryName { get; set; }
        public int[] ProductIds { get; set; }
    }
}
