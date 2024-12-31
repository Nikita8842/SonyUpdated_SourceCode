using System;
using AmboDataServices.Interface;
using AmboProvider.Interface;
using AmboLibrary.SFAManagement;
using AmboLibrary.SFAMobileApp;
using AmboLibrary.SFAMOBILEAPP;
using System.Collections.Generic;
using AmboLibrary.MasterMaintenance;

namespace AmboDataServices.Implimentation
{
    public class SFAMobileManagementDataService : ISFAMobileManagementDataService
    {
        private readonly ISFAMobileManagementProvider _ISFAMobileManagementProvider;
        public SFAMobileManagementDataService(ISFAMobileManagementProvider ISFAMobileManagementProvider)
        {
            _ISFAMobileManagementProvider = ISFAMobileManagementProvider;
        }

        public GetSFAProfile GetSFAProfile(Int64 UserId, out string Message)
        {
            return _ISFAMobileManagementProvider.GetSFAProfile(UserId, out Message);
        }

        public bool SubmitSFAAttendance(SFAAttendanceMaster InputParam, out string Message)
        {
            return Convert.ToBoolean(_ISFAMobileManagementProvider.SubmitSFAAttendance(InputParam, out Message));
        }

        public LeaveTypeList GetLeaveTypeList()
        {
            return _ISFAMobileManagementProvider.GetLeaveTypeList();
        }

        public SFAMTDAttendanceList GetSFAMTDAttendanceReport(SFAMTDAttendanceInput Input)
        {
            return _ISFAMobileManagementProvider.GetSFAMTDAttendanceReport(Input);
        }

        public bool InsertSFADemoStockRanging(SFADemoStockRangingModel Data, out string ErrorMessage)
        {
            return _ISFAMobileManagementProvider.InsertSFADemoStockRanging(Data,out ErrorMessage);
        }

        public DemoRangingStockDataList GetDemoRangingStockData(DemoRagingStockInput Input)
        {
            return _ISFAMobileManagementProvider.GetDemoRangingStockData(Input);
        }
        public bool InsertCompetitorSaleThru(SFACompetitionTrackingModel Data, out string ErrorMessage)
        {
            return _ISFAMobileManagementProvider.InsertCompetitorSaleThru(Data, out ErrorMessage);
        }
        public bool UpdateCompetitorSaleThru(SFACompetitionTrackingModel Data, out string ErrorMessage)
        {
            return _ISFAMobileManagementProvider.UpdateCompetitorSaleThru(Data, out ErrorMessage);
        }
        public SFACompetitionTrackingList GetSFACompetitionTrackingData(CTInput Input)
        {
            return _ISFAMobileManagementProvider.GetSFACompetitionTrackingData(Input);
        }
        public MonthlyModelWiseCompSalesThruList GetMonthlyModelWiseCompSalesThru(CTInput Input)
        {
            return _ISFAMobileManagementProvider.GetMonthlyModelWiseCompSalesThru(Input);
        }
        public CounterShareTrackingReport GetCounterShareTrackingReport(CTInput Input)
        {
            return _ISFAMobileManagementProvider.GetCounterShareTrackingReport(Input);
        }
        public bool InsertCompetitorDisplaySKU(CompetitorDisplaySKU Data, out string ErrorMessage)
        {
            return _ISFAMobileManagementProvider.InsertCompetitorDisplaySKU(Data,out ErrorMessage);
        }
        public CompetitorDisplaySKUReportList GetCompetitorDisplaySKUReport(CTInput Input)
        {
            return _ISFAMobileManagementProvider.GetCompetitorDisplaySKUReport(Input);
        }
        public CompetitorHeadCountReport GetCompetitorHeadCountReport(CTInput Input)
        {
            return _ISFAMobileManagementProvider.GetCompetitorHeadCountReport(Input);
        }
        public bool InsertCompetitionHeadCount(CompetitionHeadCountModel Data, out string ErrorMessage)
        {
            return _ISFAMobileManagementProvider.InsertCompetitionHeadCount(Data, out ErrorMessage);
        }
        public bool SubmitMessageReply(SFAMessageBroadcasterModel Data, out string ErrorMessage)
        {
            return _ISFAMobileManagementProvider.SubmitMessageReply(Data, out ErrorMessage);
        }
        public MessageBroadcasterList GetMessageBroadcasterList(MessageListProcInput Input)
        {
            return _ISFAMobileManagementProvider.GetMessageBroadcasterList(Input);
        }
        public SFAUserValidation ValidateSFALogin(SFAValidationInput Input, out string Message)
        {
            return _ISFAMobileManagementProvider.ValidateSFALogin(Input, out Message);
        }
        public SFASyncModel SFAMobileSync(SyncInput Input)
        {
            return _ISFAMobileManagementProvider.SFAMobileSync(Input);
        }
        public bool InsertWeeklyStoreStock(WeeklyStoreStockModel Input, out string ErrorMessage)
        {
            return _ISFAMobileManagementProvider.InsertWeeklyStoreStock(Input, out ErrorMessage);
        }
        public WeeklyStoreStockDataList GetWeeklyStoreStockDataList(WeeklyStoreStockDataInput Input)
        {
            return _ISFAMobileManagementProvider.GetWeeklyStoreStockDataList(Input);
        }
        public bool SubmitSaleThru(SFASaleThruSubmission Data, out string ErrorMessage)
        {
            return _ISFAMobileManagementProvider.SubmitSaleThru(Data, out ErrorMessage);
        }
		public bool UpdateMessageReadStatus(BroadcastMessageStatus objReadData, out string Message)
        {
            return _ISFAMobileManagementProvider.UpdateMessageReadStatus(objReadData, out Message);
        }
        public bool UpdateSaleThru(SFASaleThruSubmission Data, out string ErrorMessage)
        {
            return _ISFAMobileManagementProvider.UpdateSaleThru(Data, out ErrorMessage);
        }
        public bool UpdateSFAUser(SFAProfileUpdateInput Data, out string ErrorMessage)
        {
            return _ISFAMobileManagementProvider.UpdateSFAUser(Data, out ErrorMessage);
        }
		public List<BrandList> GetActiveBrands()
        {
            return _ISFAMobileManagementProvider.GetActiveBrands();
        }
        public List<CompetitionCountType> GetCompetitionCountTypes(CompetitorCountTypeInput Input)
        {
            return _ISFAMobileManagementProvider.GetCompetitionCountTypes(Input);
        }
        public List<TrainingList> GetTrainingsBySFAId(TrainingSearch objSearchData, out string Message)
        {
            return _ISFAMobileManagementProvider.GetTrainingsBySFAId(objSearchData, out Message);
        }
        public FeedbackForm GetFeedbackForm(SFAFeedbackDataInput Input, out string Message)
        {
            return _ISFAMobileManagementProvider.GetFeedbackForm(Input,out Message);
        }
        public bool ManageSFATrainingFeedback(SFAFeedbackData objFeedbackData, out string Message)
        {
            return _ISFAMobileManagementProvider.ManageSFATrainingFeedback(objFeedbackData, out Message);
        }
        public bool UpdateUserPassword(UserUpdatePasswordModel Data, out string ErrorMessage)
        {
            return _ISFAMobileManagementProvider.UpdateUserPassword(Data, out ErrorMessage);
        }

        public bool SubmitComboSalesData(ComboSales InputData, out string Message)
        {
            return Convert.ToBoolean(_ISFAMobileManagementProvider.SubmitComboSalesData(InputData,out Message));
        }

        public List<ModelDropdownOutput> GetMTDModelforCombo(ModelDropdownInput InputData, out string Message)
        {
            return _ISFAMobileManagementProvider.GetMTDModelforCombo(InputData, out Message);
        }

        public bool CancelComboSale(CancelComboSalesInput InputData, out string Message)
        {
            return Convert.ToBoolean(_ISFAMobileManagementProvider.CancelComboSale(InputData, out Message));
        }
    }
}
