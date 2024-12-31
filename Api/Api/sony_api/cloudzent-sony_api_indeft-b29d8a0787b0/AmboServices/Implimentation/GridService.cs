using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboUtilities;
using AmboDataServices.Interface;
using AmboUtilities.Helper;
using AmboLibrary.MasterMaintenance;
using AmboLibrary.UserManagement;
using AmboLibrary.Mappings;
using AmboLibrary.Modules;
using AmboServices.Interface;
using AmboLibrary.IncentiveManagement;

namespace AmboServices.Implimentation
{
    public class GridService : IGridService
    {
        private readonly IGridDataService _IGridDataService;
        public GridService(IGridDataService IGridDataService)
        {
            _IGridDataService = IGridDataService;
        }

        public Envelope<GridOutput<IEnumerable<CityGridData>>> GetCityMasterGrid(GridVariables objGridVariables)
        {
            var output = _IGridDataService.GetCityMasterGrid(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<CityGridData>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<CityGridData>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "City data fetched successfully." };
        }

        public Envelope<GridOutput<IEnumerable<RegionGridData>>> GetRegionMasterGrid(GridVariables objGridVariables)
        {
            var output = _IGridDataService.GetRegionMasterGrid(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<RegionGridData>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<RegionGridData>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Region data fetched successfully." };
        }

        public Envelope<GridOutput<IEnumerable<StateGridData>>> GetStateMasterGrid(GridVariables objGridVariables)
        {
            var output = _IGridDataService.GetStateMasterGrid(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<StateGridData>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<StateGridData>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "State data fetched successfully." };
        }

        public Envelope<GridOutput<IEnumerable<LocationGridData>>> GetLocationMasterGrid(LocationFilter objGridVariables)
        {
            var output = _IGridDataService.GetLocationMasterGrid(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<LocationGridData>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<LocationGridData>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Location data fetched successfully." };
        }

        public Envelope<GridOutput<IEnumerable<BranchGridData>>> GetBranchMasterGrid(GridVariables objGridVariables)
        {
            var output = _IGridDataService.GetBranchMasterGrid(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<BranchGridData>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<BranchGridData>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Branch data fetched successfully." };
        }

        public Envelope<GridOutput<IEnumerable<ProductCategoryGridData>>> GetProductCategoryMasterGrid(GridVariables objGridVariables)
        {
            var output = _IGridDataService.GetProductCategoryMasterGrid(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<ProductCategoryGridData>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<ProductCategoryGridData>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Product category data fetched successfully." };
        }

        public Envelope<GridOutput<IEnumerable<ProductSubCategoryGridData>>> GetProductSubCategoryMasterGrid(GridVariables objGridVariables)
        {
            var output = _IGridDataService.GetProductSubCategoryMasterGrid(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<ProductSubCategoryGridData>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<ProductSubCategoryGridData>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Product sub category data fetched successfully." };
        }

        public Envelope<GridOutput<IEnumerable<MaterialGridData>>> GetMaterialMasterGrid(MaterialFilter objGridVariables)
        {
            var output = _IGridDataService.GetMaterialMasterGrid(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<MaterialGridData>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<MaterialGridData>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Material data fetched successfully." };
        }

        public Envelope<GridOutput<IEnumerable<DealerGridData>>> GetDealerMasterGrid(DealerFilter objGridVariables)
        {
            var output = _IGridDataService.GetDealerMasterGrid(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<DealerGridData>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<DealerGridData>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Dealer data fetched successfully." };
        }

        public Envelope<GridOutput<IEnumerable<DealerSFAMappingGridData>>> GetDealerSFAMappingGrid(DealerSFAFilter objGridVariables)
        {
            var output = _IGridDataService.GetDealerSFAMappingGrid(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<DealerSFAMappingGridData>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<DealerSFAMappingGridData>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Dealer SFA Mapping data fetched successfully." };
        }

        public Envelope<GridOutput<IEnumerable<DealerClassificationMappingGridData>>> GetDealerClassificationMappingGrid(GridVariables objGridVariables)
        {
            var output = _IGridDataService.GetDealerClassificationMappingGrid(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<DealerClassificationMappingGridData>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<DealerClassificationMappingGridData>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Dealer Classification Mapping data fetched successfully." };
        }

        public Envelope<GridOutput<List<BroadcastMessageGridData>>> GetBroadcastMessageMasterGrid(GridVariables objGridVariables)
        {
            var output = _IGridDataService.GetBroadcastMessageMasterGrid(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<List<BroadcastMessageGridData>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<List<BroadcastMessageGridData>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Broadcast Message data fetched successfully." };
        }

        public Envelope<BroadcastMessageEDITData> BroadcastMessageMasterGridBYID(BMessageInputModel objBMessageInputModel)
        {
            var output = _IGridDataService.BroadcastMessageMasterGridBYID(objBMessageInputModel);
            return (output == null) ?
                new Envelope<BroadcastMessageEDITData> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<BroadcastMessageEDITData> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Broadcast Message data fetched successfully." };
        }

        public Envelope<IEnumerable<SFALevel>> GetSFALevelMasterData(SFALevelFilter InputParam)
        {
            var output = _IGridDataService.GetSFALevelMasterData(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<SFALevel>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<SFALevel>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "SFA Level Master Data data fetched successfully." };
        }

        public Envelope<IEnumerable<ShiftMaster>> GetShiftMasterData(ShiftFilter InputParam)
        {
            var output = _IGridDataService.GetShiftMasterData(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<ShiftMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<ShiftMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Shift Master Data data fetched successfully." };
        }

        public Envelope<SFALevel> GetSFALevelMasterDataById(SFALevelInput Input)
        {
            var Data = _IGridDataService.GetSFALevelMasterDataById(Input);
            return (Data == null) ?
                new Envelope<SFALevel> { Data = Data, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<SFALevel> { Data = Data, MessageCode = (int)Acceptable.Found, Message = "SFA Level Master Data data fetched successfully." };
        }

        public Envelope<GridOutput<IEnumerable<AssetGridData>>> GetAssetMasterGrid(GridVariables objGridVariables)
        {
            var output = _IGridDataService.GetAssetMasterGrid(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<AssetGridData>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<AssetGridData>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Asset data fetched successfully." };
        }

        public Envelope<GridOutput<IEnumerable<ProductTargetCategoryGridData>>> GetProductTargetCategoryMasterGrid(ProductTargetCategoryGridFilters objGridVariables)
        {
            var output = _IGridDataService.GetProductTargetCategoryMasterGrid(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<ProductTargetCategoryGridData>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<ProductTargetCategoryGridData>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Product target category data fetched successfully." };
        }

        public Envelope<IEnumerable<SFASubLevel>> GetSFASubLevelMasterData(SFASubLevelFilter InputParam)
        {
            var output = _IGridDataService.GetSFASubLevelMasterData(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<SFASubLevel>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<SFASubLevel>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "SFA Sub Level Master Data data fetched successfully." };
        }

        public Envelope<SFASubLevel> GetSFASubLevelMasterDataById(SFASubLevelInput Input)
        {
            var Data = _IGridDataService.GetSFASubLevelMasterDataById(Input);
            return (Data == null) ?
                new Envelope<SFASubLevel> { Data = Data, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<SFASubLevel> { Data = Data, MessageCode = (int)Acceptable.Found, Message = "SFA Sub Level Master Data data fetched successfully." };
        }

        public Envelope<GridOutput<IEnumerable<EmployeeGridData>>> GetEmployeeMasterGrid(EmployeeGridSearchFilters objGridVariables)
        {
            var output = _IGridDataService.GetEmployeeMasterGrid(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<EmployeeGridData>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<EmployeeGridData>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Employee data fetched successfully." };
        }

        public Envelope<GridOutput<IEnumerable<EmployeeStructureMappingGridData>>> GetEmployeeStructureMappingGrid(EmployeeStructureMapGridFilters objGridVariables)
        {
            var output = _IGridDataService.GetEmployeeStructureMappingGrid(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<EmployeeStructureMappingGridData>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<EmployeeStructureMappingGridData>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Employee structure mapping data fetched successfully." };
        }

        public Envelope<GridOutput<IEnumerable<SalesPICOutletMappingGridData>>> GetSalesPICOutletMappingGrid(GridVariables objGridVariables)
        {
            var output = _IGridDataService.GetSalesPICOutletMappingGrid(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<SalesPICOutletMappingGridData>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<SalesPICOutletMappingGridData>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Sales PIC Outlet mapping data fetched successfully." };
        }

        public Envelope<GridOutput<IEnumerable<UserBranchChannelPCMappingGridData>>> GetUserBranchChannelPCMappingGrid(UserBranchChannelPCMappingFilter objGridVariables)
        {
            var output = _IGridDataService.GetUserBranchChannelPCMappingGrid(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<UserBranchChannelPCMappingGridData>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<UserBranchChannelPCMappingGridData>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "User Branch Channel PC Mapping data fetched successfully." };
        }

        public Envelope<GridOutput<IEnumerable<SFAMasterforHRGridData>>> GetSFAMasterforHRGrid(SFAMasterforHRFilter objGridVariables)
        {
            var output = _IGridDataService.GetSFAMasterforHRGrid(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<SFAMasterforHRGridData>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<SFAMasterforHRGridData>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "SFA Master for HR data fetched successfully." };
        }
        public Envelope<IEnumerable<CompetitorMasterData>> GetCompetitorMasterGrid(CompetitorFilter InputParam)
        {
            var output = _IGridDataService.GetCompetitorMasterGrid(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<CompetitorMasterData>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<CompetitorMasterData>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Competitor Master data fetched successfully." };
        }
        public Envelope<IEnumerable<CompetitorProductData>> GetCompetitorProductMasterGrid(CompetitorProductFilter InputParam)
        {
            var output = _IGridDataService.GetCompetitorProductMasterGrid(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<CompetitorProductData>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<CompetitorProductData>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Competitor Product Master data fetched successfully." };
        }
        public Envelope<IEnumerable<CompetitorModelList>> GetCompetitorModelMasterGrid(CompetitorModelFilter InputParam)
        {
            var output = _IGridDataService.GetCompetitorModelMasterGrid(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<CompetitorModelList>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<CompetitorModelList>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Competitor Model Master data fetched successfully." };
        }

        public Envelope<IEnumerable<ChannelGridData>> GetChannelMasterGrid(ChannelFilter objGridVariables)
        {
            var output = _IGridDataService.GetChannelMasterGrid(objGridVariables);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<ChannelGridData>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<ChannelGridData>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Channel data fetched successfully." };
        }

        public Envelope<GridOutput<IEnumerable<SFASalaryMasterGrid>>> GetSFASalaryMasterGrid(SFASalaryMasterFilter objGridVariables)
        {
            var output = _IGridDataService.GetSFASalaryMasterGrid(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<SFASalaryMasterGrid>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<SFASalaryMasterGrid>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "SFA Salary Master data fetched successfully." };
        }

        public Envelope<GridOutput<IEnumerable<SFAGridData>>> GetSFAMasterGrid(SFAGridSearchFilters objGridVariables)
        {
            var output = _IGridDataService.GetSFAMasterGrid(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<SFAGridData>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<SFAGridData>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "SFA data fetched successfully." };
        }

        public Envelope<GridOutput<IEnumerable<ProductCategorySFAMappingGridData>>> GetProductCategorySFAMappingGrid(ProdCatSFAMapGridSearchFilters objGridVariables)
        {
            var output = _IGridDataService.GetProductCategorySFAMappingGrid(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<ProductCategorySFAMappingGridData>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<ProductCategorySFAMappingGridData>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Product Category SFA mapping fetched successfully." };
        }

        public Envelope<GridOutput<IEnumerable<TargetDateSettingMaster>>> GetTargetDateSettingGrid(GridVariables objGridVariables)
        {
            var output = _IGridDataService.GetTargetDateSettingGrid(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<TargetDateSettingMaster>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<TargetDateSettingMaster>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Target Date Setting Master data fetched successfully." };
        }

        public Envelope<GridOutput<IEnumerable<IncentiveTargetCategoryMappingGridData>>> GetIncentiveTargetCategoryMappingGrid(GridVariables objGridVariables)
        {
            var output = _IGridDataService.GetIncentiveTargetCategoryMappingGrid(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<IncentiveTargetCategoryMappingGridData>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<IncentiveTargetCategoryMappingGridData>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Incentive Category Master data fetched successfully." };
        }

        public Envelope<GridOutput<IEnumerable<BaseIncentiveGridData>>> GetBaseIncentiveGrid(GridVariables objGridVariables)
        {
            var output = _IGridDataService.GetBaseIncentiveGrid(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<BaseIncentiveGridData>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<BaseIncentiveGridData>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Base incentive definition data fetched successfully." };
        }

        public Envelope<GridOutput<IEnumerable<PerPieceIncentiveGridData>>> GetPerPieceIncentiveGrid(GridVariables objGridVariables)
        {
            var output = _IGridDataService.GetPerPieceIncentiveGrid(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<PerPieceIncentiveGridData>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<PerPieceIncentiveGridData>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Per Piece Incentive Definition data fetched successfully." };
        }

        public Envelope<GridOutput<IEnumerable<FestivalIncentiveGridData>>> GetFestivalIncentiveGrid(GridVariables objGridVariables)
        {
            var output = _IGridDataService.GetFestivalIncentiveGrid(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<FestivalIncentiveGridData>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<FestivalIncentiveGridData>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Festival Incentive Definition data fetched successfully." };
        }

        public Envelope<IEnumerable<AssetAssignmentToRDIGrid>> GetAssetAssignmentToRDIByReference(string Reference)
        {
            var output = _IGridDataService.GetAssetAssignmentToRDIByReference(Reference);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<AssetAssignmentToRDIGrid>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<AssetAssignmentToRDIGrid>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Target Date Setting Master data fetched successfully." };
        }

        public Envelope<List<AssignTargetToSFAGrid>> GetTargetToSFAByMonth(AssignTargetToSFAGet targettoSFA)
        {
            var output = _IGridDataService.GetTargetToSFAByMonth(targettoSFA);
            return (output == null || output.Count() == 0) ?
                new Envelope<List<AssignTargetToSFAGrid>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<List<AssignTargetToSFAGrid>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Assign Target to SFA data fetched successfully." };
        }

        public Envelope<List<AssignTargetToSFAGrid>> ShowTargetToSFAByMonth(AssignTargetToSFAGet targettoSFA)
        {
            var output = _IGridDataService.ShowTargetToSFAByMonth(targettoSFA);
            return (output == null || output.Count() == 0) ?
                new Envelope<List<AssignTargetToSFAGrid>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<List<AssignTargetToSFAGrid>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Assign Target to SFA data fetched successfully." };
        }

        public Envelope<IEnumerable<RoleRightsMappingGet>> GetRoleRightsMappingGrid()
        {
            var output = _IGridDataService.GetRoleRightsMappingGrid();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<RoleRightsMappingGet>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<RoleRightsMappingGet>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Role Rights Mapping data fetched successfully." };
        }

        #region Master Grid with Filters
        public Envelope<IEnumerable<RegionGridData>> GetRegionGrid(RegionFilter InputParam)
        {
            var output = _IGridDataService.GetRegionGrid(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<RegionGridData>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<RegionGridData>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Region data fetched successfully." };
        }

        public Envelope<IEnumerable<StateGridData>> GetStateGrid(StateFilter InputParam)
        {
            var output = _IGridDataService.GetStateGrid(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<StateGridData>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<StateGridData>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "State data fetched successfully." };
        }

        public Envelope<IEnumerable<CityGridData>> GetCityGrid(CityFilter InputParam)
        {
            var output = _IGridDataService.GetCityGrid(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<CityGridData>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<CityGridData>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "City data fetched successfully." };
        }

        public Envelope<IEnumerable<BranchGridData>> GetBranchGrid(BranchFilter InputParam)
        {
            var output = _IGridDataService.GetBranchGrid(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<BranchGridData>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<BranchGridData>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Branch data fetched successfully." };
        }

        public Envelope<IEnumerable<ProductCategoryGridData>> GetProductCategoryGrid(ProductCategoryFilter InputParam)
        {
            var output = _IGridDataService.GetProductCategoryGrid(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<ProductCategoryGridData>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<ProductCategoryGridData>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Product category data fetched successfully." };
        }

        public Envelope<IEnumerable<ProductSubCategoryGridData>> GetSubProductCategoryGrid(SubProductCategoryFilter InputParam)
        {
            var output = _IGridDataService.GetSubProductCategoryGrid(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<ProductSubCategoryGridData>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<ProductSubCategoryGridData>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Product sub category data fetched successfully." };
        }
        #endregion

        public Envelope<GridOutput<IEnumerable<TrainingGridData>>> GetTrainingMasterGrid(GridVariables objGridVariables)
        {
            var output = _IGridDataService.GetTrainingMasterGrid(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<TrainingGridData>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<TrainingGridData>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Training data fetched successfully." };
        }

        public Envelope<GridOutput<IEnumerable<FeedbackGridData>>> GetFeedbackMasterGrid(GridVariables objGridVariables)
        {
            var output = _IGridDataService.GetFeedbackMasterGrid(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<FeedbackGridData>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<FeedbackGridData>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Feedback form data fetched successfully." };
        }

        public Envelope<IEnumerable<OfferedEmployeeGridData>> GetOfferedEmployeeMasterGrid(OfferedEmployeeGridSearchFilters objGridVariables)
        {
            var output = _IGridDataService.GetOfferedEmployeeMasterGrid(objGridVariables);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<OfferedEmployeeGridData>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<OfferedEmployeeGridData>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Offered Employees data fetched successfully." };
        }

        public Envelope<TrainingMasterEditData> TrainingDataBYID(TInputModel objTInputModel)
        {
            var output = _IGridDataService.TrainingDataBYID(objTInputModel);
            return (output == null) ?
                new Envelope<TrainingMasterEditData> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<TrainingMasterEditData> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Broadcast Message data fetched successfully." };
        }

       

        public Envelope<GridOutput<IEnumerable<DealerSFAMappingGridData>>> GetSFADealerMappingHistoryGrid(DealerSFAFilter objGridVariables)
        {
            var output = _IGridDataService.GetSFADealerMappingHistoryGrid(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<DealerSFAMappingGridData>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<DealerSFAMappingGridData>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Dealer SFA Mapping History data fetched successfully." };
        }


        public Envelope<GridOutput<IEnumerable<SFAGridData>>> GetSFABranchChangeHistoryGrid(SFAGridSearchFilters objGridVariables)
        {
            var output = _IGridDataService.GetSFABranchChangeHistoryGrid(objGridVariables);
            return (output.data == null || output.data.Count() == 0) ?
                new Envelope<GridOutput<IEnumerable<SFAGridData>>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GridOutput<IEnumerable<SFAGridData>>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "SFA data fetched successfully." };
        }

        #region nikita kawade 9/6/2024
        public Envelope<List<QsrReportsGrid>> GetReportQSRQuantity(QSRQuantityGet targettoSFA)
        {
            var output = _IGridDataService.GetReportQSRQuantity(targettoSFA);
            return (output == null || output.Count() == 0) ?
            new Envelope<List<QsrReportsGrid>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
            new Envelope<List<QsrReportsGrid>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Assign Target to SFA data fetched successfully." };
        }
        #endregion

    }
}
