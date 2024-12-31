using System;
using AmboLibrary.SFAManagement;
using AmboUtilities;
using AmboLibrary.SFAMobileApp;
using AmboLibrary.SFAMOBILEAPP;
using AmboLibrary.MasterMaintenance;
using System.Collections.Generic;

namespace AmboServices.Interface
{
    public interface ISFAMobileManagementService
    {
        Envelope<GetSFAProfile> GetSFAProfile(Int64 UserId);
        Envelope<bool> SubmitSFAAttendance(SFAAttendanceMaster InputParam);
        Envelope<LeaveTypeList> GetLeaveTypeList();
        Envelope<SFAMTDAttendanceList> GetSFAMTDAttendanceReport(SFAMTDAttendanceInput Input);
        Envelope<bool> InsertSFADemoStockRanging(SFADemoStockRangingModel Data);
        Envelope<DemoRangingStockDataList> GetDemoRangingStockData(DemoRagingStockInput Input);
        Envelope<bool> InsertCompetitorSaleThru(SFACompetitionTrackingModel Data);
        Envelope<bool> UpdateCompetitorSaleThru(SFACompetitionTrackingModel Data);
        Envelope<SFACompetitionTrackingList> GetSFACompetitionTrackingData(CTInput Input);
        Envelope<MonthlyModelWiseCompSalesThruList> GetMonthlyModelWiseCompSalesThru(CTInput Input);
        Envelope<CounterShareTrackingReport> GetCounterShareTrackingReport(CTInput Input);
        Envelope<bool> InsertCompetitorDisplaySKU(CompetitorDisplaySKU Input);
        Envelope<CompetitorDisplaySKUReportList> GetCompetitorDisplaySKUReport(CTInput Input);
        Envelope<bool> InsertCompetitionHeadCount(CompetitionHeadCountModel Data);
        Envelope<CompetitorHeadCountReport> GetCompetitorHeadCountReport(CTInput Input);
        Envelope<bool> SubmitMessageReply(SFAMessageBroadcasterModel Data);
        Envelope<MessageBroadcasterList> GetMessageBroadcasterList(MessageListProcInput Input);
        Envelope<SFAUserValidation> ValidateSFALogin(SFAValidationInput Input);
        Envelope<SFASyncModel> SFAMobileSync(SyncInput Input);
        Envelope<bool> InsertWeeklyStoreStock(WeeklyStoreStockModel Data);
        Envelope<WeeklyStoreStockDataList> GetWeeklyStoreStockDataList(WeeklyStoreStockDataInput Input);
        Envelope<bool> SubmitSaleThru(SFASaleThruSubmission Data);
		Envelope<bool> UpdateMessageReadStatus(BroadcastMessageStatus objReadData);
        Envelope<bool> UpdateSaleThru(SFASaleThruSubmission objReadData);
        Envelope<bool> UpdateSFAUser(SFAProfileUpdateInput Data);
		Envelope<List<BrandList>> GetActiveBrands();
        Envelope<List<CompetitionCountType>> GetCompetitionCountTypes(CompetitorCountTypeInput Input);
        Envelope<List<TrainingList>> GetTrainingsBySFAId(TrainingSearch objSearchData);
        Envelope<FeedbackForm> GetFeedbackForm(SFAFeedbackDataInput Input);
        Envelope<bool> ManageSFATrainingFeedback(SFAFeedbackData objFeedbackData);
        Envelope<bool> UpdateUserPassword(UserUpdatePasswordModel Data);
        Envelope<bool> SubmitComboSalesData(ComboSales InputData);
        Envelope<List<ModelDropdownOutput>> GetMTDModelforCombo(ModelDropdownInput InputData);
        Envelope<bool> CancelComboSale(CancelComboSalesInput InputData);
    }
}
