using AmboLibrary.ReportsManagement;
using AmboLibrary.SFAMobileApp;
using AmboLibrary.UserValidation;
using AmboProvider.Interface;
using AmboUtilities.Helper;
using DBHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboProvider.Implimentation
{
    public class ReportProvider : IReportProvider
    {
        private readonly DbHelper _dataHelper;
        private readonly DbHelper _dataHelper1;
        private readonly IErrorLogProvider _IErrorLogProvider;

        public ReportProvider(IErrorLogProvider IErrorLogProvider)
        {
            _dataHelper = DbHelper.GetReportInstance("ReportConnection");
            _dataHelper1 = DbHelper.GetInstance("defaultConnection");
            _IErrorLogProvider = IErrorLogProvider;
        }

        /// <summary>
        /// To validate user login for SID App.
        /// </summary>
        /// <param name="UserDetails">User Cridentials</param>
        /// <param name="Message">Error Message</param>
        /// <returns>User Id, Name, Role Id, Role Name</returns>
        public UserDetailsModel ValidateLogin(SFAValidationInput UserDetails, out string Message)
        {
            UserDetailsModel UserData = new UserDetailsModel();
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@UserName", UserDetails.UserName, DbType.String));

                UserDetails.Password = AMBOEcryption.GetHashValue(UserDetails.Password.Trim());

                objDBParam.Add(new DbParameter("@UserPassword", UserDetails.Password, DbType.String));
                objDBParam.Add(new DbParameter("@IMEI", UserDetails.IMEI, DbType.String));
                objDBParam.Add(new DbParameter("@EncryptKey", UserDetails.EncryptKey, DbType.String));
                objDBParam.Add(new DbParameter("@SessionTime", DateTime.Now, DbType.DateTime));
                objDBParam.Add(new DbParameter("@FCMId", UserDetails.FCMId, DbType.String));

                var outputFromSP = _dataHelper1.ExecuteDataTable("[dbo].[ValidateSIDUserLogin]", objDBParam, CommandType.StoredProcedure);

                Message = Convert.ToString(outputFromSP.Rows[0]["Message"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                {
                    UserData.Name = Convert.ToString(outputFromSP.Rows[0]["Name"]);
                    UserData.UserId = Convert.ToInt64(outputFromSP.Rows[0]["Id"]);
                    UserData.RoleName = Convert.ToString(outputFromSP.Rows[0]["RoleName"]);
                    UserData.RoleId = Convert.ToInt64(outputFromSP.Rows[0]["RoleId"]);
                    UserData.CurrentAppVersion = Convert.ToInt64(outputFromSP.Rows[0]["CurrentAppVersion"]);
                    UserData.URL = Convert.ToString(outputFromSP.Rows[0]["URL"]);
                }
                UserData.Status = Convert.ToInt32(outputFromSP.Rows[0]["Status"]);
                UserData.Message = Convert.ToString(outputFromSP.Rows[0]["Message"]);
            }
            catch (Exception ex)
            {
                //Write Application Error log here
                UserData.Status = Convert.ToInt32(99);
                UserData.Message = Convert.ToString("Something went wrong. Please try again.");
            }
            return UserData;
        }

        /// <summary>
        /// To fetch Model Wise Trend Report.
        /// </summary>
        /// <param name="Input">Branch Id, Product Category Id, Product Sub Category Id, ChannelId</param>
        /// <returns>Model Wise Trend Report.</returns>
        public ModelWiseTrendList GetModelWiseTrendReport(InputParam Input)
        {
            string ProdCatId = string.Join(",", Input.ProductSubCategoryId);
            ModelWiseTrendList Data = new ModelWiseTrendList();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@BranchId", Input.BranchId, DbType.Int64));
                objDBParam.Add(new DbParameter("@ProductCategoryId", Input.ProductCategoryId, DbType.Int64));
                objDBParam.Add(new DbParameter("@UserId", Input.UserId, DbType.Int64));
                objDBParam.Add(new DbParameter("@ProductSubCategoryId", ProdCatId, DbType.String));
                objDBParam.Add(new DbParameter("@ChannelId", Input.ChannelId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetModelWiseTrendReport]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data.CurrentMonth = Convert.ToString(outputFromSP.Rows[0]["CurrentMonth"]);
                    Data.LastMonth = Convert.ToString(outputFromSP.Rows[0]["LastMonth"]);
                    Data.SecondLastMonth = Convert.ToString(outputFromSP.Rows[0]["SecondLastMonth"]);
                    Data.ThirdLastMonth = Convert.ToString(outputFromSP.Rows[0]["ThirdLastMonth"]);
                    Data.PreYrCurrentMonth = Convert.ToString(outputFromSP.Rows[0]["PreYrCurrentMonth"]);
                    Data.PreYrLastMonth = Convert.ToString(outputFromSP.Rows[0]["PreYrLastMonth"]);
                    Data.PreYrSecondLastMonth = Convert.ToString(outputFromSP.Rows[0]["PreYrSecondLastMonth"]);
                    Data.PreYrThirdLastMonth = Convert.ToString(outputFromSP.Rows[0]["PreYrThirdLastMonth"]);
                    Data.ModelWiseTrendData = (from data in outputFromSP.AsEnumerable()
                                               select new ModelWiseTrendReport
                                               {
                                                   ModelName = Convert.ToString(data.Field<string>("ModelName")),
                                                   CurrentMonthSale = Convert.ToInt64(data.Field<Int64>("CurrentMonthSale")),
                                                   LastMonthSale = Convert.ToInt64(data.Field<Int64>("LastMonthSale")),
                                                   SecondLastMonthSale = Convert.ToInt64(data.Field<Int64>("SecondLastMonthSale")),
                                                   ThirdLastMonthSale = Convert.ToInt64(data.Field<Int64>("ThirdLastMonthSale")),
                                                   PreYrCurrentMonthSale = Convert.ToInt64(data.Field<Int64>("PreYrCurrentMonthSale")),
                                                   PreYrLastMonthSale = Convert.ToInt64(data.Field<Int64>("PreYrLastMonthSale")),
                                                   PreYrSecondLastMonthSale = Convert.ToInt64(data.Field<Int64>("PreYrSecondLastMonthSale")),
                                                   PreYrThirdLastMonthSale = Convert.ToInt64(data.Field<Int64>("PreYrThirdLastMonthSale")),
                                                   ClosingStock = Convert.ToInt64(data.Field<Int64>("ClosingStock")),
                                                   DisplayStock = Convert.ToInt64(data.Field<Int64>("DisplayStock"))
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
        /// To fetch Category Wise Trend Report.
        /// </summary>
        /// <param name="Input">Branch Id, ChannelId</param>
        /// <returns>Category Wise Trend Report.</returns>
        public CategoryWiseTrendList GetCategoryWiseTrendReport(ReportInputParam Input)
        {
            CategoryWiseTrendList Data = new CategoryWiseTrendList();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@BranchId", Input.BranchId, DbType.Int64));
                objDBParam.Add(new DbParameter("@ChannelId", Input.ChannelId, DbType.Int64));
                objDBParam.Add(new DbParameter("@UserId", Input.UserId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetCategoryWiseTrendReport]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data.CurrentMonth = Convert.ToString(outputFromSP.Rows[0]["CurrentMonth"]);
                    Data.LastMonth = Convert.ToString(outputFromSP.Rows[0]["LastMonth"]);
                    Data.SecondLastMonth = Convert.ToString(outputFromSP.Rows[0]["SecondLastMonth"]);
                    Data.ThirdLastMonth = Convert.ToString(outputFromSP.Rows[0]["ThirdLastMonth"]);
                    Data.PreYrCurrentMonth = Convert.ToString(outputFromSP.Rows[0]["PreYrCurrentMonth"]);
                    Data.PreYrLastMonth = Convert.ToString(outputFromSP.Rows[0]["PreYrLastMonth"]);
                    Data.PreYrSecondLastMonth = Convert.ToString(outputFromSP.Rows[0]["PreYrSecondLastMonth"]);
                    Data.PreYrThirdLastMonth = Convert.ToString(outputFromSP.Rows[0]["PreYrThirdLastMonth"]);
                    Data.CategoryWiseTrendData = (from data in outputFromSP.AsEnumerable()
                                                  select new CategoryWiseTrendReport
                                                  {
                                                      ProductCategory = Convert.ToString(data.Field<string>("ProductCategory")),
                                                      CurrentMonthSale = Convert.ToInt64(data.Field<Int64>("CurrentMonthSale")),
                                                      LastMonthSale = Convert.ToInt64(data.Field<Int64>("LastMonthSale")),
                                                      SecondLastMonthSale = Convert.ToInt64(data.Field<Int64>("SecondLastMonthSale")),
                                                      ThirdLastMonthSale = Convert.ToInt64(data.Field<Int64>("ThirdLastMonthSale")),
                                                      PreYrCurrentMonthSale = Convert.ToInt64(data.Field<Int64>("PreYrCurrentMonthSale")),
                                                      PreYrLastMonthSale = Convert.ToInt64(data.Field<Int64>("PreYrLastMonthSale")),
                                                      PreYrSecondLastMonthSale = Convert.ToInt64(data.Field<Int64>("PreYrSecondLastMonthSale")),
                                                      PreYrThirdLastMonthSale = Convert.ToInt64(data.Field<Int64>("PreYrThirdLastMonthSale"))
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
        /// To fetch Account Wise Trend Report.
        /// </summary>
        /// <param name="Input">Master Code</param>
        /// <returns>Account Wise Trend Report.</returns>
        public AccountWiseTrendList GetAccountWiseTrendReport(AccountWiseTrendInput Input)
        {
            AccountWiseTrendList Data = new AccountWiseTrendList();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@MasterCode", Input.MasterCode, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetAccountWiseTrendReport]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data.CurrentMonth = Convert.ToString(outputFromSP.Rows[0]["CurrentMonth"]);
                    Data.LastMonth = Convert.ToString(outputFromSP.Rows[0]["LastMonth"]);
                    Data.SecondLastMonth = Convert.ToString(outputFromSP.Rows[0]["SecondLastMonth"]);
                    Data.ThirdLastMonth = Convert.ToString(outputFromSP.Rows[0]["ThirdLastMonth"]);
                    Data.PreYrCurrentMonth = Convert.ToString(outputFromSP.Rows[0]["PreYrCurrentMonth"]);
                    Data.PreYrLastMonth = Convert.ToString(outputFromSP.Rows[0]["PreYrLastMonth"]);
                    Data.PreYrSecondLastMonth = Convert.ToString(outputFromSP.Rows[0]["PreYrSecondLastMonth"]);
                    Data.PreYrThirdLastMonth = Convert.ToString(outputFromSP.Rows[0]["PreYrThirdLastMonth"]);
                    Data.AccountWiseTrendData = (from data in outputFromSP.AsEnumerable()
                                                 select new AccountWiseTrendReport
                                                 {
                                                     ProductCategory = Convert.ToString(data.Field<string>("ProductCategory")),
                                                     CurrentMonthSale = Convert.ToInt64(data.Field<Int64>("CurrentMonthSale")),
                                                     LastMonthSale = Convert.ToInt64(data.Field<Int64>("LastMonthSale")),
                                                     SecondLastMonthSale = Convert.ToInt64(data.Field<Int64>("SecondLastMonthSale")),
                                                     ThirdLastMonthSale = Convert.ToInt64(data.Field<Int64>("ThirdLastMonthSale")),
                                                     PreYrCurrentMonthSale = Convert.ToInt64(data.Field<Int64>("PreYrCurrentMonthSale")),
                                                     PreYrLastMonthSale = Convert.ToInt64(data.Field<Int64>("PreYrLastMonthSale")),
                                                     PreYrSecondLastMonthSale = Convert.ToInt64(data.Field<Int64>("PreYrSecondLastMonthSale")),
                                                     PreYrThirdLastMonthSale = Convert.ToInt64(data.Field<Int64>("PreYrThirdLastMonthSale"))
                                                 }).ToList();
                }
            }
            catch (Exception)
            {
                //Log exception here
            }
            return Data;
        }

        /// <summary>
        /// To fetch Month Sell Thru Report.
        /// </summary>
        /// <param name="Input">Branch Id, Product Category Id, Product Sub Category Id</param>
        /// <returns>Month Sell Thru Report</returns>
        public Sell_Thru_Report MonthSellThruReport(MonthWiseSellThruReportIds Input)
        {
            string ProdCatId = string.Join(",", Input.ProductSubCatIdList);
            Sell_Thru_Report Data = new Sell_Thru_Report();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@BranchId", Input.BranchId, DbType.Int64));
                objDBParam.Add(new DbParameter("@ProductCatId", Input.ProductCatId, DbType.Int64));
                objDBParam.Add(new DbParameter("@UserId", Input.UserId, DbType.Int64));
                objDBParam.Add(new DbParameter("@ProdSubCatId", ProdCatId, DbType.String));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetMonthSellThruReport]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data.Report = (from data in outputFromSP.AsEnumerable()
                                   select new MonthSellThruReportModel
                                   {
                                       Date = Convert.ToString(data.Field<string>("SaleDate")),
                                       Quantity = Convert.ToInt64(data.Field<Int64>("SalesQty"))
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
        /// To fetch Month Plan vs Actual Report.
        /// </summary>
        /// <param name="Input">Branch Id, Product Category Id, Product Sub Category Id</param>
        /// <returns>Month Plan vs Actual Report</returns>
        public Plan_vs_Actual_Graph MonthPlanvsActualReport(MonthWiseSellThruReportIds Input)
        {
            string ProdCatId = string.Join(",", Input.ProductSubCatIdList);
            Plan_vs_Actual_Graph Data = new Plan_vs_Actual_Graph();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@BranchId", Input.BranchId, DbType.Int64));
                objDBParam.Add(new DbParameter("@ProductCatId", Input.ProductCatId, DbType.Int64));
                objDBParam.Add(new DbParameter("@UserId", Input.UserId, DbType.Int64));
                objDBParam.Add(new DbParameter("@ProdSubCatId", ProdCatId, DbType.String));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[MonthPlanvsActualReport]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data.Report = (from data in outputFromSP.AsEnumerable()
                                   select new MonthPlanvsActualReportModel
                                   {
                                       Product = Convert.ToString(data.Field<string>("Product")),
                                       Actual_Data = Convert.ToInt32(data.Field<Int32>("DispStk")),
                                       Plan_Data = Convert.ToInt32(data.Field<Int32>("PlnStk"))
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
        /// To fetch Last 3 Days Sales Report.
        /// </summary>
        /// <param name="Input">Branch Id, Product Category Id, Product Sub Category Id</param>
        /// <returns>Last 3 Days Sales Report</returns>
        public LastThreeDaysData Last3DaysSalesReport(MonthWiseSellThruReportIds Input)
        {
            string ProdCatId = string.Join(",", Input.ProductSubCatIdList);
            LastThreeDaysData Data = new LastThreeDaysData();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@BranchId", Input.BranchId, DbType.Int64));
                objDBParam.Add(new DbParameter("@ProductCatId", Input.ProductCatId, DbType.Int64));
                objDBParam.Add(new DbParameter("@UserId", Input.UserId, DbType.Int64));
                objDBParam.Add(new DbParameter("@ProdSubCatId", ProdCatId, DbType.String));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[Last3DaysSalesReport]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data.CurrentDay = Convert.ToString(outputFromSP.Rows[0]["CDay"]);
                    Data.LastDay = Convert.ToString(outputFromSP.Rows[0]["LDay"]);
                    Data.SecondLastDay = Convert.ToString(outputFromSP.Rows[0]["SLDay"]);
                    Data.ModelWiseTrendLastThreeDaysData = (from data in outputFromSP.AsEnumerable()
                                                            select new Last3DaysSalesReportModel
                                                            {
                                                                ModelName = Convert.ToString(data.Field<string>("MatName")),
                                                                CurrentDaySale = Convert.ToInt64(data.Field<Int64>("CSalesQty")),
                                                                LastDaySale = Convert.ToInt64(data.Field<Int64>("LSalesQty")),
                                                                SecondLastDaySale = Convert.ToInt64(data.Field<Int64>("SLSalesQty")),
                                                                MTD = Convert.ToInt64(data.Field<Int64>("MTD"))
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
        /// To fetch MTD Sell Thru Report.
        /// </summary>
        /// <param name="Input">Dealer Id, Product Sub Category Id</param>
        /// <returns>MTD Sell Thru Report</returns>
        public MTDSellThruList GetMTDSellThruReport(MTDSellThruReportInput Input)
        {
            MTDSellThruList Data = new MTDSellThruList();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@DealerId", Input.DealerId, DbType.String));
                objDBParam.Add(new DbParameter("@ProductCategoryId", Input.ProductCategoryId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetMTDSellThruReport]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data.CurrentMonth = Convert.ToString(outputFromSP.Rows[0]["CurrentMonth"]);
                    Data.LastMonth = Convert.ToString(outputFromSP.Rows[0]["LastMonth"]);
                    Data.SecondLastMonth = Convert.ToString(outputFromSP.Rows[0]["SecondLastMonth"]);
                    Data.ThirdLastMonth = Convert.ToString(outputFromSP.Rows[0]["ThirdLastMonth"]);
                    Data.PreYrCurrentMonth = Convert.ToString(outputFromSP.Rows[0]["PreYrCurrentMonth"]);
                    Data.PreYrLastMonth = Convert.ToString(outputFromSP.Rows[0]["PreYrLastMonth"]);
                    Data.PreYrSecondLastMonth = Convert.ToString(outputFromSP.Rows[0]["PreYrSecondLastMonth"]);
                    Data.PreYrThirdLastMonth = Convert.ToString(outputFromSP.Rows[0]["PreYrThirdLastMonth"]);
                    Data.MTDSellThruData = (from data in outputFromSP.AsEnumerable()
                                            select new MTDSellThruReportModel
                                            {
                                                Name = Convert.ToString(data.Field<string>("ProductSubCat")),
                                                CurrentMonthSale = Convert.ToInt64(data.Field<Int64>("CurrentMonthSale")),
                                                LastMonthSale = Convert.ToInt64(data.Field<Int64>("LastMonthSale")),
                                                SecondLastMonthSale = Convert.ToInt64(data.Field<Int64>("SecondLastMonthSale")),
                                                ThirdLastMonthSale = Convert.ToInt64(data.Field<Int64>("ThirdLastMonthSale")),
                                                PreYrCurrentMonthSale = Convert.ToInt64(data.Field<Int64>("PreYrCurrentMonthSale")),
                                                PreYrLastMonthSale = Convert.ToInt64(data.Field<Int64>("PreYrLastMonthSale")),
                                                PreYrSecondLastMonthSale = Convert.ToInt64(data.Field<Int64>("PreYrSecondLastMonthSale")),
                                                PreYrThirdLastMonthSale = Convert.ToInt64(data.Field<Int64>("PreYrThirdLastMonthSale"))
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
        /// To fetch Plan vs Actual vs Stock Report.
        /// </summary>
        /// <param name="Input">DealerId, ProductCategoryId, ProductSubActegoryId</param>
        /// <returns>Plan vs Actual vs Stock Report</returns>
        public PlanActualStockList GetPlanActualStockReport(PlanActualStockDataInput Input)
        {
            string ProdCatId = string.Join(",", Input.ProductSubCatId);
            PlanActualStockList Data = new PlanActualStockList();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@DealerId", Input.DealerId, DbType.String));
                objDBParam.Add(new DbParameter("@ProductCatId", Input.ProductCatId, DbType.Int64));
                objDBParam.Add(new DbParameter("@ProductSubCatId", ProdCatId, DbType.String));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetPlanActualStockReport]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data.PlanActualStockData = (from data in outputFromSP.AsEnumerable()
                                                select new PlanActualStockReport
                                                {
                                                    ModelName = Convert.ToString(data.Field<string>("MatName")),
                                                    Display_Qty = Convert.ToInt64(data.Field<Int64>("DisplayStock")),
                                                    Plan_Qty = Convert.ToInt64(data.Field<Int64>("PlanStock")),
                                                    Store_Back_Stock = Convert.ToInt64(data.Field<Int64>("BackStock"))
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
        /// To fetch MTD Sony Vs Comp Sell Report.
        /// </summary>
        /// <param name="Input">DealerId, ProductCategoryId, ProductSubActegoryId</param>
        /// <returns>MTD Sony Vs Comp Sell Report</returns>
        public MTDSonyVsCompSellReport GetMTDSonyVsCompSellReport(MTDSonyVsCompSellInput Input)
        {
            string ProdCatId = string.Join(",", Input.ProductSubCatIdList);
            MTDSonyVsCompSellReport Data = new MTDSonyVsCompSellReport();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@DealerId", Input.DealerId, DbType.String));
                objDBParam.Add(new DbParameter("@ProductId", Input.ProductCatId, DbType.Int64));
                objDBParam.Add(new DbParameter("@SubProductId", ProdCatId, DbType.String));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetMTDSonyVsCompSellReport]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data.C_Month = Convert.ToString(outputFromSP.Rows[0]["CurrentMonth"]);
                    Data.L_Month = Convert.ToString(outputFromSP.Rows[0]["LastMonth"]);
                    Data.L_2_Month = Convert.ToString(outputFromSP.Rows[0]["SecondLastMonth"]);

                    List<CompetitonData> CData = new List<CompetitonData>();
                    
                    List<ModelData> Model = new List<ModelData>();

                    Model = (from data in outputFromSP.AsEnumerable()
                             select new ModelData
                             {
                                 Company = Convert.ToString(data.Field<string>("BrandName")),
                                 ModelName = Convert.ToString(data.Field<string>("ModelName")),
                                 C_Month_qty = Convert.ToInt64(data.Field<Int64>("C_Month_qty")),
                                 L_Month_Qty = Convert.ToInt64(data.Field<Int64>("L_Month_Qty")),
                                 L_2_Month_qty = Convert.ToInt64(data.Field<Int64>("L_2_Month_qty"))
                             }).ToList();


                    CData = (from data in Model.ToList()
                             group data.Company by data.Company into g
                             select new CompetitonData
                             {
                                 Company = Convert.ToString(g.Key)
                             }).ToList();


                    for (int i = 0; i < CData.Count; i++)
                    {
                        CData[i].Model = (from model in Model.AsEnumerable()
                                          where model.Company == CData[i].Company
                                          select new Model()
                                          {
                                              ModelName = Convert.ToString(model.ModelName),
                                              C_Month_qty = Convert.ToInt64(model.C_Month_qty),
                                              L_Month_Qty = Convert.ToInt64(model.L_Month_Qty),
                                              L_2_Month_qty = Convert.ToInt64(model.L_2_Month_qty)
                                          }).ToList();
                        Data.Data.Add(CData[i]);
                    }

                }
            }
            catch (Exception ex)
            {
                //Log exception here
            }
            return Data;
        }

        /// <summary>
        /// To fetch MTD Sony vs Competitor Sell Report.
        /// </summary>
        /// <param name="Input">Dealer Id, ProductId, SubProductId</param>
        /// <returns>MTD Sony vs Competitor Sell Report</returns>
        public SonyVsCompSellReportYrWise GetSonyVsCompSellReportYrWise(PlanActualStockDataInput Input)
        {
            string ProdCatId = string.Join(",", Input.ProductSubCatId);
            SonyVsCompSellReportYrWise Data = new SonyVsCompSellReportYrWise();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@DealerId", Input.DealerId, DbType.String));
                objDBParam.Add(new DbParameter("@ProductId", Input.ProductCatId, DbType.Int64));
                objDBParam.Add(new DbParameter("@SubProductId", ProdCatId, DbType.String));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetSonyvsCompSellReportYrWise]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data.CurrentMonth = Convert.ToString(outputFromSP.Rows[0]["CurrentMonth"]);
                    Data.LastMonth = Convert.ToString(outputFromSP.Rows[0]["LastMonth"]);
                    Data.SecondLastMonth = Convert.ToString(outputFromSP.Rows[0]["SecondLastMonth"]);
                    Data.ThirdLastMonth = Convert.ToString(outputFromSP.Rows[0]["ThirdLastMonth"]);
                    Data.PreYrCurrentMonth = Convert.ToString(outputFromSP.Rows[0]["PreYrCurrentMonth"]);
                    Data.PreYrLastMonth = Convert.ToString(outputFromSP.Rows[0]["PreYrLastMonth"]);
                    Data.PreYrSecondLastMonth = Convert.ToString(outputFromSP.Rows[0]["PreYrSecondLastMonth"]);
                    Data.PreYrThirdLastMonth = Convert.ToString(outputFromSP.Rows[0]["PreYrThirdLastMonth"]);
                    Data.SonyVsCompSellReportYrWiseList = (from data in outputFromSP.AsEnumerable()
                                            select new SonyVsCompSellReportYrWiseData
                                            {
                                                Brand = Convert.ToString(data.Field<string>("BrandName")),
                                                C_Month = Convert.ToInt64(data.Field<Int64>("C_Month_Qty")),
                                                C_2_Month = Convert.ToInt64(data.Field<Int64>("L_2_Month_Qty")),
                                                C_3_Month = Convert.ToInt64(data.Field<Int64>("L_3_Month_Qty")),
                                                C_1_Month = Convert.ToInt64(data.Field<Int64>("L_Month_Qty")),
                                                Last_C_Month = Convert.ToInt64(data.Field<Int64>("Last_C_Month_Qty")),
                                                Last_C_1_Month = Convert.ToInt64(data.Field<Int64>("Last_L_Month_Qty")),
                                                Last_C_2_Month = Convert.ToInt64(data.Field<Int64>("Last_L_2_Month_Qty")),
                                                Last_C_3_Month = Convert.ToInt64(data.Field<Int64>("Last_L_3_Month_Qty")),
                                                Share_C_Month = Convert.ToDecimal(data.Field<Decimal>("Share_C_Month")),
                                                Share_C_1_Month = Convert.ToDecimal(data.Field<Decimal>("Share_L_Month_Qty")),
                                                Share_C_2_Month = Convert.ToDecimal(data.Field<Decimal>("Share_L_2_Month_Qty")),
                                                Share_C_3_Month = Convert.ToDecimal(data.Field<Decimal>("Share_L_3_Month_Qty")),
                                                Last_Share_c_Month = Convert.ToDecimal(data.Field<Decimal>("Share_Last_C_Month_Qty")),
                                                Last_Share_c_1_Month = Convert.ToDecimal(data.Field<Decimal>("Share_CLast_L_Month_Qty")),
                                                Last_Share_c_2_Month = Convert.ToDecimal(data.Field<Decimal>("Share_Last_L_2_Month_Qty")),
                                                Last_Share_c_3_Month = Convert.ToDecimal(data.Field<Decimal>("Share_Last_L_3_Month_Qty"))
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
        /// To fetch Account Category Wise Trend Report.
        /// </summary>
        /// <param name="Input">MasterCode, ProductId, SubProductId</param>
        /// <returns>Account Category Wise Trend Report</returns>
        public AccountCategoryWiseTrendReport GetAccountCategoryWiseTrendReport(AccountCategoryWiseTrendInput Input)
        {
            string ProdCatId = string.Join(",", Input.SubProductId);
            AccountCategoryWiseTrendReport Data = new AccountCategoryWiseTrendReport();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@MasterCode", Input.MasterCode, DbType.String));
                objDBParam.Add(new DbParameter("@ProductId", Input.ProductId, DbType.Int64));
                objDBParam.Add(new DbParameter("@SubProductId", ProdCatId, DbType.String));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetAccCatWiseShareReport]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data.CurrentMonth = Convert.ToString(outputFromSP.Rows[0]["CurrentMonth"]);
                    Data.LastMonth = Convert.ToString(outputFromSP.Rows[0]["LastMonth"]);
                    Data.SecondLastMonth = Convert.ToString(outputFromSP.Rows[0]["SecondLastMonth"]);
                    Data.ThirdLastMonth = Convert.ToString(outputFromSP.Rows[0]["ThirdLastMonth"]);
                    Data.AccountCategoryWiseTrendData = (from data in outputFromSP.AsEnumerable()
                                                           select new AccountCategoryWiseTrendData
                                                           {
                                                               Brand = Convert.ToString(data.Field<string>("BrandName")),
                                                               CurrentMonthSale = Convert.ToInt64(data.Field<Int64>("CurrentMonthSale")),
                                                               LastMonthSale = Convert.ToInt64(data.Field<Int64>("LastMonthSale")),
                                                               SecondLastMonthSale = Convert.ToInt64(data.Field<Int64>("SecondLastMonthSale")),
                                                               ThirdLastMonthSale = Convert.ToInt64(data.Field<Int64>("ThirdLastMonthSale")),
                                                               CurrentMonthSharePer = Convert.ToDecimal(data.Field<Decimal>("CurrentMonthSharePer")),
                                                               LastMonthSharePer = Convert.ToDecimal(data.Field<Decimal>("LastMonthSharePer")),
                                                               SecondLastMonthSharePer = Convert.ToDecimal(data.Field<Decimal>("SecondLastMonthSharePer")),
                                                               ThirdLastMonthSharePer = Convert.ToDecimal(data.Field<Decimal>("ThirdLastMonthSharePer"))
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
        /// To fetch Competition Head Count Report.
        /// </summary>
        /// <param name="Input">BranchId, TypeId, ChannelId,DealerId</param>
        /// <returns>Competition Head Count Report</returns>
        public CompetitionHeadCountReportList GetCompetitionHeadCountReport(HeadCountReportInput Input)
        {
            CompetitionHeadCountReportList Data = new CompetitionHeadCountReportList();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@BranchId", Input.BranchId, DbType.String));
                objDBParam.Add(new DbParameter("@TypeId", Input.TypeId, DbType.Int64));
                objDBParam.Add(new DbParameter("@ChannelId", Input.ChannelId, DbType.Int64));
                objDBParam.Add(new DbParameter("@DealerId", Input.DealerId, DbType.Int64));
                objDBParam.Add(new DbParameter("@UserId", Input.UserId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[CompetitionHeadCountReport]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data.CompetitionHeadCountData = (from data in outputFromSP.AsEnumerable()
                                                         select new CompetitionHeadCountReport
                                                         {
                                                             BrandName = Convert.ToString(data.Field<string>("BrandName")),
                                                             SFACount = Convert.ToInt32(data.Field<Int32>("SFACount"))
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
        /// To fetch Last 3 Days Sales Report based on selected date.
        /// </summary>
        /// <param name="Input">Branch Id, Product Category Id, Product Sub Category Id,Channel Id, Date</param>
        /// <returns>Last 3 Days Sales Report</returns>
        public LastThreeDaysData Last3DaysSalesReport_V2(Last3DaysSalesInput Input)
        {
            string ProdCatId = string.Join(",", Input.ProductSubCatIdList);
            LastThreeDaysData Data = new LastThreeDaysData();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@BranchId", Input.BranchId, DbType.Int64));
                objDBParam.Add(new DbParameter("@ProductCatId", Input.ProductCatId, DbType.Int64));
                objDBParam.Add(new DbParameter("@ProdSubCatId", ProdCatId, DbType.String));
                objDBParam.Add(new DbParameter("@UserId", Input.UserId, DbType.Int64));
                objDBParam.Add(new DbParameter("@ChannelId", Input.ChannelId, DbType.Int64));
                objDBParam.Add(new DbParameter("@Date", Input.Date, DbType.DateTime));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[Last3DaysSalesReport_V2]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data.CurrentDay = Convert.ToString(outputFromSP.Rows[0]["CDay"]);
                    Data.LastDay = Convert.ToString(outputFromSP.Rows[0]["LDay"]);
                    Data.SecondLastDay = Convert.ToString(outputFromSP.Rows[0]["SLDay"]);
                    Data.ModelWiseTrendLastThreeDaysData = (from data in outputFromSP.AsEnumerable()
                                                            select new Last3DaysSalesReportModel
                                                            {
                                                                ModelName = Convert.ToString(data.Field<string>("MatName")),
                                                                CurrentDaySale = Convert.ToInt64(data.Field<Int64>("CSalesQty")),
                                                                LastDaySale = Convert.ToInt64(data.Field<Int64>("LSalesQty")),
                                                                SecondLastDaySale = Convert.ToInt64(data.Field<Int64>("SLSalesQty")),
                                                                MTD = Convert.ToInt64(data.Field<Int64>("MTD")),
                                                                CurrentSale = Convert.ToInt64(data.Field<Int64>("CurrentSale"))
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
        /// To fetch Branch Wise Sales Trend Report.
        /// </summary>
        /// <param name="Input">Branch Id, Product Category Id, Product Sub Category Id, Channel Id, Dealer Id</param>
        /// <returns>Branch Wise Sales Trend Report.</returns>
        public BranchWiseSalesReportData GetBranchWiseSalesTrendReport(BranchWiseSalesReportInput Input)
        {
            string ProdCatId = string.Join(",", Input.ProductSubCategoryId);
            BranchWiseSalesReportData Data = new BranchWiseSalesReportData();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@BranchId", Input.BranchId, DbType.Int64));
                objDBParam.Add(new DbParameter("@ProductCategoryId", Input.ProductCategoryId, DbType.Int64));
                objDBParam.Add(new DbParameter("@UserId", Input.UserId, DbType.Int64));
                objDBParam.Add(new DbParameter("@ProductSubCategoryId", ProdCatId, DbType.String));
                objDBParam.Add(new DbParameter("@ChannelId", Input.ChannelId, DbType.Int64));
                objDBParam.Add(new DbParameter("@DealerId", Input.DealerId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetBranchWiseSalesTrendReport_V2]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data.CurrentMonth = Convert.ToString(outputFromSP.Rows[0]["CurrentMonth"]);
                    Data.LastMonth = Convert.ToString(outputFromSP.Rows[0]["LastMonth"]);
                    Data.SecondLastMonth = Convert.ToString(outputFromSP.Rows[0]["SecondLastMonth"]);
                    Data.PreYrCurrentMonth = Convert.ToString(outputFromSP.Rows[0]["PreYrCurrentMonth"]);
                    Data.PreYrLastMonth = Convert.ToString(outputFromSP.Rows[0]["PreYrLastMonth"]);
                    Data.PreYrSecondLastMonth = Convert.ToString(outputFromSP.Rows[0]["PreYrSecondLastMonth"]);
                    Data.BranchWiseTrendData = (from data in outputFromSP.AsEnumerable()
                                               select new BranchWiseSalesReportModel
                                               {
                                                   Name = Convert.ToString(data.Field<string>("Name")),
                                                   CurrentMonthSale = Convert.ToInt64(data.Field<Int64>("CurrentMonthSale")),
                                                   LastMonthSale = Convert.ToInt64(data.Field<Int64>("LastMonthSale")),
                                                   SecondLastMonthSale = Convert.ToInt64(data.Field<Int64>("SecondLastMonthSale")),
                                                   PreYrCurrentMonthSale = Convert.ToInt64(data.Field<Int64>("PreYrCurrentMonthSale")),
                                                   PreYrLastMonthSale = Convert.ToInt64(data.Field<Int64>("PreYrLastMonthSale")),
                                                   PreYrSecondLastMonthSale = Convert.ToInt64(data.Field<Int64>("PreYrSecondLastMonthSale"))
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
        /// To get Branch wise sales trend report chart.
        /// </summary>
        /// <param name="Input">Branch Id</param>
        /// <returns>Branch wise sales trend report chart.</returns>
        public BranchWiseSalesChartData GetBranchWiseSalesTrendChart(BranchWiseSalesChartInput Input)
        {
            BranchWiseSalesChartData Data = new BranchWiseSalesChartData();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@BranchId", Input.BranchId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetBranchWiseSalesTrendChart]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data.BranchWiseTrendData = (from data in outputFromSP.AsEnumerable()
                                                select new BranchWiseSalesTrendChart
                                                {
                                                    SaleDate = Convert.ToString(data.Field<string>("SaleDate")),
                                                    SalesCount = Convert.ToInt64(data.Field<Int64>("SalesQty")),
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
        /// To get SID App Sync Data.
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 23, 2018
        /// </summary>
        /// <param name="Input">User Id</param>
        /// <returns>SID App Sync Data</returns>
        public SIDAppSyncModel GetSIDAppSync(SIDSyncInput Input)
        {
            SIDAppSyncModel Data = new SIDAppSyncModel();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@UserId", Input.UserId, DbType.Int64));
                var outputFromSP = _dataHelper1.ExecuteDataSet("[dbo].[SID_Analytics_Sync]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Tables[0].Rows.Count > 0)
                {
                    Data.SIDSyncBranches = (from data in outputFromSP.Tables[0].AsEnumerable()
                                                select new SIDSyncBranches
                                                {
                                                    Id = Convert.ToInt64(data.Field<Int64>("Id")),
                                                    Name = Convert.ToString(data.Field<string>("BranchName"))
                                                }).ToList();
                }
                if (outputFromSP.Tables[1].Rows.Count > 0)
                {
                    Data.SIDSyncProducts = (from data in outputFromSP.Tables[1].AsEnumerable()
                                            select new SIDSyncProducts
                                            {
                                                Id = Convert.ToInt64(data.Field<Int64>("Id")),
                                                Name = Convert.ToString(data.Field<string>("CategoryName")),
                                                GroupName = Convert.ToString(data.Field<string>("GroupName"))
                                            }).ToList();
                }
                if (outputFromSP.Tables[2].Rows.Count > 0)
                {
                    Data.SIDSyncProductGroups = (from data in outputFromSP.Tables[2].AsEnumerable()
                                            select new SIDSyncProductGroups
                                            {
                                                Id = Convert.ToInt64(data.Field<Int64>("Id")),
                                                Name = Convert.ToString(data.Field<string>("Name"))
                                            }).ToList();
                }
                if (outputFromSP.Tables[3].Rows.Count > 0)
                {
                    Data.SIDSyncSubProduct = (from data in outputFromSP.Tables[3].AsEnumerable()
                                                 select new SIDSyncSubProduct
                                                 {
                                                     Id = Convert.ToInt64(data.Field<Int64>("Id")),
                                                     Name = Convert.ToString(data.Field<string>("Name")),
                                                     ProductCategoryId = Convert.ToInt64(data.Field<Int64>("ProductCategoryId"))
                                                 }).ToList();
                }
                if (outputFromSP.Tables[4].Rows.Count > 0)
                {
                    Data.SIDSyncSubDealers = (from data in outputFromSP.Tables[4].AsEnumerable()
                                              select new SIDSyncSubDealers
                                              {
                                                  Id = Convert.ToInt64(data.Field<Int64>("Id")),
                                                  Name = Convert.ToString(data.Field<string>("Name")),
                                                  BranchId = Convert.ToInt64(data.Field<Int64>("BranchId")),
                                                  Latitude = Convert.ToString(data.Field<string>("Latitude")),
                                                  Longitude = Convert.ToString(data.Field<string>("Longitude"))
                                              }).ToList();
                }
                if (outputFromSP.Tables[5].Rows.Count > 0)
                {
                    Data.SIDSyncServiceEntityAccount = (from data in outputFromSP.Tables[5].AsEnumerable()
                                              select new SIDSyncServiceEntityAccount
                                              {
                                                  GlobalDealerName = Convert.ToString(data.Field<string>("GlobalDealerName")),
                                                  MasterCode = Convert.ToString(data.Field<string>("MasterCode")),
                                                  BranchId = Convert.ToInt64(data.Field<Int64>("BranchId")),
                                                  ChannelId = Convert.ToInt64(data.Field<Int64>("ChannelId"))
                                              }).ToList();
                }
                if (outputFromSP.Tables[6].Rows.Count > 0)
                {
                    Data.SIDSyncServiceEntityChannel = (from data in outputFromSP.Tables[6].AsEnumerable()
                                                 select new SIDSyncServiceEntityChannel
                                                 {
                                                     ChannelId = Convert.ToInt64(data.Field<Int64>("ChannelId")),
                                                     Channel = Convert.ToString(data.Field<string>("Channel"))
                                                 }).ToList();
                }
                if (outputFromSP.Tables[7].Rows.Count > 0)
                {
                    Data.SIDSyncServiceEntityModel = (from data in outputFromSP.Tables[7].AsEnumerable()
                                                        select new SIDSyncServiceEntityModel
                                                        {
                                                            Model = Convert.ToString(data.Field<string>("Model")),
                                                            ModelId = Convert.ToInt64(data.Field<Int64>("ModelId")),
                                                            ProductCategoryId = Convert.ToInt64(data.Field<Int64>("ProductCategoryId")),
                                                            ProductSubCategoryId = Convert.ToInt64(data.Field<Int64>("ProductSubCategoryId"))
                                                        }).ToList();
                }
                if (outputFromSP.Tables[8].Rows.Count > 0)
                {
                    Data.SIDSyncFestivalData = (from data in outputFromSP.Tables[8].AsEnumerable()
                                                      select new SIDSyncFestivalData
                                                      {
                                                          Id = Convert.ToInt64(data.Field<Int64>("Id")),
                                                          SchemeName = Convert.ToString(data.Field<string>("SchemeName"))
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
        /// To get SID Display Report.
        /// Dhruv Sharma, VFirst, Gurgaon
        /// </summary>
        /// <param name="Input">Input parameters</param>
        /// <returns></returns>
        public SIDDisplayList GetSIDDisplayReport(SIDDisplayReportInput Input)
        {
            SIDDisplayList Data = new SIDDisplayList();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@BranchId", Input.BranchId, DbType.Int64));
                objDBParam.Add(new DbParameter("@ProductCategoryId", Input.ProductCategoryId, DbType.Int64));
                objDBParam.Add(new DbParameter("@UserId", Input.UserId, DbType.Int64));
                objDBParam.Add(new DbParameter("@ChannelId", Input.ChannelId, DbType.Int64));
                objDBParam.Add(new DbParameter("@DealerId", Input.DealerId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetSIDDisplayReport]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data.SIDDisplayReport = (from data in outputFromSP.AsEnumerable()
                                                select new SIDDisplayReport
                                                {
                                                    Date = Convert.ToString(data.Field<string>("Date")),
                                                    DealerName = Convert.ToString(data.Field<string>("DealerName")),
                                                    DisplayQty = Convert.ToInt64(data.Field<Int64>("DisplayQty")),
                                                    ModelName = Convert.ToString(data.Field<string>("ModelName")),
                                                    PlannedQty = Convert.ToInt64(data.Field<Int64>("PlannedQty")),
                                                    StoreBackStock = Convert.ToInt64(data.Field<Int64>("StoreBackStock")),
                                                    WeekISO = Convert.ToString(data.Field<string>("WeekISO")),
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
        /// To get Festival Report data for SID.
        /// Dhruv Sharma, VFirst, Gurgaon
        /// </summary>
        /// <param name="Input">Filters</param>
        /// <returns>Festival Report</returns>
        public FestivalReportList GetFestivalReport(FestivalReportInput Input)
        {
            FestivalReportList Data = new FestivalReportList();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@RoleId", Input.RoleId, DbType.Int64));
                objDBParam.Add(new DbParameter("@FestivalTypeId", Input.FestivalTypeId, DbType.Int64));
                objDBParam.Add(new DbParameter("@BranchId", Input.BranchId, DbType.Int64));
                objDBParam.Add(new DbParameter("@ProductCategoryId", Input.ProductCategoryId, DbType.Int64));
                objDBParam.Add(new DbParameter("@ChannelId", Input.ChannelId, DbType.Int64));
                objDBParam.Add(new DbParameter("@FromDate", Input.FromDate, DbType.DateTime));
                objDBParam.Add(new DbParameter("@ToDate", Input.ToDate, DbType.DateTime));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetFestivalReport]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data.FestivalReport = (from data in outputFromSP.AsEnumerable()
                                             select new FestivalReport
                                             {
                                                 CurrVSPrev = Convert.ToInt64(data.Field<Int64>("CurrVSPrev")),
                                                 CurrYear = Convert.ToInt64(data.Field<Int64>("CurrYear")),
                                                 Name = Convert.ToString(data.Field<string>("Name")),
                                                 Prev2Year = Convert.ToInt64(data.Field<Int64>("Prev3Year")),
                                                 PrevVS2ndPrev = Convert.ToInt64(data.Field<Int64>("PrevVS2ndPrev")),
                                                 PrevYear = Convert.ToInt64(data.Field<Int64>("PrevYear"))
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
        /// To get Daily Ranging Graph data for SID.
        /// Dhruv Sharma, VFirst, Gurgaon
        /// </summary>
        /// <param name="Input">Filters</param>
        /// <returns>Daily Ranging Graph data</returns>
        public DailyRangingChartReportData GetDailyRangingGraphReport(DailyRangingChartReportInputModel Input)
        {
            DailyRangingChartReportData Data = new DailyRangingChartReportData();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@UserId", Input.UserId, DbType.Int64));
                objDBParam.Add(new DbParameter("@BranchId", Input.BranchId, DbType.Int32));
                objDBParam.Add(new DbParameter("@ProductCategoryId", Input.ProductCategoryId, DbType.Int32));
                objDBParam.Add(new DbParameter("@ProductSubCategoryId", Input.ProductSubCategoryId, DbType.Int32));
                objDBParam.Add(new DbParameter("@ModelId", Input.ModelId, DbType.Int32));
                objDBParam.Add(new DbParameter("@startdate", Input.StartDate, DbType.DateTime));
                var outputFromSP = _dataHelper1.ExecuteDataTable("[dbo].[GetDailyRangingGraphReport]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data.DailyRangingChartReport = (from data in outputFromSP.AsEnumerable()
                                           select new DailyRangingChartReportModel
                                           {
                                               Day = Convert.ToString(data.Field<string>("QDay")),
                                               PlannedQuantity = Convert.ToString(data.Field<string>("PlannedQuantity")),
                                               Quantity = Convert.ToString(data.Field<string>("Quantity"))
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
        /// To get Target vs Achievement report data for SID.
        /// Dhruv Sharma, VFirst, Gurgaon
        /// </summary>
        /// <param name="Input">Filters</param>
        /// <returns>Target vs Achievement report data</returns>
        public TargetvsAchievementReportModel GetTargetvsAchievementReport(TargetvsAchievementInputModel Input)
        {
            TargetvsAchievementReportModel Data = new TargetvsAchievementReportModel();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@DealerId", Input.DealerId, DbType.Int32));
                var outputFromSP = _dataHelper1.ExecuteDataTable("[dbo].[GetSIDTargetvsAchievementReport]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data.C_Month = DateTime.Now.ToString("MMM yyyy");
                    Data.L_Month = DateTime.Now.AddMonths(-1).ToString("MMM yyyy");
                    Data.L_2_Month = DateTime.Now.AddMonths(-2).ToString("MMM yyyy");
                    Data.TargetvsAchievementReport = (from data in outputFromSP.AsEnumerable()
                                                    select new TargetvsAchievementModel
                                                    {
                                                        ProductCategoryId = Convert.ToString(data.Field<string>("ProductCategoryId")),
                                                        ProductCategory = Convert.ToString(data.Field<string>("ProductCategory")),
                                                        QtyMonthAch1 = Convert.ToString(data.Field<string>("QtyMonthAch1")),
                                                        QtyMonthAch2 = Convert.ToString(data.Field<string>("QtyMonthAch2")),
                                                        QtyMonthAch3 = Convert.ToString(data.Field<string>("QtyMonthAch3")),
                                                        QtyMonthTarget1 = Convert.ToString(data.Field<string>("QtyMonthTarget1")),
                                                        QtyMonthTarget2 = Convert.ToString(data.Field<string>("QtyMonthTarget2")),
                                                        QtyMonthTarget3 = Convert.ToString(data.Field<string>("QtyMonthTarget3")),
                                                        ValueMonthAch1 = Convert.ToString(data.Field<string>("ValueMonthAch1")),
                                                        ValueMonthAch2 = Convert.ToString(data.Field<string>("ValueMonthAch2")),
                                                        ValueMonthAch3 = Convert.ToString(data.Field<string>("ValueMonthAch3")),
                                                        ValueMonthTarget1 = Convert.ToString(data.Field<string>("ValueMonthTarget1")),
                                                        ValueMonthTarget2 = Convert.ToString(data.Field<string>("ValueMonthTarget2")),
                                                        ValueMonthTarget3 = Convert.ToString(data.Field<string>("ValueMonthTarget3"))
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
        /// To fetch Compt Head Count for mobile.
        /// </summary>
        /// <param name="Input">BranchId,ChannelId,CompetitionBrand,StoreId</param>
        /// <returns>Competition Head Count for mobile</returns>
        public ComptHeadCountList GetComptHeadCount(ComptHeadCountParam Input)
        {
            ComptHeadCountList Data = new ComptHeadCountList();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@BranchId", Input.BranchId, DbType.Int32));
                objDBParam.Add(new DbParameter("@ChannelId", Input.ChannelId, DbType.Int32));
                objDBParam.Add(new DbParameter("@CompBrandId", Input.CompBrandId, DbType.Int32));
                objDBParam.Add(new DbParameter("@StoreId", Input.StoreId, DbType.Int32));
                var outputFromSP = _dataHelper1.ExecuteDataTable("[dbo].[GetComptHeadcount]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data.ComptHeadCountData = (from data in outputFromSP.AsEnumerable()
                                                     select new ComptHeadCount
                                                     {
                                                         Company = Convert.ToString(data.Field<string>("Company")),
                                                         Type = Convert.ToString(data.Field<string>("Type")),
                                                         Count = Convert.ToInt32(data.Field<Int32>("Count"))
                                                     }).ToList();
                }
            }
            catch (Exception ex)
            {
                //Log exception here
            }
            return Data;
        }
    }
}
