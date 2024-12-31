using System;
using System.Web.Http;
using System.Web.Http.Cors;
using AmboServices.Interface;
using AmboUtilities;
using AmboLibrary.SFAManagement;
using AmboLibrary.SFAMobileApp;
using AmboUtilities.Helper;
using AmboLibrary.SFAMOBILEAPP;
using AmboLibrary.Abstract;
using AmboLibrary.MasterMaintenance;

namespace SonyAmbo.Controllers
{
    /// <summary>
    /// Controller to Validate All the details 
    /// for SFA Mobile Only
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [ExceptionHandling]
    [Compression]
    public class SFAMobileManagementController : ApiController
    {
        /// <summary>
        /// Constructor
        /// </summary>
        private readonly ISFAMobileManagementService _ISFAMobileManagementService;
        public SFAMobileManagementController(ISFAMobileManagementService ISFAMobileManagementService)
        {
            _ISFAMobileManagementService = ISFAMobileManagementService;
        }

        /// <summary>
        /// Method to Get SAF Profile details 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetProfile(Envelope<Master>List)
        {
            var getlist = _ISFAMobileManagementService.GetSFAProfile(List.Data.UserId);
            return Ok(getlist);
        }

        /// <summary>
        /// Method to Submit SFA Attendance
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult SubmitSFAAttendance(Envelope<SFAAttendanceMaster> InputParam)
        {
            var getlist = _ISFAMobileManagementService.SubmitSFAAttendance(InputParam.Data);
            return Ok(getlist);
        }

        /// <summary>
        /// To fetch the list of Leave Types for Dropdown.
        /// Dhruv Sharma, ValueFirst
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetLeaveTypeList()
        {
            var getlist = _ISFAMobileManagementService.GetLeaveTypeList();
            return Ok(getlist);
        }

        /// <summary>
        /// To fetch SFA's MTD Attendance report for mobile.
        /// Dhruv Sharma, ValueFirst
        /// </summary>
        /// <param name="InputParam">SFA Id</param>
        /// <returns>SFA's MTD Attendance Report Data</returns>
        [HttpPost]
        public IHttpActionResult GetSFAMTDAttendanceReport(Envelope<SFAMTDAttendanceInput> InputParam)
        {
            var getlist = _ISFAMobileManagementService.GetSFAMTDAttendanceReport(InputParam.Data);
            return Ok(getlist);
        }

        /// <summary>
        /// To insert SFA's demo stock ranging information subbmited from Mobile App.
        /// Dhruv Sharma, ValueFirst
        /// May 2, 2018
        /// </summary>
        /// <param name="Input">List of information to be submitted.</param>
        /// <returns>Status of the operation.</returns>
        [HttpPost]
        public IHttpActionResult InsertSFADemoStockRanging(Envelope<SFADemoStockRangingModel> Input)
        {
            var Report = _ISFAMobileManagementService.InsertSFADemoStockRanging(Input.Data);
            return Ok(Report);
        }

        /// <summary>
        /// To get Demo Ranging Stock Data.
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 2, 2018
        /// </summary>
        /// <param name="Input">SFAId, ProductCategoryId, ProductSubCategoryId</param>
        /// <returns>Demo Ranging Stock Data</returns>
        [HttpPost]
        public IHttpActionResult GetDemoRangingStockData(Envelope<DemoRagingStockInput> Input)
        {
            var Report = _ISFAMobileManagementService.GetDemoRangingStockData(Input.Data);
            return Ok(Report);
        }

        /// <summary>
        /// To insert Competitor Sales Thru
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 7, 2018
        /// </summary>
        /// <param name="Data">Fields to push</param>
        /// <param name="ErrorMessage">Output error message</param>
        /// <returns>Status of the operation</returns>
        [HttpPost]
        public IHttpActionResult InsertCompetitorSaleThru(Envelope<SFACompetitionTrackingModel> Data)
        {
            var Report = _ISFAMobileManagementService.InsertCompetitorSaleThru(Data.Data);
            return Ok(Report);
        }

        /// <summary>
        /// To update Competitor Sales Thru
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 7, 2018
        /// </summary>
        /// <param name="Data">Fields to push</param>
        /// <param name="ErrorMessage">Output error message</param>
        /// <returns>Status of the operation</returns>
        [HttpPost]
        public IHttpActionResult UpdateCompetitorSaleThru(Envelope<SFACompetitionTrackingModel> Data)
        {
            var Report = _ISFAMobileManagementService.UpdateCompetitorSaleThru(Data.Data);
            return Ok(Report);
        }

        /// <summary>
        /// To get list of SFA Competition Tracking Data.
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 7, 2018
        /// </summary>
        /// <param name="Input">User Id</param>
        /// <returns>List of SFA Competition Tracking Data.</returns>
        [HttpPost]
        public IHttpActionResult GetSFACompetitionTrackingData(Envelope<CTInput> Input)
        {
            var Report = _ISFAMobileManagementService.GetSFACompetitionTrackingData(Input.Data);
            return Ok(Report);
        }

        /// <summary>
        /// To get Monthly Model Wise Competitor Sales Thru Report.
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 8, 2018
        /// </summary>
        /// <param name="Input">User Id</param>
        /// <returns>Monthly Model Wise Competitor Sales Thru Report Data</returns>
        [HttpPost]
        public IHttpActionResult GetMonthlyModelWiseCompSalesThru(Envelope<CTInput> Input)
        {
            var Report = _ISFAMobileManagementService.GetMonthlyModelWiseCompSalesThru(Input.Data);
            return Ok(Report);
        }

        /// <summary>
        /// To get Counter Share Tracking Report.
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 8, 2018
        /// </summary>
        /// <param name="Input">User Id</param>
        /// <returns>Counter Share Tracking Report.</returns>
        [HttpPost]
        public IHttpActionResult GetCounterShareTrackingReport(Envelope<CTInput> Input)
        {
            var Report = _ISFAMobileManagementService.GetCounterShareTrackingReport(Input.Data);
            return Ok(Report);
        }

        /// <summary>
        /// To insert Competitor Display SKU data.
        /// </summary>
        /// <param name="Data">Input parametres to insert.</param>
        /// <returns>Status of the operation.</returns>
        [HttpPost]
        public IHttpActionResult InsertCompetitorDisplaySKU(Envelope<CompetitorDisplaySKU> Data)
        {
            var Report = _ISFAMobileManagementService.InsertCompetitorDisplaySKU(Data.Data);
            return Ok(Report);
        }

        /// <summary>
        /// To get Competitor Display SKU Report.
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 8, 2018
        /// </summary>
        /// <param name="Input">login Id</param>
        /// <returns>Competitor Display SKU Report</returns>
        [HttpPost]
        public IHttpActionResult GetCompetitorDisplaySKUReport(Envelope<CTInput> Input)
        {
            var Report = _ISFAMobileManagementService.GetCompetitorDisplaySKUReport(Input.Data);
            return Ok(Report);
        }

        /// <summary>
        /// To insert Competition Head Count
        /// Dhruv Sharma
        /// </summary>
        /// <param name="Input">Input to insert.</param>
        /// <returns>Status of the operation.</returns>
        [HttpPost]
        public IHttpActionResult InsertCompetitionHeadCount(Envelope<CompetitionHeadCountModel> Input)
        {
            var Report = _ISFAMobileManagementService.InsertCompetitionHeadCount(Input.Data);
            return Ok(Report);
        }

        /// <summary>
        /// To get Competition Head Count Report data.
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 9, 2018
        /// </summary>
        /// <param name="Input">SFA Id</param>
        /// <returns>Competition Head Count Report data.</returns>
        [HttpPost]
        public IHttpActionResult GetCompetitorHeadCountReport(Envelope<CTInput> Input)
        {
            var Report = _ISFAMobileManagementService.GetCompetitorHeadCountReport(Input.Data);
            return Ok(Report);
        }

        /// <summary>
        /// To submit Message Broadcated Reply.
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 10, 2018
        /// </summary>
        /// <param name="Input">Data to put</param>
        /// <returns>Status of operation</returns>
        [HttpPost]
        public IHttpActionResult SubmitMessageReply(Envelope<SFAMessageBroadcasterModel> Input)
        {
            var Report = _ISFAMobileManagementService.SubmitMessageReply(Input.Data);
            return Ok(Report);
        }

        /// <summary>
        /// To get broadcasted messages of Role selected.
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 10, 2018
        /// </summary>
        /// <param name="Input">SFA Id</param>
        /// <returns>Broadcasted messages of SFA.</returns>
        [HttpPost]
        public IHttpActionResult GetMessageBroadcasterList(Envelope<MessageListProcInput> Input)
        {
            var Report = _ISFAMobileManagementService.GetMessageBroadcasterList(Input.Data);
            return Ok(Report);
        }

        /// <summary>
        /// To validate SFA mobile login.
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 11, 2018
        /// </summary>
        /// <param name="Input">Login Details</param>
        /// <returns>User Details</returns>
        [HttpPost]
        public IHttpActionResult ValidateSFALogin(Envelope<SFAValidationInput> Input)
        {
            var Report = _ISFAMobileManagementService.ValidateSFALogin(Input.Data);
            return Ok(Report);
        }

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
        [HttpPost]
        public IHttpActionResult SFAMobileSync(Envelope<SyncInput> Input)
        {
            var Report = _ISFAMobileManagementService.SFAMobileSync(Input.Data);
            return Ok(Report);
        }

        /// <summary>
        /// To insert Weekly Store Stock report.
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 14, 2018
        /// </summary>
        /// <param name="Input">Data to input.</param>
        /// <returns>Status of the operation.</returns>
        [HttpPost]
        public IHttpActionResult InsertWeeklyStoreStock(Envelope<WeeklyStoreStockModel> Input)
        {
            var Report = _ISFAMobileManagementService.InsertWeeklyStoreStock(Input.Data);
            return Ok(Report);
        }

        /// <summary>
        /// To get Weekly Store Stock Data List.
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 14, 2018
        /// </summary>
        /// <param name="Input">SFA Id</param>
        /// <returns>List of Weekly Store Stock Data</returns>
        [HttpPost]
        public IHttpActionResult GetWeeklyStoreStockDataList(Envelope<WeeklyStoreStockDataInput> Input)
        {
            var Report = _ISFAMobileManagementService.GetWeeklyStoreStockDataList(Input.Data);
            return Ok(Report);
        }

        /// <summary>
        /// To submit daily sales.
        /// </summary>
        /// <param name="Input">To submit</param>
        /// <returns>Status of the operation</returns>
        [HttpPost]
        public IHttpActionResult SubmitSaleThru(Envelope<SFASaleThruSubmission> Input)
        {
            var Report = _ISFAMobileManagementService.SubmitSaleThru(Input.Data);
            return Ok(Report);
        }
		
		/// <summary>
        /// To update read status of a broadcasted message.
        /// Nikhil Thakral, ValueFirst, Gurugram
        /// May 17, 2018
        /// </summary>
        /// <param name="Input"></param>
        /// <returns>true/false.</returns>
        [HttpPost]
        public IHttpActionResult UpdateMessageReadStatus(Envelope<BroadcastMessageStatus> Input)
        {
            var output = _ISFAMobileManagementService.UpdateMessageReadStatus(Input.Data);
            return Ok(output);
        }

        /// <summary>
        /// To update sales thru
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 24, 2018
        /// </summary>
        /// <param name="Input">Data to be updated</param>
        /// <returns>Status of operation</returns>
        [HttpPost]
        public IHttpActionResult UpdateSaleThru(Envelope<SFASaleThruSubmission> Input)
        {
            var output = _ISFAMobileManagementService.UpdateSaleThru(Input.Data);
            return Ok(output);
        }

        /// <summary>
        /// To update SFA user profile from App
        /// Dhruv Sharma, Vfirst, Gurgaon
        /// June 1, 2018
        /// </summary>
        /// <param name="Input">Input</param>
        /// <returns>Status of the operation</returns>
        [HttpPost]
        public IHttpActionResult UpdateSFAUser(Envelope<SFAProfileUpdateInput> Input)
        {
            var output = _ISFAMobileManagementService.UpdateSFAUser(Input.Data);
            return Ok(output);
        }
		
		/// <summary>
        /// To get active brands list
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 24, 2018
        /// </summary>
        /// <param name="Input">Data to be updated</param>
        /// <returns>Status of operation</returns>
        [HttpPost]
        public IHttpActionResult GetActiveBrands(Envelope<MasterAbstract> objData)
        {
            var output = _ISFAMobileManagementService.GetActiveBrands();
            return Ok(output);
        }

        /// <summary>
        /// To get active competitor count type list
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 24, 2018
        /// </summary>
        /// <param name="Input">Data to be updated</param>
        /// <returns>Status of operation</returns>
        [HttpPost]
        public IHttpActionResult GetCompetitionCountTypes(Envelope<CompetitorCountTypeInput> objData)
        {
            var output = _ISFAMobileManagementService.GetCompetitionCountTypes(objData.Data);
            return Ok(output);
        }

        /// <summary>
        /// To get active training list for SFA
        /// Nikhil Thakral, ValueFirst, Gurugram
        /// July 24, 2018
        /// </summary>
        /// <param name="objData">SFA ID</param>
        /// <returns>Status of operation</returns>
        [HttpPost]
        public IHttpActionResult GetTrainingsBySFAId(Envelope<TrainingSearch> objData)
        {
            var output = _ISFAMobileManagementService.GetTrainingsBySFAId(objData.Data);
            return Ok(output);
        }

        /// <summary>
        /// To get active feedback form
        /// Nikhil Thakral, ValueFirst, Gurugram
        /// July 24, 2018
        /// </summary>
        /// <param name="objData"></param>
        /// <returns>Feedback Form</returns>
        [HttpPost]
        public IHttpActionResult GetFeedbackForm(Envelope<SFAFeedbackDataInput> objData)
        {
            var output = _ISFAMobileManagementService.GetFeedbackForm(objData.Data);
            return Ok(output);
        }

        /// <summary>
        /// To submit feedback form
        /// Nikhil Thakral, ValueFirst, Gurugram
        /// July 24, 2018
        /// </summary>
        /// <param name="objData"></param>
        /// <returns>Feedback Form</returns>
        [HttpPost]
        public IHttpActionResult ManageSFATrainingFeedback(Envelope<SFAFeedbackData> objData)
        {
            var output = _ISFAMobileManagementService.ManageSFATrainingFeedback(objData.Data);
            return Ok(output);
        }

        /// <summary>
        /// To update user password from App
        /// Dhruv Sharma, Vfirst, Gurgaon
        /// June 24, 2018
        /// </summary>
        /// <param name="Data">Input</param>
        /// <param name="ErrorMessage">Message</param>
        /// <returns>Status of the operation</returns>
        [HttpPost]
        public IHttpActionResult UpdateUserPassword(Envelope<UserUpdatePasswordModel> Data)
        {
            var output = _ISFAMobileManagementService.UpdateUserPassword(Data.Data);
            return Ok(output);
        }

        /// <summary>
        /// To submit combo sales from app
        /// Bela Nalavade, Vfirst, Gurgaon
        /// May 6, 2019
        /// </summary>
        /// <param name="Data">Input</param>
        /// <param name="ErrorMessage">Message</param>
        /// <returns>Status of the operation</returns>
        [HttpPost]
        public IHttpActionResult SubmitComboSalesData(Envelope<ComboSales> Data)
        {
            var output = _ISFAMobileManagementService.SubmitComboSalesData(Data.Data);
            return Ok(output);
        }

        /// <summary>
        /// To get materials for combo sales
        /// Bela Nalavade, ValueFirst, Gurugram
        /// May 07, 2019
        /// </summary>
        /// <param name="objData">SFA ID</param>
        /// <returns>list of data</returns>
        [HttpPost]
        public IHttpActionResult GetMTDModelforCombo(Envelope<ModelDropdownInput> objData)
        {
            var output = _ISFAMobileManagementService.GetMTDModelforCombo(objData.Data);
            return Ok(output);
        }

        /// <summary>
        /// To cancel combo sales from app
        /// Bela Nalavade, Vfirst, Gurgaon
        /// June 13, 2019
        /// </summary>
        /// <param name="Data">Input</param>
        /// <param name="ErrorMessage">Message</param>
        /// <returns>Status of the operation</returns>
        [HttpPost]
        public IHttpActionResult CancelComboSale(Envelope<CancelComboSalesInput> Data)
        {
            var output = _ISFAMobileManagementService.CancelComboSale(Data.Data);
            return Ok(output);
        }
    }
}
