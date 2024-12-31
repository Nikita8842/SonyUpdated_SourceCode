using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.SFAMobileApp
{
    public class SFASaleThruSubmission
    {
        public Int64 ProductCategoryId { get; set; }
        public Int64 ProductSubCategoryId { get; set; }
        public Int64 UserId { get; set; }
        public Int64 DailySalesId { get; set; }
        //public Int64 MaterialId { get; set; }
        //public Int64 Quantity { get; set; }
        public string CustomerName { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string SerialNo { get; set; }
        public bool IsBulkSale { get; set; }
        public string Remarks { get; set; }
        public Int64 Achievement { get; set; }
        public List<SFASaleThruModelType> SFASaleThruModelType { get; set; }
        public SFASaleThruSubmission()
        {
            SFASaleThruModelType = new List<SFASaleThruModelType>();
        }
    }

    public class SFASaleThruModelType
    {
        public Int64 ModelId { get; set; }
        public Int64 Quantity { get; set; }
        public bool IsBulk { get; set; }
    }

    public class ComboSales
    {
        public Int64 UserId { get; set; }
        public Int64 DailySalesId { get; set; }
        public Int64 ComboDailySalesId { get; set; }
        public Int64 MaterialId { get; set; }
        public Int64 ComboMaterialId { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal ComboSellingPrice { get; set; }
        public Int64 ProductId { get; set; }
        public Int64 ComboProductId { get; set; }
        public Int64 SubProductId { get; set; }
        public Int64 ComboSubProductId { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceImage { get; set; }
        public string ComboInvoiceNumber { get; set; }
        public string ComboInvoiceImage { get; set; }
    }

    public class ModelDropdownInput
    {
        public Int64 UserId { get; set; }
        public Int64 ProSubCatId { get; set; }
        public Int64 DailySalesId { get; set; }
        public Int64 MaterialId { get; set; }
    }

    public class ModelDropdownOutput
    {
        public Int64 DailySalesId { get; set; }
        public Int64 MaterialId { get; set; }
        public string MaterialName { get; set; }
    }

    public class CancelComboSalesInput
    {
        public Int64 ComboId1 { get; set; }
        public Int64 ComboId2 { get; set; }
    }
}
