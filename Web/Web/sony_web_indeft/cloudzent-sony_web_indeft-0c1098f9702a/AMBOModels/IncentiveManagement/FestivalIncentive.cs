using AMBOModels.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.IncentiveManagement
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
        [Required(AllowEmptyStrings = false, ErrorMessage = "(Required)")]
        public string SchemeName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "(Required)")]
        public string DateFrom { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "(Required)")]
        public string DateTo { get; set; }
        [Required(ErrorMessage = "(Required)")]
        public bool IsCalculateBase { get; set; }
        [Required(ErrorMessage = "(Required)")]
        public bool IsPayBase { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "(Required)")]
        public List<Int64> DivisionIds { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "(Required)")]
        public List<Int64> BranchIds { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "(Required)")]
        public List<Int64> ChannelIds { get; set; }
        //[Required(AllowEmptyStrings = false, ErrorMessage = "(Required)")]
        //public List<Int64> ProCatIds { get; set; }
        //[Required(AllowEmptyStrings = false, ErrorMessage = "(Required)")]
        //public List<Int64> TargetCatIds { get; set; }
        public DataTable ExcelData { get; set; }
    }

    public class CreateFestivalIncentiveSlab : DownloadFestivalIncentiveDefinitionExcel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "(Required)")]
        public string SchemeName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "(Required)")]
        public string DateFrom { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "(Required)")]
        public string DateTo { get; set; }
        [Required(ErrorMessage = "(Required)")]
        public bool IsCalculateBase { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "(Required)")]
        public override List<Int64> ProductCategoryIds { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "(Required)")]
        public List<Int64> TargetCategoryIds { get; set; }
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

    public class DeleteFestivalIncentive : MasterAbstract
    {
        #region Abstract Overrides
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        #endregion Abstract Overrides

        [Range(1, Int64.MaxValue, ErrorMessage = "ID not sent for deleting this record.")]
        public Int64 SchemeId { get; set; }
    }
}
