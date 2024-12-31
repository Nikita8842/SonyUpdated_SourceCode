using System;
using System.Linq;
using System.Data;
using DBHelper;
using AmboUtilities.Helper;
using AmboLibrary.SFAManagement;
using AmboProvider.Interface;
using System.IO;
using AmboLibrary.SFAMobileApp;
using System.Web;
using System.Configuration;
using AmboLibrary.SFAMOBILEAPP;
using AmboLibrary.MasterMaintenance;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AmboProvider.Implimentation
{
    /// <summary>
    /// SFA Mobile Management Provider
    /// </summary>
    public class SFAMobileManagementProvider : ISFAMobileManagementProvider
    {
        private readonly DbHelper _dataHelper;
        private readonly IErrorLogProvider _IErrorLogProvider;

        public SFAMobileManagementProvider(IErrorLogProvider IErrorLogProvider)
        {
            _dataHelper = DbHelper.GetInstance("defaultConnection");
            _IErrorLogProvider = IErrorLogProvider;
        }        

        public GetSFAProfile GetSFAProfile(Int64 UserId, out string Message)
        {
            Message = string.Empty;

            DbParameterCollection sqlparam = new DbParameterCollection();
            sqlparam.Add(new DbParameter("@UserId", UserId, DbType.Int64));

            var datatable = _dataHelper.ExecuteDataSet("[dbo].[GetSFAProfileDetails]", sqlparam, CommandType.StoredProcedure);

            if (datatable.Tables[0].Rows.Count > 0)
            {
                var getList = (from item in datatable.Tables[0].AsEnumerable()
                               select new GetSFAProfile
                               {
                                   SFARole = Convert.ToString(item.Field<string>("RoleName")),
                                   FullName = Convert.ToString(item.Field<string>("SFAName")),
                                   Email = Convert.ToString(AMBOEcryption.DecryptData(item.Field<string>("Email"), true)),
                                   DOJ = Convert.ToDateTime(item.Field<DateTime>("DOJ")),
                                   Mobile = Convert.ToString(item.Field<string>("Mobile")),
                                   SFALevel = Convert.ToString(item.Field<string>("SFALevel")),
                                   AssociateDealer = Convert.ToString(item.Field<string>("DealerName")),
                                   TimeIn = Convert.ToString(item.Field<string>("TimeIN")),
                                   Category = Convert.ToString(item.Field<string>("Category")),
                                   Location = Convert.ToString(item.Field<string>("Location"))
                               }).FirstOrDefault();

                Message = "SFA Profile";
                return getList;
            }

            Message = "Please update your profile";
            return null;
        }

        public int SubmitSFAAttendance(SFAAttendanceMaster InputParam, out string Message)
        {
            Message = string.Empty;
            string path = string.Empty;
            string base64String = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                if (InputParam.Image != null)
                {
                    path = InputParam.Image.Length > 0 ? "Attendance_Images/" + InputParam.UserId + "_" + DateTime.Now.ToString("dd-MM-yyyy") + "_" + InputParam.AttendanceTypeId + ".jpg" : "";
                    base64String = InputParam.Image;
                }
                objDBParam.Add(new DbParameter("@UserId", InputParam.UserId, DbType.Int64));
                objDBParam.Add(new DbParameter("@AttendanceTypeId", InputParam.AttendanceTypeId, DbType.Int64));
                objDBParam.Add(new DbParameter("@IsParent", InputParam.IsParent, DbType.Int32));
                objDBParam.Add(new DbParameter("@Location", InputParam.Location, DbType.String));
                objDBParam.Add(new DbParameter("@Longitude", InputParam.Longitude, DbType.String));
                objDBParam.Add(new DbParameter("@Latitude", InputParam.Latitude, DbType.String));
                objDBParam.Add(new DbParameter("@Remarks", InputParam.Remarks, DbType.String));
                objDBParam.Add(new DbParameter("@ImagePath", path, DbType.String));
                objDBParam.Add(new DbParameter("@FromDate", InputParam.FromDate, DbType.DateTime));
                objDBParam.Add(new DbParameter("@ToDate", InputParam.ToDate, DbType.DateTime));
                objDBParam.Add(new DbParameter("@DealerId", InputParam.DealerId, DbType.Int64));
                objDBParam.Add(new DbParameter("@IMEI", InputParam.IMEI, DbType.String));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[SubmitSFAAttendance]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                {
                    if (InputParam.AttendanceTypeId == 1)
                    {
                        if (InputParam.Image != null)
                        {
                            if (!String.IsNullOrEmpty(InputParam.Image))
                            {
                                //move file path to controller
                                string filePath = System.Web.HttpContext.Current.Server.MapPath("~/" + path);
                                string DirectoryPath = System.Web.HttpContext.Current.Server.MapPath("~/" + "Attendance_Images");
                                if (!Directory.Exists(DirectoryPath))
                                {
                                    Directory.CreateDirectory(DirectoryPath);
                                }
                                if (File.Exists(filePath))
                                {
                                    File.SetAttributes(filePath, FileAttributes.Normal);
                                    File.Delete(filePath);
                                }
                                File.WriteAllBytes(filePath, Convert.FromBase64String(base64String));
                            }
                        }
                    }
                    return 1;
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return 0;
        }

        /// <summary>
        /// To fetch the list of Leave Types for Dropdown.
        /// </summary>
        /// <returns></returns>
        public LeaveTypeList GetLeaveTypeList()
        {
            var getList = new LeaveTypeList();
            var datatable = _dataHelper.ExecuteDataSet("[dbo].[GetLeaveTypes]", CommandType.StoredProcedure);

            if (datatable.Tables[0].Rows.Count > 0)
            {
                getList.LeaveType = (from item in datatable.Tables[0].AsEnumerable()
                                     select new LeaveType
                                     {
                                         Id = Convert.ToInt32(item.Field<Int32>("Id")),
                                         TypeName = Convert.ToString(item.Field<string>("AttendanceType"))
                                     }).ToList();
                return getList;
            }
            return getList;
        }

        /// <summary>
        /// To fetch SFA's MTD Attendance report for mobile.
        /// </summary>
        /// <param name="Input">SFA Id</param>
        /// <returns>SFA's MTD Attendance Report Data</returns>
        public SFAMTDAttendanceList GetSFAMTDAttendanceReport(SFAMTDAttendanceInput Input)
        {
            var getList = new SFAMTDAttendanceList();

            DbParameterCollection sqlparam = new DbParameterCollection();
            sqlparam.Add(new DbParameter("@SFAId", Input.SFAId, DbType.Int64));

            var datatable = _dataHelper.ExecuteDataSet("[dbo].[GetSFAMTDAttendance]", sqlparam, CommandType.StoredProcedure);

            if (datatable.Tables[0].Rows.Count > 0)
            {
                getList.SFAMTDAttendance = (from item in datatable.Tables[0].AsEnumerable()
                                            select new SFAMTDAttendance
                                            {
                                                Date = Convert.ToString(item.Field<string>("CDate")),
                                                Status = Convert.ToString(item.Field<string>("Status"))
                                            }).ToList();
                return getList;
            }
            return getList;
        }

        #region Demo Stock Ranging
        /// <summary>
        /// To insert SFA's demo stock ranging information subbmited from Mobile App.
        /// Dhruv Sharma, ValueFirst
        /// May 2, 2018
        /// </summary>
        /// <param name="Data">List of information to be submitted.</param>
        /// <param name="ErrorMessage">Message returned from database.</param>
        /// <returns>Status of the operation.</returns>
        public bool InsertSFADemoStockRanging(SFADemoStockRangingModel Data, out string ErrorMessage)
        {
            ErrorMessage = "";
            DbParameterCollection SqlParam;
            int Count = 0;
            foreach (var Model in Data.DemoStockRangingModel)
            {
                SqlParam = new DbParameterCollection();
                SqlParam.Add(new DbParameter("@SFA_Id", Data.SFAId, DbType.Int64));
                SqlParam.Add(new DbParameter("@ProductCategoryId", Data.ProductCategoryId, DbType.Int64));
                SqlParam.Add(new DbParameter("@ProductSubCategoryId", Data.ProductSubCategoryId, DbType.Int64));
                SqlParam.Add(new DbParameter("@ModelId", Model.ModelId, DbType.Int64));
                SqlParam.Add(new DbParameter("@Quantity", Model.Quantity, DbType.Int64));

                var datatable = _dataHelper.ExecuteDataTable("[dbo].[Insert_SFA_DemoStockRanging]", SqlParam, CommandType.StoredProcedure);

                if (Convert.ToInt32(datatable.Rows[0]["ErrorCode"]) == 1)
                    Count = Count + 1;
            }
            if (Count == Data.DemoStockRangingModel.Count())
            {
                ErrorMessage = "Data submitted successfully.";
                return true;
            }
            else if (Count != 0)
            {
                ErrorMessage = "Data submitted successfully.";
                return false;
            }
            else
            {
                ErrorMessage = "Data not submitted. Please try again.";
                return false;
            }
        }

        /// <summary>
        /// To get Demo Ranging Stock Data.
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 2, 2018
        /// </summary>
        /// <param name="Input">SFAId, ProductCategoryId, ProductSubCategoryId</param>
        /// <returns>Demo Ranging Stock Data</returns>
        public DemoRangingStockDataList GetDemoRangingStockData(DemoRagingStockInput Input)
        {
            var getList = new DemoRangingStockDataList();

            DbParameterCollection sqlparam = new DbParameterCollection();
            sqlparam.Add(new DbParameter("@SFA_Id", Input.SFAId, DbType.Int64));
            sqlparam.Add(new DbParameter("@ProductCategoryId", Input.ProductCategoryId, DbType.Int64));
            var datatable = _dataHelper.ExecuteDataSet("[dbo].[GetSFADemoStockRangingData]", sqlparam, CommandType.StoredProcedure);

            if (datatable.Tables[0].Rows.Count > 0)
            {
                getList.DemoRangingStockData = (from item in datatable.Tables[0].AsEnumerable()
                                                select new DemoRangingStockData
                                                {
                                                    ActualDate = Convert.ToString(item.Field<DateTime>("ActualDate").ToString("dd MMM yyyy")),
                                                    DisplayQuantity = Convert.ToString(item.Field<Int32>("DisplayQuantity")),
                                                    MaterialCode = Convert.ToString(item.Field<string>("MaterialCode")),
                                                    PlannedQuantity = Convert.ToString(item.Field<Int32>("PlannedQuantity")),
                                                    Week = Convert.ToString(item.Field<string>("Week"))
                                                }).ToList();
                return getList;
            }
            return getList;
        }
        #endregion

        #region Competitor Tracking
        /// <summary>
        /// To insert Competitor Sales Thru
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 7, 2018
        /// </summary>
        /// <param name="Data">Fields to push</param>
        /// <param name="ErrorMessage">Output error message</param>
        /// <returns>Status of the operation</returns>
        public bool InsertCompetitorSaleThru(SFACompetitionTrackingModel Data, out string ErrorMessage)
        {
            ErrorMessage = "";
            DbParameterCollection SqlParam;
            SqlParam = new DbParameterCollection();
            SqlParam.Add(new DbParameter("@CompanyId", Data.CompanyId, DbType.Int64));
            SqlParam.Add(new DbParameter("@ProductId", Data.ProductId, DbType.Int64));
            SqlParam.Add(new DbParameter("@DealerId", Data.DealerId, DbType.Int64));
            SqlParam.Add(new DbParameter("@UserId", Data.UserId, DbType.Int64));
            SqlParam.Add(new DbParameter("@Date", Data.Date, DbType.DateTime));

            var datatable = _dataHelper.ExecuteDataTable("[dbo].[InsertCompetitorSaleThru]", SqlParam, CommandType.StoredProcedure);

            if (Convert.ToInt32(datatable.Rows[0]["ErrorCode"]) == 1)
            {
                Int64 SalesId = Convert.ToInt64(datatable.Rows[0]["Id"]);
                return InsertSalesDetails(Data, out ErrorMessage, SalesId);
            }
            else
            {
                ErrorMessage = Convert.ToString(datatable.Rows[0]["ErrorMessage"]);
                return false;
            }
        }

        /// <summary>
        /// To update Competitor Sales Thru
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 7, 2018
        /// </summary>
        /// <param name="Data">Fields to push</param>
        /// <param name="ErrorMessage">Output error message</param>
        /// <returns>Status of the operation</returns>
        public bool UpdateCompetitorSaleThru(SFACompetitionTrackingModel Data, out string ErrorMessage)
        {
            ErrorMessage = "";
            DbParameterCollection SqlParam;
            SqlParam = new DbParameterCollection();
            SqlParam.Add(new DbParameter("@CompanyId", Data.CompanyId, DbType.Int64));
            SqlParam.Add(new DbParameter("@ProductId", Data.ProductId, DbType.Int64));
            SqlParam.Add(new DbParameter("@UserId", Data.UserId, DbType.Int64));
            SqlParam.Add(new DbParameter("@SalesId", Data.SalesId, DbType.Int64));
            SqlParam.Add(new DbParameter("@Date", Data.Date, DbType.DateTime));

            var datatable = _dataHelper.ExecuteDataTable("[dbo].[UpdateCompetitorSalesThru]", SqlParam, CommandType.StoredProcedure);

            if (Convert.ToInt32(datatable.Rows[0]["ErrorCode"]) == 1)
            {
                return InsertSalesDetails(Data, out ErrorMessage, Data.SalesId);
            }
            else
            {
                ErrorMessage = Convert.ToString(datatable.Rows[0]["ErrorMessage"]);
                return false;
            }
        }

        /// <summary>
        /// To insert sales details after creation or updation of sale.
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 7, 2018
        /// </summary>
        /// <param name="Data">Values to insert</param>
        /// <param name="ErrorMessage">Output message</param>
        /// <param name="SalesId">Sales Lead Id</param>
        /// <returns>Status of the operation.</returns>
        private bool InsertSalesDetails(SFACompetitionTrackingModel Data, out string ErrorMessage, long SalesId)
        {
            int Count = 0;
            DbParameterCollection SqlParam;
            foreach (var Model in Data.TackingMaterial)
            {
                SqlParam = new DbParameterCollection();
                SqlParam = new DbParameterCollection();
                SqlParam.Add(new DbParameter("@MaterialName", Model.MaterialName, DbType.String));
                SqlParam.Add(new DbParameter("@SalesId", SalesId, DbType.Int64));
                SqlParam.Add(new DbParameter("@Quantity", Model.Quantity, DbType.Int64));
                SqlParam.Add(new DbParameter("@UserId", Data.UserId, DbType.Int64));
                SqlParam.Add(new DbParameter("@Date", Data.Date, DbType.DateTime));

                var datatable2 = _dataHelper.ExecuteDataTable("[dbo].[InsertCompetitorSalesDetails]", SqlParam, CommandType.StoredProcedure);

                if (Convert.ToInt32(datatable2.Rows[0]["ErrorCode"]) == 1)
                    Count = Count + 1;
            }
            if (Count != 0)
            {
                ErrorMessage = "Data submitted successfully.";
                return true;
            }
            else
            {
                ErrorMessage = "Data not submitted. Please try again.";
                return false;
            }
        }

        /// <summary>
        /// To get list of SFA Competition Tracking Data.
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 7, 2018
        /// </summary>
        /// <param name="Input">User Id</param>
        /// <returns>List of SFA Competition Tracking Data.</returns>
        public SFACompetitionTrackingList GetSFACompetitionTrackingData(CTInput Input)
        {
            var getList = new SFACompetitionTrackingList();

            DbParameterCollection sqlparam = new DbParameterCollection();
            sqlparam.Add(new DbParameter("@SFAId", Input.Id, DbType.Int64));

            var datatable = _dataHelper.ExecuteDataSet("[dbo].[GetCompetitorSaleReport]", sqlparam, CommandType.StoredProcedure);

            if (datatable.Tables[0].Rows.Count > 0)
            {
                getList.SFACompetitionTracking = (from item in datatable.Tables[0].AsEnumerable()
                                                  select new SFACompetitionTracking
                                                  {
                                                      SalesId = Convert.ToInt64(item.Field<Int64>("SalesId")),
                                                      Company = Convert.ToString(item.Field<string>("Company")),
                                                      CompanyId = Convert.ToInt64(item.Field<Int64>("CompanyId")),
                                                      SonyProductCatName = Convert.ToString(item.Field<string>("SonyProductCatName")),
                                                      SonyProductId = Convert.ToInt64(item.Field<Int64>("SonyProductId")),
                                                      ProductCategory = Convert.ToString(item.Field<string>("ProductCategory")),
                                                      MaterialName = Convert.ToString(item.Field<string>("MaterialName")),
                                                      Quantity = Convert.ToString(item.Field<string>("Quantity")),
                                                      Total = Convert.ToInt64(item.Field<Int64>("Total"))
                                                  }).ToList();
                return getList;
            }
            return getList;
        }

        /// <summary>
        /// To get Monthly Model Wise Competitor Sales Thru Report.
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 8, 2018
        /// </summary>
        /// <param name="Input">User Id</param>
        /// <returns>Monthly Model Wise Competitor Sales Thru Report Data</returns>
        public MonthlyModelWiseCompSalesThruList GetMonthlyModelWiseCompSalesThru(CTInput Input)
        {
            var getList = new MonthlyModelWiseCompSalesThruList();

            DbParameterCollection sqlparam = new DbParameterCollection();
            sqlparam.Add(new DbParameter("@LoginId", Input.Id, DbType.Int64));

            var datatable = _dataHelper.ExecuteDataSet("[dbo].[GetMonthlyModelWiseCompetition]", sqlparam, CommandType.StoredProcedure);

            if (datatable.Tables[0].Rows.Count > 0)
            {
                getList.MonthlyModelWiseCompSalesThru = (from item in datatable.Tables[0].AsEnumerable()
                                                         select new MonthlyModelWiseCompSalesThru
                                                         {
                                                             Id = Convert.ToInt64(item.Field<Int64>("id")),
                                                             Product = Convert.ToString(item.Field<string>("Product")),
                                                             Model = Convert.ToString(item.Field<string>("Model")),
                                                             Quantity = Convert.ToString(item.Field<string>("Quantity"))
                                                         }).ToList();
                return getList;
            }
            return getList;
        }

        /// <summary>
        /// To get Counter Share Tracking Report.
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 8, 2018
        /// </summary>
        /// <param name="Input">User Id</param>
        /// <returns>Counter Share Tracking Report.</returns>
        public CounterShareTrackingReport GetCounterShareTrackingReport(CTInput Input)
        {
            var getList = new CounterShareTrackingReport();

            DbParameterCollection sqlparam = new DbParameterCollection();
            sqlparam.Add(new DbParameter("@LoginId", Input.Id, DbType.Int64));
            sqlparam.Add(new DbParameter("@ProductCategoryId", Input.ProductCategoryId, DbType.Int64));
            var datatable = _dataHelper.ExecuteDataSet("[dbo].[GetCounterShareTrackingReport]", sqlparam, CommandType.StoredProcedure);

            if (datatable.Tables[0].Rows.Count > 0)
            {
                getList.CounterShareTracking = (from item in datatable.Tables[0].AsEnumerable()
                                                select new CounterShareTracking
                                                {
                                                    Id = Convert.ToInt64(item.Field<Int64>("id")),
                                                    Brand = Convert.ToString(item.Field<string>("Brand")),
                                                    CurrentMonthSale = Convert.ToString(item.Field<string>("CurrentMonthSale")),
                                                    LastMonthSale = Convert.ToString(item.Field<string>("LastMonthSale")),
                                                    CurrentMonthShare = Convert.ToString(item.Field<string>("CurrentMonthShare"))
                                                }).ToList();
                return getList;
            }
            return getList;
        }
        #endregion

        #region Competitor Display SKU
        /// <summary>
        /// To insert Competitor Display SKU data.
        /// </summary>
        /// <param name="Data">Input parametres to insert.</param>
        /// <param name="ErrorMessage">Output Message from DB Procedure.</param>
        /// <returns>Status of the operation.</returns>
        public bool InsertCompetitorDisplaySKU(CompetitorDisplaySKU Data, out string ErrorMessage)
        {
            ErrorMessage = "";
            int Count = 0;
            DbParameterCollection SqlParam;
            for (int i = 0; i < Data.CompetitorDisplaySKUModels.Count(); i++)
            {
                SqlParam = new DbParameterCollection();
                SqlParam.Add(new DbParameter("@CompetitorCompanyId", Data.CompetitorCompanyId, DbType.Int64));
                SqlParam.Add(new DbParameter("@ProductCategoryId", Data.ProductCategoryId, DbType.Int64));
                SqlParam.Add(new DbParameter("@ModelId", Data.CompetitorDisplaySKUModels[i].ModelId, DbType.Int64));
                SqlParam.Add(new DbParameter("@Quantity", Data.CompetitorDisplaySKUModels[i].Quantity, DbType.Int64));
                SqlParam.Add(new DbParameter("@LoginId", Data.LoginId, DbType.Int64));

                var datatable = _dataHelper.ExecuteDataTable("[dbo].[InsertCompetitorDisplaySKU]", SqlParam, CommandType.StoredProcedure);

                if (Convert.ToInt32(datatable.Rows[0]["ErrorCode"]) == 1)
                {
                    Count = Count + 1;
                }
            }
            if (Count == Data.CompetitorDisplaySKUModels.Count())
            {
                ErrorMessage = "Data inserted successfully.";
                return true;
            }
            else
            {
                ErrorMessage = "Something went wrong. Please try again.";
                return false;
            }
        }

        /// <summary>
        /// To get Competitor Display SKU Report.
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 8, 2018
        /// </summary>
        /// <param name="Input">login Id</param>
        /// <returns>Competitor Display SKU Report</returns>
        public CompetitorDisplaySKUReportList GetCompetitorDisplaySKUReport(CTInput Input)
        {
            var getList = new CompetitorDisplaySKUReportList();

            DbParameterCollection sqlparam = new DbParameterCollection();
            sqlparam.Add(new DbParameter("@LoginId", Input.Id, DbType.Int64));
            sqlparam.Add(new DbParameter("@ProductCategoryId", Input.ProductCategoryId, DbType.Int64));

            var datatable = _dataHelper.ExecuteDataSet("[dbo].[GetCompDisplaySKUReport]", sqlparam, CommandType.StoredProcedure);

            if (datatable.Tables[0].Rows.Count > 0)
            {
                getList.CompetitorDisplaySKUReport = (from item in datatable.Tables[0].AsEnumerable()
                                                      select new CompetitorDisplaySKUReport
                                                      {
                                                          CompetitionCode = Convert.ToString(item.Field<string>("CompetitionCode")),
                                                          CompetitionProductCategory = Convert.ToString(item.Field<string>("CompetitionProductCategory")),
                                                          Model = Convert.ToString(item.Field<string>("Model")),
                                                          Quantity = Convert.ToInt64(item.Field<Int64>("Quantity"))
                                                      }).ToList();
                return getList;
            }
            return getList;
        }
        #endregion

        #region Competition Headcount Report
        /// <summary>
        /// To insert Competition Head Count
        /// Dhruv Sharma
        /// </summary>
        /// <param name="Data">Input to insert.</param>
        /// <param name="ErrorMessage">Ouput Error Message</param>
        /// <returns>Status of the operation.</returns>
        public bool InsertCompetitionHeadCount(CompetitionHeadCountModel Data, out string ErrorMessage)
        {
            ErrorMessage = "";
            int Count = 0;
            DbParameterCollection SqlParam;
            for (int i = 0; i < Data.CompetitionSFACount.Count(); i++)
            {
                SqlParam = new DbParameterCollection();
                SqlParam.Add(new DbParameter("@SFAId", Data.SFAId, DbType.Int64));
                SqlParam.Add(new DbParameter("@CompanyId", Data.CompanyId, DbType.Int64));
                SqlParam.Add(new DbParameter("@TypeId", Data.CompetitionSFACount[i].TypeId, DbType.Int64));
                SqlParam.Add(new DbParameter("@SFACount", Data.CompetitionSFACount[i].SFACount, DbType.Int64));

                var datatable = _dataHelper.ExecuteDataTable("[dbo].[InsertCompetitionSFACount]", SqlParam, CommandType.StoredProcedure);

                if (Convert.ToInt32(datatable.Rows[0]["ErrorCode"]) == 1)
                {
                    Count = Count + 1;
                }
            }
            if (Count == Data.CompetitionSFACount.Count())
            {
                ErrorMessage = "Data inserted successfully.";
                return true;
            }
            else
            {
                ErrorMessage = "Something went wrong. Please try again.";
                return false;
            }
        }

        /// <summary>
        /// To get Competition Head Count Report data.
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 9, 2018
        /// </summary>
        /// <param name="Input">SFA Id</param>
        /// <returns>Competition Head Count Report data.</returns>
        public CompetitorHeadCountReport GetCompetitorHeadCountReport(CTInput Input)
        {
            var getList = new CompetitorHeadCountReport();

            DbParameterCollection sqlparam = new DbParameterCollection();
            sqlparam.Add(new DbParameter("@SFAId", Input.Id, DbType.Int64));

            var datatable = _dataHelper.ExecuteDataSet("[dbo].[GetCompetitionHeadcountReport]", sqlparam, CommandType.StoredProcedure);

            if (datatable.Tables[0].Rows.Count > 0)
            {
                getList.CompetitionHeadCount = (from item in datatable.Tables[0].AsEnumerable()
                                                select new CompetitionHeadCount
                                                {
                                                    Company = Convert.ToString(item.Field<string>("Company")),
                                                    CountType = Convert.ToString(item.Field<string>("CountType")),
                                                    HeadCount = Convert.ToString(item.Field<string>("HeadCount"))
                                                }).ToList();
                return getList;
            }
            return getList;
        }
        #endregion

        #region Message Broadcaster
        /// <summary>
        /// To submit Message Broadcated Reply.
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 10, 2018
        /// </summary>
        /// <param name="Data">Data to put</param>
        /// <param name="ErrorMessage">Output Message</param>
        /// <returns>Status of operation</returns>
        public bool SubmitMessageReply(SFAMessageBroadcasterModel Data, out string ErrorMessage)
        {
            try
            {
                ErrorMessage = "";
                DbParameterCollection SqlParam;
                SqlParam = new DbParameterCollection();
                SqlParam.Add(new DbParameter("@BroadcastMessageId", Data.BroadcastMessageId, DbType.Int64));
                SqlParam.Add(new DbParameter("@SFAId", Data.SFAId, DbType.Int64));
                SqlParam.Add(new DbParameter("@Reply", Data.Reply, DbType.String));
                SqlParam.Add(new DbParameter("@FileExtention", Data.FileExtention, DbType.String));

                var datatable = _dataHelper.ExecuteDataTable("[dbo].[SubmitBroadcastMessageReply]", SqlParam, CommandType.StoredProcedure);
                ErrorMessage = Convert.ToString(datatable.Rows[0]["ReturnMessage"]);
                if (Convert.ToInt32(datatable.Rows[0]["ReturnStatus"]) == 1)
                {
                    if (Data.File != null && Data.File != "")
                    {
                        byte[] bytesFile = Convert.FromBase64String(Data.File);
                        MemoryStream ms = new MemoryStream(bytesFile, 0, bytesFile.Length);
                        string strFilePath = Path.Combine(HttpContext.Current.Server.MapPath("~/PhotoReport/BroadcastMessageReply/"), Convert.ToString(datatable.Rows[0]["FileName"]));
                        FileStream fs = new FileStream(strFilePath, FileMode.Create);

                        ms.WriteTo(fs);
                        ms.Close();
                        fs.Close();
                        fs.Dispose();
                    }
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                ErrorMessage = "Something went wrong. Please try again.";
                return false;
            }
        }

        /// <summary>
        /// To get broadcasted messages of Role Selected.
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 10, 2018
        /// </summary>
        /// <param name="Input">SFA Id</param>
        /// <returns>Broadcasted messages of SFA.</returns>
        public MessageBroadcasterList GetMessageBroadcasterList(MessageListProcInput Input)
        {
            var getList = new MessageBroadcasterList();

            DbParameterCollection sqlparam = new DbParameterCollection();
            sqlparam.Add(new DbParameter("@UserId", Input.UserId, DbType.Int64));

            var datatable = _dataHelper.ExecuteDataSet("[dbo].[GetMessageBroadcasterData_V1]", sqlparam, CommandType.StoredProcedure);

            if (datatable.Tables[0].Rows.Count > 0)
            {
                getList.Messages = (from item in datatable.Tables[0].AsEnumerable()
                                    select new MessageBroadcasterModel
                                    {
                                        BroadcastMessageId = Convert.ToString(item.Field<string>("BroadcastMessageId")),
                                        FileName = System.Configuration.ConfigurationManager.AppSettings["BroadMesImagePath"].ToString() + "/Uploads/Broadcast/" + Convert.ToString(item.Field<string>("FileName")),
                                        Message = Convert.ToString(item.Field<string>("Message")),
                                        StartDate = Convert.ToString(item.Field<string>("StartDate")),
                                        EndDate = Convert.ToString(item.Field<string>("EndDate")),
                                        Name = Convert.ToString(item.Field<string>("Name")),
                                        Status = Convert.ToBoolean(item.Field<bool>("Status"))
                                    }).ToList();
                return getList;
            }
            return getList;
        }
        #endregion

        #region SFA Login Validation
        /// <summary>
        /// To validate SFA mobile login.
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 11, 2018
        /// </summary>
        /// <param name="Input">Login Details</param>
        /// <param name="Message">Output Message</param>
        /// <returns>User Details</returns>
        public SFAUserValidation ValidateSFALogin(SFAValidationInput Input, out string Message)
        {
            var UserDetails = new SFAUserValidation();
            var UserDetailsasset = new SFAUserValidation();

            DbParameterCollection objDBParam = new DbParameterCollection();
            DbParameterCollection objDBParam1 = new DbParameterCollection();
            objDBParam.Add(new DbParameter("@UserName", Input.UserName, DbType.String));

            Input.Password = AMBOEcryption.GetHashValue(Input.Password.Trim());

            objDBParam.Add(new DbParameter("@UserPassword", Input.Password, DbType.String));
            objDBParam.Add(new DbParameter("@EncryptKey", Input.EncryptKey, DbType.String));
            objDBParam.Add(new DbParameter("@SessionTime", Input.SessionTime, DbType.DateTime));
            objDBParam.Add(new DbParameter("@IMEI", Input.IMEI, DbType.String));
            objDBParam.Add(new DbParameter("@FCMId", Input.FCMId, DbType.String));

            var datatable = _dataHelper.ExecuteDataTable("[dbo].[ValidateSFALogin]", objDBParam, CommandType.StoredProcedure);
            Message = Convert.ToString(datatable.Rows[0]["Message"]);
            if (Convert.ToInt32(datatable.Rows[0]["Status"]) == 1)
            {
                UserDetails = (from item in datatable.AsEnumerable()
                               select new SFAUserValidation
                               {
                                   //AssetIssued = Convert.ToString(item.Field<string>("AssetIssued")),
                                   Category = Convert.ToString(item.Field<string>("Category")),
                                   Dealer = Convert.ToString(item.Field<string>("CounterDetails")),
                                   EmailId = Convert.ToString(AMBOEcryption.DecryptData(item.Field<string>("MailId"), true)),
                                   DOJ = Convert.ToString(item.Field<string>("DOJ")),
                                   GPSLocation = Convert.ToString(item.Field<string>("GPSLocation")),
                                   Location = Convert.ToString(item.Field<string>("lcname")),
                                   MobileNo = Convert.ToString(item.Field<string>("mob_no")),
                                   Name = Convert.ToString(item.Field<string>("UserName")),
                                   Role = Convert.ToString(item.Field<string>("RoleName")),
                                   SFAId = Convert.ToInt64(item.Field<Int64>("UserId")),
                                   TimeIn = Convert.ToString(item.Field<string>("TimeIn")),
                                   SFALevel = Convert.ToString(item.Field<string>("SFALevel")),
                                   CurrentAppVersion = Convert.ToInt64(item.Field<Int64>("CurrentAppVersion")),
                                   URL = Convert.ToString(item.Field<string>("URL"))
                               }).FirstOrDefault();

                objDBParam1.Add(new DbParameter("@UserId", UserDetails.SFAId, DbType.Int64));
                var datatable1 = _dataHelper.ExecuteDataTable("[dbo].[GetAssetsAssigned]", objDBParam1, CommandType.StoredProcedure);
                UserDetailsasset = (from item1 in datatable1.AsEnumerable()
                               select new SFAUserValidation
                               {
                                   AssetIssued = Convert.ToString(item1.Field<string>("AssetIssued")),
                               }).FirstOrDefault();
                UserDetails.AssetIssued = UserDetailsasset.AssetIssued;
            }
            return UserDetails;
        }
        #endregion

        #region SFA App Sync
        /// <summary>
        /// To provide SFA Mobile sync data.
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 11, 2018
        /// </summary>
        /// <param name="Input">SFA Id</param>
        /// <returns>
        /// Dealer Data
        /// Attendance Type Data
        /// Sony Product, Sub Product and Materials
        /// Competitor Products, Subproducts and Materials
        /// </returns>
        public SFASyncModel SFAMobileSync(SyncInput Input)
        {
            SFASyncModel SyncData = new SFASyncModel();
            SyncData.SplashImage = ConfigurationManager.AppSettings["SitePath"].ToString();
            try
            {
                DbParameterCollection sqlparam = new DbParameterCollection();
                sqlparam.Add(new DbParameter("@SFAId", Input.SFAId, DbType.Int64));

                //Attendance Type Sync Data
                var datatable = _dataHelper.ExecuteDataTable("[dbo].[GetAttendanceType]", CommandType.StoredProcedure);
                if (datatable.Rows.Count > 0)
                {
                    SyncData.Attendance = (from item in datatable.AsEnumerable()
                                           select new SyncAttendance
                                           {
                                               ID = Convert.ToInt64(item.Field<Int64>("Id")),
                                               Value = Convert.ToString(item.Field<string>("AttendanceType"))
                                           }).ToList();
                }

                //Dealer Sync Data
                var datatable2 = new DataTable();
                datatable2 = _dataHelper.ExecuteDataTable("[dbo].[GetDealerSyncData]", sqlparam, CommandType.StoredProcedure);
                if (datatable2.Rows.Count > 0)
                {
                    SyncData.Dealer = (from item in datatable2.AsEnumerable()
                                       select new SyncDealer
                                       {
                                           ID = Convert.ToInt64(item.Field<Int64>("Id")),
                                           Code = Convert.ToString(item.Field<string>("DealerCode")),
                                           Name = Convert.ToString(item.Field<string>("DealerName")),
                                           Latitude = Convert.ToString(item.Field<string>("Latitude")),
                                           Longitude = Convert.ToString(item.Field<string>("Longitude"))
                                       }).ToList();
                }

                //Outlet Data (For RSO)
                DbParameterCollection sqlparam1 = new DbParameterCollection();
                sqlparam1.Add(new DbParameter("@UserId", Input.SFAId, DbType.Int64));
                var datatable6 = new DataTable();
                datatable6 = _dataHelper.ExecuteDataTable("[dbo].[GetOutletData]", sqlparam1, CommandType.StoredProcedure);
                if (datatable6.Rows.Count > 0)
                {
                    SyncData.SyncOutletData = (from item in datatable6.AsEnumerable()
                                               select new SyncOutletData
                                               {
                                                   OutletId = Convert.ToInt64(item.Field<Int64>("OutletId")),
                                                   OutletName = Convert.ToString(item.Field<string>("OutletName"))
                                               }).ToList();
                }

                //Sony Sync Data
                var datatable3 = new DataTable();
                datatable3 = _dataHelper.ExecuteDataTable("[dbo].[GetSonySyncData]", sqlparam, CommandType.StoredProcedure);
                if (datatable3.Rows.Count > 0)
                {
                    var ProductCategories = (from item in datatable3.AsEnumerable()
                                             select new ProductSyncData
                                             {
                                                 ProductCatId = Convert.ToInt64(item.Field<Int64>("ProductCatId")),
                                                 CategoryName = Convert.ToString(item.Field<string>("CategoryName")),
                                                 ProductSubCatId = Convert.ToInt64(item.Field<Int64>("ProductSubCatId")),
                                                 SubCategoryName = Convert.ToString(item.Field<string>("SubCategoryName")),
                                                 MaterialId = Convert.ToInt64(item.Field<Int64>("MaterialId")),
                                                 MaterialName = Convert.ToString(item.Field<string>("MaterialName")),
                                                 MaterialCode = Convert.ToString(item.Field<string>("MaterialCode"))
                                             }).ToList();

                    var ProductCat = (from item in ProductCategories.AsEnumerable()
                                      group item by item.ProductCatId into g
                                      select new SyncDefault
                                      {
                                          ID = Convert.ToInt64(g.Select(p => p.ProductCatId).FirstOrDefault()),
                                          Value = Convert.ToString(g.Select(p => p.CategoryName).FirstOrDefault())
                                      }).Distinct().ToList();

                    for (int i = 0; i < ProductCat.Count(); i++)
                    {
                        SyncProductCat Cat = new SyncProductCat();
                        Cat.ID = ProductCat[i].ID;
                        Cat.Value = ProductCat[i].Value;
                        var ProductSubCat = (from item in ProductCategories
                                             where item.ProductCatId == ProductCat[i].ID
                                             group item by item.ProductSubCatId into g
                                             select new SyncDefault
                                             {
                                                 ID = Convert.ToInt64(g.Select(p => p.ProductSubCatId).FirstOrDefault()),
                                                 Value = Convert.ToString(g.Select(p => p.SubCategoryName).FirstOrDefault())
                                             }).Distinct().ToList();
                        SyncProductSubCat SubCat = new SyncProductSubCat();
                        for (int j = 0; j < ProductSubCat.Count(); j++)
                        {
                            SubCat = new SyncProductSubCat();
                            SubCat.ID = ProductSubCat[j].ID;
                            SubCat.Value = ProductSubCat[j].Value;
                            SubCat.Material = (from item in ProductCategories
                                               where item.ProductCatId == ProductCat[i].ID && item.ProductSubCatId == ProductSubCat[j].ID
                                               select new SyncMaterial
                                               {
                                                   ID = Convert.ToInt64(item.MaterialId),
                                                   Value = Convert.ToString(item.MaterialName),
                                                   Code = Convert.ToString(item.MaterialCode),
                                               }).ToList();
                            Cat.ProductSubCategory.Add(SubCat);
                        }
                        SyncData.ProductCategory.Add(Cat);
                    }
                }

                //For Competitor Sync
                var datatable4 = new DataTable();
                datatable4 = _dataHelper.ExecuteDataTable("[dbo].[GetCompSyncData]", sqlparam, CommandType.StoredProcedure);
                if (datatable4.Rows.Count > 0)
                {
                    var SonyProductCategories = (from item in datatable4.AsEnumerable()
                                                 select new CompProductSyncData
                                                 {
                                                     SonyProductCatId = Convert.ToInt64(item.Field<Int64>("SonyProductCatId")),
                                                     SonyCategoryName = Convert.ToString(item.Field<string>("SonyCategoryName")),
                                                     CompetitorId = Convert.ToInt64(item.Field<Int64>("CompetitorId")),
                                                     CompetitorName = Convert.ToString(item.Field<string>("CompetitorName")),
                                                     CompetitorCode = Convert.ToString(item.Field<string>("CompetitorCode")),
                                                     ProductCatId = Convert.ToInt64(item.Field<Int64>("ProductCatId")),
                                                     CategoryName = Convert.ToString(item.Field<string>("CategoryName")),
                                                     MaterialId = Convert.ToInt64(item.Field<Int64>("MaterialId")),
                                                     MaterialName = Convert.ToString(item.Field<string>("MaterialName"))
                                                 }).ToList();

                    var SonyProductCat = (from item in SonyProductCategories.AsEnumerable()
                                          group item by item.SonyProductCatId into g
                                          select new SyncDefault
                                          {
                                              ID = Convert.ToInt64(g.Select(p => p.SonyProductCatId).FirstOrDefault()),
                                              Value = Convert.ToString(g.Select(p => p.SonyCategoryName).FirstOrDefault())
                                          }).Distinct().ToList();

                    for (int i = 0; i < SonyProductCat.Count(); i++)
                    {
                        SyncSonyProductCategory SCat = new SyncSonyProductCategory();
                        SCat.ID = SonyProductCat[i].ID;
                        SCat.Value = SonyProductCat[i].Value;
                        var Competitor = (from item in SonyProductCategories
                                          where item.SonyProductCatId == SonyProductCat[i].ID
                                          group item by item.CompetitorId into g
                                          select new SyncDefault
                                          {
                                              ID = Convert.ToInt64(g.Select(p => p.CompetitorId).FirstOrDefault()),
                                              Value = Convert.ToString(g.Select(p => p.CompetitorName).FirstOrDefault()),
                                              Code = Convert.ToString(g.Select(p => p.CompetitorCode).FirstOrDefault())
                                          }).Distinct().ToList();
                        for (int j = 0; j < Competitor.Count(); j++)
                        {
                            SyncCompany Comp = new SyncCompany();
                            Comp.ID = Competitor[j].ID;
                            Comp.Value = Competitor[j].Value;
                            Comp.Code = Competitor[j].Code;
                            var ProductSubCat = (from item in SonyProductCategories
                                                 where item.SonyProductCatId == SonyProductCat[i].ID && item.CompetitorId == Competitor[j].ID
                                                 group item by item.ProductCatId into g
                                                 select new SyncDefault
                                                 {
                                                     ID = Convert.ToInt64(g.Select(p => p.ProductCatId).FirstOrDefault()),
                                                     Value = Convert.ToString(g.Select(p => p.CategoryName).FirstOrDefault())
                                                 }).Distinct().ToList();
                            for (int k = 0; k < ProductSubCat.Count(); k++)
                            {
                                SyncProductSubCat CPCat = new SyncProductSubCat();
                                CPCat.ID = ProductSubCat[k].ID;
                                CPCat.Value = ProductSubCat[k].Value;
                                CPCat.Material = (from item in SonyProductCategories
                                                  where item.SonyProductCatId == SonyProductCat[i].ID && item.CompetitorId == Competitor[j].ID && item.ProductCatId == ProductSubCat[k].ID
                                                  select new SyncMaterial
                                                  {
                                                      ID = Convert.ToInt64(item.MaterialId),
                                                      Value = Convert.ToString(item.MaterialName)
                                                  }).ToList();
                                Comp.ProductCategory.Add(CPCat);
                            }
                            SCat.Company.Add(Comp);
                        }
                        SyncData.SonyProductCategory.Add(SCat);
                    }
                }
                return SyncData;
            }
            catch (Exception Ex)
            {
                SyncData = new SFASyncModel();
                return SyncData;
                //Error Log not yet implemented.
            }
        }
        #endregion

        #region Store Stock
        /// <summary>
        /// To insert Weekly Store Stock report.
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 14, 2018
        /// </summary>
        /// <param name="Input">Data to input.</param>
        /// <param name="ErrorMessage">Response error message.</param>
        /// <returns>Status of the operation.</returns>
        public bool InsertWeeklyStoreStock(WeeklyStoreStockModel Input, out string ErrorMessage)
        {
            ErrorMessage = "";
            DbParameterCollection SqlParam;
            SqlParam = new DbParameterCollection();
            SqlParam.Add(new DbParameter("@ProductCatId", Input.ProductCategoryId, DbType.Int64));
            SqlParam.Add(new DbParameter("@ProductSubCatId", Input.ProductSubCategoryId, DbType.Int64));
            SqlParam.Add(new DbParameter("@UserId", Input.SFAId, DbType.Int64));

            var datatable = _dataHelper.ExecuteDataTable("[dbo].[InsertWeeklyStoreStock]", SqlParam, CommandType.StoredProcedure);

            if (Convert.ToInt32(datatable.Rows[0]["ErrorCode"]) == 1)
            {
                Int64 SalesId = Convert.ToInt64(datatable.Rows[0]["Id"]);
                DbParameterCollection SqlParam1;
                int Count = 0;
                foreach (var Details in Input.WeeklyStoreStockDetails)
                {
                    SqlParam1 = new DbParameterCollection();
                    SqlParam1.Add(new DbParameter("@MaterialId", Details.MaterialId, DbType.String));
                    SqlParam1.Add(new DbParameter("@ClosingStockId", SalesId, DbType.Int64));
                    SqlParam1.Add(new DbParameter("@Quantity", Details.Quantity, DbType.Int64));
                    SqlParam1.Add(new DbParameter("@UserId", Input.SFAId, DbType.Int64));

                    var datatable2 = _dataHelper.ExecuteDataTable("[dbo].[InsertWeeklyStoreStockDetails]", SqlParam1, CommandType.StoredProcedure);

                    if (Convert.ToInt32(datatable2.Rows[0]["ErrorCode"]) == 1)
                        Count = Count + 1;
                }
                if (Count != 0)
                {
                    ErrorMessage = "Data submitted successfully.";
                    return true;
                }
                else
                {
                    ErrorMessage = "Data not submitted. Please try again.";
                    return false;
                }
            }
            else
            {
                ErrorMessage = Convert.ToString(datatable.Rows[0]["ErrorMessage"]);
                return false;
            }
        }

        /// <summary>
        /// To get Weekly Store Stock Data List.
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 14, 2018
        /// </summary>
        /// <param name="Input">SFA Id</param>
        /// <returns>List of Weekly Store Stock Data</returns>
        public WeeklyStoreStockDataList GetWeeklyStoreStockDataList(WeeklyStoreStockDataInput Input)
        {
            var getList = new WeeklyStoreStockDataList();

            DbParameterCollection sqlparam = new DbParameterCollection();
            sqlparam.Add(new DbParameter("@SFAId", Input.SFAId, DbType.Int64));

            var datatable = _dataHelper.ExecuteDataSet("[dbo].[GetWeeklyStoreStockDetails]", sqlparam, CommandType.StoredProcedure);

            if (datatable.Tables[0].Rows.Count > 0)
            {
                getList.WeeklyStoreStockData = (from item in datatable.Tables[0].AsEnumerable()
                                                select new WeeklyStoreStockData
                                                {
                                                    Id = Convert.ToInt64(item.Field<Int64>("Id")),
                                                    ProductCategory = Convert.ToString(item.Field<string>("ProductCategory")),
                                                    ProductSubCategory = Convert.ToString(item.Field<string>("ProductSubCategory")),
                                                    MaterialName = Convert.ToString(item.Field<string>("MaterialName")),
                                                    Quantity = Convert.ToInt64(item.Field<Int64>("Quantity"))
                                                }).ToList();
                return getList;
            }
            return getList;
        }
        #endregion

        /// <summary>
        /// To submit daily sales.
        /// </summary>
        /// <param name="Data">To submit</param>
        /// <param name="ErrorMessage">Output Message</param>
        /// <returns>Status of the operation</returns>
        public bool SubmitSaleThru(SFASaleThruSubmission Data, out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                DbParameterCollection SqlParam;
                SqlParam = new DbParameterCollection();
                SqlParam.Add(new DbParameter("@ProductCategoryId", Data.ProductCategoryId, DbType.Int64));
                SqlParam.Add(new DbParameter("@ProductSubCategoryId", Data.ProductSubCategoryId, DbType.Int64));
                SqlParam.Add(new DbParameter("@UserId", Data.UserId, DbType.Int64));
                SqlParam.Add(new DbParameter("@CustomerName", Data.CustomerName, DbType.String));
                SqlParam.Add(new DbParameter("@MobileNo", Data.MobileNo, DbType.String));
                SqlParam.Add(new DbParameter("@EmailId", Data.EmailId, DbType.String));
                SqlParam.Add(new DbParameter("@SerialNo", Data.SerialNo, DbType.String));
                SqlParam.Add(new DbParameter("@IsBulkSale", Data.IsBulkSale, DbType.Boolean));
                SqlParam.Add(new DbParameter("@Remarks", Data.Remarks, DbType.String));

                var datatable = _dataHelper.ExecuteDataTable("[dbo].[SubmitSalesThru]", SqlParam, CommandType.StoredProcedure);
                ErrorMessage = Convert.ToString(datatable.Rows[0]["ErrorMessage"]);
                if (Convert.ToInt32(datatable.Rows[0]["ErrorCode"]) == 1)
                {
                    Int64 SalesId = Convert.ToInt64(datatable.Rows[0]["SalesId"]);
                    DbParameterCollection SqlParam1;
                    foreach (var Model in Data.SFASaleThruModelType)
                    {
                        SqlParam1 = new DbParameterCollection();
                        SqlParam1.Add(new DbParameter("@DailySalesId", SalesId, DbType.Int64));
                        SqlParam1.Add(new DbParameter("@ProductCategoryId", Data.ProductCategoryId, DbType.Int64));
                        SqlParam1.Add(new DbParameter("@ProductSubCategoryId", Data.ProductSubCategoryId, DbType.Int64));
                        SqlParam1.Add(new DbParameter("@UserId", Data.UserId, DbType.Int64));
                        SqlParam1.Add(new DbParameter("@MaterialId", Model.ModelId, DbType.Int64));
                        SqlParam1.Add(new DbParameter("@Quantity", Model.Quantity, DbType.Int64));
                        SqlParam1.Add(new DbParameter("@IsBulk", Model.IsBulk));

                        var datatable1 = _dataHelper.ExecuteDataTable("[dbo].[SubmitSalesThruDetails]", SqlParam1, CommandType.StoredProcedure);
                        ErrorMessage = Convert.ToString(datatable1.Rows[0]["ErrorMessage"]);
                        if (Convert.ToInt32(datatable1.Rows[0]["ErrorCode"]) == 1)
                        {
                            DbParameterCollection SqlParam2;
                            SqlParam2 = new DbParameterCollection();
                            SqlParam2.Add(new DbParameter("@ProductCatId", Data.ProductCategoryId, DbType.Int64));
                            SqlParam2.Add(new DbParameter("@UserId", Data.UserId, DbType.Int64));
                            SqlParam2.Add(new DbParameter("@Achievement", Data.Achievement, DbType.Int64));

                            var datatable2 = _dataHelper.ExecuteDataTable("[dbo].[SubmitTargetAchievement]", SqlParam2, CommandType.StoredProcedure);
                            ErrorMessage = ErrorMessage + Convert.ToString(datatable2.Rows[0]["ErrorMessage"]);
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception Ex)
            {
                return false;
            }
        }

        /// <summary>
        /// To update read status of a broadcasted message.
        /// Nikhil Thakral, ValueFirst, Gurugram
        /// May 17, 2018
        /// </summary>
        /// <param name="objReadData"></param>
        /// <param name="Message"></param>
        /// <returns>true/false.</returns>
        public bool UpdateMessageReadStatus(BroadcastMessageStatus objReadData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@MessageId", String.Join(",",objReadData.MessageId), DbType.String));
                objDBParam.Add(new DbParameter("@UserId", objReadData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[UpdateMessageReadStatus]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                return Convert.ToBoolean(outputFromSP.Rows[0]["Status"]);
            }
            catch (Exception ex)
            {
                //Write Application Error log here
                Message = "Could not update read message status. Exception occured in API.";
            }
            return false;
        }

        /// <summary>
        /// To update sales thru
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 24, 2018
        /// </summary>
        /// <param name="Data">Data to be updated</param>
        /// <param name="ErrorMessage">Output Message</param>
        /// <returns>Status of operation</returns>
        public bool UpdateSaleThru(SFASaleThruSubmission Data, out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                DbParameterCollection SqlParam;
                SqlParam = new DbParameterCollection();
                SqlParam.Add(new DbParameter("@SaleId", Data.DailySalesId, DbType.Int64));
                SqlParam.Add(new DbParameter("@ProductCategoryId", Data.ProductCategoryId, DbType.Int64));
                SqlParam.Add(new DbParameter("@ProductSubCategoryId", Data.ProductSubCategoryId, DbType.Int64));
                SqlParam.Add(new DbParameter("@UserId", Data.UserId, DbType.Int64));
                SqlParam.Add(new DbParameter("@CustomerName", Data.CustomerName, DbType.String));
                SqlParam.Add(new DbParameter("@MobileNo", Data.MobileNo, DbType.String));
                SqlParam.Add(new DbParameter("@EmailId", Data.EmailId, DbType.String));
                SqlParam.Add(new DbParameter("@IsBulkSale", Data.IsBulkSale, DbType.Boolean));
                SqlParam.Add(new DbParameter("@Remarks", Data.Remarks, DbType.String));

                var datatable = _dataHelper.ExecuteDataTable("[dbo].[UpdateSalesThru]", SqlParam, CommandType.StoredProcedure);
                ErrorMessage = Convert.ToString(datatable.Rows[0]["ErrorMessage"]);
                if (Convert.ToInt32(datatable.Rows[0]["ErrorCode"]) == 1)
                {
                    Int64 PreProduct = Convert.ToInt64(datatable.Rows[0]["PreProduct"]);
                    Int64 PreQuantity = Convert.ToInt64(datatable.Rows[0]["Quantity"]);
                    DbParameterCollection SqlParam1;
                    foreach (var Model in Data.SFASaleThruModelType)
                    {
                        SqlParam1 = new DbParameterCollection();
                        SqlParam1.Add(new DbParameter("@DailySalesId", Data.DailySalesId, DbType.Int64));
                        SqlParam1.Add(new DbParameter("@ProductCategoryId", Data.ProductCategoryId, DbType.Int64));
                        SqlParam1.Add(new DbParameter("@ProductSubCategoryId", Data.ProductSubCategoryId, DbType.Int64));
                        SqlParam1.Add(new DbParameter("@UserId", Data.UserId, DbType.Int64));
                        SqlParam1.Add(new DbParameter("@MaterialId", Model.ModelId, DbType.Int64));
                        SqlParam1.Add(new DbParameter("@Quantity", Model.Quantity, DbType.Int64));

                        var datatable1 = _dataHelper.ExecuteDataTable("[dbo].[SubmitSalesThruDetails]", SqlParam1, CommandType.StoredProcedure);
                        ErrorMessage = Convert.ToString(datatable1.Rows[0]["ErrorMessage"]);
                        if (Convert.ToInt32(datatable1.Rows[0]["ErrorCode"]) == 1)
                        {
                            int IsSameProduct = 0;
                            if (PreProduct == Data.ProductCategoryId)
                                IsSameProduct = 1;
                            else
                                IsSameProduct = 0;
                            DbParameterCollection SqlParam2;
                            SqlParam2 = new DbParameterCollection();
                            SqlParam2.Add(new DbParameter("@OldProductCategoryId", PreProduct, DbType.Int64));
                            SqlParam2.Add(new DbParameter("@ProductCategoryId", Data.ProductCategoryId, DbType.Int64));
                            SqlParam2.Add(new DbParameter("@UserId", Data.UserId, DbType.Int64));
                            SqlParam2.Add(new DbParameter("@OldQuantity", PreQuantity, DbType.Int64));
                            SqlParam2.Add(new DbParameter("@NewQuantity", Model.Quantity, DbType.Int64));
                            SqlParam2.Add(new DbParameter("@IsSameProductCat", IsSameProduct, DbType.Boolean));

                            var datatable2 = _dataHelper.ExecuteDataTable("[dbo].[UpdateSalesThruAchievement]", SqlParam2, CommandType.StoredProcedure);
                            ErrorMessage = ErrorMessage + Convert.ToString(datatable2.Rows[0]["ErrorMessage"]);
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception Ex)
            {
                return false;
            }
        }

        /// <summary>
        /// To update SFA user profile from App
        /// Dhruv Sharma, Vfirst, Gurgaon
        /// June 1, 2018
        /// </summary>
        /// <param name="Data">Input</param>
        /// <param name="ErrorMessage">Message</param>
        /// <returns>Status of the operation</returns>
        public bool UpdateSFAUser(SFAProfileUpdateInput Data, out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                DbParameterCollection SqlParam;
                SqlParam = new DbParameterCollection();
                SqlParam.Add(new DbParameter("@UserId", Data.UserId, DbType.Int64));
                SqlParam.Add(new DbParameter("@Mobile", Data.Mobile, DbType.String));
                SqlParam.Add(new DbParameter("@EmailId", (Data.EmailId == null) ? "" : AMBOEcryption.EncryptData(Data.EmailId.Trim(), true), DbType.String));

                var datatable = _dataHelper.ExecuteDataTable("[dbo].[UpdateUserDetailsByApp]", SqlParam, CommandType.StoredProcedure);
                ErrorMessage = Convert.ToString(datatable.Rows[0]["ErrorMessage"]);
                if (Convert.ToInt32(datatable.Rows[0]["ErrorCode"]) == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception Ex)
            {
                return false;
            }
        }
		
		public List<BrandList> GetActiveBrands()
        {
            var getList = new List<BrandList>();
            var datatable = _dataHelper.ExecuteDataTable("[dbo].[GetActiveBrands]", CommandType.StoredProcedure);

            if (datatable.Rows.Count > 0)
            {
                getList = (from item in datatable.AsEnumerable()
                                     select new BrandList
                                     {
                                         BrandId = Convert.ToInt64(item.Field<Int64>("BrandId")),
                                         BrandName = Convert.ToString(item.Field<string>("BrandName"))
                                     }).ToList();
                return getList;
            }
            return getList;
        }

        public List<CompetitionCountType> GetCompetitionCountTypes(CompetitorCountTypeInput Input)
        {
            DbParameterCollection SqlParam = new DbParameterCollection();
            SqlParam.Add(new DbParameter("@UserId", Input.UserId, DbType.Int64));
            SqlParam.Add(new DbParameter("@CompetitorId", Input.CompetitorId, DbType.Int64));
            var getList = new List<CompetitionCountType>();
            var datatable = _dataHelper.ExecuteDataTable("[dbo].[GetCompetitionCountTypes_V2]",SqlParam, CommandType.StoredProcedure);

            if (datatable.Rows.Count > 0)
            {
                getList = (from item in datatable.AsEnumerable()
                           select new CompetitionCountType
                           {
                               Id = Convert.ToInt64(item.Field<Int64>("Id")),
                               Type = Convert.ToString(item.Field<string>("Type"))
                           }).ToList();
                return getList;
            }
            return getList;
        }

        public List<TrainingList> GetTrainingsBySFAId(TrainingSearch objSearchData, out string Message)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            List<TrainingList> objOutput = new List<TrainingList>();
            try
            {
                objDBParam.Add(new DbParameter("@SFAId", objSearchData.SFAId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetTrainingsBySFAId]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    objOutput = (from training in outputFromSP.AsEnumerable()
                                  select new TrainingList
                                  {
                                      TrainingId = Convert.ToInt64(training.Field<Int64>("TrainingId")),
                                      TrainerName = Convert.ToString(training.Field<string>("TrainerName")),
                                      CourseName = Convert.ToString(training.Field<string>("CourseName")),
                                      //ProductCategory = Convert.ToString(training.Field<string>("ProductCategory")),
                                      ChannelName = Convert.ToString(training.Field<string>("ChannelName")),
                                      FromDate = Convert.ToString(training.Field<string>("FromDate")),
                                      ToDate = Convert.ToString(training.Field<string>("ToDate")),
                                      IsActive = Convert.ToBoolean(training.Field<bool>("IsActive"))
                                  }).ToList();
                    Message = "Trainings for this SFA fetched successfully.";
                }
                else
                    Message = "No Trainings found for this SFA.";
            }
            catch (Exception ex)
            {
                Message = "An exception occured. Please try later or contact your administrator.";
            }
            return objOutput;
        }

        public FeedbackForm GetFeedbackForm(SFAFeedbackDataInput Input, out string Message)
        {
            FeedbackForm objFeedbackForm = new FeedbackForm();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@TrainingId", Input.TrainingId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataSet("[dbo].[GetFeedbackForm]",objDBParam, CommandType.StoredProcedure);
                if (outputFromSP == null)
                    Message = "No feedback form found. Please contact administrator.";
                else if(outputFromSP.Tables[0] != null && outputFromSP.Tables[0].Rows.Count > 0 &&
                    outputFromSP.Tables[1] != null && outputFromSP.Tables[1].Rows.Count > 0)
                {
                    objFeedbackForm.Titles = (from title in outputFromSP.Tables[0].AsEnumerable()
                                              select new FeedbackFormTitles
                                              {
                                                  TitleId = Convert.ToInt64(title.Field<Int64>("TitleId")),
                                                  TitleName = Convert.ToString(title.Field<string>("TitleName"))
                                              }).ToList();

                    foreach(FeedbackFormTitles title in objFeedbackForm.Titles)
                    {
                        title.SubTitles = (from subtitle in outputFromSP.Tables[1].AsEnumerable().Where(x => x.Field<Int64>("TitleId") == title.TitleId) 
                                           select new FeedbackFormSubTitles
                                           {
                                               SubTitleId = Convert.ToInt64(subtitle.Field<Int64>("SubTitleId")),
                                               SubTitleName = Convert.ToString(subtitle.Field<string>("SubTitleName"))
                                           }).ToList();
                    }
                    Message = "Feedback form fetched successfully.";
                }
                else
                    Message = "No feedback form found. Please contact administrator.";
            }
            catch(Exception ex)
            {
                Message = "Exception occured while fetching feedback form. Please contact administrator.";
            }
            return objFeedbackForm;
        }

        public bool ManageSFATrainingFeedback(SFAFeedbackData objFeedbackData, out string Message)
        {
            Message = string.Empty;
            SqlParameter[] objDBParam = new SqlParameter[4];
            DataTable dt = new DataTable();
            DataRow row;
            try
            {
                if(objFeedbackData.SFAId == 0 || objFeedbackData.TrainingId == 0 || objFeedbackData.Comment == null || objFeedbackData.Comment == "")
                {
                    Message = "Invalid data entered. Please try again or contact your administrator.";
                    return false;
                }

                dt.Columns.Add("SubTitleId");
                dt.Columns.Add("Rating");

                if(objFeedbackData != null && objFeedbackData.FeedbackRatings != null && objFeedbackData.FeedbackRatings.Count > 0)
                {
                    foreach (TrainingRatingList ratingmap in objFeedbackData.FeedbackRatings)
                    {
                        row = dt.NewRow();
                        row["SubTitleId"] = ratingmap.SubTitleId;
                        row["Rating"] = ratingmap.Rating;
                        dt.Rows.Add(row);
                    }
                }
                else
                {
                    Message = "No ratings submitted. Please try again or contact your administrator.";
                    return false;
                }

                objDBParam[0] = new SqlParameter("@SFAId", objFeedbackData.SFAId);
                objDBParam[1] = new SqlParameter("@TrainingId", objFeedbackData.TrainingId);
                objDBParam[2] = new SqlParameter("@Comment", objFeedbackData.Comment);
                objDBParam[3] = new SqlParameter("@dtRatings", dt);
                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[ManageSFATrainingFeedback]", objDBParam);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return true;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return false;
        }

        /// <summary>
        /// To update user password from App
        /// Dhruv Sharma, Vfirst, Gurgaon
        /// June 24, 2018
        /// </summary>
        /// <param name="Data">Input</param>
        /// <param name="ErrorMessage">Message</param>
        /// <returns>Status of the operation</returns>
        public bool UpdateUserPassword(UserUpdatePasswordModel Data, out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                DbParameterCollection SqlParam;
                SqlParam = new DbParameterCollection();
                SqlParam.Add(new DbParameter("@UserId", Data.UserId, DbType.Int64));
                SqlParam.Add(new DbParameter("@Password", AMBOEcryption.GetHashValue(Data.Password), DbType.String));

                var datatable = _dataHelper.ExecuteDataTable("[dbo].[UpdateUserPasswordfromApp]", SqlParam, CommandType.StoredProcedure);
                ErrorMessage = Convert.ToString(datatable.Rows[0]["ErrorMessage"]);
                if (Convert.ToInt32(datatable.Rows[0]["ErrorCode"]) == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception Ex)
            {
                return false;
            }
        }

        public int SubmitComboSalesData(ComboSales InputData, out string Message)
        {
            Message = string.Empty;
            string path = string.Empty;
            string base64String = string.Empty;
            string combopath = string.Empty;
            string combobase64String = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                if (InputData.InvoiceImage != null)
                {
                    path = InputData.InvoiceImage.Length > 0 ? "Invoice_Images/" + InputData.UserId + "_" + InputData.InvoiceNumber + "_" + ".jpg" : "";
                    base64String = InputData.InvoiceImage;
                    if (!String.IsNullOrEmpty(InputData.InvoiceImage))
                    {
                        //move file path to controller
                        string filePath = System.Web.HttpContext.Current.Server.MapPath("~/" + path);
                        string DirectoryPath = System.Web.HttpContext.Current.Server.MapPath("~/" + "Invoice_Images");
                        if (!Directory.Exists(DirectoryPath))
                        {
                            Directory.CreateDirectory(DirectoryPath);
                        }
                        if (File.Exists(filePath))
                        {
                            File.SetAttributes(filePath, FileAttributes.Normal);
                            File.Delete(filePath);
                        }
                        File.WriteAllBytes(filePath, Convert.FromBase64String(base64String));
                    }
                }
                if (InputData.ComboInvoiceImage != null)
                {
                    combopath = InputData.ComboInvoiceImage.Length > 0 ? "Invoice_Images/" + InputData.UserId + "_" + InputData.ComboInvoiceNumber + "_" + ".jpg" : "";
                    combobase64String = InputData.ComboInvoiceImage;
                    if (!String.IsNullOrEmpty(InputData.ComboInvoiceImage))
                    {
                        //move file path to controller
                        string filePath = System.Web.HttpContext.Current.Server.MapPath("~/" + combopath);
                        string DirectoryPath = System.Web.HttpContext.Current.Server.MapPath("~/" + "Invoice_Images");
                        if (!Directory.Exists(DirectoryPath))
                        {
                            Directory.CreateDirectory(DirectoryPath);
                        }
                        if (File.Exists(filePath))
                        {
                            File.SetAttributes(filePath, FileAttributes.Normal);
                            File.Delete(filePath);
                        }
                        File.WriteAllBytes(filePath, Convert.FromBase64String(combobase64String));
                    }
                }
                objDBParam.Add(new DbParameter("@UserId", InputData.UserId, DbType.Int64));
                objDBParam.Add(new DbParameter("@DailySalesId", InputData.DailySalesId, DbType.Int64));
                objDBParam.Add(new DbParameter("@MaterialId", InputData.MaterialId, DbType.Int64));
                objDBParam.Add(new DbParameter("@ProductId", InputData.ProductId, DbType.Int64)); 
                objDBParam.Add(new DbParameter("@SubProductId", InputData.SubProductId, DbType.Int64));
                objDBParam.Add(new DbParameter("@SellingPrice", InputData.SellingPrice, DbType.Double));  
                objDBParam.Add(new DbParameter("@InvoiceNo", InputData.InvoiceNumber, DbType.String));
                objDBParam.Add(new DbParameter("@InvoiceImg", path, DbType.String));
                objDBParam.Add(new DbParameter("@ComboDailySalesId", InputData.ComboDailySalesId, DbType.Int64));
                objDBParam.Add(new DbParameter("@ComboMaterialId", InputData.ComboMaterialId, DbType.Int64));
                objDBParam.Add(new DbParameter("@ComboProductId", InputData.ComboProductId, DbType.Int64));
                objDBParam.Add(new DbParameter("@ComboSubProductId", InputData.ComboSubProductId, DbType.Int64));
                objDBParam.Add(new DbParameter("@ComboSellingPrice", InputData.ComboSellingPrice, DbType.Double));
                objDBParam.Add(new DbParameter("@ComboInvoiceNo", InputData.ComboInvoiceNumber, DbType.String));
                objDBParam.Add(new DbParameter("@ComboInvoiceImg", combopath, DbType.String));
                
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[SubmitComboSalesData]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return 0;
        }

        public List<ModelDropdownOutput> GetMTDModelforCombo(ModelDropdownInput InputData, out string Message)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            List<ModelDropdownOutput> objOutput = new List<ModelDropdownOutput>();
            try
            {
                objDBParam.Add(new DbParameter("@LoginId", InputData.UserId, DbType.Int64));
                objDBParam.Add(new DbParameter("@ProductSubCat", InputData.ProSubCatId, DbType.Int64));
                objDBParam.Add(new DbParameter("@DailySalesId", InputData.DailySalesId, DbType.Int64));
                objDBParam.Add(new DbParameter("@MaterialId", InputData.MaterialId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetMTDModelforCombo]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    objOutput = (from training in outputFromSP.AsEnumerable()
                                 select new ModelDropdownOutput
                                 {
                                     DailySalesId = Convert.ToInt64(training.Field<Int64>("DailySalesId")),
                                     MaterialId = Convert.ToInt64(training.Field<Int64>("MaterialId")),
                                     MaterialName = Convert.ToString(training.Field<string>("MaterialName")),
                                     
                                 }).ToList();
                    Message = "Materials for combo fetched successfully.";
                }
                else
                    Message = "No materials found.";
            }
            catch (Exception ex)
            {
                Message = "An exception occured. Please try later or contact your administrator.";
            }
            return objOutput;
        }

        public int CancelComboSale(CancelComboSalesInput InputData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@ComboId1", InputData.ComboId1, DbType.Int64));
                objDBParam.Add(new DbParameter("@ComboId2", InputData.ComboId2, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[CancelComboSale]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("CancelComboSale", ex.StackTrace, ex.Message);
                Message = "Could not update combo sale data. Exception occured in API.";
            }
            return 0;
        }
    }
}