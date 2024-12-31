using AmboLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.IncentiveManagement
{
    public class FestivalIncentiveGridData
    {
        public Int64 SchemeId { get; set; }
        public string SchemeName { get; set; }
        public string CalculateBaseIncentive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class CreateFestivalIncentive : DownloadFestivalIncentiveDefinitionExcel
    {
        public string SchemeName { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public bool IsCalculateBase { get; set; }
        public bool IsPayBase { get; set; }
        //public List<Int64> ProCatIds { get; set; }
        //public List<Int64> TargetCatIds { get; set; }
        public List<Int64> DivisionIds { get; set; }
        public List<Int64> BranchIds { get; set; }
        public List<Int64> ChannelIds { get; set; }
        public DataTable ExcelData { get; set; }
    }

    public class DownloadFestivalIncentiveDefinitionExcel : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 SchemeId { get; set; }
        public virtual List<Int64> ProductCategoryIds { get; set; }
    }

    public class FestivalIncentiveDefinitionValues : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override long UserId { get; set; }

        public DataTable ExcelData { get; set; }
    }

    public class GetFestivalIncentiveValues : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override long UserId { get; set; }

        public Int64 SchemeId { get; set; }
    }

    public class CreateFestivalIncentiveSlab : DownloadFestivalIncentiveDefinitionExcel
    {
        public string SchemeName { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public bool IsCalculateBase { get; set; }
        public override List<Int64> ProductCategoryIds { get; set; }
        public List<Int64> TargetCategoryIds { get; set; }
        public DataTable ExcelData { get; set; }
    }

    public class DeleteFestivalIncentive : MasterAbstract
    {
        #region Abstract Overrides
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        #endregion Abstract Overrides

        public Int64 SchemeId { get; set; }
    }
}
