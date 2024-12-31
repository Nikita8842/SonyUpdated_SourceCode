using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.ReportsManagement
{
    public class SFAMobileReportsModel
    {
    }
    
    public class ReportInput
    {
        public Int64 LoginId { get; set; }
        public Int64 ProductSubCatId { get; set; }
    }

    #region SFA Attendance Report For RDI
    public class SFAAttendanceReportForRDIInput
    {        
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public Int64 UserId { get; set; }
    }

    public class SFAAttendanceReportForRDI
    {
        public string AttDate { get; set; }
        public long AttendanceTypeId { get; set; }
        public string AttendanceType { get; set; }
        public int Total { get; set; }
    }
    #endregion

    #region My MTD Sales
    public class MTDModelWiseDSRReportModel
    {
        public Int64 DailySalesId { get; set; }
        public Int64 MaterialId { get; set; }
        public Int64 Id { get; set; }
        public string Product { get; set; }
        public string Model { get; set; }
        public Int32 Quantity { get; set; }
    }

    public class MTDModelWiseComboReportModel
    {
        public Int64 DailySalesId { get; set; }
        public Int64 MaterialId { get; set; }
        public Int64 ProductCatId { get; set; }
        public string ProductCategory { get; set; }
        public Int64 ProductSubCatId { get; set; }
        public string ProductSubCat { get; set; }
        public string Model { get; set; }
        public Int32 Quantity { get; set; }
        public Int32 RemainingQuantity { get; set; }
        public string SellingDate { get; set; }
    }

    public class MTDComboSalesReportModel
    {
        public string Date { get; set; }
        public string ProductCategory { get; set; }
        public string ProductSubCategory { get; set; }
        public string Material { get; set; }
        public decimal SellingPrice { get; set; }
        public string InvoiceNumber { get; set; }
        public string ComboProductCategory { get; set; }
        public string ComboProductSubCategory { get; set; }
        public string ComboMaterial { get; set; }
        public decimal ComboSellingPrice { get; set; }
        public string ComboInvoiceNumber { get; set; }
        public Int64 ComboId1 { get; set; }
        public Int64 ComboId2 { get; set; }
    }

    public class MTDModelWiseDSRReportList
    {
        public List<MTDModelWiseDSRReportModel> MTDModelWiseDSRReportModel { get; set; }
        public MTDModelWiseDSRReportList()
        {
            MTDModelWiseDSRReportModel = new List<MTDModelWiseDSRReportModel>();
        }
    }
    #endregion

    #region Today Sales Report
    public class TodaySalesReportModel
    {
        public Int64 Id { get; set; }
        public string ProductCategory { get; set; }
        public string ProductSubCategory { get; set; }
        public string MaterialName { get; set; }
        public Int32 Quantity { get; set; }
        public string Barcode { get; set; }
        public string Type { get; set; }
        public string CustomerName { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string SerialNo { get; set; }
        public string Remarks { get; set; }
        public Int64 Total { get; set; }
    }
    public class TodaySalesReportList
    {
        public List<TodaySalesReportModel> TodaySalesReportModel { get; set; }
        public TodaySalesReportList()
        {
            TodaySalesReportModel = new List<TodaySalesReportModel>();
        }
    }
    #endregion

    #region Last Day Sales Report
    public class LastDaySalesReportModel
    {
        public Int64 Id { get; set; }
        public string CreationDate { get; set; }
        public string MatarialName { get; set; }
        public string ProductCategory { get; set; }
        public Int64 Quantity { get; set; }
        public string ProductSubCategory { get; set; }
    }
    public class LastDaySalesReportList
    {
        public List<LastDaySalesReportModel> LastDaySalesReportModel { get; set; }
        public LastDaySalesReportList()
        {
            LastDaySalesReportModel = new List<LastDaySalesReportModel>();
        }
    }

    public class ComboSalesReportInput
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public Int64 UserId { get; set; }
        public int BranchId { get; set; }
    }
    #endregion
}