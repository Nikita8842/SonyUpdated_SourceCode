using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using AmboLibrary.MasterMaintenance;
using AmboLibrary.Modules;
using AmboServices.Interface;
using AmboUtilities;
using AmboLibrary.Mappings;
using AmboLibrary.UserManagement;
using AmboUtilities.Helper;

namespace SonyAmbo.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [ExceptionHandling]
    //[Compression]
    public class GridController : ApiController
    {
        private readonly IGridService _IGridService;

        public GridController(IGridService IGridService)
        {
            _IGridService = IGridService;
        }

        [HttpPost]
        public IHttpActionResult GetRegionMasterGrid(Envelope<GridVariables> param)
        {
            var output = _IGridService.GetRegionMasterGrid(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetStateMasterGrid(Envelope<GridVariables> param)
        {
            var output = _IGridService.GetStateMasterGrid(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetCityMasterGrid(Envelope<GridVariables> param)
        {
            var output = _IGridService.GetCityMasterGrid(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetLocationMasterGrid(Envelope<LocationFilter> param)
        {
            var output = _IGridService.GetLocationMasterGrid(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetBranchMasterGrid(Envelope<GridVariables> param)
        {
            var output = _IGridService.GetBranchMasterGrid(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetProductCategoryMasterGrid(Envelope<GridVariables> param)
        {
            var output = _IGridService.GetProductCategoryMasterGrid(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetProductSubCategoryMasterGrid(Envelope<GridVariables> param)
        {
            var output = _IGridService.GetProductSubCategoryMasterGrid(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetMaterialMasterGrid(Envelope<MaterialFilter> param)
        {
            var output = _IGridService.GetMaterialMasterGrid(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetDealerMasterGrid(Envelope<DealerFilter> param)
        {
            var output = _IGridService.GetDealerMasterGrid(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetDealerSFAMappingGrid(Envelope<DealerSFAFilter> param)
        {
            var output = _IGridService.GetDealerSFAMappingGrid(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetDealerClassificationMappingGrid(Envelope<GridVariables> param)
        {
            var output = _IGridService.GetDealerClassificationMappingGrid(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetBroadcastMessageMasterGrid(Envelope<GridVariables> param)
        {
            var output = _IGridService.GetBroadcastMessageMasterGrid(param.Data);
            //code to empty file name if its not uploaded on desired Directory
            for (int i = 0; i < output.Data.data.Count(); i++)
            {
                if (!System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/Uploads/Broadcast/" + output.Data.data[i].FileName)))
                {
                    output.Data.data[i].FileName = "";
                }
            }
            return Ok(output);

        }

        [HttpPost]
        public IHttpActionResult BroadcastMessageMasterGridBYID(Envelope<BMessageInputModel> param)
        {
            var output = _IGridService.BroadcastMessageMasterGridBYID(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetAssetMasterGrid(Envelope<GridVariables> param)
        {
            var output = _IGridService.GetAssetMasterGrid(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetEmployeeMasterGrid(Envelope<EmployeeGridSearchFilters> param)
        {
            var output = _IGridService.GetEmployeeMasterGrid(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetEmployeeStructureMappingGrid(Envelope<EmployeeStructureMapGridFilters> param)
        {
            var output = _IGridService.GetEmployeeStructureMappingGrid(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetSalesPICOutletMappingGrid(Envelope<GridVariables> param)
        {
            var output = _IGridService.GetSalesPICOutletMappingGrid(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetUserBranchChannelPCMappingGrid(Envelope<UserBranchChannelPCMappingFilter> param)
        {
            var output = _IGridService.GetUserBranchChannelPCMappingGrid(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetSFAMasterGrid(Envelope<SFAGridSearchFilters> param)
        {
            var output = _IGridService.GetSFAMasterGrid(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetProductCategorySFAMappingGrid(Envelope<ProdCatSFAMapGridSearchFilters> param)
        {
            var output = _IGridService.GetProductCategorySFAMappingGrid(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetChannelMasterGrid(Envelope<ChannelFilter> param)
        {
            var output = _IGridService.GetChannelMasterGrid(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetProductTargetCategoryMasterGrid(Envelope<ProductTargetCategoryGridFilters> param)
        {
            var output = _IGridService.GetProductTargetCategoryMasterGrid(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// To fetch all SFA Level Master Data.
        /// </summary>
        /// <returns>SFA Level Master Data.</returns>
        [HttpPost]
        public IHttpActionResult GetSFALevelMasterData(Envelope<SFALevelFilter> InputParam)
        {
            var output = _IGridService.GetSFALevelMasterData(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// This is for Shift master.
        /// </summary>
        /// <param name="InputParam"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetShiftMasterData(Envelope<ShiftFilter> InputParam)
        {
            var output = _IGridService.GetShiftMasterData(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// To fetch SFA Level master data by Id.
        /// </summary>
        /// <param name="Input">SFA Level Id</param>
        /// <returns>SFA Level master data by Id.</returns>
        [HttpPost]
        public IHttpActionResult GetSFALevelMasterDataById(Envelope<SFALevelInput> Input)
        {
            var output = _IGridService.GetSFALevelMasterDataById(Input.Data);
            return Ok(output);
        }

        /// <summary>
        /// To fetch all SFA Sub Level Master Data.
        /// </summary>
        /// <returns>SFA Sub Level Master Data.</returns>
        [HttpPost]
        public IHttpActionResult GetSFASubLevelMasterData(Envelope<SFASubLevelFilter> InputParam)
        {
            var output = _IGridService.GetSFASubLevelMasterData(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// To fetch SFA Sub Level master data by Id.
        /// </summary>
        /// <param name="Input">SFA Sub Level Id</param>
        /// <returns>SFA Sub Level master data by Id.</returns>
        [HttpPost]
        public IHttpActionResult GetSFASubLevelMasterDataById(Envelope<SFASubLevelInput> Input)
        {
            var output = _IGridService.GetSFASubLevelMasterDataById(Input.Data);
            return Ok(output);
        }

        /// <summary>
        /// To fetch sfa details for HR by Login Id from User, Employee and Employee for HR table
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetSFAMasterforHRGrid(Envelope<SFAMasterforHRFilter> param)
        {
            var output = _IGridService.GetSFAMasterforHRGrid(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// To get Competitor Master data.
        /// </summary>
        /// <returns>Competitor Master data.</returns>
        [HttpPost]
        public IHttpActionResult GetCompetitorMasterGrid(Envelope<CompetitorFilter> InputParam)
        {
            var output = _IGridService.GetCompetitorMasterGrid(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// To get Competitor Product Category Master Data.
        /// </summary>
        /// <returns>Competitor Product Category Master Data.</returns>
        [HttpPost]
        public IHttpActionResult GetCompetitorProductMasterGrid(Envelope<CompetitorProductFilter> InputParam)
        {
            var output = _IGridService.GetCompetitorProductMasterGrid(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// To get Competitor's model master grid data.
        /// </summary>
        /// <returns>Competitor's model master grid data.</returns>
        [HttpPost]
        public IHttpActionResult GetCompetitorModelMasterGrid(Envelope<CompetitorModelFilter> InputParam)
        {
            var output = _IGridService.GetCompetitorModelMasterGrid(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// To fetch sfa salary details for HR by Login Id from User, Employee and salary master table
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetSFASalaryMasterGrid(Envelope<SFASalaryMasterFilter> param)
        {
            var output = _IGridService.GetSFASalaryMasterGrid(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetTargetDateSettingGrid(Envelope<GridVariables> param)
        {
            var output = _IGridService.GetTargetDateSettingGrid(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetAssetAssignmentToRDIByReference(Envelope<AssetAssignmentToRDIGet> param)
        {
            var output = _IGridService.GetAssetAssignmentToRDIByReference(param.Data.Reference);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetTargetToSFAByMonth(Envelope<AssignTargetToSFAGet> param)
        {
            var output = _IGridService.GetTargetToSFAByMonth(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult ShowTargetToSFAByMonth(Envelope<AssignTargetToSFAGet> param)
        {
            var output = _IGridService.ShowTargetToSFAByMonth(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetIncentiveTargetCategoryMappingGrid(Envelope<GridVariables> param)
        {
            var output = _IGridService.GetIncentiveTargetCategoryMappingGrid(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetBaseIncentiveGrid(Envelope<GridVariables> param)
        {
            var output = _IGridService.GetBaseIncentiveGrid(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetPerPieceIncentiveGrid(Envelope<GridVariables> param)
        {
            var output = _IGridService.GetPerPieceIncentiveGrid(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetFestivalIncentiveGrid(Envelope<GridVariables> param)
        {
            var output = _IGridService.GetFestivalIncentiveGrid(param.Data);
            return Ok(output);
        }

        [HttpGet]
        public IHttpActionResult GetRoleRightsMappingGrid()
        {
            var output = _IGridService.GetRoleRightsMappingGrid();
            return Ok(output);
        }

        #region Master Grid with Filters
        [HttpPost]
        public IHttpActionResult GetRegionGrid(Envelope<RegionFilter> InputParam)
        {
            var output = _IGridService.GetRegionGrid(InputParam.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetStateGrid(Envelope<StateFilter> InputParam)
        {
            var output = _IGridService.GetStateGrid(InputParam.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetCityGrid(Envelope<CityFilter> InputParam)
        {
            var output = _IGridService.GetCityGrid(InputParam.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetBranchGrid(Envelope<BranchFilter> InputParam)
        {
            var output = _IGridService.GetBranchGrid(InputParam.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetProductCategoryGrid(Envelope<ProductCategoryFilter> InputParam)
        {
            var output = _IGridService.GetProductCategoryGrid(InputParam.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetSubProductCategoryGrid(Envelope<SubProductCategoryFilter> InputParam)
        {
            var output = _IGridService.GetSubProductCategoryGrid(InputParam.Data);
            return Ok(output);
        }
        #endregion

        [HttpPost]
        public IHttpActionResult GetTrainingMasterGrid(Envelope<GridVariables> param)
        {
            var output = _IGridService.GetTrainingMasterGrid(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetFeedbackMasterGrid(Envelope<GridVariables> param)
        {
            var output = _IGridService.GetFeedbackMasterGrid(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetOfferedEmployeeMasterGrid(Envelope<OfferedEmployeeGridSearchFilters> param)
        {
            var output = _IGridService.GetOfferedEmployeeMasterGrid(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult TrainingDataBYID(Envelope<TInputModel> param)
        {
            var output = _IGridService.TrainingDataBYID(param.Data);
            return Ok(output);
        }
        [HttpPost]
        public IHttpActionResult GetSFABranchChangeHistoryGrid(Envelope<SFAGridSearchFilters> param)
        {
            var output = _IGridService.GetSFABranchChangeHistoryGrid(param.Data);
            return Ok(output);
        }


        [HttpPost]
        public IHttpActionResult GetSFADealerMappingHistoryGrid(Envelope<DealerSFAFilter> param)
        {
            var output = _IGridService.GetSFADealerMappingHistoryGrid(param.Data);
            return Ok(output);
        }


        [HttpPost]
        public IHttpActionResult GetReportQSRQuantity(Envelope<QSRQuantityGet> param)
        {
            var output = _IGridService.GetReportQSRQuantity(param.Data);
            return Ok(output);
        }
    }
}