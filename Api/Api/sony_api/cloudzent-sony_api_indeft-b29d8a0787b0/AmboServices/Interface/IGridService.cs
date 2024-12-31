using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboLibrary.Mappings;
using AmboLibrary.MasterMaintenance;
using AmboLibrary.Modules;
using AmboLibrary.UserManagement;
using AmboUtilities;
using AmboLibrary.IncentiveManagement;

namespace AmboServices.Interface
{
    public interface IGridService
    {
        Envelope<GridOutput<IEnumerable<RegionGridData>>> GetRegionMasterGrid(GridVariables objGridVariables);

        Envelope<GridOutput<IEnumerable<StateGridData>>> GetStateMasterGrid(GridVariables objGridVariables);

        Envelope<GridOutput<IEnumerable<CityGridData>>> GetCityMasterGrid(GridVariables objGridVariables);

        Envelope<GridOutput<IEnumerable<LocationGridData>>> GetLocationMasterGrid(LocationFilter objGridVariables);

        Envelope<GridOutput<IEnumerable<BranchGridData>>> GetBranchMasterGrid(GridVariables objGridVariables);

        Envelope<GridOutput<IEnumerable<ProductCategoryGridData>>> GetProductCategoryMasterGrid(GridVariables objGridVariables);

        Envelope<GridOutput<IEnumerable<ProductSubCategoryGridData>>> GetProductSubCategoryMasterGrid(GridVariables objGridVariables);

        Envelope<GridOutput<IEnumerable<MaterialGridData>>> GetMaterialMasterGrid(MaterialFilter objGridVariables);

        Envelope<GridOutput<IEnumerable<DealerGridData>>> GetDealerMasterGrid(DealerFilter objGridVariables);

        Envelope<GridOutput<IEnumerable<DealerSFAMappingGridData>>> GetDealerSFAMappingGrid(DealerSFAFilter objGridVariables);

        Envelope<GridOutput<IEnumerable<DealerClassificationMappingGridData>>> GetDealerClassificationMappingGrid(GridVariables objGridVariables);

        Envelope<GridOutput<List<BroadcastMessageGridData>>> GetBroadcastMessageMasterGrid(GridVariables objGridVariables);
        
		Envelope<BroadcastMessageEDITData> BroadcastMessageMasterGridBYID(BMessageInputModel objBMessageInputModel);

        Envelope<GridOutput<IEnumerable<AssetGridData>>> GetAssetMasterGrid(GridVariables objGridVariables);

        Envelope<GridOutput<IEnumerable<EmployeeGridData>>> GetEmployeeMasterGrid(EmployeeGridSearchFilters objGridVariables);

        Envelope<GridOutput<IEnumerable<EmployeeStructureMappingGridData>>> GetEmployeeStructureMappingGrid(EmployeeStructureMapGridFilters objGridVariables);

        Envelope<GridOutput<IEnumerable<SalesPICOutletMappingGridData>>> GetSalesPICOutletMappingGrid(GridVariables objGridVariables);

        Envelope<GridOutput<IEnumerable<UserBranchChannelPCMappingGridData>>> GetUserBranchChannelPCMappingGrid(UserBranchChannelPCMappingFilter objGridVariables);

        Envelope<GridOutput<IEnumerable<SFAGridData>>> GetSFAMasterGrid(SFAGridSearchFilters objGridVariables);

        Envelope<GridOutput<IEnumerable<ProductCategorySFAMappingGridData>>> GetProductCategorySFAMappingGrid(ProdCatSFAMapGridSearchFilters objGridVariables);

        Envelope<IEnumerable<ChannelGridData>> GetChannelMasterGrid(ChannelFilter objGridVariables);

        Envelope<GridOutput<IEnumerable<ProductTargetCategoryGridData>>> GetProductTargetCategoryMasterGrid(ProductTargetCategoryGridFilters objGridVariables);

        Envelope<IEnumerable<SFALevel>> GetSFALevelMasterData(SFALevelFilter InputParam);

        Envelope<IEnumerable<ShiftMaster>> GetShiftMasterData(ShiftFilter InputParam);

        Envelope<SFALevel> GetSFALevelMasterDataById(SFALevelInput Input);

        Envelope<IEnumerable<SFASubLevel>> GetSFASubLevelMasterData(SFASubLevelFilter InputParam);

        Envelope<SFASubLevel> GetSFASubLevelMasterDataById(SFASubLevelInput Input);

        Envelope<GridOutput<IEnumerable<SFAMasterforHRGridData>>> GetSFAMasterforHRGrid(SFAMasterforHRFilter objGridVariables);

        Envelope<IEnumerable<CompetitorMasterData>> GetCompetitorMasterGrid(CompetitorFilter InputParam);

        Envelope<IEnumerable<CompetitorProductData>> GetCompetitorProductMasterGrid(CompetitorProductFilter InputParam);

        Envelope<IEnumerable<CompetitorModelList>> GetCompetitorModelMasterGrid(CompetitorModelFilter InputParam);

        Envelope<GridOutput<IEnumerable<SFASalaryMasterGrid>>> GetSFASalaryMasterGrid(SFASalaryMasterFilter objGridVariables);

        Envelope<GridOutput<IEnumerable<TargetDateSettingMaster>>> GetTargetDateSettingGrid(GridVariables objGridVariables);

        Envelope<GridOutput<IEnumerable<IncentiveTargetCategoryMappingGridData>>> GetIncentiveTargetCategoryMappingGrid(GridVariables objGridVariables);

        Envelope<GridOutput<IEnumerable<BaseIncentiveGridData>>> GetBaseIncentiveGrid(GridVariables objGridVariables);

        Envelope<GridOutput<IEnumerable<PerPieceIncentiveGridData>>> GetPerPieceIncentiveGrid(GridVariables objGridVariables);

        Envelope<IEnumerable<AssetAssignmentToRDIGrid>> GetAssetAssignmentToRDIByReference(string Reference);

        Envelope<List<AssignTargetToSFAGrid>> GetTargetToSFAByMonth(AssignTargetToSFAGet targettoSFA);

        Envelope<List<AssignTargetToSFAGrid>> ShowTargetToSFAByMonth(AssignTargetToSFAGet targettoSFA);

        Envelope<GridOutput<IEnumerable<FestivalIncentiveGridData>>> GetFestivalIncentiveGrid(GridVariables objGridVariables);

        Envelope<IEnumerable<RoleRightsMappingGet>> GetRoleRightsMappingGrid();

        Envelope<IEnumerable<RegionGridData>> GetRegionGrid(RegionFilter InputParam);

        Envelope<IEnumerable<StateGridData>> GetStateGrid(StateFilter InputParam);

        Envelope<IEnumerable<CityGridData>> GetCityGrid(CityFilter InputParam);

        Envelope<IEnumerable<BranchGridData>> GetBranchGrid(BranchFilter InputParam);

        Envelope<IEnumerable<ProductCategoryGridData>> GetProductCategoryGrid(ProductCategoryFilter InputParam);

        Envelope<IEnumerable<ProductSubCategoryGridData>> GetSubProductCategoryGrid(SubProductCategoryFilter InputParam);

        Envelope<GridOutput<IEnumerable<TrainingGridData>>> GetTrainingMasterGrid(GridVariables objGridVariables);

        Envelope<GridOutput<IEnumerable<FeedbackGridData>>> GetFeedbackMasterGrid(GridVariables objGridVariables);

        Envelope<IEnumerable<OfferedEmployeeGridData>> GetOfferedEmployeeMasterGrid(OfferedEmployeeGridSearchFilters objGridVariables);

        Envelope<TrainingMasterEditData> TrainingDataBYID(TInputModel objBMessageInputModel);

        Envelope<GridOutput<IEnumerable<SFAGridData>>> GetSFABranchChangeHistoryGrid(SFAGridSearchFilters objGridVariables);

        Envelope<GridOutput<IEnumerable<DealerSFAMappingGridData>>> GetSFADealerMappingHistoryGrid(DealerSFAFilter objGridVariables);

        Envelope<List<QsrReportsGrid>> GetReportQSRQuantity(QSRQuantityGet targettoSFA);
    }
}
