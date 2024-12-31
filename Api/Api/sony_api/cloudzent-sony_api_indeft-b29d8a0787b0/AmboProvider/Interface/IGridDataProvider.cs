using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboLibrary.Mappings;
using AmboLibrary.MasterMaintenance;
using AmboLibrary.Modules;
using AmboLibrary.UserManagement;
using AmboLibrary.IncentiveManagement;

namespace AmboProvider.Interface
{
    public interface IGridDataProvider
    {
        GridOutput<IEnumerable<RegionGridData>> GetRegionMasterGrid(GridVariables objGridVariables);

        GridOutput<IEnumerable<StateGridData>> GetStateMasterGrid(GridVariables objGridVariables);

        GridOutput<IEnumerable<CityGridData>> GetCityMasterGrid(GridVariables objGridVariables);

        GridOutput<IEnumerable<LocationGridData>> GetLocationMasterGrid(LocationFilter objGridVariables);

        GridOutput<IEnumerable<BranchGridData>> GetBranchMasterGrid(GridVariables objGridVariables);

        GridOutput<IEnumerable<ProductCategoryGridData>> GetProductCategoryMasterGrid(GridVariables objGridVariables);

        GridOutput<IEnumerable<ProductSubCategoryGridData>> GetProductSubCategoryMasterGrid(GridVariables objGridVariables);

        GridOutput<IEnumerable<MaterialGridData>> GetMaterialMasterGrid(MaterialFilter objGridVariables);

        GridOutput<IEnumerable<DealerGridData>> GetDealerMasterGrid(DealerFilter objGridVariables);

        GridOutput<IEnumerable<DealerSFAMappingGridData>> GetDealerSFAMappingGrid(DealerSFAFilter objGridVariables);

        GridOutput<IEnumerable<DealerClassificationMappingGridData>> GetDealerClassificationMappingGrid(GridVariables objGridVariables);

        GridOutput<List<BroadcastMessageGridData>> GetBroadcastMessageMasterGrid(GridVariables objGridVariables);

        BroadcastMessageEDITData BroadcastMessageMasterGridBYID(BMessageInputModel objBMessageInputModel);



        GridOutput<IEnumerable<AssetGridData>> GetAssetMasterGrid(GridVariables objGridVariables);

        GridOutput<IEnumerable<ProductTargetCategoryGridData>> GetProductTargetCategoryMasterGrid(ProductTargetCategoryGridFilters objGridVariables);

        GridOutput<IEnumerable<EmployeeGridData>> GetEmployeeMasterGrid(EmployeeGridSearchFilters objGridVariables);

        GridOutput<IEnumerable<EmployeeStructureMappingGridData>> GetEmployeeStructureMappingGrid(EmployeeStructureMapGridFilters objGridVariables);

        GridOutput<IEnumerable<SalesPICOutletMappingGridData>> GetSalesPICOutletMappingGrid(GridVariables objGridVariables);

        GridOutput<IEnumerable<UserBranchChannelPCMappingGridData>> GetUserBranchChannelPCMappingGrid(UserBranchChannelPCMappingFilter objGridVariables);

        GridOutput<IEnumerable<SFAGridData>> GetSFAMasterGrid(SFAGridSearchFilters objGridVariables);

        GridOutput<IEnumerable<ProductCategorySFAMappingGridData>> GetProductCategorySFAMappingGrid(ProdCatSFAMapGridSearchFilters objGridVariables);

        IEnumerable<ChannelGridData> GetChannelMasterGrid(ChannelFilter objGridVariables);

        IEnumerable<SFALevel> GetSFALevelMasterData(SFALevelFilter InputParam);

        IEnumerable<ShiftMaster> GetShiftMasterData(ShiftFilter InputParam);

        SFALevel GetSFALevelMasterDataById(SFALevelInput Input);

        IEnumerable<SFASubLevel> GetSFASubLevelMasterData(SFASubLevelFilter InputParam);

        SFASubLevel GetSFASubLevelMasterDataById(SFASubLevelInput Input);

        GridOutput<IEnumerable<SFAMasterforHRGridData>> GetSFAMasterforHRGrid(SFAMasterforHRFilter objGridVariables);

        IEnumerable<CompetitorMasterData> GetCompetitorMasterGrid(CompetitorFilter InputParam);

        IEnumerable<CompetitorProductData> GetCompetitorProductMasterGrid(CompetitorProductFilter InputParam);

        IEnumerable<CompetitorModelList> GetCompetitorModelMasterGrid(CompetitorModelFilter InputParam);

        GridOutput<IEnumerable<SFASalaryMasterGrid>> GetSFASalaryMasterGrid(SFASalaryMasterFilter objGridVariables);

        GridOutput<IEnumerable<TargetDateSettingMaster>> GetTargetDateSettingGrid(GridVariables objGridVariables);

        GridOutput<IEnumerable<IncentiveTargetCategoryMappingGridData>> GetIncentiveTargetCategoryMappingGrid(GridVariables objGridVariables);

        GridOutput<IEnumerable<BaseIncentiveGridData>> GetBaseIncentiveGrid(GridVariables objGridVariables);

        GridOutput<IEnumerable<PerPieceIncentiveGridData>> GetPerPieceIncentiveGrid(GridVariables objGridVariables);

        IEnumerable<AssetAssignmentToRDIGrid> GetAssetAssignmentToRDIByReference(string Reference);

        List<AssignTargetToSFAGrid> GetTargetToSFAByMonth(AssignTargetToSFAGet targettoSFA);

        List<AssignTargetToSFAGrid> ShowTargetToSFAByMonth(AssignTargetToSFAGet targettoSFA);

        GridOutput<IEnumerable<FestivalIncentiveGridData>> GetFestivalIncentiveGrid(GridVariables objGridVariables);

        IEnumerable<RoleRightsMappingGet> GetRoleRightsMappingGrid();

        IEnumerable<RegionGridData> GetRegionGrid(RegionFilter InputParam);

        IEnumerable<StateGridData> GetStateGrid(StateFilter InputParam);

        IEnumerable<CityGridData> GetCityGrid(CityFilter InputParam);

        IEnumerable<BranchGridData> GetBranchGrid(BranchFilter InputParam);

        IEnumerable<ProductCategoryGridData> GetProductCategoryGrid(ProductCategoryFilter InputParam);

        IEnumerable<ProductSubCategoryGridData> GetSubProductCategoryGrid(SubProductCategoryFilter InputParam);

        GridOutput<IEnumerable<TrainingGridData>> GetTrainingMasterGrid(GridVariables objGridVariables);

        GridOutput<IEnumerable<FeedbackGridData>> GetFeedbackMasterGrid(GridVariables objGridVariables);

        IEnumerable<OfferedEmployeeGridData> GetOfferedEmployeeMasterGrid(OfferedEmployeeGridSearchFilters objGridVariables);

        TrainingMasterEditData TrainingDataBYID(TInputModel objBMessageInputModel);

        GridOutput<IEnumerable<SFAGridData>> GetSFABranchChangeHistoryGrid(SFAGridSearchFilters objGridVariables);

        GridOutput<IEnumerable<DealerSFAMappingGridData>> GetSFADealerMappingHistoryGrid(DealerSFAFilter objGridVariables);

        List<QsrReportsGrid> GetReportQSRQuantity(QSRQuantityGet targettoSFA);

        
    }
}
