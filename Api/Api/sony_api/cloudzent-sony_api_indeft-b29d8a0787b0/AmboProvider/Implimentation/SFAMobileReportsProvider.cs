using AmboLibrary.ReportsManagement;
using AmboLibrary.SFAMobileApp;
using AmboLibrary.WebReports;
using AmboProvider.Interface;
using DBHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboProvider.Implimentation
{
    public class SFAMobileReportsProvider : ISFAMobileReportsProvider
    {
        private readonly DbHelper _dataHelper;
        private readonly IErrorLogProvider _IErrorLogProvider;

        public SFAMobileReportsProvider(IErrorLogProvider IErrorLogProvider)
        {
            _dataHelper = DbHelper.GetInstance("defaultConnection");
            _IErrorLogProvider = IErrorLogProvider;
        }

        /// <summary>
        /// To get MDT Model Wise DSR Report.
        /// </summary>
        /// <param name="Input">LoginId</param>
        /// <returns>MDT Model Wise DSR Report Data.</returns>
        public MTDModelWiseDSRReportList GetMTDSalesReport(ReportInput Input)
        {
            MTDModelWiseDSRReportList Data = new MTDModelWiseDSRReportList();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@LoginId", Input.LoginId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetMTDModelWiseDSRReport]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data.MTDModelWiseDSRReportModel = (from data in outputFromSP.AsEnumerable()
                                                       select new MTDModelWiseDSRReportModel
                                                       {
                                                           Product = Convert.ToString(data.Field<string>("Product")),
                                                           Model = Convert.ToString(data.Field<string>("Model")),
                                                           Id = Convert.ToInt64(data.Field<Int64>("id")),
                                                           Quantity = Convert.ToInt32(data.Field<Int64>("Quantity"))
                                                       }).ToList();
                }
            }
            catch (Exception Ex)
            {
                //Log exception here
            }
            return Data;
        }

        /// <summary>
        /// To get Today Sales Report.
        /// </summary>
        /// <param name="Input">Login Id</param>
        /// <returns>Today Sales Report</returns>
        public TodaySalesReportList GetTodaySalesReport(ReportInput Input)
        {
            TodaySalesReportList Data = new TodaySalesReportList();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@LoginId", Input.LoginId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetTodaySalesReport]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data.TodaySalesReportModel = (from data in outputFromSP.AsEnumerable()
                                                       select new TodaySalesReportModel
                                                       {
                                                           ProductCategory = Convert.ToString(data.Field<string>("ProductCategory")),
                                                           Id = Convert.ToInt64(data.Field<Int64>("id")),
                                                           Quantity = Convert.ToInt32(data.Field<Int64>("Quantity")),
                                                           ProductSubCategory = Convert.ToString(data.Field<string>("ProductSubCategory")),
                                                           Barcode = Convert.ToString(data.Field<string>("Barcode")),
                                                           Type = Convert.ToString(data.Field<string>("Type")),
                                                           MaterialName = Convert.ToString(data.Field<string>("MaterialName")),
                                                           CustomerName = Convert.ToString(data.Field<string>("CustomerName")),
                                                           EmailId = Convert.ToString(data.Field<string>("EmailId")),
                                                           MobileNo = Convert.ToString(data.Field<string>("MobileNo")),
                                                           Remarks = Convert.ToString(data.Field<string>("Remarks")),
                                                           SerialNo = Convert.ToString(data.Field<string>("SerialNo")),
                                                           Total = Convert.ToInt64(data.Field<Int64>("Total"))
                                                       }).ToList();
                }
            }
            catch (Exception Ex)
            {
                //Log exception here
            }
            return Data;
        }

        /// <summary>
        /// To get Last Day Sales Report.
        /// </summary>
        /// <param name="Input">Login Id</param>
        /// <returns>Last Day Sales Report</returns>
        public LastDaySalesReportList GetLastDaySalesReport(ReportInput Input)
        {
            LastDaySalesReportList Data = new LastDaySalesReportList();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@LoginId", Input.LoginId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetLastDaySales]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data.LastDaySalesReportModel = (from data in outputFromSP.AsEnumerable()
                                                  select new LastDaySalesReportModel
                                                  {
                                                      ProductCategory = Convert.ToString(data.Field<string>("ProductCategory")),
                                                      Id = Convert.ToInt64(data.Field<Int64>("id")),
                                                      Quantity = Convert.ToInt32(data.Field<Int64>("Quantity")),
                                                      ProductSubCategory = Convert.ToString(data.Field<string>("ProductSubCategory")),
                                                      CreationDate = Convert.ToString(data.Field<string>("CreationDate")),
                                                      MatarialName = Convert.ToString(data.Field<string>("MatarialName"))
                                                  }).ToList();
                }
            }
            catch (Exception ex)
            {
                //Log exception here
            }
            return Data;
        }

        /// <summary>
        /// To get SFA Target vs Achievement Report data.
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 3, 2018
        /// </summary>
        /// <param name="Input">SFA Id</param>
        /// <returns>SFA Target vs Achievement Report data.</returns>
        public SFATargetvsAchievementList GetSFATargetvsAchievementReport(SFATvsAInput Input)
        {
            SFATargetvsAchievementList Data = new SFATargetvsAchievementList();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@SFAId", Input.SFAId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetSFATargetVsAchievement]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data.SFATargetvsAchievement = (from data in outputFromSP.AsEnumerable()
                                                    select new SFATargetvsAchievementModel
                                                    {
                                                        TargetCategory = Convert.ToString(data.Field<string>("TargetCategory")),
                                                        QtyAch = Convert.ToString(data.Field<string>("QtyAch")),
                                                        QtyTagret = Convert.ToString(data.Field<string>("QtyTagret")),
                                                        ValueAch = Convert.ToString(data.Field<string>("ValueAch")),
                                                        ValueTarget = Convert.ToString(data.Field<string>("ValueTarget"))
                                                    }).ToList();
                }
            }
            catch (Exception)
            {
                //Log exception here
            }
            return Data;
        }

        public List<SFAFestivalTargetvsAchievementModel> GetSFAFestivalTargetVsAchievement(SFATvsAInput Input)
        {
            List<SFAFestivalTargetvsAchievementModel> Data = new List<SFAFestivalTargetvsAchievementModel>();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@SFAId", Input.SFAId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetSFAFestivalTargetVsAchievement]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data = (from data in outputFromSP.AsEnumerable()
                                                   select new SFAFestivalTargetvsAchievementModel
                                                   {
                                                       SchemeName = Convert.ToString(data.Field<string>("SchemeName")),
                                                       TargetCategory = Convert.ToString(data.Field<string>("TargetCategory")),
                                                       QtyAch = Convert.ToString(data.Field<string>("QtyAch")),
                                                       QtyTagret = Convert.ToString(data.Field<string>("QtyTagret")),
                                                       ValueAch = Convert.ToString(data.Field<string>("ValueAch")),
                                                       ValueTarget = Convert.ToString(data.Field<string>("ValueTarget"))
                                                   }).ToList();
                }
            }
            catch (Exception)
            {
                //Log exception here
            }
            return Data;
        }

        public List<MTDModelWiseComboReportModel> GetMTDModelWiseComboReport(ReportInput Input)
        {
            List<MTDModelWiseComboReportModel> Data = new List<MTDModelWiseComboReportModel>();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@LoginId", Input.LoginId, DbType.Int64));
                objDBParam.Add(new DbParameter("@ProductSubCat", Input.ProductSubCatId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetMTDModelWiseComboReport]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data = (from data in outputFromSP.AsEnumerable()
                                                       select new MTDModelWiseComboReportModel
                                                       {
                                                           DailySalesId = Convert.ToInt64(data.Field<Int64>("DailySalesId")),
                                                           MaterialId = Convert.ToInt64(data.Field<Int64>("MaterialId")),
                                                           ProductCategory = Convert.ToString(data.Field<string>("ProductCategory")),
                                                           Model = Convert.ToString(data.Field<string>("MaterialName")),
                                                           ProductCatId = Convert.ToInt64(data.Field<Int64>("ProductCatId")),
                                                           ProductSubCat = Convert.ToString(data.Field<string>("ProductSubCat")),
                                                           ProductSubCatId = Convert.ToInt64(data.Field<Int64>("ProductSubCatId")),
                                                           Quantity = Convert.ToInt32(data.Field<Int64>("Quantity")),
                                                           RemainingQuantity = Convert.ToInt32(data.Field<Int64>("RemainingQuantity")),
                                                           SellingDate = Convert.ToString(data.Field<string>("SellingDate"))
                                                       }).ToList();
                }
            }
            catch (Exception Ex)
            {
                //Log exception here
            }
            return Data;
        }

        public List<MTDComboSalesReportModel> GetMTDComboSalesReport(ReportInput Input)
        {
            List<MTDComboSalesReportModel> Data = new List<MTDComboSalesReportModel>();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@LoginId", Input.LoginId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetMTDComboSalesReport]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data = (from data in outputFromSP.AsEnumerable()
                            select new MTDComboSalesReportModel
                            {
                                Date = Convert.ToString(data.Field<string>("Date")),
                                ProductCategory = Convert.ToString(data.Field<string>("ProductCategory")),
                                ProductSubCategory = Convert.ToString(data.Field<string>("ProductSubCategory")),
                                Material = Convert.ToString(data.Field<string>("Material")),
                                SellingPrice = Convert.ToDecimal(data.Field<decimal>("SellingPrice")),
                                InvoiceNumber = Convert.ToString(data.Field<string>("InvoiceNumber")),
                                ComboProductCategory = Convert.ToString(data.Field<string>("ComboProductCategory")),
                                ComboProductSubCategory = Convert.ToString(data.Field<string>("ComboProductSubCategory")),
                                ComboMaterial = Convert.ToString(data.Field<string>("ComboMaterial")),
                                ComboSellingPrice = Convert.ToDecimal(data.Field<decimal>("ComboSellingPrice")),
                                ComboInvoiceNumber = Convert.ToString(data.Field<string>("ComboInvoiceNumber")),
                                ComboId1 = Convert.ToInt64(data.Field<Int64>("ComboId1")),
                                ComboId2 = Convert.ToInt64(data.Field<Int64>("ComboId2")),
                            }).ToList();
                }
            }
            catch (Exception Ex)
            {
                //Log exception here
            }
            return Data;
        }

        public List<SFAAttendanceReportForRDI> GetAttendanceCountSFAForRDI(SFAAttendanceReportForRDIInput Input)
        {

            List<SFAAttendanceReportForRDI> ListofDates = null;
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@UserId", Input.UserId, DbType.Int64));
                objDBParam.Add(new DbParameter("@StartDate1", Input.StartDate, DbType.String));
                objDBParam.Add(new DbParameter("@EndDate1", Input.EndDate, DbType.String));

                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetAttendanceCountSFAForRDI]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofDates = (from item in outputFromSP.AsEnumerable()
                                   select new SFAAttendanceReportForRDI
                                   {
                                       AttDate = item.Field<string>("AttDate"),
                                       AttendanceTypeId = item.Field<long>("AttendanceTypeId"),
                                       AttendanceType = item.Field<string>("AttendanceType"),
                                       Total = item.Field<Int32>("Total"),
                                   }).ToList();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAttendanceCountSFAForRDI", ex.StackTrace, ex.Message);
            }
            return ListofDates;

        }

        public DataTable GetComboSalesReportMobile(ComboSalesReportInput Input)
        {

            DataTable dtComboSalesReportMobile = null;
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@UserId", Input.UserId, DbType.Int64));
                objDBParam.Add(new DbParameter("@FromDate", Input.StartDate, DbType.DateTime));
                objDBParam.Add(new DbParameter("@ToDate", Input.EndDate, DbType.DateTime));
                objDBParam.Add(new DbParameter("@BranchId", Input.BranchId, DbType.Int32));

                dtComboSalesReportMobile = _dataHelper.ExecuteDataTable("[dbo].[GetComboSalesReportMobile]", objDBParam, CommandType.StoredProcedure);
                
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetComboSalesReportMobile", ex.StackTrace, ex.Message);
            }
            return dtComboSalesReportMobile.Rows.Count > 0 ? dtComboSalesReportMobile : null;

        }
    }
}
