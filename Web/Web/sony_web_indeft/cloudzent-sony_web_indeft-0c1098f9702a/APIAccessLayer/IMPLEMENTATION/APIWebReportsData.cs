using APIAccessLayer.INTERFACE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMBOModels.MasterMaintenance;
using AMBOModels.Reports;
using APIAccessLayer.Helper;
using Newtonsoft.Json;
using System.Data;
using System.Web.Script.Serialization;

namespace APIAccessLayer.IMPLEMENTATION
{
    public class APIWebReportsData : IAPIWebReportsData
    {
        public Envelope<GridOutput<IEnumerable<CompetitionDisplayReportData>>> GetCompetitionDisplayReport(CompetitionDisplayReportFilters objGridData)
        {
            Envelope<CompetitionDisplayReportFilters> postData = new Envelope<CompetitionDisplayReportFilters>();
            Envelope<GridOutput<IEnumerable<CompetitionDisplayReportData>>> output;
            APIClient<Envelope<CompetitionDisplayReportFilters>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<CompetitionDisplayReportFilters>>();
                string apiUrl = "api/WebReports/GetCompetitionDisplayReport";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<CompetitionDisplayReportData>>>>(apiOutput.Result);
                return output;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Envelope<GridOutput<IEnumerable<CompetitionSFACountReportData>>> GetCompetitionSFACountReport(CompetitionSFACountReportFilters objGridData)
        {
            Envelope<CompetitionSFACountReportFilters> postData = new Envelope<CompetitionSFACountReportFilters>();
            Envelope<GridOutput<IEnumerable<CompetitionSFACountReportData>>> output;
            APIClient<Envelope<CompetitionSFACountReportFilters>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<CompetitionSFACountReportFilters>>();
                string apiUrl = "api/WebReports/GetCompetitionSFACountReport";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<CompetitionSFACountReportData>>>>(apiOutput.Result);
                return output;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Envelope<DownloadCompetitionSFACountReportData> DownloadCompetitionSFACountReportData(CompetitionSFACountReportFilters objGridData)
        {
            Envelope<CompetitionSFACountReportFilters> postData = new Envelope<CompetitionSFACountReportFilters>();
            Envelope<DownloadCompetitionSFACountReportData> output;
            APIClient<Envelope<CompetitionSFACountReportFilters>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<CompetitionSFACountReportFilters>>();
                string apiUrl = "api/WebReports/DownloadCompetitionSFACountReportData";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<DownloadCompetitionSFACountReportData>>(apiOutput.Result);
                return output;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Envelope<GridOutput<List<MessageReportData>>> GetMessageReport(MessageReportFilters objGridData)
        {
            Envelope<MessageReportFilters> postData = new Envelope<MessageReportFilters>();
            Envelope<GridOutput<List<MessageReportData>>> output;
            APIClient<Envelope<MessageReportFilters>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<MessageReportFilters>>();
                string apiUrl = "api/WebReports/GetMessageReport";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<List<MessageReportData>>>>(apiOutput.Result);
                return output;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Envelope<GridOutput<IEnumerable<TargetVsAchievementReportData>>> GetTargetVsAchievementReport(TargetVsAchievementReportFilters objGridData)
        {
            Envelope<TargetVsAchievementReportFilters> postData = new Envelope<TargetVsAchievementReportFilters>();
            Envelope<GridOutput<IEnumerable<TargetVsAchievementReportData>>> output;
            APIClient<Envelope<TargetVsAchievementReportFilters>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<TargetVsAchievementReportFilters>>();
                string apiUrl = "api/WebReports/GetTargetVsAchievementReport";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<TargetVsAchievementReportData>>>>(apiOutput.Result);
                return output;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Envelope<GridOutput<IEnumerable<DailySalesWithAttributeReportGrid>>> GetDailySalesWithAttributeReport(DailySalesWithAttributesReport objGridData)
        {
            Envelope<DailySalesWithAttributesReport> postData = new Envelope<DailySalesWithAttributesReport>();
            Envelope<GridOutput<IEnumerable <DailySalesWithAttributeReportGrid>>> output;
            APIClient<Envelope<DailySalesWithAttributesReport>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<DailySalesWithAttributesReport>>();
                string apiUrl = "api/WebReports/GetDailySalesWithAttributeReport";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<DailySalesWithAttributeReportGrid>>>>(apiOutput.Result);
                return output;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Envelope<GridOutput<IEnumerable<DailySalesReportIMEIGrid>>> GetDailySalesIMEIReport(DailySalesReportIMEI objGridData)
        {
            Envelope<DailySalesReportIMEI> postData = new Envelope<DailySalesReportIMEI>();
            Envelope< GridOutput < IEnumerable <DailySalesReportIMEIGrid>>> output = new Envelope<GridOutput<IEnumerable<DailySalesReportIMEIGrid>>>();
            APIClient<Envelope<DailySalesReportIMEI>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<DailySalesReportIMEI>>();
                string apiUrl = "api/WebReports/GetDailySalesIMEIReport";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<DailySalesReportIMEIGrid>>>>(apiOutput.Result);
                return output;
            }
            catch (Exception ex)
            {
                output.Data = null;
                output.MessageCode = (int)NotAcceptable.NotFound;
                output.Message = ex.Message;
                return output;
            }
        }

        /// <summary>
        /// To fetch Daily Timing Report from Database.
        /// Dhruv Sharma, ValueFirst, Gurgaon
        /// </summary>
        /// <param name="Input">Input Parameters</param>
        /// <returns>Daily Timing Report</returns>
        public Envelope<GridOutput<IEnumerable<DailyTimingReport>>> GetDailyTimingReport(DailyTimingReportInput objGridData)
        {
            Envelope<DailyTimingReportInput> postData = new Envelope<DailyTimingReportInput>();
            Envelope<GridOutput<IEnumerable<DailyTimingReport>>> output = new Envelope<GridOutput<IEnumerable<DailyTimingReport>>>();
            APIClient<Envelope<DailyTimingReportInput>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<DailyTimingReportInput>>();
                string apiUrl = "api/WebReports/GetDailyTimingReport";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<DailyTimingReport>>>>(apiOutput.Result);
                //foreach (var item in output.Data.data)
                //{
                //    if (item.Attendance == "L")
                //    {
                //        item.Attendance = "Forgot To Punch";
                //    }
                //}
                return output;
            }
            catch (Exception ex)
            {
                output.Data = null;
                output.MessageCode = (int)NotAcceptable.NotFound;
                output.Message = ex.Message;
                return output;
            }
        }
		
		public Envelope<GridOutput<IEnumerable<StockReportGrid>>> GetStockReport(StockReport objGridVariables)
        {
            Envelope<StockReport> postData = new Envelope<StockReport>();
            Envelope<GridOutput<IEnumerable<StockReportGrid>>> output = new Envelope<GridOutput<IEnumerable<StockReportGrid>>>();
            APIClient<Envelope<StockReport>> client;
            postData.Data = objGridVariables;
            try
            {
                client = new APIClient<Envelope<StockReport>>();
                string apiUrl = "api/WebReports/GetStockReport";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<StockReportGrid>>>>(apiOutput.Result);
                return output;
            }
            catch (Exception ex)
            {
                output.Data = null;
                output.MessageCode = (int)NotAcceptable.NotFound;
                output.Message = ex.Message;
                return output;
            }
        }

        public Envelope<GridOutput<List<DisplayReportGrid>>> GetDisplayReport(DisplayReport objGridVariables)
        {
            Envelope<DisplayReport> postData = new Envelope<DisplayReport>();
            Envelope<GridOutput<List<DisplayReportGrid>>> output = new Envelope<GridOutput<List<DisplayReportGrid>>>();
            APIClient<Envelope<DisplayReport>> client;
            postData.Data = objGridVariables;
            try
            {
                client = new APIClient<Envelope<DisplayReport>>();
                string apiUrl = "api/WebReports/GetDisplayReport";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<List<DisplayReportGrid>>>>(apiOutput.Result);
                return output;
            }
            catch (Exception ex)
            {
                output.Data = null;
                output.MessageCode = (int)NotAcceptable.NotFound;
                output.Message = ex.Message;
                return output;
            }
        }

        /// <summary>
        /// To fetch Monthly Attendance Report from Database.
        /// Dhruv Sharma, ValueFirst, Gurgaon
        /// </summary>
        /// <param name="Input">Input Parameters</param>
        /// <returns>Monthly Attendance Report</returns>
        public Envelope<DataTable> GetMonthlyAttendanceReport(MonthlyAttendanceReportInput Input)
        {
            Envelope<MonthlyAttendanceReportInput> postData = new Envelope<MonthlyAttendanceReportInput>();
            Envelope<DataTable> output = new Envelope<DataTable>();
            APIClient<Envelope<MonthlyAttendanceReportInput>> client;
            postData.Data = Input;
            try
            {
                client = new APIClient<Envelope<MonthlyAttendanceReportInput>>();
                string apiUrl = "api/WebReports/GetMonthlyAttendanceReport";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<DataTable>>(apiOutput.Result);

                //foreach (var item in output.Data.DataTable)
                //{
                //    if (item.Attendance == "L")
                //    {
                //        item.Attendance = "Forgot To Punch";
                //    }
                //}


                return output;
            }
            catch (Exception ex)
            {
                output.Data = null;
                output.MessageCode = (int)NotAcceptable.NotFound;
                output.Message = ex.Message;
                return output;
            }
        }
        public Envelope<DataTable> GetMonthlyAttendanceReportDownload(MonthlyAttendanceReportInput Input)
        {
            Envelope<MonthlyAttendanceReportInput> postData = new Envelope<MonthlyAttendanceReportInput>();
            Envelope<DataTable> output = new Envelope<DataTable>();
            APIClient<Envelope<MonthlyAttendanceReportInput>> client;
            postData.Data = Input;
            try
            {
                client = new APIClient<Envelope<MonthlyAttendanceReportInput>>();
                string apiUrl = "api/WebReports/GetMonthlyAttendanceReportDownload";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<DataTable>>(apiOutput.Result);
                return output;
            }
            catch (Exception ex)
            {
                output.Data = null;
                output.MessageCode = (int)NotAcceptable.NotFound;
                output.Message = ex.Message;
                return output;
            }
        }



        /// <summary>
        /// To fetch Monthly Attendance Report from Database.
        /// Vinod Sorari, ValueFirst, Gurgaon
        /// </summary>
        /// <param name="Input">Input Parameters</param>
        /// <returns>isApprovedBranch</returns>
        /// 
        public Envelope<bool> IsApprovedBranch(MonthlyAttendanceReportInput Input)
        {
            Envelope<MonthlyAttendanceReportInput> postData = new Envelope<MonthlyAttendanceReportInput>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<MonthlyAttendanceReportInput>> client;
            postData.Data = Input;
            try
            {
                client = new APIClient<Envelope<MonthlyAttendanceReportInput>>();
                string apiUrl = "api/WebReports/IsApprovedBranch";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
                return output;
            }
            catch (Exception ex)
            {
                output.Data = false;
                output.MessageCode = (int)NotAcceptable.NotFound;
                output.Message = ex.Message;
                return output;
            }
        }


        public Envelope<DataTable> GetTrainingReport(TrainingReport objGridVariables)
        {
            Envelope<TrainingReport> postData = new Envelope<TrainingReport>();
            Envelope<DataTable> output = new Envelope<DataTable>();
            APIClient<Envelope<TrainingReport>> client;
            postData.Data = objGridVariables;
            try
            {
                client = new APIClient<Envelope<TrainingReport>>();
                string apiUrl = "api/WebReports/GetTrainingReport";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<DataTable>>(apiOutput.Result);
                return output;
            }
            catch (Exception ex)
            {
                output.Data = null;
                output.MessageCode = (int)NotAcceptable.NotFound;
                output.Message = ex.Message;
                return output;
            }
        }

        public Envelope<bool> UpdateAttendance(UpdateMonthlyReport objFormData)
        {
            Envelope<UpdateMonthlyReport> postData = new Envelope<UpdateMonthlyReport>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<UpdateMonthlyReport>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<UpdateMonthlyReport>>();
                string apiUrl = "api/MasterMaintenance/UpdateMonthlyReport";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;

        }

        public Envelope<DataTable> GetMonthlyAttendanceApprovalReport(UpdateMonthlyReport Input)
        {
            Envelope<UpdateMonthlyReport> postData = new Envelope<UpdateMonthlyReport>();
            Envelope<DataTable> output = new Envelope<DataTable>();
            APIClient<Envelope<UpdateMonthlyReport>> client;
            postData.Data = Input;
            try
            {
                client = new APIClient<Envelope<UpdateMonthlyReport>>();
                string apiUrl = "api/WebReports/GetMonthlyAttendanceApprovalReport";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<DataTable>>(apiOutput.Result);
                return output;
            }
            catch (Exception ex)
            {
                output.Data = null;
                output.MessageCode = (int)NotAcceptable.NotFound;
                output.Message = ex.Message;
                return output;
            }
        }

        public Envelope<List<ApprovalGrid>> GETApprovalDateStatusWise(UpdateMonthlyReport Input)
        {

            Envelope<UpdateMonthlyReport> postData = new Envelope<UpdateMonthlyReport>();
            Envelope<List<ApprovalGrid>> output = new Envelope<List<ApprovalGrid>>();
            APIClient<Envelope<UpdateMonthlyReport>> client;
            postData.Data = Input;

            try
            {
                client = new APIClient<Envelope<UpdateMonthlyReport>>();
                string apiUrl = "api/WebReports/GETApprovalDateStatusWise";//checked
                var apiOutput = client.Post(apiUrl, postData);
                 output = JsonConvert.DeserializeObject<Envelope<List<ApprovalGrid>>>(apiOutput.Result);
                return output;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public Envelope<GridOutput<IEnumerable<ComboSalesReportGrid>>> GetComboSaleseReport(ComboSalesReport objGridVariables)
        {
            
            Envelope<ComboSalesReport> postData = new Envelope<ComboSalesReport>();
            Envelope<GridOutput<IEnumerable<ComboSalesReportGrid>>> output;
            APIClient<Envelope<ComboSalesReport>> client;
            postData.Data = objGridVariables;
            try
            {
                client = new APIClient<Envelope<ComboSalesReport>>();
                string apiUrl = "api/WebReports/GetComboSaleseReport";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<ComboSalesReportGrid>>>>(apiOutput.Result);
                return output;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Envelope<GridOutput<IEnumerable<FestivalTargetVsAchievementReportData>>> GetFestivalTargetVsAchievementReport(FestivalTargetVsAchievementReportFilters objGridVariables)
        {
            Envelope<FestivalTargetVsAchievementReportFilters> postData = new Envelope<FestivalTargetVsAchievementReportFilters>();
            Envelope<GridOutput<IEnumerable<FestivalTargetVsAchievementReportData>>> output;
            APIClient<Envelope<FestivalTargetVsAchievementReportFilters>> client;
            postData.Data = objGridVariables;
            try
            {
                client = new APIClient<Envelope<FestivalTargetVsAchievementReportFilters>>();
                string apiUrl = "api/WebReports/GetFestivalTargetVsAchievementReport";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<FestivalTargetVsAchievementReportData>>>>(apiOutput.Result);
                return output;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

       


    }
}
