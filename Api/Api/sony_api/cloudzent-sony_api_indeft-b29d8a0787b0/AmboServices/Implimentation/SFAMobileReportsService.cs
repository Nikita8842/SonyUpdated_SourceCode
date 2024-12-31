using AmboDataServices.Interface;
using AmboLibrary.ReportsManagement;
using AmboLibrary.SFAMobileApp;
using AmboServices.Interface;
using AmboUtilities;
using AmboUtilities.Helper;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Data;

namespace AmboServices.Implimentation
{
    public class SFAMobileReportsService: ISFAMobileReportsService
    {
        private readonly ISFAMobileReportsDataService _SFAMobileReportsData;
        public SFAMobileReportsService(ISFAMobileReportsDataService ISFAMobileReportsDataService)
        {
            _SFAMobileReportsData = ISFAMobileReportsDataService;
        }

        public Envelope<MTDModelWiseDSRReportList> GetMTDSalesReport(ReportInput InputParam)
        {
            var output = _SFAMobileReportsData.GetMTDSalesReport(InputParam);
            return (output == null || output.MTDModelWiseDSRReportModel.Count() == 0) ?
                new Envelope<MTDModelWiseDSRReportList> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<MTDModelWiseDSRReportList> { Data = output, MessageCode = (int)Acceptable.Found, Message = "MDT Model Wise DSR Report Data fetched successfully." };
        }

        public Envelope<TodaySalesReportList> GetTodaySalesReport(ReportInput Input)
        {
            var output = _SFAMobileReportsData.GetTodaySalesReport(Input);
            return (output == null || output.TodaySalesReportModel.Count() == 0) ?
                new Envelope<TodaySalesReportList> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<TodaySalesReportList> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Today Sales Report Data fetched successfully." };
        }

        public Envelope<LastDaySalesReportList> GetLastDaySalesReport(ReportInput Input)
        {
            var output = _SFAMobileReportsData.GetLastDaySalesReport(Input);
            return (output == null || output.LastDaySalesReportModel.Count() == 0) ?
                new Envelope<LastDaySalesReportList> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<LastDaySalesReportList> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Last Day Sales Report Data fetched successfully." };
        }

        public Envelope<SFATargetvsAchievementList> GetSFATargetvsAchievementReport(SFATvsAInput Input)
        {
            var GetList = _SFAMobileReportsData.GetSFATargetvsAchievementReport(Input);
            return (GetList == null || GetList.SFATargetvsAchievement.Count() == 0) ?
                new Envelope<SFATargetvsAchievementList> {Data=GetList, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<SFATargetvsAchievementList> { Data = GetList, MessageCode = (int)Acceptable.Found, Message = "SFA Target vs Achievement Report Data fetched successfully." };
        }

        public Envelope<List<MTDModelWiseComboReportModel>> GetMTDModelWiseComboReport(ReportInput Input)
        {
            var output = _SFAMobileReportsData.GetMTDModelWiseComboReport(Input);
            return (output == null || output.Count() == 0) ?
                new Envelope<List<MTDModelWiseComboReportModel>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<List<MTDModelWiseComboReportModel>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "MDT Model Wise Combo Report Data fetched successfully." };
        }

        public Envelope<List<SFAFestivalTargetvsAchievementModel>> GetSFAFestivalTargetVsAchievement(SFATvsAInput Input)
        {
            var output = _SFAMobileReportsData.GetSFAFestivalTargetVsAchievement(Input);
            return (output == null || output.Count() == 0) ?
                new Envelope<List<SFAFestivalTargetvsAchievementModel>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<List<SFAFestivalTargetvsAchievementModel>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Festival Target vs Acheivement Data fetched successfully." };
        }

        public Envelope<List<MTDComboSalesReportModel>> GetMTDComboSalesReport(ReportInput Input)
        {
            var output = _SFAMobileReportsData.GetMTDComboSalesReport(Input);
            return (output == null || output.Count() == 0) ?
                new Envelope<List<MTDComboSalesReportModel>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<List<MTDComboSalesReportModel>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "MDT Combo Sales Report Data fetched successfully." };
        }

        public Envelope<List<SFAAttendanceReportForRDI>> GetAttendanceCountSFAForRDI(SFAAttendanceReportForRDIInput Input)
        {
            var output = _SFAMobileReportsData.GetAttendanceCountSFAForRDI(Input);
            return (output == null || output.Count() == 0) ?
                new Envelope<List<SFAAttendanceReportForRDI>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<List<SFAAttendanceReportForRDI>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Data fetched successfully." };
        }

        public Envelope<DataTable> GetComboSalesReportMobile(ComboSalesReportInput Input)
        {
            var output = _SFAMobileReportsData.GetComboSalesReportMobile(Input);
            return (output == null) ?
                new Envelope<DataTable> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<DataTable> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Data fetched successfully." };
        }
    }
}
