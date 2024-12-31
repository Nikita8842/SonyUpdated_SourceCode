using AmboLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.Mappings
{
    public class ProdDefUnderTargetCat : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override long UserId { get; set; }

        public Int64 ProductCategoryId { get; set; }
        public Int64 ProductSubCategoryId { get; set; }
        //public DataTable MappingGrid { get; set; }
    }

    public class ProdDefUnderTargetCatforAllMat : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override long UserId { get; set; }

        public Int64[] ProductCategoryId { get; set; }
        public Int64[] ProductSubCategoryId { get; set; }
        //public DataTable MappingGrid { get; set; }
    }

    public class ProdDefUnderTargetCatGridData : ProdDefUnderTargetCat
    {
        public string ProductCategoryName { get; set; }
        public string ProductSubCategoryName { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialName { get; set; }
        public string MOP { get; set; }
        public List<TargetCategory> SelectedTargetCategoryIds { get; set; }
    }

    public class ProdDefUnderTargetCatGridOutput
    {
        public List<ProdDefUnderTargetCatGridData> objGridRows { get; set; }
        public List<TargetCategory> TargetCategoryList { get; set; }
    }
    public class TargetCategory
    {
        public Int64 TargetCategoryId { get; set; }
        public string TargetCategoryName { get; set; }
    }
}
