using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMBOModels;
using AMBOModels.IncentiveManagement;
using AMBOModels.Mappings;
using AMBOModels.MasterMaintenance;
using AMBOModels.Modules;
using AMBOModels.UserManagement;
using APIAccessLayer.Helper;
using System.Data;
using AMBOModels.Reports;

namespace APIAccessLayer.INTERFACE
{
    public interface IAPIGridData
    {
        GridOutput<IEnumerable<RegionGridData>> GetRegionMasterGrid(GridVariables objGridData);

        GridOutput<IEnumerable<StateGridData>> GetStateMasterGrid(GridVariables objGridData);

        GridOutput<IEnumerable<CityGridData>> GetCityMasterGrid(GridVariables objGridData);

        GridOutput<IEnumerable<LocationGridData>> GetLocationMasterGrid(LocationFilter objGridData);

        GridOutput<IEnumerable<BranchGridData>> GetBranchMasterGrid(GridVariables objGridData);

        GridOutput<IEnumerable<ProductCategoryGridData>> GetProductCategoryMasterGrid(GridVariables objGridData);

        GridOutput<IEnumerable<ProductSubCategoryGridData>> GetProductSubCategoryMasterGrid(GridVariables objGridData);

        GridOutput<IEnumerable<MaterialGridData>> GetMaterialMasterGrid(MaterialFilter objGridData);

        GridOutput<IEnumerable<DealerGridData>> GetDealerMasterGrid(DealerFilter objGridData);

        GridOutput<IEnumerable<DealerSFAMappingGridData>> GetDealerSFAMappingGrid(DealerSFAFilter objGridData);

        GridOutput<IEnumerable<DealerClassificationMappingGridData>> GetDealerClassificationMappingGrid(GridVariables objGridVariables);

        GridOutput<IEnumerable<AssetGridData>> GetAssetMasterGrid(GridVariables objGridData);

        GridOutput<IEnumerable<EmployeeGridData>> GetEmployeeMasterGrid(EmployeeGridSearchFilters objGridData);

        GridOutput<IEnumerable<EmployeeStructureMappingGridData>> GetEmployeeStructureMappingGrid(EmployeeStructureMapGridFilters objGridData);

        GridOutput<IEnumerable<SalesPICOutletMappingGridData>> GetSalesPICOutletMappingGrid(GridVariables objGridData);

        GridOutput<IEnumerable<UserBranchChannelPCMappingGridData>> GetUserBranchChannelPCMappingGrid(UserBranchChannelPCMappingFilter objGridData);

        GridOutput<IEnumerable<SFAGridData>> GetSFAMasterGrid(SFAGridSearchFilters objGridData);

        GridOutput<IEnumerable<ProductCategorySFAMappingGridData>> GetProductCategorySFAMappingGrid(ProdCatSFAMapGridSearchFilters objGridVariables);

        IEnumerable<ChannelGridData> GetChannelMasterGrid(ChannelFilter objGridData);

        GridOutput<IEnumerable<ProductTargetCategoryGridData>> GetProductTargetCategoryMasterGrid(ProductTargetCategoryGridFilters objGridData);

        GridOutput<List<BroadcastMessageGridData>> GetBroadcastMessageMasterGrid(GridVariables objGridData);

       BroadcastMessageEDITData BroadcastMessageMasterGridBYID(BMessageInputModel objBMessageInputModel);

        
        List<ProductCategoryGroupMaster> GetProductCategoryGroup(ProCatGroupFilter objGridData);        

        List<CompetitorProductData> GetCompetitorProductMasterGrid(CompetitorProductFilter InputParam);

        List<CompetitorModelList> GetCompetitorModelMasterGrid(CompetitorModelFilter InputParam);
		
		GridOutput<IEnumerable<SFAMasterforHR>> GetSFAMasterforHR(SFAMasterforHRFilter objGridData);

        GridOutput<IEnumerable<SFASalaryMasterGrid>> GetSFASalaryMaster(SFASalaryMasterFilter objGridData);

        GridOutput<IEnumerable<TargetDateSettingMaster>> GetTargetDateSettingGrid(GridVariables objGridData);

        GridOutput<IEnumerable<IncentiveTargetCategoryMappingGridData>> GetIncentiveTargetCategoryMappingGrid(GridVariables objGridData);

        GridOutput<IEnumerable<BaseIncentiveGridData>> GetBaseIncentiveGrid(GridVariables objGridData);

        GridOutput<IEnumerable<PerPieceIncentiveGridData>> GetPerPieceIncentiveGrid(GridVariables objGridData);

        IEnumerable<AssetAssignmentToRDIGrid> GetAssetAssignmentToRDIByReference(AssetAssignmentToRDIGet objGridData);

        List<AssignTargetToSFAGrid> GetTargetToSFAByMonth(AssignTargetToSFAGet objGridData);

        List<AssignTargetToSFAGrid> ShowTargetToSFAByMonth(AssignTargetToSFAGet objGridData);

        GridOutput<IEnumerable<FestivalIncentiveGridData>> GetFestivalIncentiveGrid(GridVariables objGridData);

        List<RoleRightsMappingGet> GetRoleRightsMappingGrid();
        Envelope<DataTable> GetMonthlyAttendanceReportGrid(MonthlyAttendanceReportInput Input);
		
		List<CompetitorMasterData> GetCompetitorMasterGrid(CompetitorFilter InputParam);

        List<SFALevel> GetSFALevelMasterData(SFALevelFilter InputParam);

        List<ShiftMaster> GetShiftMaster(ShiftFilter InputParam);

        List<SFASubLevel> GetSFASubLevelMasterData(SFASubLevelFilter InputParam);

        List<RegionGridData> GetRegionGrid(RegionFilter InputParam);

        List<StateGridData> GetStateGrid(StateFilter InputParam);

        List<CityGridData> GetCityGrid(CityFilter InputParam);

        List<BranchGridData> GetBranchGrid(BranchFilter InputParam);

        List<ProductCategoryGridData> GetProductCategoryGrid(ProductCategoryFilter InputParam);

        List<ProductSubCategoryGridData> GetSubProductCategoryGrid(SubProductCategoryFilter InputParam);

        GridOutput<IEnumerable<TrainingGridData>> GetTrainingMasterGrid(GridVariables objGridData);

        GridOutput<IEnumerable<FeedbackGridData>> GetFeedbackMasterGrid(GridVariables objGridData);

        TrainingMasterEditData TrainingDataBYID(TInputModel objBMessageInputModel);

        GridOutput<IEnumerable<SFAGridData>> GetSFABranchChangeHistoryGrid(SFAGridSearchFilters objGridData);

        GridOutput<IEnumerable<DealerSFAMappingGridData>> GetSFADealerMappingHistoryGrid(DealerSFAFilter objGridData);

        List<QsrReportsGrid> GetQuantityReport(QSRQuantityGet objGridData);

        List<QsrReportsGrid> ShowQuantityReport(QSRQuantityGet objGridData);

    }
}
