using AmboDataServices.Interface;
using AmboServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboLibrary.MasterMaintenance;
using AmboLibrary.WebReports;
using AmboUtilities;
using AmboUtilities.Helper;
using System.Data;

namespace AmboServices.Implimentation
{
    public class WebReportsService : IWebReportsService
    {
        private readonly IWebReportsDataService _IWebReportsDataService;
        public WebReportsService(IWebReportsDataService IWebReportsDataService)
        {
            _IWebReportsDataService = IWebReportsDataService;
        }

        public Envelope<GridOutput<IEnumerable<CompetitionDisplayReportData>>> GetCompetitionDisplayReport(CompetitionDisplayReportFilters objGridVariables)
        {
            var output = _IWebReportsDataService.GetCompetitionDisplayReport(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<CompetitionDisplayReportData>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<CompetitionDisplayReportData>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Competition Display Report fetched successfully." };
        }

        public Envelope<GridOutput<IEnumerable<CompetitionSFACountReportData>>> GetCompetitionSFACountReport(CompetitionSFACountReportFilters objGridVariables)
        {
            var output = _IWebReportsDataService.GetCompetitionSFACountReport(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<CompetitionSFACountReportData>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<CompetitionSFACountReportData>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Competition SFA Count Report fetched successfully." };
        }

        public Envelope<DownloadCompetitionSFACountReportData> DownloadCompetitionSFACountReportData(CompetitionSFACountReportFilters objGridVariables)
        {
            var output = _IWebReportsDataService.DownloadCompetitionSFACountReportData(objGridVariables);
            return (output.ExcelData == null || output.ExcelData.Rows.Count == 0) ?
                new Envelope<DownloadCompetitionSFACountReportData> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<DownloadCompetitionSFACountReportData> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Competition SFA Count Report fetched successfully." };
        }

        public Envelope<GridOutput<IEnumerable<MessageReportData>>> GetMessageReport(MessageReportFilters objGridVariables)
        {
            var output = _IWebReportsDataService.GetMessageReport(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<MessageReportData>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<MessageReportData>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Message Report fetched successfully." };
        }

        public Envelope<GridOutput<IEnumerable<TargetVsAchievementReportData>>> GetTargetVsAchievementReport(TargetVsAchievementReportFilters objGridVariables)
        {
            var output = _IWebReportsDataService.GetTargetVsAchievementReport(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<TargetVsAchievementReportData>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<TargetVsAchievementReportData>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Target Vs Achievement Report fetched successfully." };
        }

        #region Daily Sales With Attribute
        public Envelope<GridOutput<IEnumerable<DailySalesWithAttributeReportGrid>>> GetDailySalesWithAttributeReport(DailySalesWithAttributesReport objGridVariables)
        {
            var output = _IWebReportsDataService.GetDailySalesWithAttributeReport(objGridVariables);
            return (output == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<DailySalesWithAttributeReportGrid>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<DailySalesWithAttributeReportGrid>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Daily Sales with Attribute Report fetched successfully." };
        }
        #endregion

        #region Daily Sales IMEI
        public Envelope<GridOutput<IEnumerable<DailySalesReportIMEIGrid>>> GetDailySalesIMEIReport(DailySalesReportIMEI objGridVariables)
        {
            var output = _IWebReportsDataService.GetDailySalesIMEIReport(objGridVariables);
            return (output == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<DailySalesReportIMEIGrid>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<DailySalesReportIMEIGrid>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Daily Sales IMEI Report fetched successfully." };
        }
        #endregion

        public Envelope<GridOutput<IEnumerable<DailyTimingReport>>> GetDailyTimingReport(DailyTimingReportInput Input)
        {
            var output = _IWebReportsDataService.GetDailyTimingReport(Input);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<DailyTimingReport>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<DailyTimingReport>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Daily Timing Report fetched successfully." };
        }
		
		#region Stock Report
        public Envelope<GridOutput<IEnumerable<StockReportGrid>>> GetStockReport(StockReport objGridVariables)
        {
            var output = _IWebReportsDataService.GetStockReport(objGridVariables);
            return (output == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<StockReportGrid>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<StockReportGrid>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Stock Report fetched successfully." };
        }
        #endregion

        #region Display Report
        public Envelope<GridOutput<IEnumerable<DisplayReportGrid>>> GetDisplayReport(DisplayReport objGridVariables)
        {
            var output = _IWebReportsDataService.GetDisplayReport(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<DisplayReportGrid>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<DisplayReportGrid>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Display Report data fetched successfully." };
        }
        #endregion

        public Envelope<DataTable> GetMonthlyAttendanceReport(MonthlyAttendanceReportInput Input)
        {
            var output = _IWebReportsDataService.GetMonthlyAttendanceReport(Input);
            return (output == null || output.Rows.Count == 0) ?
                new Envelope<DataTable> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<DataTable> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Monthly Attendance Report fetched successfully." };
        }
        public Envelope<DataTable> GetMonthlyAttendanceReportDownload(MonthlyAttendanceReportInput Input)
        {
            var output = _IWebReportsDataService.GetMonthlyAttendanceReportDownload(Input);
            return (output == null || output.Rows.Count == 0) ?
                new Envelope<DataTable> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<DataTable> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Monthly Attendance Report fetched successfully." };
        }

        public Envelope<bool> IsApprovedBranch(MonthlyAttendanceReportInput Input)
        {
             
            var Message = string.Empty;
            var output = _IWebReportsDataService.IsApprovedBranch(Input ,out Message);
            return (output == false) ?
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No attendance approved." } :
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Approved Attendance record found successfully." };
        }
        
        public Envelope<DataTable> GetTrainingReport(TrainingReport objGridVariables)
        {
            var output = _IWebReportsDataService.GetTrainingReport(objGridVariables);
            return (output == null || output.Rows.Count == 0) ?
                new Envelope<DataTable> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<DataTable> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Training Report fetched successfully." };
        }

        public Envelope<DataTable> GetMonthlyAttendanceApprovalReport(UpdateMonthlyReport Input)
        {
            var output = _IWebReportsDataService.GetMonthlyAttendanceApprovalReport(Input);
            return (output == null || output.Rows.Count == 0) ?
                new Envelope<DataTable> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<DataTable> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Monthly Attendance Report fetched successfully." };
        }

        public Envelope<List<ApprovalGrid>> GETApprovalDateStatusWise(UpdateMonthlyReport Input)
        {
            var output = _IWebReportsDataService.GETApprovalDateStatusWise(Input);
            return (output == null || output.Count() == 0) ?
                new Envelope<List<ApprovalGrid>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<List<ApprovalGrid>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Approval status data fetched successfully." };
        }

        #region ComboSales Report
        public Envelope<GridOutput<IEnumerable<ComboSalesReportGrid>>> GetComboSaleseReport(ComboSalesReport objGridVariables)
        {
            var output = _IWebReportsDataService.GetComboSaleseReport(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<ComboSalesReportGrid>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<ComboSalesReportGrid>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Combo sales data fetched successfully." };
        }

        public Envelope<GridOutput<IEnumerable<FestivalTargetVsAchievementReportData>>> GetFestivalTargetVsAchievementReport(FestivalTargetVsAchievementReportFilters objGridVariables)
        {
            var output = _IWebReportsDataService.GetFestivalTargetVsAchievementReport(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<FestivalTargetVsAchievementReportData>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<FestivalTargetVsAchievementReportData>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Festival target vs achievement data fetched successfully." };
        }
        #endregion

        
    }

}
