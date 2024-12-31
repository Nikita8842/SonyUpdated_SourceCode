using System;
using AmboLibrary.SFAManagement;
using AmboLibrary.SFAMobileApp;
using AmboLibrary.SFAMOBILEAPP;
using System.Collections.Generic;
using AmboLibrary.MasterMaintenance;

namespace AmboDataServices.Interface
{
    public interface ISFAMobileManagementDataService
    {
        GetSFAProfile GetSFAProfile(Int64 UserId, out string Message);
        bool SubmitSFAAttendance(SFAAttendanceMaster InputParam, out string Message);
        LeaveTypeList GetLeaveTypeList();
        SFAMTDAttendanceList GetSFAMTDAttendanceReport(SFAMTDAttendanceInput Input);
        bool InsertSFADemoStockRanging(SFADemoStockRangingModel Data, out string ErrorMessage);
        DemoRangingStockDataList GetDemoRangingStockData(DemoRagingStockInput Input);
        bool InsertCompetitorSaleThru(SFACompetitionTrackingModel Data, out string ErrorMessage);
        bool UpdateCompetitorSaleThru(SFACompetitionTrackingModel Data, out string ErrorMessage);
        SFACompetitionTrackingList GetSFACompetitionTrackingData(CTInput Input);
        MonthlyModelWiseCompSalesThruList GetMonthlyModelWiseCompSalesThru(CTInput Input);
        CounterShareTrackingReport GetCounterShareTrackingReport(CTInput Input);
        bool InsertCompetitorDisplaySKU(CompetitorDisplaySKU Data, out string ErrorMessage);
        CompetitorDisplaySKUReportList GetCompetitorDisplaySKUReport(CTInput Input);
        bool InsertCompetitionHeadCount(CompetitionHeadCountModel Data, out string ErrorMessage);
        CompetitorHeadCountReport GetCompetitorHeadCountReport(CTInput Input);
        bool SubmitMessageReply(SFAMessageBroadcasterModel Data, out string ErrorMessage);
        MessageBroadcasterList GetMessageBroadcasterList(MessageListProcInput Input);
        SFAUserValidation ValidateSFALogin(SFAValidationInput Input, out string Message);
        SFASyncModel SFAMobileSync(SyncInput Input);
        bool InsertWeeklyStoreStock(WeeklyStoreStockModel Input, out string ErrorMessage);
        WeeklyStoreStockDataList GetWeeklyStoreStockDataList(WeeklyStoreStockDataInput Input);
        bool SubmitSaleThru(SFASaleThruSubmission Data, out string ErrorMessage);
		bool UpdateMessageReadStatus(BroadcastMessageStatus objReadData, out string Message);
        bool UpdateSaleThru(SFASaleThruSubmission Data, out string ErrorMessage);
        bool UpdateSFAUser(SFAProfileUpdateInput Data, out string ErrorMessage);
		List<BrandList> GetActiveBrands();
        List<CompetitionCountType> GetCompetitionCountTypes(CompetitorCountTypeInput Input);
        List<TrainingList> GetTrainingsBySFAId(TrainingSearch objSearchData, out string Message);
        FeedbackForm GetFeedbackForm(SFAFeedbackDataInput Input, out string Message);
        bool ManageSFATrainingFeedback(SFAFeedbackData objFeedbackData, out string Message);
        bool UpdateUserPassword(UserUpdatePasswordModel Data, out string ErrorMessage);
        bool SubmitComboSalesData(ComboSales InputData, out string Message);
        List<ModelDropdownOutput> GetMTDModelforCombo(ModelDropdownInput InputData, out string Message);
        bool CancelComboSale(CancelComboSalesInput InputData, out string Message);
    }
}
