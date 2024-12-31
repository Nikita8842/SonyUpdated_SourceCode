using AmboProvider.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboLibrary.MasterMaintenance;
using AmboLibrary.WebReports;
using DBHelper;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace AmboProvider.Implimentation
{
    public class WebReportsProvider : IWebReportsProvider
    {
        private readonly DbHelper _dataHelper;
        private readonly IErrorLogProvider _IErrorLogProvider;

        public WebReportsProvider(IErrorLogProvider IErrorLogProvider)
        {
            _dataHelper = DbHelper.GetInstance("defaultConnection");
            _IErrorLogProvider = IErrorLogProvider;
        }

        //public GridOutput<IEnumerable<CompetitionDisplayReportData>> GetCompetitionDisplayReport(CompetitionDisplayReportFilters objGridVariables)
        //{
        //    GridOutput<IEnumerable<CompetitionDisplayReportData>> output = new GridOutput<IEnumerable<CompetitionDisplayReportData>>();
        //    try
        //    {
        //        output.draw = objGridVariables.draw;
        //        DataTable dtDealers = new DataTable();
        //        DataTable dtCompanies = new DataTable();
        //        DateTime FromDate = new DateTime(Convert.ToInt32(objGridVariables.FromDate.Split('/')[2]), Convert.ToInt32(objGridVariables.FromDate.Split('/')[1]), Convert.ToInt32(objGridVariables.FromDate.Split('/')[0]), 0, 0, 0);
        //        DateTime ToDate = new DateTime(Convert.ToInt32(objGridVariables.ToDate.Split('/')[2]), Convert.ToInt32(objGridVariables.ToDate.Split('/')[1]), Convert.ToInt32(objGridVariables.ToDate.Split('/')[0]), 23, 59, 59);
        //        DataRow row;

        //        dtDealers.Columns.Add("DealerId");
        //        if (objGridVariables.DealerIds != null)
        //        {
        //            foreach (Int64 dealer in objGridVariables.DealerIds)
        //            {
        //                row = dtDealers.NewRow();
        //                row["DealerId"] = dealer;
        //                dtDealers.Rows.Add(row);
        //            }
        //        }

        //        dtCompanies.Columns.Add("CompanyId");
        //        if (objGridVariables.CompanyIds != null)
        //        {
        //            foreach (Int64 company in objGridVariables.CompanyIds)
        //            {
        //                row = dtCompanies.NewRow();
        //                row["CompanyId"] = company;
        //                dtCompanies.Rows.Add(row);
        //            }
        //        }

        //        SqlParameter[] objDBParam = new SqlParameter[12];

        //        objDBParam[0] = new SqlParameter("@PageStart", objGridVariables.start);
        //        objDBParam[1] = new SqlParameter("@PageLength", objGridVariables.length);
        //        objDBParam[2] = new SqlParameter("@FromDate", FromDate);
        //        objDBParam[3] = new SqlParameter("@ToDate", ToDate);
        //        objDBParam[4] = new SqlParameter("@BranchId", objGridVariables.BranchId);
        //        objDBParam[5] = new SqlParameter("@StateId", objGridVariables.StateId);
        //        objDBParam[6] = new SqlParameter("@SFAId", objGridVariables.SFAId);
        //        objDBParam[7] = new SqlParameter("@ProductCategoryId", objGridVariables.ProductCategoryId);
        //        objDBParam[8] = new SqlParameter("@ClassificationId", objGridVariables.ClassificationId);
        //        objDBParam[9] = new SqlParameter("@DivisionId", objGridVariables.DivisionId);
        //        objDBParam[10] = new SqlParameter("@DealerIds", dtDealers);
        //        objDBParam[11] = new SqlParameter("@CompanyIds", dtCompanies);

        //        var outputFromSP = _dataHelper.ExecuteProcedureForDataset("[dbo].[GetCompetitionDisplayReport]", objDBParam);

        //        if (outputFromSP.Tables[1].Rows.Count > 0)
        //        {
        //            output.data = (from records in outputFromSP.Tables[1].AsEnumerable()
        //                           select new CompetitionDisplayReportData
        //                           {
        //                               ActualDate = Convert.ToString(records.Field<string>("ActualDate")),
        //                               BranchName = Convert.ToString(records.Field<string>("BranchName")),
        //                               Brand = Convert.ToString(records.Field<string>("Brand")),
        //                               Channel = Convert.ToString(records.Field<string>("Channel")),
        //                               City = Convert.ToString(records.Field<string>("City")),
        //                               DealerClass = Convert.ToString(records.Field<string>("DealerClass")),
        //                               DealerCode = Convert.ToString(records.Field<string>("DealerCode")),
        //                               DealerState = Convert.ToString(records.Field<string>("DealerState")),
        //                               Division = Convert.ToString(records.Field<string>("Division")),
        //                               Location = Convert.ToString(records.Field<string>("Location")),
        //                               MasterCode = Convert.ToString(records.Field<string>("MasterCode")),
        //                               Model = Convert.ToString(records.Field<string>("Model")),
        //                               OutletName = Convert.ToString(records.Field<string>("OutletName")),
        //                               ProdCat = Convert.ToString(records.Field<string>("ProdCat")),
        //                               Quantity = Convert.ToString(records.Field<Int64>("Quantity")),
        //                               SFAName = Convert.ToString(records.Field<string>("SFAName")),
        //                               SFACode = Convert.ToString(records.Field<string>("SFACode"))
        //                           });
        //            output.recordsFiltered = Convert.ToInt32(outputFromSP.Tables[1].Rows[0]["FilteredCount"]);
        //        }
        //        else
        //        {
        //            output.data = new List<CompetitionDisplayReportData>();
        //            output.recordsFiltered = 0;
        //        }
        //        output.recordsTotal = Convert.ToInt32(outputFromSP.Tables[0].Rows[0]["TotalCount"]);
        //    }
        //    catch (Exception ex)
        //    {
        //        _IErrorLogProvider.CreateErrorLog("CompetitionDisplay Report", ex.StackTrace, ex.Message);
        //    }
        //    return output;
        //}


        public GridOutput<IEnumerable<CompetitionDisplayReportData>> GetCompetitionDisplayReport(CompetitionDisplayReportFilters objGridVariables)
        {
            GridOutput<IEnumerable<CompetitionDisplayReportData>> output = new GridOutput<IEnumerable<CompetitionDisplayReportData>>();
            try
            {
                output.draw = objGridVariables.draw;
                DataTable dtDealers = new DataTable();
                DataTable dtCompanies = new DataTable();
                DateTime FromDate = new DateTime(Convert.ToInt32(objGridVariables.FromDate.Split('/')[2]), Convert.ToInt32(objGridVariables.FromDate.Split('/')[1]), Convert.ToInt32(objGridVariables.FromDate.Split('/')[0]), 0, 0, 0);
                DateTime ToDate = new DateTime(Convert.ToInt32(objGridVariables.ToDate.Split('/')[2]), Convert.ToInt32(objGridVariables.ToDate.Split('/')[1]), Convert.ToInt32(objGridVariables.ToDate.Split('/')[0]), 23, 59, 59);
                DataRow row;

                dtDealers.Columns.Add("DealerId");
                if (objGridVariables.DealerIds != null)
                {
                    foreach (Int64 dealer in objGridVariables.DealerIds)
                    {
                        row = dtDealers.NewRow();
                        row["DealerId"] = dealer;
                        dtDealers.Rows.Add(row);
                    }
                }

                dtCompanies.Columns.Add("CompanyId");
                if (objGridVariables.CompanyIds != null)
                {
                    foreach (Int64 company in objGridVariables.CompanyIds)
                    {
                        row = dtCompanies.NewRow();
                        row["CompanyId"] = company;
                        dtCompanies.Rows.Add(row);
                    }
                }

                SqlParameter[] objDBParam = new SqlParameter[12];

                objDBParam[0] = new SqlParameter("@PageStart", objGridVariables.start);
                objDBParam[1] = new SqlParameter("@PageLength", objGridVariables.length);
                objDBParam[2] = new SqlParameter("@FromDate", FromDate);
                objDBParam[3] = new SqlParameter("@ToDate", ToDate);
                objDBParam[4] = new SqlParameter("@BranchId", objGridVariables.BranchId);
                objDBParam[5] = new SqlParameter("@StateId", objGridVariables.StateId);
                objDBParam[6] = new SqlParameter("@SFAId", objGridVariables.SFAId);
                objDBParam[7] = new SqlParameter("@ProductCategoryId", objGridVariables.ProductCategoryId);
                objDBParam[8] = new SqlParameter("@ClassificationId", objGridVariables.ClassificationId);
                objDBParam[9] = new SqlParameter("@DivisionId", objGridVariables.DivisionId);
                objDBParam[10] = new SqlParameter("@DealerIds", dtDealers);
                objDBParam[11] = new SqlParameter("@CompanyIds", dtCompanies);

                var outputFromSP = _dataHelper.ExecuteProcedureForDataset("[dbo].[GetCompetitionDisplayReport]", objDBParam);

                if (outputFromSP.Tables[1].Rows.Count > 0)
                {
                    output.data = (from records in outputFromSP.Tables[1].AsEnumerable()
                                   select new CompetitionDisplayReportData
                                   {
                                       ActualDate = Convert.ToString(records.Field<string>("ActualDate")),
                                       BranchName = Convert.ToString(records.Field<string>("BranchName")),
                                       Brand = Convert.ToString(records.Field<string>("Brand")),
                                       Channel = Convert.ToString(records.Field<string>("Channel")),
                                       City = Convert.ToString(records.Field<string>("City")),
                                       DealerClass = Convert.ToString(records.Field<string>("DealerClass")),
                                       DealerCode = Convert.ToString(records.Field<string>("DealerCode")),
                                       DealerState = Convert.ToString(records.Field<string>("DealerState")),
                                       Division = Convert.ToString(records.Field<string>("Division")),
                                       Location = Convert.ToString(records.Field<string>("Location")),
                                       MasterCode = Convert.ToString(records.Field<string>("MasterCode")),
                                       Model = Convert.ToString(records.Field<string>("Model")),
                                       OutletName = Convert.ToString(records.Field<string>("OutletName")),
                                       ProdCat = Convert.ToString(records.Field<string>("ProdCat")),
                                       Quantity = Convert.ToString(records.Field<Int64>("Quantity")),
                                       SFAName = Convert.ToString(records.Field<string>("SFAName")),
                                       SFACode = Convert.ToString(records.Field<string>("SFACode"))
                                   });
                    output.recordsFiltered = Convert.ToInt32(outputFromSP.Tables[1].Rows[0]["FilteredCount"]);
                }
                else
                {
                    output.data = new List<CompetitionDisplayReportData>();
                    output.recordsFiltered = 0;
                }
                output.recordsTotal = Convert.ToInt32(outputFromSP.Tables[0].Rows[0]["TotalCount"]);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("CompetitionDisplay Report", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public GridOutput<IEnumerable<CompetitionSFACountReportData>> GetCompetitionSFACountReport(CompetitionSFACountReportFilters objGridVariables)
        {
            GridOutput<IEnumerable<CompetitionSFACountReportData>> output = new GridOutput<IEnumerable<CompetitionSFACountReportData>>();
            output.draw = objGridVariables.draw;
            DataTable dtDealers = new DataTable();
            DateTime FromDate = new DateTime(Convert.ToInt32(objGridVariables.FromDate.Split('/')[2]), Convert.ToInt32(objGridVariables.FromDate.Split('/')[1]), Convert.ToInt32(objGridVariables.FromDate.Split('/')[0]), 0, 0, 0);
            DateTime ToDate = new DateTime(Convert.ToInt32(objGridVariables.ToDate.Split('/')[2]), Convert.ToInt32(objGridVariables.ToDate.Split('/')[1]), Convert.ToInt32(objGridVariables.ToDate.Split('/')[0]), 23, 59, 59);
            DataRow row;
            try
            {
                dtDealers.Columns.Add("DealerId");
                if (objGridVariables.DealerIds != null)
                {
                    foreach (Int64 dealer in objGridVariables.DealerIds)
                    {
                        row = dtDealers.NewRow();
                        row["DealerId"] = dealer;
                        dtDealers.Rows.Add(row);
                    }
                }
                //else
                //{
                //    row = dtDealers.NewRow();
                //    row["DealerId"] = 0;
                //    dtDealers.Rows.Add(row);
                //}


                SqlParameter[] objDBParam = new SqlParameter[10];

                objDBParam[0] = new SqlParameter("@PageStart", objGridVariables.start);
                objDBParam[1] = new SqlParameter("@PageLength", objGridVariables.length);
                objDBParam[2] = new SqlParameter("@FromDate", FromDate);
                objDBParam[3] = new SqlParameter("@ToDate", ToDate);
                objDBParam[4] = new SqlParameter("@BranchId", objGridVariables.BranchId);
                objDBParam[5] = new SqlParameter("@StateId", objGridVariables.StateId);
                objDBParam[6] = new SqlParameter("@SFAId", objGridVariables.SFAId);
                objDBParam[7] = new SqlParameter("@ClassificationId", objGridVariables.ClassificationId);
                objDBParam[8] = new SqlParameter("@DivisionId", objGridVariables.DivisionId);
                objDBParam[9] = new SqlParameter("@DealerIds", dtDealers);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetCompetitionSFACountReport_Old]", objDBParam);

                if (outputFromSP.Rows.Count > 0)
                {
                    output.data = (from records in outputFromSP.AsEnumerable()
                                   select new CompetitionSFACountReportData
                                   {
                                       BranchName = records.Field<string>("BranchName"),
                                       Brand = records.Field<string>("Brand"),
                                       Channel = records.Field<string>("Channel"),
                                       City = records.Field<string>("City"),
                                       DealerClass = records.Field<string>("DealerClass"),
                                       DealerCode = records.Field<string>("DealerCode"),
                                       DealerState = records.Field<string>("DealerState"),
                                       Division = records.Field<string>("Division"),
                                       Location = records.Field<string>("Location"),
                                       MasterCode = records.Field<string>("MasterCode"),
                                       SFAName = records.Field<string>("SFAName"),
                                       SFACode = records.Field<string>("SFACode"),
                                       Count = records.Field<Int64>("Count"),
                                       DealerName = records.Field<string>("DealerName"),
                                       IncentiveCategory = records.Field<string>("IncentiveCategory"),
                                       SFALevel = records.Field<string>("SFALevel"),
                                       Type = records.Field<string>("Type")
                                   });
                    output.recordsFiltered = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                    output.recordsTotal = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                }
                else
                {
                    output.data = new List<CompetitionSFACountReportData>();
                    output.recordsFiltered = 0;
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("CompetitionSFACount Report", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public DownloadCompetitionSFACountReportData DownloadCompetitionSFACountReportData(CompetitionSFACountReportFilters objGridVariables)
        {
            DownloadCompetitionSFACountReportData output = new DownloadCompetitionSFACountReportData();
            DataTable dtDealers = new DataTable();
            DateTime FromDate = new DateTime(Convert.ToInt32(objGridVariables.FromDate.Split('/')[2]), Convert.ToInt32(objGridVariables.FromDate.Split('/')[1]), Convert.ToInt32(objGridVariables.FromDate.Split('/')[0]), 0, 0, 0);
            DateTime ToDate = new DateTime(Convert.ToInt32(objGridVariables.ToDate.Split('/')[2]), Convert.ToInt32(objGridVariables.ToDate.Split('/')[1]), Convert.ToInt32(objGridVariables.ToDate.Split('/')[0]), 23, 59, 59);
            DataRow row;
            try
            {
                dtDealers.Columns.Add("DealerId");
                if (objGridVariables.DealerIds != null)
                {
                    foreach (Int64 dealer in objGridVariables.DealerIds)
                    {
                        row = dtDealers.NewRow();
                        row["DealerId"] = dealer;
                        dtDealers.Rows.Add(row);
                    }
                }
                else
                {
                    row = dtDealers.NewRow();
                    row["DealerId"] = 0;
                    dtDealers.Rows.Add(row);
                }


                SqlParameter[] objDBParam = new SqlParameter[10];

                objDBParam[0] = new SqlParameter("@PageStart", objGridVariables.start);
                objDBParam[1] = new SqlParameter("@PageLength", objGridVariables.length);
                objDBParam[2] = new SqlParameter("@FromDate", FromDate);
                objDBParam[3] = new SqlParameter("@ToDate", ToDate);
                objDBParam[4] = new SqlParameter("@BranchId", objGridVariables.BranchId);
                objDBParam[5] = new SqlParameter("@StateId", objGridVariables.StateId);
                objDBParam[6] = new SqlParameter("@SFAId", objGridVariables.SFAId);
                objDBParam[7] = new SqlParameter("@ClassificationId", objGridVariables.ClassificationId);
                objDBParam[8] = new SqlParameter("@DivisionId", objGridVariables.DivisionId);
                objDBParam[9] = new SqlParameter("@DealerIds", dtDealers);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetCompetitionSFACountReport_Old]", objDBParam);

                DataRow newrow;
                if (outputFromSP.Rows.Count > 0)
                {

                    output.ExcelData = outputFromSP;

                    //for (int i = 0; i < 2*outputFromSP.Rows.Count; i++)
                    //{
                    //    newrow = output.ExcelData.NewRow();
                    //    newrow[0] = 198763;
                    //    newrow[1] = 55667788;
                    //    newrow[2] = "Cochin";
                    //    newrow[3] = "3G Mobile World";
                    //    newrow[4] = "Calicut";
                    //    newrow[5] = "Mavoor Road";
                    //    newrow[6] = "Dealers";
                    //    newrow[7] = "Kerala";
                    //    newrow[8] = "1015375";
                    //    newrow[9] = "8129";
                    //    newrow[10] = "N/A";
                    //    newrow[11] = "S9809";
                    //    newrow[12] = "ARSHAD V";
                    //    newrow[13] = "C1";
                    //    newrow[14] = "Mobile 1";
                    //    newrow[15] = "MOBILE";
                    //    newrow[16] = "Samsung";
                    //    newrow[17] = "Mobile";
                    //    newrow[18] = 2;

                    //    output.ExcelData.Rows.Add(newrow);
                    //}

                }
                else
                    output.ExcelData = new DataTable();
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("DownloadCompetitionSFACountReportData", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public GridOutput<IEnumerable<MessageReportData>> GetMessageReport(MessageReportFilters objGridVariables)
        {
            GridOutput<IEnumerable<MessageReportData>> output = new GridOutput<IEnumerable<MessageReportData>>();
            output.draw = objGridVariables.draw;
            DataTable dtDealers = new DataTable();
            DateTime FromDate = new DateTime(Convert.ToInt32(objGridVariables.FromDate.Split('/')[2]), Convert.ToInt32(objGridVariables.FromDate.Split('/')[1]), Convert.ToInt32(objGridVariables.FromDate.Split('/')[0]), 0, 0, 0);
            DateTime ToDate = new DateTime(Convert.ToInt32(objGridVariables.ToDate.Split('/')[2]), Convert.ToInt32(objGridVariables.ToDate.Split('/')[1]), Convert.ToInt32(objGridVariables.ToDate.Split('/')[0]), 23, 59, 59);
            DataRow row;
            try
            {
                dtDealers.Columns.Add("DealerId");
                if (objGridVariables.DealerIds != null)
                {
                    foreach (Int64 dealer in objGridVariables.DealerIds)
                    {
                        row = dtDealers.NewRow();
                        row["DealerId"] = dealer;
                        dtDealers.Rows.Add(row);
                    }
                }
                SqlParameter[] objDBParam = new SqlParameter[10];

                objDBParam[0] = new SqlParameter("@PageStart", objGridVariables.start);
                objDBParam[1] = new SqlParameter("@PageLength", objGridVariables.length);
                objDBParam[2] = new SqlParameter("@FromDate", FromDate);
                objDBParam[3] = new SqlParameter("@ToDate", ToDate);
                objDBParam[4] = new SqlParameter("@BranchId", objGridVariables.BranchId);
                objDBParam[5] = new SqlParameter("@StateId", objGridVariables.StateId);
                objDBParam[6] = new SqlParameter("@SFAId", objGridVariables.SFAId);
                objDBParam[7] = new SqlParameter("@DivisionId", objGridVariables.DivisionId);
                objDBParam[8] = new SqlParameter("@MessageTypeId", objGridVariables.MessageTypeId);
                objDBParam[9] = new SqlParameter("@DealerIds", dtDealers);

                var outputFromSP = _dataHelper.ExecuteProcedureForDataset("[dbo].[GetMessageReport]", objDBParam);

                if (outputFromSP.Tables[0].Rows.Count > 0)
                {
                    output.data = (from records in outputFromSP.Tables[0].AsEnumerable()
                                   select new MessageReportData
                                   {
                                       Branch = Convert.ToString(records.Field<string>("BranchName")),
                                       Dealer = Convert.ToString(records.Field<string>("DealerName")),
                                       IncentiveCategory = Convert.ToString(records.Field<string>("CategoryName")),
                                       Message = Convert.ToString(records.Field<string>("Message")),
                                       MessageFile = Convert.ToString(records.Field<string>("FileName")),
                                       MessageType = Convert.ToString(records.Field<string>("MessageType")),
                                       Reply = Convert.ToString(records.Field<string>("Reply")),//
                                       ReplyDate = Convert.ToString(records.Field<string>("ReplyDate")),//
                                       ReplyFile = Convert.ToString(records.Field<string>("ReplyFileName")),//
                                       SendDate = Convert.ToString(records.Field<string>("SendDate")),
                                       SFA = Convert.ToString(records.Field<string>("SFAName")),
                                       SFADivision = Convert.ToString(records.Field<string>("DivisionName")),
                                       SFALevel = Convert.ToString(records.Field<string>("SFALevel")),
                                       State = Convert.ToString(records.Field<string>("StateName")),
                                       ValidFrom = Convert.ToString(records.Field<string>("ValidFrom")),//
                                       ValidTo = Convert.ToString(records.Field<string>("ValidTo")),//
                                       Channel = Convert.ToString(records.Field<string>("ChannelName")),
                                       City = Convert.ToString(records.Field<string>("CityName")),
                                       DealerCode = Convert.ToString(records.Field<string>("DealerCode")),
                                       Location = Convert.ToString(records.Field<string>("LocationName")),
                                       MasterCode = Convert.ToString(records.Field<string>("SAPCode")),
                                       SFACode = Convert.ToString(records.Field<string>("SFACode"))
                                   });
                    output.recordsFiltered = Convert.ToInt32(outputFromSP.Tables[0].Rows[0]["FilteredCount"]);
                    output.recordsTotal = Convert.ToInt32(outputFromSP.Tables[0].Rows[0]["FilteredCount"]);
                }
                else
                {
                    output.data = new List<MessageReportData>();
                    output.recordsFiltered = 0;
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("Message Report", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public GridOutput<IEnumerable<TargetVsAchievementReportData>> GetTargetVsAchievementReport(TargetVsAchievementReportFilters objGridVariables)
        {
            GridOutput<IEnumerable<TargetVsAchievementReportData>> output = new GridOutput<IEnumerable<TargetVsAchievementReportData>>();
            output.draw = objGridVariables.draw;
            DataTable dtProdCat = new DataTable();
            //DateTime inputMonth = new DateTime(
            //    Convert.ToInt32(objGridVariables.Month.Split('/')[1]), 
            //    Convert.ToInt32(objGridVariables.Month.Split('/')[0]), 
            //    1, 0, 0, 0);
            DataRow row;
            try
            {
                dtProdCat.Columns.Add("UserId");
                dtProdCat.Columns.Add("ProdCatId");
                if (objGridVariables.ProductCategoryIds != null)
                {
                    foreach (Int64 prodcat in objGridVariables.ProductCategoryIds)
                    {
                        row = dtProdCat.NewRow();
                        row["ProdCatId"] = prodcat;
                        row["UserId"] = objGridVariables.UserId;
                        dtProdCat.Rows.Add(row);
                    }
                }
                SqlParameter[] objDBParam = new SqlParameter[7];

                objDBParam[0] = new SqlParameter("@PageStart", objGridVariables.start);
                objDBParam[1] = new SqlParameter("@PageLength", objGridVariables.length);
                objDBParam[2] = new SqlParameter("@Month", objGridVariables.Month);
                objDBParam[3] = new SqlParameter("@BranchId", objGridVariables.BranchId);
                objDBParam[4] = new SqlParameter("@SFAId", objGridVariables.SFAId);
                objDBParam[5] = new SqlParameter("@DivisionId", objGridVariables.DivisionId);
                objDBParam[6] = new SqlParameter("@ProductCategories", dtProdCat);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetTargetVsAchievementReport]", objDBParam);

                if (outputFromSP.Rows.Count > 0)
                {
                    output.data = (from records in outputFromSP.AsEnumerable()
                                   select new TargetVsAchievementReportData
                                   {
                                       BranchName = records.Field<string>("Branch"),
                                       SFAName = records.Field<string>("SFAName"),
                                       SFACode = records.Field<string>("SFACode"),
                                       SFALevel = records.Field<string>("SFALevel"),
                                       DealerName = records.Field<string>("DealerName"),
                                       DealerCode = records.Field<string>("DealerCode"),
                                       SAPCode = records.Field<string>("SAPCode"),
                                       Channel = records.Field<string>("Channel"),
                                       City = records.Field<string>("City"),
                                       Location = records.Field<string>("Location"),
                                       ProductCategory = records.Field<string>("ProductCategory"),
                                       TargetCategory = records.Field<string>("TargetCategory"),
                                       IncentiveCategory = records.Field<string>("IncentiveCategory"),
                                       Division = records.Field<string>("Division"),
                                       TargetQty = records.Field<Int64>("QtyTarget"),
                                       AchQty = records.Field<Int64>("QtyAch"),
                                       TargetValue = records.Field<Int64>("ValueTarget"),
                                       AchValue = records.Field<Int64>("ValueAch")
                                   });
                    output.recordsFiltered = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                    output.recordsTotal = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                }
                else
                {
                    output.data = new List<TargetVsAchievementReportData>();
                    output.recordsFiltered = 0;
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("TargetVsAchievement Report", ex.StackTrace, ex.Message);
            }
            return output;
        }

        #region Daily Sales With Attribute
        public GridOutput<IEnumerable<DailySalesWithAttributeReportGrid>> GetDailySalesWithAttributeReport(DailySalesWithAttributesReport objGridVariables)
        {
            GridOutput<IEnumerable<DailySalesWithAttributeReportGrid>> output = new GridOutput<IEnumerable<DailySalesWithAttributeReportGrid>>();
            output.draw = objGridVariables.draw;
            DataTable dtDealers = new DataTable();
            DataTable dtCompanies = new DataTable();
            DataRow row;
            try
            {
                dtDealers.Columns.Add("DealerId");
                if (objGridVariables.DealerIds != null)
                {
                    foreach (Int64 dealer in objGridVariables.DealerIds)
                    {
                        row = dtDealers.NewRow();
                        row["DealerId"] = dealer;
                        dtDealers.Rows.Add(row);
                    }
                }

                dtCompanies.Columns.Add("CompanyId");
                if (objGridVariables.CompanyIds != null)
                {
                    foreach (Int64 company in objGridVariables.CompanyIds)
                    {
                        row = dtCompanies.NewRow();
                        row["CompanyId"] = company;
                        dtCompanies.Rows.Add(row);
                    }
                }

                SqlParameter[] objDBParam = new SqlParameter[14];

                objDBParam[0] = new SqlParameter("@PageStart", objGridVariables.start);
                objDBParam[1] = new SqlParameter("@PageLength", objGridVariables.length);
                objDBParam[2] = new SqlParameter("@FromDate", objGridVariables.FromDate);
                objDBParam[3] = new SqlParameter("@ToDate", objGridVariables.ToDate);
                objDBParam[4] = new SqlParameter("@BranchId", objGridVariables.BranchId);
                objDBParam[5] = new SqlParameter("@StateId", objGridVariables.StateId);
                objDBParam[6] = new SqlParameter("@SFAId", objGridVariables.SFAId);
                objDBParam[7] = new SqlParameter("@ProductCategoryId", objGridVariables.ProductCategoryId);
                objDBParam[8] = new SqlParameter("@ProductSubCategoryId", objGridVariables.ProSubCatId);
                objDBParam[9] = new SqlParameter("@ClassificationId", objGridVariables.ClassificationId);
                objDBParam[10] = new SqlParameter("@DivisionId", objGridVariables.DivisionId);
                objDBParam[11] = new SqlParameter("@DealerIds", dtDealers);
                objDBParam[12] = new SqlParameter("@CompanyIds", dtCompanies);
                objDBParam[13] = new SqlParameter("@SFAType", objGridVariables.SFAType);

                var outputFromSP = _dataHelper.ExecuteProcedureForDataset("[dbo].[GetDailySalesWithAttributeReport]", objDBParam);

                if (outputFromSP.Tables[0].Rows.Count > 0)
                {
                    output.data = (from records in outputFromSP.Tables[0].AsEnumerable()
                                   select new DailySalesWithAttributeReportGrid
                                   {
                                       Branch = Convert.ToString(records.Field<string>("Branch")),
                                       Date = Convert.ToString(records.Field<string>("Date")),
                                       Month = Convert.ToString(records.Field<string>("Month")),
                                       DealerName = Convert.ToString(records.Field<string>("DealerName")),
                                       City = Convert.ToString(records.Field<string>("City")),
                                       Location = Convert.ToString(records.Field<string>("Location")),
                                       PayerName = Convert.ToString(records.Field<string>("PayerName")),
                                       Channel = Convert.ToString(records.Field<string>("Channel")),
                                       State = Convert.ToString(records.Field<string>("DealerState")),
                                       SAPCode = Convert.ToString(records.Field<string>("MasterCode")),
                                       DealerCode = Convert.ToString(records.Field<string>("DealerCode")),
                                       DealerClassification = Convert.ToString(records.Field<string>("DealerClassification")),
                                       SFACode = Convert.ToString(records.Field<string>("SFACode")),
                                       SFAName = Convert.ToString(records.Field<string>("SFAName")),
                                       SFALevel = Convert.ToString(records.Field<string>("SFALevel")),
                                       IncentiveCate = Convert.ToString(records.Field<string>("IncentiveCategory")),
                                       CompanyName = Convert.ToString(records.Field<string>("CompanyName")),
                                       ProCat = Convert.ToString(records.Field<string>("ProductCategory")),
                                       ProSubCat = Convert.ToString(records.Field<string>("ProductSubCategory")),
                                       ProSubCatDescription = Convert.ToString(records.Field<string>("ProductSubCategoryDescription")),
                                       SonyMaterial = Convert.ToString(records.Field<string>("Material")),
                                       Division = Convert.ToString(records.Field<string>("Division")),
                                       MOP = Convert.ToString(records.Field<string>("MOP")),
                                       Quantity = Convert.ToString(records.Field<string>("Quantity")),
                                       TotalMOP = Convert.ToString(records.Field<string>("TotalMOP")),
                                       //CoreCategory = Convert.ToString(records.Field<string>("CoreCategory")),
                                       SubCatGroup = Convert.ToString(records.Field<string>("SubCategoryGrouping")),
                                       Segment = Convert.ToString(records.Field<string>("Segment")),
                                       Resolution = Convert.ToString(records.Field<string>("Resolution")),
                                       Internet = Convert.ToString(records.Field<string>("Internet")),
                                       S3D = Convert.ToString(records.Field<string>("3D")),
                                       TVType = Convert.ToString(records.Field<string>("TVType")),
                                       SFAType = Convert.ToString(records.Field<string>("SFAType")),
                                       QSRStatus= Convert.ToString(records.Field<string>("QSRStatus")),
                                       QSRQuantity= Convert.ToString(records.Field<string>("QSRQuantity")),

                                   });
                    output.recordsFiltered = Convert.ToInt32(outputFromSP.Tables[0].Rows[0]["FilteredCount"]);
                    output.recordsTotal = Convert.ToInt32(outputFromSP.Tables[0].Rows[0]["FilteredCount"]);
                }
                else
                {
                    output.data = new List<DailySalesWithAttributeReportGrid>();
                    output.recordsFiltered = 0;
                }

            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("DailySalesWithAttribute Report", ex.StackTrace, ex.Message);
            }
            return output;
        }
        #endregion

        #region Daily Sales With IMEI
        public GridOutput<IEnumerable<DailySalesReportIMEIGrid>> GetDailySalesIMEIReport(DailySalesReportIMEI objGridVariables)
        {
            GridOutput<IEnumerable<DailySalesReportIMEIGrid>> output = new GridOutput<IEnumerable<DailySalesReportIMEIGrid>>();
            output.draw = objGridVariables.draw;
            DataTable dtDealers = new DataTable();

            DataRow row;
            try
            {
                dtDealers.Columns.Add("DealerId");
                if (objGridVariables.DealerIds != null)
                {
                    foreach (Int64 dealer in objGridVariables.DealerIds)
                    {
                        row = dtDealers.NewRow();
                        row["DealerId"] = dealer;
                        dtDealers.Rows.Add(row);
                    }
                }

                SqlParameter[] objDBParam = new SqlParameter[12];

                objDBParam[0] = new SqlParameter("@PageStart", objGridVariables.start);
                objDBParam[1] = new SqlParameter("@PageLength", objGridVariables.length);
                objDBParam[2] = new SqlParameter("@FromDate", objGridVariables.FromDate);
                objDBParam[3] = new SqlParameter("@ToDate", objGridVariables.ToDate);
                objDBParam[4] = new SqlParameter("@BranchId", objGridVariables.BranchId);
                objDBParam[5] = new SqlParameter("@StateId", objGridVariables.StateId);
                objDBParam[6] = new SqlParameter("@SFAId", objGridVariables.SFAId);
                objDBParam[7] = new SqlParameter("@ProductCategoryId", objGridVariables.ProductCategoryId);
                objDBParam[8] = new SqlParameter("@ProductSubCategoryId", objGridVariables.ProSubCatId);
                objDBParam[9] = new SqlParameter("@ClassificationId", objGridVariables.ClassificationId);
                objDBParam[10] = new SqlParameter("@DivisionId", objGridVariables.DivisionId);
                objDBParam[11] = new SqlParameter("@DealerIds", dtDealers);

                var outputFromSP = _dataHelper.ExecuteProcedureForDataset("[dbo].[GetDailySalesIMEIReport]", objDBParam);

                if (outputFromSP.Tables[0].Rows.Count > 0)
                {
                    output.data = (from records in outputFromSP.Tables[0].AsEnumerable()
                                   select new DailySalesReportIMEIGrid
                                   {
                                       Branch = Convert.ToString(records.Field<string>("Branch")),
                                       Date = Convert.ToString(records.Field<string>("Date")),
                                       DealerName = Convert.ToString(records.Field<string>("DealerName")),
                                       City = Convert.ToString(records.Field<string>("City")),
                                       Location = Convert.ToString(records.Field<string>("Location")),
                                       Channel = Convert.ToString(records.Field<string>("Channel")),
                                       State = Convert.ToString(records.Field<string>("DealerState")),
                                       SAPCode = Convert.ToString(records.Field<string>("MasterCode")),
                                       DealerCode = Convert.ToString(records.Field<string>("DealerCode")),
                                       DealerClassification = Convert.ToString(records.Field<string>("DealerClassification")),
                                       SFACode = Convert.ToString(records.Field<string>("SFACode")),
                                       SFAName = Convert.ToString(records.Field<string>("SFAName")),
                                       SFALevel = Convert.ToString(records.Field<string>("SFALevel")),
                                       IncentiveCate = Convert.ToString(records.Field<string>("IncentiveCategory")),
                                       ProCat = Convert.ToString(records.Field<string>("ProductCategory")),
                                       ProSubCat = Convert.ToString(records.Field<string>("ProductSubCategory")),
                                       ProSubCatDescription = Convert.ToString(records.Field<string>("ProductSubCategoryDescription")),
                                       SonyMaterial = Convert.ToString(records.Field<string>("Material")),
                                       Division = Convert.ToString(records.Field<string>("Division")),
                                       Quantity = Convert.ToString(records.Field<string>("Quantity")),
                                       //CoreCategory = Convert.ToString(records.Field<string>("CoreCategory")),
                                       IMEI = Convert.ToString(records.Field<string>("IMEI")),
                                       Type = Convert.ToString(records.Field<string>("Type"))
                                   });
                    output.recordsFiltered = Convert.ToInt32(outputFromSP.Tables[0].Rows[0]["FilteredCount"]);
                    output.recordsTotal = Convert.ToInt32(outputFromSP.Tables[0].Rows[0]["FilteredCount"]);
                }
                else
                {
                    output.data = new List<DailySalesReportIMEIGrid>();
                    output.recordsFiltered = 0;
                }

            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("DailySalesIMEI Report", ex.StackTrace, ex.Message);
            }
            return output;
        }
        #endregion

        #region Daily Timing Report [Renamed from Daily Attendance Report]
        /// <summary>
        /// To fetch Daily Timing Report from Database.
        /// Dhruv Sharma, ValueFirst, Gurgaon
        /// </summary>
        /// <param name="Input">Input Parameters</param>
        /// <returns>Daily Timing Report</returns>
        public GridOutput<IEnumerable<DailyTimingReport>> GetDailyTimingReport(DailyTimingReportInput Input)
        {
            GridOutput<IEnumerable<DailyTimingReport>> DailyTimingReportList = new GridOutput<IEnumerable<DailyTimingReport>>();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@BranchId", Input.BranchId, DbType.Int64));
                objDBParam.Add(new DbParameter("@SFAId", Input.SFAId, DbType.Int64));
                objDBParam.Add(new DbParameter("@StartDate", DateTime.ParseExact(Input.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture), DbType.DateTime));
                objDBParam.Add(new DbParameter("@EndDate", DateTime.ParseExact(Input.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture), DbType.DateTime));
                objDBParam.Add(new DbParameter("@AttType", Input.AttType, DbType.Int64));
                objDBParam.Add(new DbParameter("@PageStart", Input.start, DbType.Int64));
                objDBParam.Add(new DbParameter("@PageLength", Input.length, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetDailyTimingReport]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    DailyTimingReportList.data = (from data in outputFromSP.AsEnumerable()
                                                  select new DailyTimingReport
                                                  {
                                                      Attendance = Convert.ToString(data.Field<string>("Attendance")),
                                                      AttendanceTypeId = Convert.ToInt64(data.Field<Int64>("AttendanceTypeId")),
                                                      BranchName = Convert.ToString(data.Field<string>("BranchName")),
                                                      CoreSalesStatus = Convert.ToString(data.Field<string>("CoreSalesStatus")),
                                                      CreationDate = Convert.ToString(data.Field<string>("CreationDate")),
                                                      DealerCity = Convert.ToString(data.Field<string>("DealerCity")),
                                                      DealerCode = Convert.ToString(data.Field<string>("DealerCode")),
                                                      DealerLocation = Convert.ToString(data.Field<string>("DealerLocation")),
                                                      DealerName = Convert.ToString(data.Field<string>("DealerName")),
                                                      ImagePath = Convert.ToString(data.Field<string>("ImagePath")),
                                                      IMEI1 = Convert.ToString(data.Field<string>("IMEI1")),
                                                      IncentiveCategory = Convert.ToString(data.Field<string>("IncentiveCategory")),
                                                      LoginId = Convert.ToString(data.Field<string>("LoginId")),
                                                      MasterCode = Convert.ToString(data.Field<string>("MasterCode1")),
                                                      Region = Convert.ToString(data.Field<string>("Region")),
                                                      RowNum = Convert.ToInt64(data.Field<Int64>("RowNum")),
                                                      SalesStatus = Convert.ToString(data.Field<string>("SalesStatus")),
                                                      SFACategory = Convert.ToString(data.Field<string>("SFACategory")),
                                                      SFACode = Convert.ToString(data.Field<string>("SFACode")),
                                                      SFALevel = Convert.ToString(data.Field<string>("SFALevel")),
                                                      SFALocation = Convert.ToString(data.Field<string>("SFALocation")),
                                                      SFAName = Convert.ToString(data.Field<string>("SFAName")),
                                                      TimeIn = Convert.ToString(data.Field<string>("TimeIn")),
                                                      TimeOut = Convert.ToString(data.Field<string>("TimeOut")),
                                                      TotalWorkingHrs = Convert.ToString(data.Field<string>("TotalWorkingHrs")),
                                                      ShiftName = Convert.ToString(data.Field<string>("ShiftName")),
                                                    //  StoreLocation = Convert.ToString(data.Field<string>("StoreLocation")),
                                                  }).ToList();
                    DailyTimingReportList.recordsFiltered = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                    DailyTimingReportList.recordsTotal = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                }
                else
                {
                    DailyTimingReportList.data = new List<DailyTimingReport>();
                    DailyTimingReportList.recordsFiltered = 0;
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("DailyTiming Report", ex.StackTrace, ex.Message);
            }
            return DailyTimingReportList;
        }
        #endregion

        #region Stock Report
        public GridOutput<IEnumerable<StockReportGrid>> GetStockReport(StockReport objGridVariables)
        {
            GridOutput<IEnumerable<StockReportGrid>> output = new GridOutput<IEnumerable<StockReportGrid>>();
            output.draw = objGridVariables.draw;
            DataTable dtDealers = new DataTable();

            DataRow row;
            try
            {
                dtDealers.Columns.Add("DealerId");
                if (objGridVariables.DealerIds != null)
                {
                    foreach (Int64 dealer in objGridVariables.DealerIds)
                    {
                        row = dtDealers.NewRow();
                        row["DealerId"] = dealer;
                        dtDealers.Rows.Add(row);
                    }
                }

                SqlParameter[] objDBParam = new SqlParameter[12];

                objDBParam[0] = new SqlParameter("@PageStart", objGridVariables.start);
                objDBParam[1] = new SqlParameter("@PageLength", objGridVariables.length);
                objDBParam[2] = new SqlParameter("@FromDate", objGridVariables.FromDate);
                objDBParam[3] = new SqlParameter("@ToDate", objGridVariables.ToDate);
                objDBParam[4] = new SqlParameter("@BranchId", objGridVariables.BranchId);
                objDBParam[5] = new SqlParameter("@StateId", objGridVariables.StateId);
                objDBParam[6] = new SqlParameter("@SFAId", objGridVariables.SFAId);
                objDBParam[7] = new SqlParameter("@ProductCategoryId", objGridVariables.ProductCategoryId);
                objDBParam[8] = new SqlParameter("@ProductSubCategoryId", objGridVariables.ProSubCatId);
                objDBParam[9] = new SqlParameter("@ClassificationId", objGridVariables.ClassificationId);
                objDBParam[10] = new SqlParameter("@DealerIds", dtDealers);
                objDBParam[11] = new SqlParameter("@SFATypeId", objGridVariables.SFATypeId);

                var outputFromSP = _dataHelper.ExecuteProcedureForDataset("[dbo].[GetStockReport]", objDBParam);

                if (outputFromSP.Tables[0].Rows.Count > 0)
                {
                    output.data = (from records in outputFromSP.Tables[0].AsEnumerable()
                                   select new StockReportGrid
                                   {
                                       Branch = Convert.ToString(records.Field<string>("Branch")),
                                       Date = Convert.ToString(records.Field<string>("Date")),
                                       DealerName = Convert.ToString(records.Field<string>("DealerName")),
                                       City = Convert.ToString(records.Field<string>("City")),
                                       Location = Convert.ToString(records.Field<string>("Location")),
                                       Channel = Convert.ToString(records.Field<string>("Channel")),
                                       State = Convert.ToString(records.Field<string>("DealerState")),
                                       SAPCode = Convert.ToString(records.Field<string>("MasterCode")),
                                       PartyName = Convert.ToString(records.Field<string>("PartyName")),
                                       DealerCode = Convert.ToString(records.Field<string>("DealerCode")),
                                       DealerClassification = Convert.ToString(records.Field<string>("DealerClassification")),
                                       SFACode = Convert.ToString(records.Field<string>("SFACode")),
                                       SFAName = Convert.ToString(records.Field<string>("SFAName")),
                                       SFALevel = Convert.ToString(records.Field<string>("SFALevel")),
                                       IncentiveCate = Convert.ToString(records.Field<string>("IncentiveCategory")),
                                       ProCat = Convert.ToString(records.Field<string>("ProductCategory")),
                                       ProSubCat = Convert.ToString(records.Field<string>("ProductSubCategory")),
                                       ProSubCatDescription = Convert.ToString(records.Field<string>("ProductSubCategoryDescription")),
                                       SonyMaterial = Convert.ToString(records.Field<string>("Material")),
                                       Quantity = Convert.ToString(records.Field<string>("Quantity")),
                                       SFAType = Convert.ToString(records.Field<string>("SFAType")),
                                       //CoreCategory = Convert.ToString(records.Field<string>("CoreCategory")),
                                   });
                    output.recordsFiltered = Convert.ToInt32(outputFromSP.Tables[0].Rows[0]["FilteredCount"]);
                    output.recordsTotal = Convert.ToInt32(outputFromSP.Tables[0].Rows[0]["FilteredCount"]);
                }
                else
                {
                    output.data = new List<StockReportGrid>();
                    output.recordsFiltered = 0;
                }

            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("Stock Report", ex.StackTrace, ex.Message);
            }
            return output;
        }
        #endregion

        //#region Display Report
        //public GridOutput<IEnumerable<DisplayReportGrid>> GetDisplayReport(DisplayReport objGridVariables)
        //{
        //    GridOutput<IEnumerable<DisplayReportGrid>> output = new GridOutput<IEnumerable<DisplayReportGrid>>();
        //    output.draw = objGridVariables.draw;
        //    DataTable dtDealers = new DataTable();
        //    DbParameterCollection objDBParam = new DbParameterCollection();
        //    DataRow row;

        //    try
        //    {
        //        objDBParam.Add(new DbParameter("@PageStart", objGridVariables.start, DbType.Int32));
        //        objDBParam.Add(new DbParameter("@PageLength", objGridVariables.length, DbType.Int32));
        //        objDBParam.Add(new DbParameter("@FromDate", objGridVariables.FromDate));
        //        objDBParam.Add(new DbParameter("@ToDate", objGridVariables.ToDate));
        //        objDBParam.Add(new DbParameter("@BranchId", objGridVariables.BranchId, DbType.Int64));
        //        objDBParam.Add(new DbParameter("@DealerId", objGridVariables.DealerId, DbType.Int64));
        //        objDBParam.Add(new DbParameter("@StateId", objGridVariables.StateId, DbType.Int64));
        //        objDBParam.Add(new DbParameter("@ProductCategoryId", objGridVariables.ProductCategoryId, DbType.Int64));
        //        objDBParam.Add(new DbParameter("@SFATypeId", objGridVariables.SFATypeId, DbType.Int64));

        //        var outputFromSP = _dataHelper.ExecuteDataSet("[dbo].[GetDisplayReportAndCompetitiopnDisplayReport]", objDBParam, CommandType.StoredProcedure);
        //       // var outputFromSP = _dataHelper.ExecuteDataSet("[dbo].[GetDisplayReport]", objDBParam, CommandType.StoredProcedure);

        //        if (outputFromSP.Tables[0].Rows.Count > 0)
        //        {
        //            output.data = (from records in outputFromSP.Tables[0].AsEnumerable()
        //                           select new DisplayReportGrid
        //                           {
        //                               Branch = Convert.ToString(records.Field<string>("BranchName")),
        //                               Date = Convert.ToString(records.Field<string>("ActualDate")),
        //                               DealerName = Convert.ToString(records.Field<string>("OutletName")),
        //                               City = Convert.ToString(records.Field<string>("City")),
        //                               Location = Convert.ToString(records.Field<string>("Location")),
        //                               Channel = Convert.ToString(records.Field<string>("Channel")),
        //                               State = Convert.ToString(records.Field<string>("DealerState")),
        //                               SAPCode = Convert.ToString(records.Field<string>("MasterCode")),
        //                               PartyName = Convert.ToString(records.Field<string>("PartyName")),
        //                               DealerCode = Convert.ToString(records.Field<string>("DealerCode")),
        //                               DealerClassification = Convert.ToString(records.Field<string>("DealerClassification")),
        //                               SFACode = Convert.ToString(records.Field<string>("SFACode")),
        //                               SFAName = Convert.ToString(records.Field<string>("SFAName")),
        //                               SFALevel = Convert.ToString(records.Field<string>("SFALevel")),
        //                               IncentiveCate = Convert.ToString(records.Field<string>("IncentiveCategory")),
        //                               ProCat = Convert.ToString(records.Field<string>("ProductCategory")),
        //                               ProSubCat = Convert.ToString(records.Field<string>("ProductSubCategory")),
        //                               SonyMaterial = Convert.ToString(records.Field<string>("MaterialName")),
        //                               Division = Convert.ToString(records.Field<string>("Division")),
        //                               PlannedQuantity = Convert.ToString(records.Field<string>("PlannedQuantity")),
        //                               DisplayQuantity = Convert.ToString(records.Field<string>("DisplayQuantity")),
        //                               Gap = Convert.ToString(records.Field<string>("Gap")),
        //                               SFAType = Convert.ToString(records.Field<string>("SFAType")),
        //                               SubCatGroup = Convert.ToString(records.Field<string>("SubCategoryGrouping")),
        //                               Segment = Convert.ToString(records.Field<string>("Segment")),
        //                               Resolution = Convert.ToString(records.Field<string>("Resolution")),
        //                               Internet = Convert.ToString(records.Field<string>("Internet")),
        //                               S3D = Convert.ToString(records.Field<string>("3D")),
        //                               TVType = Convert.ToString(records.Field<string>("TVType")),
        //                               FY = Convert.ToString(records.Field<string>("FY")),
        //                               //Model = Convert.ToString(records.Field<string>("Model")),
        //                               Brand = Convert.ToString(records.Field<string>("Brand")),
        //                               Quantity = Convert.ToString(records.Field<string>("Quantity"))

        //                           });
        //            output.recordsFiltered = Convert.ToInt32(outputFromSP.Tables[0].Rows[0]["FilteredCount"]);
        //            output.recordsTotal = Convert.ToInt32(outputFromSP.Tables[0].Rows[0]["FilteredCount"]);
        //        }
        //        else
        //        {
        //            output.data = new List<DisplayReportGrid>();
        //            output.recordsFiltered = 0;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        _IErrorLogProvider.CreateErrorLog("Display Report", ex.StackTrace, ex.Message);
        //    }
        //    return output;
        //}
        //#endregion

        #region Display Report
        public GridOutput<IEnumerable<DisplayReportGrid>> GetDisplayReport(DisplayReport objGridVariables)
        {
            GridOutput<IEnumerable<DisplayReportGrid>> output = new GridOutput<IEnumerable<DisplayReportGrid>>();
            output.draw = objGridVariables.draw;
            DataTable dtDealers = new DataTable();
            DbParameterCollection objDBParam = new DbParameterCollection();
            DataRow row;

            try
            {
                objDBParam.Add(new DbParameter("@PageStart", objGridVariables.start, DbType.Int32));
                objDBParam.Add(new DbParameter("@PageLength", objGridVariables.length, DbType.Int32));
                objDBParam.Add(new DbParameter("@FromDate", objGridVariables.FromDate));
                objDBParam.Add(new DbParameter("@ToDate", objGridVariables.ToDate));
                objDBParam.Add(new DbParameter("@BranchId", objGridVariables.BranchId, DbType.Int64));
                objDBParam.Add(new DbParameter("@DealerId", objGridVariables.DealerId, DbType.Int64));
                objDBParam.Add(new DbParameter("@StateId", objGridVariables.StateId, DbType.Int64));
                objDBParam.Add(new DbParameter("@ProductCategoryId", objGridVariables.ProductCategoryId, DbType.Int64));
                objDBParam.Add(new DbParameter("@SFATypeId", objGridVariables.SFATypeId, DbType.Int64));

                //var outputFromSP = _dataHelper.ExecuteDataSet("[dbo].[GetDisplayReportAndCompetitiopnDisplayReport]", objDBParam, CommandType.StoredProcedure);
                var outputFromSP = _dataHelper.ExecuteDataSet("[dbo].[GetDisplayReport]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Tables[0].Rows.Count > 0)
                {
                    output.data = (from records in outputFromSP.Tables[0].AsEnumerable()
                                   select new DisplayReportGrid
                                   {
                                       Branch = Convert.ToString(records.Field<string>("BranchName")),
                                       Date = Convert.ToString(records.Field<string>("ActualDate")),
                                       DealerName = Convert.ToString(records.Field<string>("OutletName")),
                                       City = Convert.ToString(records.Field<string>("City")),
                                       Location = Convert.ToString(records.Field<string>("Location")),
                                       Channel = Convert.ToString(records.Field<string>("Channel")),
                                       State = Convert.ToString(records.Field<string>("DealerState")),
                                       SAPCode = Convert.ToString(records.Field<string>("MasterCode")),
                                       PartyName = Convert.ToString(records.Field<string>("PartyName")),
                                       DealerCode = Convert.ToString(records.Field<string>("DealerCode")),
                                       DealerClassification = Convert.ToString(records.Field<string>("DealerClassification")),
                                       SFACode = Convert.ToString(records.Field<string>("SFACode")),
                                       SFAName = Convert.ToString(records.Field<string>("SFAName")),
                                       SFALevel = Convert.ToString(records.Field<string>("SFALevel")),
                                       IncentiveCate = Convert.ToString(records.Field<string>("IncentiveCategory")),
                                       ProCat = Convert.ToString(records.Field<string>("ProductCategory")),
                                       ProSubCat = Convert.ToString(records.Field<string>("ProductSubCategory")),
                                       SonyMaterial = Convert.ToString(records.Field<string>("MaterialName")),
                                       Division = Convert.ToString(records.Field<string>("Division")),
                                       PlannedQuantity = Convert.ToString(records.Field<string>("PlannedQuantity")),
                                       DisplayQuantity = Convert.ToString(records.Field<string>("DisplayQuantity")),
                                       Gap = Convert.ToString(records.Field<string>("Gap")),
                                       SFAType = Convert.ToString(records.Field<string>("SFAType")),
                                       SubCatGroup = Convert.ToString(records.Field<string>("SubCategoryGrouping")),
                                       Segment = Convert.ToString(records.Field<string>("Segment")),
                                       Resolution = Convert.ToString(records.Field<string>("Resolution")),
                                       Internet = Convert.ToString(records.Field<string>("Internet")),
                                       S3D = Convert.ToString(records.Field<string>("3D")),
                                       TVType = Convert.ToString(records.Field<string>("TVType")),
                                       FY = Convert.ToString(records.Field<string>("FY")),
                                       //Model = Convert.ToString(records.Field<string>("Model")),
                                       //Brand = Convert.ToString(records.Field<string>("Brand")),
                                       //Quantity = Convert.ToString(records.Field<string>("AmboQuantity"))
                                       CompanyName = Convert.ToString(records.Field<string>("CompanyName")),
                                      // QSRStatus = Convert.ToString(records.Field<string>("QSRStatus")),

                                   });
                    output.recordsFiltered = Convert.ToInt32(outputFromSP.Tables[0].Rows[0]["FilteredCount"]);
                    output.recordsTotal = Convert.ToInt32(outputFromSP.Tables[0].Rows[0]["FilteredCount"]);
                }
                else
                {
                    output.data = new List<DisplayReportGrid>();
                    output.recordsFiltered = 0;
                }

            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("Display Report", ex.StackTrace, ex.Message);
            }
            return output;
        }
        #endregion

        #region Monthly Attendance Report
        /// <summary>
        /// To fetch Monthly Attendance Report from Database.
        /// Dhruv Sharma, ValueFirst, Gurgaon
        /// </summary>
        /// <param name="Input">Input Parameters</param>
        /// <returns>Monthly Attendance Report</returns>
        public DataTable GetMonthlyAttendanceReport(MonthlyAttendanceReportInput Input)
        {
            DataTable Data = new DataTable();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@BranchId", Input.BranchId, DbType.Int64));
                objDBParam.Add(new DbParameter("@SFAId", Input.SFAId, DbType.Int64));
                objDBParam.Add(new DbParameter("@Month", Input.Month, DbType.Int64));
                objDBParam.Add(new DbParameter("@StartDate", DateTime.ParseExact(Input.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture), DbType.DateTime));
                objDBParam.Add(new DbParameter("@EndDate", DateTime.ParseExact(Input.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture), DbType.DateTime));
                objDBParam.Add(new DbParameter("@SFAType", Input.SFATypeId, DbType.Int64));
                objDBParam.Add(new DbParameter("@UserId", Input.UserId, DbType.Int64));
                Data = _dataHelper.ExecuteDataTable("[dbo].[GetMonthlyAttendanceReport]", objDBParam, CommandType.StoredProcedure);

                //foreach (DataRow data in Data.Rows) 
                //{
                //    if(data[""])
                //}
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("MonthlyAttendance Report", ex.StackTrace, ex.Message);
            }
            return Data;
        }
        public DataTable GetMonthlyAttendanceReportDownload(MonthlyAttendanceReportInput Input)
        {
            DataTable Data = new DataTable();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@BranchId", Input.BranchId, DbType.Int64));
                objDBParam.Add(new DbParameter("@SFAId", Input.SFAId, DbType.Int64));
                objDBParam.Add(new DbParameter("@Month", Input.Month, DbType.Int64));
                objDBParam.Add(new DbParameter("@StartDate", DateTime.ParseExact(Input.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture), DbType.DateTime));
                objDBParam.Add(new DbParameter("@EndDate", DateTime.ParseExact(Input.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture), DbType.DateTime));
                objDBParam.Add(new DbParameter("@SFAType", Input.SFATypeId, DbType.Int64));
                objDBParam.Add(new DbParameter("@UserId", Input.UserId, DbType.Int64));
                Data = _dataHelper.ExecuteDataTable("[dbo].[GetMonthlyAttendanceReport_Download]", objDBParam, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("MonthlyAttendance Report", ex.StackTrace, ex.Message);
            }
            return Data;
        }
        #endregion

        #region Approved Attendance for branch 
        /// <summary>
        /// To fetch Approved Attendance for branch.
        /// Vinod Sorari, ValueFirst, Gurgaon
        /// </summary>
        /// <param name="Input">Input Parameters</param>
        /// <returns>Is Approved Attendance for branch</returns>
        public int IsApprovedBranch(MonthlyAttendanceReportInput Input, out string Message)
        {
            DataTable Data = new DataTable();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@BranchId", Input.BranchId, DbType.Int64));
                // objDBParam.Add(new DbParameter("@SFAId", Input.SFAId, DbType.Int64));
                objDBParam.Add(new DbParameter("@StartDate", DateTime.ParseExact(Input.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture), DbType.DateTime));
                objDBParam.Add(new DbParameter("@EndDate", DateTime.ParseExact(Input.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture), DbType.DateTime));
                //objDBParam.Add(new DbParameter("@SFAType", Input.SFATypeId, DbType.Int64));
                //objDBParam.Add(new DbParameter("@UserId", Input.UserId, DbType.Int64));
                Data = _dataHelper.ExecuteDataTable("[dbo].[IsApprovedBranch]", objDBParam, CommandType.StoredProcedure);


                Message = Convert.ToString(Data.Rows[0]["Message"]);
                //  Status = Convert.ToInt32(Data.Rows[0]["Status"]);
                if (Convert.ToBoolean(Data.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("IsApprovedBranch", ex.StackTrace, ex.Message);
                Message = "Error occured, please try again.";
            }

            return 0;
        }
        #endregion



        #region Training report
        public DataTable GetTrainingReport(TrainingReport objGridVariables)
        {
            DataTable outputFromSP = new DataTable();

            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();

                //objDBParam.Add(new DbParameter("@PageStart", objGridVariables.draw));
                //objDBParam.Add(new DbParameter("@PageLength", objGridVariables.length));
                objDBParam.Add(new DbParameter("@FromDate", objGridVariables.FromDate));
                objDBParam.Add(new DbParameter("@ToDate", objGridVariables.ToDate));
                objDBParam.Add(new DbParameter("@TrainingId", objGridVariables.TrainingId));
                objDBParam.Add(new DbParameter("@BranchId", objGridVariables.BranchId));


                outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetTrainingReport]", objDBParam, CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("Training report", ex.StackTrace, ex.Message);
            }
            return outputFromSP;
        }
        #endregion


        #region Attendance Approval Report
        public DataTable GetMonthlyAttendanceApprovalReport(UpdateMonthlyReport Input)
        {
            DataTable Data = new DataTable();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@UserId", Input.UserId, DbType.Int64));
                objDBParam.Add(new DbParameter("@SFACode", Input.SFACode, DbType.String));
                objDBParam.Add(new DbParameter("@Branch", Input.BranchId, DbType.String));
                objDBParam.Add(new DbParameter("@StartDate", DateTime.ParseExact(Input.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture), DbType.DateTime));
                objDBParam.Add(new DbParameter("@EndDate", DateTime.ParseExact(Input.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture), DbType.DateTime));
                Data = _dataHelper.ExecuteDataTable("[dbo].[GETAttendanceApprovalRecord]", objDBParam, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("MonthlyAttendance Approval Report", ex.StackTrace, ex.Message);
            }
            return Data;
        }

        public List<ApprovalGrid> GETApprovalDateStatusWise(UpdateMonthlyReport Input)
        {

            List<ApprovalGrid> ListofDates = null;
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@UserId", Input.UserId, DbType.Int64));
                objDBParam.Add(new DbParameter("@SFAId", Input.SFAId, DbType.Int64));
                objDBParam.Add(new DbParameter("@StartDate", Input.StartDate, DbType.String));
                objDBParam.Add(new DbParameter("@EndDate", Input.EndDate, DbType.String));

                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GETApprovalDateStatusWise]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofDates = (from region in outputFromSP.AsEnumerable()
                                   select new ApprovalGrid
                                   {
                                       AttendanceDates = region.Field<string>("AttendanceDate"),
                                       NewAttendanceType = region.Field<string>("NewAttendanceType"),
                                       OldAttendanceType = region.Field<string>("OldAttendanceType"),
                                       Approval = region.Field<string>("Approval"),
                                       SFAIds = region.Field<Int64>("SFAId"),
                         

                    DealerCode = region.Field<Int64>("DealerCode")
                }).ToList();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GETApprovalDateStatusWise", ex.StackTrace, ex.Message);
            }
            return ListofDates;

        }
        #endregion

        #region Combo Sales Report
        public GridOutput<IEnumerable<ComboSalesReportGrid>> GetComboSaleseReport(ComboSalesReport objGridVariables)
        {
            GridOutput<IEnumerable<ComboSalesReportGrid>> output = new GridOutput<IEnumerable<ComboSalesReportGrid>>();
            output.draw = objGridVariables.draw;
            DataTable dtDealers = new DataTable();
            DataTable dtCompanies = new DataTable();
            DataRow row;
            try
            {
                dtDealers.Columns.Add("DealerId");
                if (objGridVariables.DealerIds != null)
                {
                    foreach (Int64 dealer in objGridVariables.DealerIds)
                    {
                        row = dtDealers.NewRow();
                        row["DealerId"] = dealer;
                        dtDealers.Rows.Add(row);
                    }
                }

                //dtCompanies.Columns.Add("CompanyId");
                //if (objGridVariables.CompanyIds != null)
                //{
                //    foreach (Int64 company in objGridVariables.CompanyIds)
                //    {
                //        row = dtCompanies.NewRow();
                //        row["CompanyId"] = company;
                //        dtCompanies.Rows.Add(row);
                //    }
                //}

                SqlParameter[] objDBParam = new SqlParameter[12];

                objDBParam[0] = new SqlParameter("@PageStart", objGridVariables.start);
                objDBParam[1] = new SqlParameter("@PageLength", objGridVariables.length);
                objDBParam[2] = new SqlParameter("@FromDate", objGridVariables.FromDate);
                objDBParam[3] = new SqlParameter("@ToDate", objGridVariables.ToDate);
                objDBParam[4] = new SqlParameter("@BranchId", objGridVariables.BranchId);
                objDBParam[5] = new SqlParameter("@StateId", objGridVariables.StateId);
                objDBParam[6] = new SqlParameter("@SFAId", objGridVariables.SFAId);
                objDBParam[7] = new SqlParameter("@ProductCategoryId", objGridVariables.ProductCategoryId);
                objDBParam[8] = new SqlParameter("@ProductSubCategoryId", objGridVariables.ProSubCatId);
                objDBParam[9] = new SqlParameter("@ClassificationId", objGridVariables.ClassificationId);
                objDBParam[10] = new SqlParameter("@DivisionId", objGridVariables.DivisionId);
                objDBParam[11] = new SqlParameter("@DealerIds", dtDealers);
                //objDBParam[12] = new SqlParameter("@CompanyIds", dtCompanies);

                var outputFromSP = _dataHelper.ExecuteProcedureForDataset("[dbo].[GetComboSalesReport]", objDBParam);

                if (outputFromSP.Tables[0].Rows.Count > 0)
                {
                    output.data = (from records in outputFromSP.Tables[0].AsEnumerable()
                                   select new ComboSalesReportGrid
                                   {
                                       Branch = Convert.ToString(records.Field<string>("Branch")),
                                       Date = Convert.ToString(records.Field<string>("Date")),
                                       Month = Convert.ToString(records.Field<string>("Month")),
                                       DealerName = Convert.ToString(records.Field<string>("DealerName")),
                                       City = Convert.ToString(records.Field<string>("City")),
                                       Location = Convert.ToString(records.Field<string>("Location")),
                                       PayerName = Convert.ToString(records.Field<string>("PayerName")),
                                       Channel = Convert.ToString(records.Field<string>("Channel")),
                                       State = Convert.ToString(records.Field<string>("DealerState")),
                                       SAPCode = Convert.ToString(records.Field<string>("MasterCode")),
                                       DealerCode = Convert.ToString(records.Field<string>("DealerCode")),
                                       DealerClassification = Convert.ToString(records.Field<string>("DealerClassification")),
                                       SFACode = Convert.ToString(records.Field<string>("SFACode")),
                                       SFAName = Convert.ToString(records.Field<string>("SFAName")),
                                       SFALevel = Convert.ToString(records.Field<string>("SFALevel")),
                                       IncentiveCate = Convert.ToString(records.Field<string>("IncentiveCategory")),
                                       CompanyName = Convert.ToString(records.Field<string>("CompanyName")),
                                       Division = Convert.ToString(records.Field<string>("Division")),
                                       ProCat1 = Convert.ToString(records.Field<string>("ProductCategory1")),
                                       ComboMaterial1 = Convert.ToString(records.Field<string>("Material1")),
                                       SaleDate1 = Convert.ToString(records.Field<string>("SellDate1")),
                                       InvoiceNo1 = Convert.ToString(records.Field<string>("InvoiceNo1")),
                                       InvoiceImage1 = Convert.ToString(records.Field<string>("InvoiceImage1")),
                                       ProCat2 = Convert.ToString(records.Field<string>("ProductCategory2")),
                                       ComboMaterial2 = Convert.ToString(records.Field<string>("ComboMaterial2")),
                                       SaleDate2 = Convert.ToString(records.Field<string>("SellDate2")),
                                       InvoiceNo2 = Convert.ToString(records.Field<string>("InvoiceNo2")),
                                       InvoiceImage2 = Convert.ToString(records.Field<string>("InvoiceImage2")),

                                   });
                    output.recordsFiltered = Convert.ToInt32(outputFromSP.Tables[0].Rows[0]["FilteredCount"]);
                    output.recordsTotal = Convert.ToInt32(outputFromSP.Tables[0].Rows[0]["FilteredCount"]);
                }
                else
                {
                    output.data = new List<ComboSalesReportGrid>();
                    output.recordsFiltered = 0;
                }

            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("ComboSales Report", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public GridOutput<IEnumerable<FestivalTargetVsAchievementReportData>> GetFestivalTargetVsAchievementReport(FestivalTargetVsAchievementReportFilters objGridVariables)
        {
            GridOutput<IEnumerable<FestivalTargetVsAchievementReportData>> output = new GridOutput<IEnumerable<FestivalTargetVsAchievementReportData>>();
            output.draw = objGridVariables.draw;
            DataTable dtProdCat = new DataTable();
            //DateTime inputMonth = new DateTime(
            //    Convert.ToInt32(objGridVariables.Month.Split('/')[1]), 
            //    Convert.ToInt32(objGridVariables.Month.Split('/')[0]), 
            //    1, 0, 0, 0);
            DataRow row;
            try
            {
                dtProdCat.Columns.Add("UserId");
                dtProdCat.Columns.Add("ProdCatId");
                if (objGridVariables.ProductCategoryIds != null)
                {
                    foreach (Int64 prodcat in objGridVariables.ProductCategoryIds)
                    {
                        row = dtProdCat.NewRow();
                        row["ProdCatId"] = prodcat;
                        row["UserId"] = objGridVariables.UserId;
                        dtProdCat.Rows.Add(row);
                    }
                }
                SqlParameter[] objDBParam = new SqlParameter[7];

                objDBParam[0] = new SqlParameter("@PageStart", objGridVariables.start);
                objDBParam[1] = new SqlParameter("@PageLength", objGridVariables.length);
                objDBParam[2] = new SqlParameter("@SchemeId", objGridVariables.SchemeId);
                objDBParam[3] = new SqlParameter("@BranchId", objGridVariables.BranchId);
                objDBParam[4] = new SqlParameter("@SFAId", objGridVariables.SFAId);
                objDBParam[5] = new SqlParameter("@DivisionId", objGridVariables.DivisionId);
                objDBParam[6] = new SqlParameter("@ProductCategories", dtProdCat);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetFestivalTargetVsAchievementReport]", objDBParam);

                if (outputFromSP.Rows.Count > 0)
                {
                    output.data = (from records in outputFromSP.AsEnumerable()
                                   select new FestivalTargetVsAchievementReportData
                                   {
                                       SchemeName = records.Field<string>("SchemeName"),
                                       BranchName = records.Field<string>("Branch"),
                                       SFAName = records.Field<string>("SFAName"),
                                       SFACode = records.Field<string>("SFACode"),
                                       SFALevel = records.Field<string>("SFALevel"),
                                       DealerName = records.Field<string>("DealerName"),
                                       DealerCode = records.Field<string>("DealerCode"),
                                       SAPCode = records.Field<string>("SAPCode"),
                                       Channel = records.Field<string>("Channel"),
                                       City = records.Field<string>("City"),
                                       Location = records.Field<string>("Location"),
                                       ProductCategory = records.Field<string>("ProductCategory"),
                                       TargetCategory = records.Field<string>("TargetCategory"),
                                       IncentiveCategory = records.Field<string>("IncentiveCategory"),
                                       Division = records.Field<string>("Division"),
                                       TargetQty = records.Field<Int64>("QtyTarget"),
                                       AchQty = records.Field<Int64>("QtyAch"),
                                       TargetValue = records.Field<Int64>("ValueTarget"),
                                       AchValue = records.Field<Int64>("ValueAch")
                                   });
                    output.recordsFiltered = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                    output.recordsTotal = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                }
                else
                {
                    output.data = new List<FestivalTargetVsAchievementReportData>();
                    output.recordsFiltered = 0;
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("TargetVsAchievement Report", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public DataTable GetMonthlyAttendanceApprovalReportDownload(UpdateMonthlyReport Input)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
