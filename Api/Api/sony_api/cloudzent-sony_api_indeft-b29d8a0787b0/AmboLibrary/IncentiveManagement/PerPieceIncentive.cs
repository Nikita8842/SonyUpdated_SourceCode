using AmboLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.IncentiveManagement
{
    public class PerPieceIncentiveGridData
    {
        public Int64 SchemeId { get; set; }
        public string SchemeName { get; set; }
        public string Applicability { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }

    public class CreatePerPieceIncentive : DownloadPerPieceIncentiveExcel
    {

        public string SchemeName { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public List<Int64> TargetCategoryIds { get; set; }
        public List<Int64> PerPieceTargetCategoryIds { get; set; }
        public List<Int64> DivisionIds { get; set; }
        public List<Int64> BranchIds { get; set; }
        public List<Int64> ChannelIds { get; set; }
        public DataTable ExcelData { get; set; }
    }

    public class DownloadPerPieceIncentiveExcel : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 SchemeId { get; set; }
        public Int64 Applicability { get; set; }//values are 1:Product Name, 2:Target Category
        public List<Int64> ProductCategoryIds { get; set; }
    }

    public class PerPieceIncentiveValues : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override long UserId { get; set; }

        public DataTable ExcelData { get; set; }
    }
	
	public class GetPerPieceIncentiveValues : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override long UserId { get; set; }

        public Int64 SchemeId { get; set; }
    }

    public class PerPieceIncentiveSchemeByProductId
    {
        public Int64 ProductCategoryId { get; set; }
    }

    public class PerPieceIncentiveSchemeGet
    {        
        public Int64 SchemeId { get; set; }
        public string SchemeName { get; set; }
    }

    public class PerPieceIncentiveCreate : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialName { get; set; }
        public Int64 MaterialId { get; set; }
        public Int64 SchemeId { get; set; }
        public string SchemeName { get; set; }
        public Int64 Min { get; set; }
        public Int64 Max { get; set; }
        public double IncentiveAmount { get; set; }
        public double MinAch { get; set; }
        public double MaxAch { get; set; }
        public List<PerPieceIncentiveList> listPerPieceVal { get; set; }
    }

    public class DeletePerPieceIncentive : MasterAbstract
    {
        #region Abstract Overrides
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        #endregion Abstract Overrides

        public Int64 SchemeId { get; set; }
    }

    public class PerPieceIncentiveList
    {
        public Int64? SchemeId { get; set; }
        public Int64? Min { get; set; }
        public Int64? Max { get; set; }
        public double? IncentiveAmount { get; set; }
        public double? MinAch { get; set; }
        public double? MaxAch { get; set; }
    }
}
