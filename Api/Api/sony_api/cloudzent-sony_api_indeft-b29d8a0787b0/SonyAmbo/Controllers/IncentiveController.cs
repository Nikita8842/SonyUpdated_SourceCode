using AmboLibrary.IncentiveManagement;
using AmboLibrary.WebReports;
using AmboServices.Interface;
using AmboUtilities;
using AmboUtilities.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SonyAmbo.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [ExceptionHandling]
    [Compression]
    public class IncentiveController : ApiController
    {
        private readonly IIncentiveService _IIncentiveService;

        public IncentiveController(IIncentiveService IIncentiveService)
        {
            _IIncentiveService = IIncentiveService;
        }

        #region Base Incentive
        /// <summary>
        /// Create a new base incentive definition. 
        /// </summary>
        /// <param name="objFormData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult ManageBaseIncentiveDefinition(Envelope<CreateBaseIncentiveForm> objFormData)
        {
            var output = _IIncentiveService.ManageBaseIncentiveDefinition(objFormData.Data);
            return Ok(output);
        }

        /// <summary>
        /// Delete an existing base incentive definition on basis of target category id. 
        /// </summary>
        /// <param name="objFormData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteBaseIncentiveDefinition(Envelope<DeleteBaseIncentiveForm> objFormData)
        {
            var output = _IIncentiveService.DeleteBaseIncentiveDefinition(objFormData.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get Base Incentive Definition By Target Category Id. 
        /// </summary>
        /// <param name="objFormData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetBaseIncentiveDefinitionByTargetCategoryId(Envelope<GetBaseIncentive> objFormData)
        {
            var output = _IIncentiveService.GetBaseIncentiveDefinitionByTargetCategoryId(objFormData.Data);
            return Ok(output);
        }
        #endregion Base Incentive

        /// <summary>
        /// Get Per Piece Incentive Definition excel file data. 
        /// </summary>
        /// <param name="objDownloadData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetPerPieceIncentiveExcelFile(Envelope<DownloadPerPieceIncentiveExcel> objDownloadData)
        {
            var output = _IIncentiveService.GetPerPieceIncentiveExcelFile(objDownloadData.Data);
            return Ok(output);
        }

        /// <summary>
        /// Create a new per piece incentive definition. 
        /// </summary>
        /// <param name="objFormData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult ManagePerPieceIncentiveDefinition(Envelope<CreatePerPieceIncentive> objFormData)
        {
            var output = _IIncentiveService.ManagePerPieceIncentiveDefinition(objFormData.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get per piece Incentive scheme By scheme Id. 
        /// </summary>
        /// <param name="objFormData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetPerPieceIncentiveDefinitionBySchemeId(Envelope<GetPerPieceIncentiveValues> objFormData)
        {
            var output = _IIncentiveService.GetPerPieceIncentiveDefinitionBySchemeId(objFormData.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get per piece Incentive scheme By Product Category Id. 
        /// </summary>
        /// <param name="objFormData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetPerPieceIncentiveSchemeByProductId(Envelope<PerPieceIncentiveSchemeByProductId> objFormData)
        {
            var output = _IIncentiveService.GetPerPieceIncentiveSchemeByProductId(objFormData.Data);
            return Ok(output);
        }

        /// <summary>
        /// Create a material-per piece mapping for new material. 
        /// </summary>
        /// <param name="mappingData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult ManagePerPieceMaterialMapping(Envelope<PerPieceIncentiveCreate> mappingData)
        {
            var output = _IIncentiveService.ManagePerPieceMaterialMapping(mappingData.Data);
            return Ok(output);
        }

        /// <summary>
        /// Delete a per piece incentive scheme. 
        /// </summary>
        /// <param name="objFormData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeletePerPieceIncentiveDefinition(Envelope<DeletePerPieceIncentive> objFormData)
        {
            var output = _IIncentiveService.DeletePerPieceIncentiveDefinition(objFormData.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get Festival Incentive Definition excel file data. 
        /// </summary>
        /// <param name="objDownloadData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetFestivalIncentiveDefinitionExcelFile(Envelope<DownloadFestivalIncentiveDefinitionExcel> objDownloadData)
        {
            var output = _IIncentiveService.GetFestivalIncentiveDefinitionExcelFile(objDownloadData.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get Festival Incentive slab Definition excel file data. 
        /// </summary>
        /// <param name="objDownloadData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetFestivalIncentiveSlabDefinitionExcelFile(Envelope<DownloadFestivalIncentiveDefinitionExcel> objDownloadData)
        {
            var output = _IIncentiveService.GetFestivalIncentiveSlabDefinitionExcelFile(objDownloadData.Data);
            return Ok(output);
        }

        /// <summary>
        /// Create/Update festival incentive definition. 
        /// </summary>
        /// <param name="objFormData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult ManageFestivalIncentiveDefinition(Envelope<CreateFestivalIncentive> objFormData)
        {
            var output = _IIncentiveService.ManageFestivalIncentiveDefinition(objFormData.Data);
            return Ok(output);
        }

        /// <summary>
        /// Create/Update festival incentive slab definition. 
        /// </summary>
        /// <param name="objFormData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult ManageFestivalIncentiveSlabDefinition(Envelope<CreateFestivalIncentiveSlab> objFormData)
        {
            var output = _IIncentiveService.ManageFestivalIncentiveSlabDefinition(objFormData.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get festival Incentive scheme By scheme Id. 
        /// </summary>
        /// <param name="objFormData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetFestivalIncentiveDefinitionBySchemeId(Envelope<GetFestivalIncentiveValues> objFormData)
        {
            var output = _IIncentiveService.GetFestivalIncentiveDefinitionBySchemeId(objFormData.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get festival Incentive scheme slab definition By scheme Id. 
        /// </summary>
        /// <param name="objFormData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetFestivalIncentiveSlabDefinitionBySchemeId(Envelope<GetFestivalIncentiveValues> objFormData)
        {
            var output = _IIncentiveService.GetFestivalIncentiveSlabDefinitionBySchemeId(objFormData.Data);
            return Ok(output);
        }

        /// <summary>
        /// Delete a festival incentive scheme. 
        /// </summary>
        /// <param name="objFormData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteFestivalIncentiveDefinition(Envelope<DeleteFestivalIncentive> objFormData)
        {
            var output = _IIncentiveService.DeleteFestivalIncentiveDefinition(objFormData.Data);
            return Ok(output);
        }

        #region IncentiveReport
        /// <summary>
        /// To fetch Base Per Piece Incentive Report.
        /// </summary>
        /// <param name="param">Branch	SFA Name	Division	Month 	Product Category</param>
        /// <returns>Base Per Piece Incentive Report.</returns>
        [HttpPost]
        public IHttpActionResult GetBasePerPieceIncentiveReport(Envelope<BasePerPieceIncentiveReportInputParam> param)
        {
            var output = _IIncentiveService.GetBasePerPieceIncentiveReport(param.Data);
            return Ok(output);
        }
        /// <summary>
        /// To fetch Base Per Piece Incentive detail Report.
        /// </summary>
        /// <param name="param">Branch	SFA Name	Division	Month 	Product Category</param>
        /// <returns>Base Per Piece Incentive Report.</returns>
        [HttpPost]
        public IHttpActionResult GetBasePerPieceIncentiveDetailReport(Envelope<BasePerPieceIncentiveReportInputParam> param)
        {
            var output = _IIncentiveService.GetBasePerPieceIncentiveDetailReport(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// To fetch Festival Incentive Report.
        /// </summary>
        /// <param name="param">Branch	SFA Name	Division	FestivalSchemeId 	Product Category</param>
        /// <returns>Festival Incentive Report.</returns>
        [HttpPost]
        public IHttpActionResult GetFestivalIncentiveReport(Envelope<FestivalIncentiveReportInputParam> param)
        {
            var output = _IIncentiveService.GetFestivalIncentiveReport(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// To fetch Festival Incentive detail Report.
        /// </summary>
        /// <param name="param">Branch	SFA Name	Division	FestivalScheme 	Product Category</param>
        /// <returns>Festival Incentive Detail Report.</returns>
        [HttpPost]
        public IHttpActionResult GetFestivalIncentiveDetailReport(Envelope<FestivalIncentiveReportInputParam> param)
        {
            var output = _IIncentiveService.GetFestivalIncentiveDetailReport(param.Data);
            return Ok(output);
        }


        /// <summary>
        /// To fetch sell thru tracker.
        /// </summary>
        /// <param name="param">FestivalNameId</param>
        /// <returns>Festival sell thru tracker.</returns>
        [HttpPost]
        public IHttpActionResult GetFestivalSellThruTracker(Envelope<FestivalSellThruTrackerInputParam> param)
        {
            var output = _IIncentiveService.GetFestivalSellThruTracker(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// To fetch Festival name details.
        /// </summary>
        /// <returns>Festival name details.</returns>
        [HttpPost]
        public IHttpActionResult GetFestivalNameDetails()
        {
            var output = _IIncentiveService.GetFestivalNameDetails();
            return Ok(output);
        }
        #endregion

        #region Incentive Report nikita 9/03/2024
        [HttpPost]
        public IHttpActionResult GetBasePerPieceIncentiveReportQSR(Envelope<BasePerPieceIncentiveReportInputParamQSR> param)
        {
            var output = _IIncentiveService.GetBasePerPieceIncentiveReportQSR(param.Data);
            return Ok(output);
        }
        #endregion

    }
}