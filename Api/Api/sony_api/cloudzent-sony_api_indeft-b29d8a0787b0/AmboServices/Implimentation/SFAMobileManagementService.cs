using System;
using AmboDataServices.Interface;
using AmboLibrary.SFAManagement;
using AmboServices.Interface;
using AmboUtilities;
using AmboUtilities.Helper;
using AmboLibrary.SFAMobileApp;
using AmboLibrary.SFAMOBILEAPP;
using AmboLibrary.MasterMaintenance;
using System.Collections.Generic;

namespace AmboServices.Implimentation
{
    public class SFAMobileManagementService : ISFAMobileManagementService
    {
        private readonly ISFAMobileManagementDataService _ISFAMobileManagementDataService;
        public SFAMobileManagementService(ISFAMobileManagementDataService ISFAMobileManagementDataService)
        {
            _ISFAMobileManagementDataService = ISFAMobileManagementDataService;
        }

        public Envelope<GetSFAProfile> GetSFAProfile(Int64 UserId)
        {
            var Message = string.Empty;
            var getList = _ISFAMobileManagementDataService.GetSFAProfile(UserId, out Message);

            return (getList != null) ?
                new Envelope<GetSFAProfile> {Data=getList, MessageCode=(int)Acceptable.Found, Message=Message } :
                new Envelope<GetSFAProfile> {Data=getList, MessageCode=(int)NotAcceptable.NotFound, Message=Message };
        }

        public Envelope<bool> SubmitSFAAttendance(SFAAttendanceMaster InputParam)
        {
            var Message = string.Empty;
            var output = _ISFAMobileManagementDataService.SubmitSFAAttendance(InputParam, out Message);

            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<LeaveTypeList> GetLeaveTypeList()
        {
            var List = _ISFAMobileManagementDataService.GetLeaveTypeList();

            return (List !=null && List.LeaveType.Count!=0) ?
                new Envelope<LeaveTypeList> { Data = List, MessageCode = (int)Acceptable.Created, Message = "Leave list fetched successfully." } :
                new Envelope<LeaveTypeList> { Data = List, MessageCode = (int)NotAcceptable.NotAcceptable, Message = "No Data Found." };
        }

        public Envelope<SFAMTDAttendanceList> GetSFAMTDAttendanceReport(SFAMTDAttendanceInput Input)
        {
            var List = _ISFAMobileManagementDataService.GetSFAMTDAttendanceReport(Input);

            return (List != null && List.SFAMTDAttendance.Count != 0) ?
                new Envelope<SFAMTDAttendanceList> { Data = List, MessageCode = (int)Acceptable.Created, Message = "SFA's MTD Attendance report fetched successfully." } :
                new Envelope<SFAMTDAttendanceList> { Data = List, MessageCode = (int)NotAcceptable.NotAcceptable, Message = "No Data Found." };
        }

        public Envelope<bool> InsertSFADemoStockRanging(SFADemoStockRangingModel Data)
        {
            var Message = string.Empty;
            var output = _ISFAMobileManagementDataService.InsertSFADemoStockRanging(Data, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<DemoRangingStockDataList> GetDemoRangingStockData(DemoRagingStockInput Input)
        {
            var List = _ISFAMobileManagementDataService.GetDemoRangingStockData(Input);

            return (List != null && List.DemoRangingStockData.Count != 0) ?
                new Envelope<DemoRangingStockDataList> { Data = List, MessageCode = (int)Acceptable.Created, Message = "Demo Ranging Stock Data fetched successfully." } :
                new Envelope<DemoRangingStockDataList> { Data = List, MessageCode = (int)NotAcceptable.NotAcceptable, Message = "No Data Found." };
        }

        public Envelope<bool> InsertCompetitorSaleThru(SFACompetitionTrackingModel Data)
        {
            var Message = string.Empty;
            var output = _ISFAMobileManagementDataService.InsertCompetitorSaleThru(Data, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<bool> UpdateCompetitorSaleThru(SFACompetitionTrackingModel Data)
        {
            var Message = string.Empty;
            var output = _ISFAMobileManagementDataService.UpdateCompetitorSaleThru(Data, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<SFACompetitionTrackingList> GetSFACompetitionTrackingData(CTInput Input)
        {
            var List = _ISFAMobileManagementDataService.GetSFACompetitionTrackingData(Input);

            return (List != null && List.SFACompetitionTracking.Count != 0) ?
                new Envelope<SFACompetitionTrackingList> { Data = List, MessageCode = (int)Acceptable.Created, Message = "SFA Competition Tracking Data fetched successfully." } :
                new Envelope<SFACompetitionTrackingList> { Data = List, MessageCode = (int)NotAcceptable.NotAcceptable, Message = "No data found." };
        }

        public Envelope<MonthlyModelWiseCompSalesThruList> GetMonthlyModelWiseCompSalesThru(CTInput Input)
        {
            var List = _ISFAMobileManagementDataService.GetMonthlyModelWiseCompSalesThru(Input);

            return (List != null && List.MonthlyModelWiseCompSalesThru.Count != 0) ?
                new Envelope<MonthlyModelWiseCompSalesThruList> { Data = List, MessageCode = (int)Acceptable.Created, Message = "Monthly Model Wise Competition Sales Data fetched successfully." } :
                new Envelope<MonthlyModelWiseCompSalesThruList> { Data = List, MessageCode = (int)NotAcceptable.NotAcceptable, Message = "No Data Found." };
        }

        public Envelope<CounterShareTrackingReport> GetCounterShareTrackingReport(CTInput Input)
        {
            var List = _ISFAMobileManagementDataService.GetCounterShareTrackingReport(Input);

            return (List != null && List.CounterShareTracking.Count != 0) ?
                new Envelope<CounterShareTrackingReport> { Data = List, MessageCode = (int)Acceptable.Created, Message = "Counter Share Tracking Data fetched successfully." } :
                new Envelope<CounterShareTrackingReport> { Data = List, MessageCode = (int)NotAcceptable.NotAcceptable, Message = "No Data Found." };
        }

        public Envelope<bool> InsertCompetitorDisplaySKU(CompetitorDisplaySKU Input)
        {
            string Message = "";
            var Output = _ISFAMobileManagementDataService.InsertCompetitorDisplaySKU(Input,out Message);

            return (Output == true) ?
                new Envelope<bool> { Data = Output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = Output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<CompetitorDisplaySKUReportList> GetCompetitorDisplaySKUReport(CTInput Input)
        {
            var List = _ISFAMobileManagementDataService.GetCompetitorDisplaySKUReport(Input);

            return (List != null && List.CompetitorDisplaySKUReport.Count != 0) ?
                new Envelope<CompetitorDisplaySKUReportList> { Data = List, MessageCode = (int)Acceptable.Created, Message = "Competition Display SKU fetched successfully." } :
                new Envelope<CompetitorDisplaySKUReportList> { Data = List, MessageCode = (int)NotAcceptable.NotAcceptable, Message = "No Data Found." };
        }

        public Envelope<bool> InsertCompetitionHeadCount(CompetitionHeadCountModel Data)
        {
            string Message = "";
            var Output = _ISFAMobileManagementDataService.InsertCompetitionHeadCount(Data, out Message);

            return (Output == true) ?
                new Envelope<bool> { Data = Output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = Output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<CompetitorHeadCountReport> GetCompetitorHeadCountReport(CTInput Input)
        {
            var List = _ISFAMobileManagementDataService.GetCompetitorHeadCountReport(Input);

            return (List != null && List.CompetitionHeadCount.Count != 0) ?
                new Envelope<CompetitorHeadCountReport> { Data = List, MessageCode = (int)Acceptable.Created, Message = "Competition Head Count report fetched successfully." } :
                new Envelope<CompetitorHeadCountReport> { Data = List, MessageCode = (int)NotAcceptable.NotAcceptable, Message = "No Data Found." };
        }

        public Envelope<bool> SubmitMessageReply(SFAMessageBroadcasterModel Data)
        {
            string Message = "";
            var Output = _ISFAMobileManagementDataService.SubmitMessageReply(Data, out Message);

            return (Output == true) ?
                new Envelope<bool> { Data = Output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = Output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<MessageBroadcasterList> GetMessageBroadcasterList(MessageListProcInput Input)
        {
            var List = _ISFAMobileManagementDataService.GetMessageBroadcasterList(Input);

            return (List != null && List.Messages.Count != 0) ?
                new Envelope<MessageBroadcasterList> { Data = List, MessageCode = (int)Acceptable.Created, Message = "Broadcasted Messages fetched successfully." } :
                new Envelope<MessageBroadcasterList> { Data = List, MessageCode = (int)NotAcceptable.NotAcceptable, Message = "No Data Found." };
        }

        public Envelope<SFAUserValidation> ValidateSFALogin(SFAValidationInput Input)
        {
            string Message = "";
            var List = _ISFAMobileManagementDataService.ValidateSFALogin(Input, out Message);

            return (List != null && List.SFAId != 0) ?
                new Envelope<SFAUserValidation> { Data = List, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<SFAUserValidation> { Data = List, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<SFASyncModel> SFAMobileSync(SyncInput Input)
        {
            var List = _ISFAMobileManagementDataService.SFAMobileSync(Input);

            return (List != null) ?
                new Envelope<SFASyncModel> { Data = List, MessageCode = (int)Acceptable.Created, Message = "Sync Data Fetched." } :
                new Envelope<SFASyncModel> { Data = List, MessageCode = (int)NotAcceptable.NotAcceptable, Message = "No Data Found." };
        }

        public Envelope<bool> InsertWeeklyStoreStock(WeeklyStoreStockModel Data)
        {
            string Message = "";
            var Output = _ISFAMobileManagementDataService.InsertWeeklyStoreStock(Data, out Message);

            return (Output == true) ?
                new Envelope<bool> { Data = Output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = Output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<WeeklyStoreStockDataList> GetWeeklyStoreStockDataList(WeeklyStoreStockDataInput Input)
        {
            var List = _ISFAMobileManagementDataService.GetWeeklyStoreStockDataList(Input);

            return (List != null && List.WeeklyStoreStockData.Count != 0) ?
                new Envelope<WeeklyStoreStockDataList> { Data = List, MessageCode = (int)Acceptable.Created, Message = "Weekly Store Stock data fetched successfully." } :
                new Envelope<WeeklyStoreStockDataList> { Data = List, MessageCode = (int)NotAcceptable.NotAcceptable, Message = "No data found." };
        }

        public Envelope<bool> SubmitSaleThru(SFASaleThruSubmission Data)
        {
            var Message = string.Empty;
            var output = _ISFAMobileManagementDataService.SubmitSaleThru(Data, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }
		
		public Envelope<bool> UpdateMessageReadStatus(BroadcastMessageStatus objReadData)
        {
            string Message = "";
            var Output = _ISFAMobileManagementDataService.UpdateMessageReadStatus(objReadData, out Message);
            return (Output == true) ?
                new Envelope<bool> { Data = Output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = Output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<bool> UpdateSaleThru(SFASaleThruSubmission objReadData)
        {
            string Message = "";
            var Output = _ISFAMobileManagementDataService.UpdateSaleThru(objReadData, out Message);
            return (Output == true) ?
                new Envelope<bool> { Data = Output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = Output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<bool> UpdateSFAUser(SFAProfileUpdateInput Data)
        {
            var Message = string.Empty;
            var Status = _ISFAMobileManagementDataService.UpdateSFAUser(Data, out Message);

            return (Status != false) ?
                new Envelope<bool> { Data = Status, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = Status, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }
		
		public Envelope<List<BrandList>> GetActiveBrands()
        {
            var List = _ISFAMobileManagementDataService.GetActiveBrands();

            return (List != null && List.Count != 0) ?
                new Envelope<List<BrandList>> { Data = List, MessageCode = (int)Acceptable.Found, Message = "Brands list fetched successfully." } :
                new Envelope<List<BrandList>> { Data = List, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." };
        }

        public Envelope<List<CompetitionCountType>> GetCompetitionCountTypes(CompetitorCountTypeInput Input)
        {
            var List = _ISFAMobileManagementDataService.GetCompetitionCountTypes(Input);

            return (List != null && List.Count != 0) ?
                new Envelope<List<CompetitionCountType>> { Data = List, MessageCode = (int)Acceptable.Found, Message = "Competition Count Type list fetched successfully." } :
                new Envelope<List<CompetitionCountType>> { Data = List, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." };
        }

        public Envelope<List<TrainingList>> GetTrainingsBySFAId(TrainingSearch objSearchData)
        {
            var Message = string.Empty;
            var output = _ISFAMobileManagementDataService.GetTrainingsBySFAId(objSearchData, out Message);
            return (output == null || output.Count == 0)?
                new Envelope<List<TrainingList>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = Message }:
                new Envelope<List<TrainingList>> { Data = output, MessageCode = (int)Acceptable.Found, Message = Message };
        }

        public Envelope<FeedbackForm> GetFeedbackForm(SFAFeedbackDataInput Input)
        {
            var Message = string.Empty;
            var output = _ISFAMobileManagementDataService.GetFeedbackForm(Input,out Message);
            return (output == null || output.Titles == null || output.Titles.Count == 0) ?
                new Envelope<FeedbackForm> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = Message } :
                new Envelope<FeedbackForm> { Data = output, MessageCode = (int)Acceptable.Found, Message = Message };
        }

        public Envelope<bool> ManageSFATrainingFeedback(SFAFeedbackData objFeedbackData)
        {
            var Message = string.Empty;
            var output = _ISFAMobileManagementDataService.ManageSFATrainingFeedback(objFeedbackData, out Message);
            return (!output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message };
        }

        public Envelope<bool> UpdateUserPassword(UserUpdatePasswordModel Data)
        {
            var Message = string.Empty;
            var output = _ISFAMobileManagementDataService.UpdateUserPassword(Data, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<bool> SubmitComboSalesData(ComboSales InputData)
        {
            var Message = string.Empty;
            var output = _ISFAMobileManagementDataService.SubmitComboSalesData(InputData, out Message);

            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<List<ModelDropdownOutput>> GetMTDModelforCombo(ModelDropdownInput InputData)
        {
            var Message = string.Empty;
            var List = _ISFAMobileManagementDataService.GetMTDModelforCombo(InputData, out Message);

            return (List != null && List.Count != 0) ?
                new Envelope<List<ModelDropdownOutput>> { Data = List, MessageCode = (int)Acceptable.Found, Message = "Materials for combo sales fetched successfully." } :
                new Envelope<List<ModelDropdownOutput>> { Data = List, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." };
        }

        public Envelope<bool> CancelComboSale(CancelComboSalesInput InputData)
        {
            var Message = string.Empty;
            var output = _ISFAMobileManagementDataService.CancelComboSale(InputData, out Message);

            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }
    }
}
