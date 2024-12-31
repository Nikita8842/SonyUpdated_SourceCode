using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboLibrary.MasterMaintenance;
using AmboLibrary.UserManagement;
using AmboLibrary.Mappings;
using AmboDataServices.Interface;
using AmboProvider.Interface;
using AmboLibrary.Modules;
using AmboLibrary.IncentiveManagement;

namespace AmboDataServices.Implimentation
{
    public class GridDataService : IGridDataService
    {
        private readonly IGridDataProvider _IGridDataProvider;

        public GridDataService(IGridDataProvider IGridDataProvider)
        {
            _IGridDataProvider = IGridDataProvider;
        }

        public GridOutput<IEnumerable<CityGridData>> GetCityMasterGrid(GridVariables objGridVariables)
        {
            return _IGridDataProvider.GetCityMasterGrid(objGridVariables);
        }

        public GridOutput<IEnumerable<RegionGridData>> GetRegionMasterGrid(GridVariables objGridVariables)
        {
            return _IGridDataProvider.GetRegionMasterGrid(objGridVariables);
        }

        public GridOutput<IEnumerable<StateGridData>> GetStateMasterGrid(GridVariables objGridVariables)
        {
            return _IGridDataProvider.GetStateMasterGrid(objGridVariables);
        }

        public GridOutput<IEnumerable<LocationGridData>> GetLocationMasterGrid(LocationFilter objGridVariables)
        {
            return _IGridDataProvider.GetLocationMasterGrid(objGridVariables);
        }

        public GridOutput<IEnumerable<BranchGridData>> GetBranchMasterGrid(GridVariables objGridVariables)
        {
            return _IGridDataProvider.GetBranchMasterGrid(objGridVariables);
        }

        public GridOutput<IEnumerable<ProductCategoryGridData>> GetProductCategoryMasterGrid(GridVariables objGridVariables)
        {
            return _IGridDataProvider.GetProductCategoryMasterGrid(objGridVariables);
        }

        public GridOutput<IEnumerable<ProductSubCategoryGridData>> GetProductSubCategoryMasterGrid(GridVariables objGridVariables)
        {
            return _IGridDataProvider.GetProductSubCategoryMasterGrid(objGridVariables);
        }

        public GridOutput<IEnumerable<MaterialGridData>> GetMaterialMasterGrid(MaterialFilter objGridVariables)
        {
            return _IGridDataProvider.GetMaterialMasterGrid(objGridVariables);
        }

        public GridOutput<IEnumerable<DealerGridData>> GetDealerMasterGrid(DealerFilter objGridVariables)
        {
            return _IGridDataProvider.GetDealerMasterGrid(objGridVariables);
        }

        public GridOutput<IEnumerable<DealerSFAMappingGridData>> GetDealerSFAMappingGrid(DealerSFAFilter objGridVariables)
        {
            return _IGridDataProvider.GetDealerSFAMappingGrid(objGridVariables);
        }

        public GridOutput<IEnumerable<DealerClassificationMappingGridData>> GetDealerClassificationMappingGrid(GridVariables objGridVariables)
        {
            return _IGridDataProvider.GetDealerClassificationMappingGrid(objGridVariables);
        }

        public GridOutput<List<BroadcastMessageGridData>> GetBroadcastMessageMasterGrid(GridVariables objGridVariables)
        {
            return _IGridDataProvider.GetBroadcastMessageMasterGrid(objGridVariables);
        }


        public BroadcastMessageEDITData BroadcastMessageMasterGridBYID(BMessageInputModel objBMessageInputModel)
        {
            return _IGridDataProvider.BroadcastMessageMasterGridBYID(objBMessageInputModel);
        }


        public IEnumerable<SFALevel> GetSFALevelMasterData(SFALevelFilter InputParam)
        {
            return _IGridDataProvider.GetSFALevelMasterData(InputParam);
        }

        public IEnumerable<ShiftMaster> GetShiftMasterData(ShiftFilter InputParam)
        {
            return _IGridDataProvider.GetShiftMasterData(InputParam);
        }

        public SFALevel GetSFALevelMasterDataById(SFALevelInput Input)
        {
            return _IGridDataProvider.GetSFALevelMasterDataById(Input);
        }

        public GridOutput<IEnumerable<AssetGridData>> GetAssetMasterGrid(GridVariables objGridVariables)
        {
            return _IGridDataProvider.GetAssetMasterGrid(objGridVariables);
        }

        public GridOutput<IEnumerable<ProductTargetCategoryGridData>> GetProductTargetCategoryMasterGrid(ProductTargetCategoryGridFilters objGridVariables)
        {
            return _IGridDataProvider.GetProductTargetCategoryMasterGrid(objGridVariables);
        }

        public IEnumerable<SFASubLevel> GetSFASubLevelMasterData(SFASubLevelFilter InputParam)
        {
            return _IGridDataProvider.GetSFASubLevelMasterData(InputParam);
        }

        public SFASubLevel GetSFASubLevelMasterDataById(SFASubLevelInput Input)
        {
            return _IGridDataProvider.GetSFASubLevelMasterDataById(Input);
        }
		
		public GridOutput<IEnumerable<EmployeeGridData>> GetEmployeeMasterGrid(EmployeeGridSearchFilters objGridVariables)
        {
            return _IGridDataProvider.GetEmployeeMasterGrid(objGridVariables);
        }

        public GridOutput<IEnumerable<EmployeeStructureMappingGridData>> GetEmployeeStructureMappingGrid(EmployeeStructureMapGridFilters objGridVariables)
        {
            return _IGridDataProvider.GetEmployeeStructureMappingGrid(objGridVariables);
        }

        public GridOutput<IEnumerable<SalesPICOutletMappingGridData>> GetSalesPICOutletMappingGrid(GridVariables objGridVariables)
        {
            return _IGridDataProvider.GetSalesPICOutletMappingGrid(objGridVariables);
        }

        public GridOutput<IEnumerable<UserBranchChannelPCMappingGridData>> GetUserBranchChannelPCMappingGrid(UserBranchChannelPCMappingFilter objGridVariables)
        {
            return _IGridDataProvider.GetUserBranchChannelPCMappingGrid(objGridVariables);
        }

        public GridOutput<IEnumerable<SFAMasterforHRGridData>> GetSFAMasterforHRGrid(SFAMasterforHRFilter objGridVariables)
        {
            return _IGridDataProvider.GetSFAMasterforHRGrid(objGridVariables);
        }
        public IEnumerable<CompetitorMasterData> GetCompetitorMasterGrid(CompetitorFilter InputParam)
        {
            return _IGridDataProvider.GetCompetitorMasterGrid(InputParam);
        }

        public IEnumerable<CompetitorProductData> GetCompetitorProductMasterGrid(CompetitorProductFilter InputParam)
        {
            return _IGridDataProvider.GetCompetitorProductMasterGrid(InputParam);
        }

        public IEnumerable<CompetitorModelList> GetCompetitorModelMasterGrid(CompetitorModelFilter InputParam)
        {
            return _IGridDataProvider.GetCompetitorModelMasterGrid(InputParam);
        }
		
		public IEnumerable<ChannelGridData> GetChannelMasterGrid(ChannelFilter objGridVariables)
        {
            return _IGridDataProvider.GetChannelMasterGrid(objGridVariables);
        }
		
		public GridOutput<IEnumerable<SFASalaryMasterGrid>> GetSFASalaryMasterGrid(SFASalaryMasterFilter objGridVariables)
        {
            return _IGridDataProvider.GetSFASalaryMasterGrid(objGridVariables);
        }

        public GridOutput<IEnumerable<SFAGridData>> GetSFAMasterGrid(SFAGridSearchFilters objGridVariables)
        {
            return _IGridDataProvider.GetSFAMasterGrid(objGridVariables);
        }
		
		public GridOutput<IEnumerable<TargetDateSettingMaster>> GetTargetDateSettingGrid(GridVariables objGridVariables)
        {
            return _IGridDataProvider.GetTargetDateSettingGrid(objGridVariables);
        }

        public GridOutput<IEnumerable<IncentiveTargetCategoryMappingGridData>> GetIncentiveTargetCategoryMappingGrid(GridVariables objGridVariables)
        {
            return _IGridDataProvider.GetIncentiveTargetCategoryMappingGrid(objGridVariables);
        }

        public GridOutput<IEnumerable<BaseIncentiveGridData>> GetBaseIncentiveGrid(GridVariables objGridVariables)
        {
            return _IGridDataProvider.GetBaseIncentiveGrid(objGridVariables);
        }

        public GridOutput<IEnumerable<PerPieceIncentiveGridData>> GetPerPieceIncentiveGrid(GridVariables objGridVariables)
        {
            return _IGridDataProvider.GetPerPieceIncentiveGrid(objGridVariables);
        }

        public GridOutput<IEnumerable<FestivalIncentiveGridData>> GetFestivalIncentiveGrid(GridVariables objGridVariables)
        {
            return _IGridDataProvider.GetFestivalIncentiveGrid(objGridVariables);
        }

        public IEnumerable<AssetAssignmentToRDIGrid> GetAssetAssignmentToRDIByReference(string Reference)
        {
            return _IGridDataProvider.GetAssetAssignmentToRDIByReference(Reference);
        }
		
		public GridOutput<IEnumerable<ProductCategorySFAMappingGridData>> GetProductCategorySFAMappingGrid(ProdCatSFAMapGridSearchFilters objGridVariables)
        {
            return _IGridDataProvider.GetProductCategorySFAMappingGrid(objGridVariables);
        }
		
		public List<AssignTargetToSFAGrid> GetTargetToSFAByMonth(AssignTargetToSFAGet targettoSFA)
        {
            return _IGridDataProvider.GetTargetToSFAByMonth(targettoSFA);
        }

        public List<AssignTargetToSFAGrid> ShowTargetToSFAByMonth(AssignTargetToSFAGet targettoSFA)
        {
            return _IGridDataProvider.ShowTargetToSFAByMonth(targettoSFA);
        }

        public IEnumerable<RoleRightsMappingGet> GetRoleRightsMappingGrid()
        {
            return _IGridDataProvider.GetRoleRightsMappingGrid();
        }

        #region Master Grid with Filters
        public IEnumerable<RegionGridData> GetRegionGrid(RegionFilter InputParam)
        {
            return _IGridDataProvider.GetRegionGrid(InputParam);
        }

        public IEnumerable<StateGridData> GetStateGrid(StateFilter InputParam)
        {
            return _IGridDataProvider.GetStateGrid(InputParam);
        }

        public IEnumerable<CityGridData> GetCityGrid(CityFilter InputParam)
        {
            return _IGridDataProvider.GetCityGrid(InputParam);
        }

        public IEnumerable<BranchGridData> GetBranchGrid(BranchFilter InputParam)
        {
            return _IGridDataProvider.GetBranchGrid(InputParam);
        }

        public IEnumerable<ProductCategoryGridData> GetProductCategoryGrid(ProductCategoryFilter InputParam)
        {
            return _IGridDataProvider.GetProductCategoryGrid(InputParam);
        }

        public IEnumerable<ProductSubCategoryGridData> GetSubProductCategoryGrid(SubProductCategoryFilter InputParam)
        {
            return _IGridDataProvider.GetSubProductCategoryGrid(InputParam);
        }

        public GridOutput<IEnumerable<TrainingGridData>> GetTrainingMasterGrid(GridVariables objGridVariables)
        {
            return _IGridDataProvider.GetTrainingMasterGrid(objGridVariables);
        }

        public GridOutput<IEnumerable<FeedbackGridData>> GetFeedbackMasterGrid(GridVariables objGridVariables)
        {
            return _IGridDataProvider.GetFeedbackMasterGrid(objGridVariables);
        }

        public IEnumerable<OfferedEmployeeGridData> GetOfferedEmployeeMasterGrid(OfferedEmployeeGridSearchFilters objGridVariables)
        {
            return _IGridDataProvider.GetOfferedEmployeeMasterGrid(objGridVariables);
        }
        #endregion

        public TrainingMasterEditData TrainingDataBYID(TInputModel objBMessageInputModel)
        {
            return _IGridDataProvider.TrainingDataBYID(objBMessageInputModel);
        }

     

        public GridOutput<IEnumerable<DealerSFAMappingGridData>> GetSFADealerMappingHistoryGrid(DealerSFAFilter objGridVariables)
        {
            return _IGridDataProvider.GetSFADealerMappingHistoryGrid(objGridVariables);
        }

        public GridOutput<IEnumerable<SFAGridData>> GetSFABranchChangeHistoryGrid(SFAGridSearchFilters objGridVariables)
        {
            return _IGridDataProvider.GetSFABranchChangeHistoryGrid(objGridVariables);
        }

        public List<QsrReportsGrid> GetReportQSRQuantity(QSRQuantityGet targettoSFA)
        {
            return _IGridDataProvider.GetReportQSRQuantity(targettoSFA);
        }
    }
}
