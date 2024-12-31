using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboProvider.Interface;
using AmboDataServices.Interface;
using AmboLibrary.MasterMaintenance;
using System.Data;
using AmboLibrary.WebReports;

namespace AmboDataServices.Implimentation
{
    public class MasterMaintenanceDataService : IMasterMaintenanceDataService
    {
        private readonly IMasterMaintenanceProvider _IMasterMaintenanceProvider;

        public MasterMaintenanceDataService(IMasterMaintenanceProvider IMasterMaintenanceProvider)
        {
            _IMasterMaintenanceProvider = IMasterMaintenanceProvider;
        }

        #region Region Master API
        public bool CreateRegion(RegionMaster regionData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.CreateRegion(regionData, out Message));
        }

        public IEnumerable<RegionMaster> GetAllRegions()
        {
            return _IMasterMaintenanceProvider.GetAllRegions();
        }

        public bool DeleteRegion(RegionMaster regionData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.DeleteRegion(regionData, out Message));
        }

        public RegionMaster GetRegionById(Int64 regionId)
        {
            return _IMasterMaintenanceProvider.GetRegionById(regionId);
        }

        public bool UpdateRegion(RegionMaster regionData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.UpdateRegion(regionData, out Message));
        }
        #endregion Region Master API

        #region State Master API
        public bool CreateNewState(CreateStateForm stateData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.CreateState(stateData, out Message));
        }

        public bool DeleteState(DeleteStateForm stateData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.DeleteState(stateData, out Message));
        }

        public bool UpdateState(UpdateStateForm stateData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.UpdateState(stateData, out Message));
        }

        public IEnumerable<StateMaster> GetAllStates()
        {
            return _IMasterMaintenanceProvider.GetAllStates();
        }

        public StateMaster GetStateById(Int64 stateId)
        {
            return _IMasterMaintenanceProvider.GetStateById(stateId);
        }
        #endregion State Master API

        #region Branch Master API
        public bool AddNewBrachMaster(CreateBranchForm list, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.AddNewBrachMaster(list, out Message));
        }

        public bool DeleteBrach(DeleteBranchForm List, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.DeleteBrach(List, out Message));
        }

        public GetBranchDetail GetAllBranchDetails()
        {
            return (_IMasterMaintenanceProvider.GetAllBranchDetails());
        }

        public bool UpdateBrachMaster(UpdateBranchForm List, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.UpdateBrachMaster(List, out Message));
        }
        #endregion Branch Master API

        #region City Master API
        public bool CreateCity(CreateCityForm cityData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.CreateCity(cityData, out Message));
        }

        public IEnumerable<CityMaster> GetAllCities()
        {
            return _IMasterMaintenanceProvider.GetAllCities();
        }

        public bool DeleteCity(DeleteCityForm cityData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.DeleteCity(cityData, out Message));
        }

        public CityMaster GetCityById(Int64 cityId)
        {
            return _IMasterMaintenanceProvider.GetCityById(cityId);
        }

        public bool UpdateCity(UpdateCityForm cityData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.UpdateCity(cityData, out Message));
        }
        #endregion City Master API

        #region Location Master API
        public bool CreateLocation(CreateLocationForm locationData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.CreateLocation(locationData, out Message));
        }

        public IEnumerable<LocationMaster> GetAllLocations()
        {
            return _IMasterMaintenanceProvider.GetAllLocations();
        }

        public bool DeleteLocation(DeleteLocationForm locationData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.DeleteLocation(locationData, out Message));
        }

        public LocationMaster GetLocationById(Int64 locationId)
        {
            return _IMasterMaintenanceProvider.GetLocationById(locationId);
        }

        public bool UpdateLocation(UpdateLocationForm locationData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.UpdateLocation(locationData, out Message));
        }
        #endregion Location Master API

        #region Product Category Master API
        public bool CreateProductCategory(CreateProductCategoryForm productCategoryData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.CreateProductCategory(productCategoryData, out Message));
        }

        public bool UpdateProductCategory(UpdateProductCategoryForm productCategoryData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.UpdateProductCategory(productCategoryData, out Message));
        }

        public bool DeleteProductCategory(DeleteProductCategoryForm productCategoryData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.DeleteProductCategory(productCategoryData, out Message));
        }

        public IEnumerable<ProductCategoryMaster> GetAllProductCategories()
        {
            return _IMasterMaintenanceProvider.GetAllProductCategories();
        }

        public ProductCategoryMaster GetProductCategoryById(Int64 productCategoryId)
        {
            return _IMasterMaintenanceProvider.GetProductCategoryById(productCategoryId);
        }

        #endregion Product Category Master API

        #region Product Sub Category Master API
        public bool CreateProductSubCategory(ProductSubCategoryMaster productSubCategoryData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.CreateProductSubCategory(productSubCategoryData, out Message));
        }

        public bool UpdateProductSubCategory(ProductSubCategoryMaster productSubCategoryData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.UpdateProductSubCategory(productSubCategoryData, out Message));
        }

        public bool DeleteProductSubCategory(ProductSubCategoryMaster productSubCategoryData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.DeleteProductSubCategory(productSubCategoryData, out Message));
        }

        public IEnumerable<ProductSubCategoryMaster> GetAllProductSubCategories()
        {
            return _IMasterMaintenanceProvider.GetAllProductSubCategories();
        }

        public ProductSubCategoryMaster GetProductSubCategoryById(Int64 productSubCategoryId)
        {
            return _IMasterMaintenanceProvider.GetProductSubCategoryById(productSubCategoryId);
        }
        #endregion Product Sub Category Master API

        #region Material Master API
        public bool CreateMaterial(MaterialMaster materialData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.CreateMaterial(materialData, out Message));
        }

        public IEnumerable<MaterialMaster> GetAllMaterials()
        {
            return _IMasterMaintenanceProvider.GetAllMaterials();
        }

        public bool DeleteMaterial(MaterialMaster materialData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.DeleteMaterial(materialData, out Message));
        }

        public MaterialMaster GetMaterialById(MaterialMaster InputParam)
        {
            return _IMasterMaintenanceProvider.GetMaterialById(InputParam);
        }

        public MaterialMaster GetMaterialByMaterialCode(MaterialMaster InputParam)
        {
            return _IMasterMaintenanceProvider.GetMaterialByMaterialCode(InputParam);
        }

        public bool UpdateMaterial(MaterialMaster materialData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.UpdateMaterial(materialData, out Message));
        }

        public MaterialCodeGet GetMaterialIdByMaterialCode(MaterialCodeGet InputParam, out string Message)
        {
            return _IMasterMaintenanceProvider.GetMaterialIdByMaterialCode(InputParam,out Message);
        }
        #endregion Material Master API

        #region Channel Master API
        public bool CreateChannel(ChannelMaster channelData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.CreateChannel(channelData, out Message));
        }

        public IEnumerable<ChannelMaster> GetAllChannels()
        {
            return _IMasterMaintenanceProvider.GetAllChannels();
        }

        public bool DeleteChannel(ChannelMaster channelData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.DeleteChannel(channelData, out Message));
        }

        public ChannelMaster GetChannelById(Int64 channelId)
        {
            return _IMasterMaintenanceProvider.GetChannelById(channelId);
        }

        public bool UpdateChannel(ChannelMaster channelData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.UpdateChannel(channelData, out Message));
        }
        #endregion Channel Master API

        #region SFA Level Master API
        public bool CreateSFALevel(SFALevelMaster SFALevelData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.CreateSFALevel(SFALevelData, out Message));
        }

        public IEnumerable<SFALevelMaster> GetAllSFALevels()
        {
            return _IMasterMaintenanceProvider.GetAllSFALevels();
        }

        public bool DeleteSFALevel(SFALevelInput SFALevelData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.DeleteSFALevel(SFALevelData, out Message));
        }

        public SFALevelMaster GetSFALevelById(Int64 SFALevelId)
        {
            return _IMasterMaintenanceProvider.GetSFALevelById(SFALevelId);
        }

        public bool UpdateSFALevel(SFALevelMaster SFALevelData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.UpdateSFALevel(SFALevelData, out Message));
        }
        #endregion SFA Level Master API

        #region SFA Sub Level Master API
        public bool CreateSFASubLevel(SFASubLevelMaster SFASubLevelData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.CreateSFASubLevel(SFASubLevelData, out Message));
        }

        public IEnumerable<SFASubLevelMaster> GetAllSFASubLevels()
        {
            return _IMasterMaintenanceProvider.GetAllSFASubLevels();
        }

        public bool DeleteSFASubLevel(SFASubLevelInput SFASubLevelData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.DeleteSFASubLevel(SFASubLevelData, out Message));
        }

        public SFASubLevelMaster GetSFASubLevelById(Int64 SFASubLevelId)
        {
            return _IMasterMaintenanceProvider.GetSFASubLevelById(SFASubLevelId);
        }

        public bool UpdateSFASubLevel(SFASubLevelMaster SFASubLevelData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.UpdateSFASubLevel(SFASubLevelData, out Message));
        }
        #endregion SFA Sub Level Master API

        #region Competitor Master API
        public bool CreateCompetitor(CompetitorMaster competitorData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.CreateCompetitor(competitorData, out Message));
        }

        public IEnumerable<CompetitorMaster> GetAllCompetitors()
        {
            return _IMasterMaintenanceProvider.GetAllCompetitors();
        }

        public bool DeleteCompetitor(CompetitorMaster competitorData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.DeleteCompetitor(competitorData, out Message));
        }

        public CompetitorMaster GetCompetitorById(Int64 competitorId)
        {
            return _IMasterMaintenanceProvider.GetCompetitorById(competitorId);
        }

        public bool UpdateCompetitor(CompetitorMaster competitorData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.UpdateCompetitor(competitorData, out Message));
        }
        #endregion Competitor Master API

        #region Competitor Product Master API
        public bool CreateCompetitorProduct(CompetitorProductMaster competitorProductData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.CreateCompetitorProduct(competitorProductData, out Message));
        }

        public IEnumerable<CompetitorProductMaster> GetAllCompetitorProducts()
        {
            return _IMasterMaintenanceProvider.GetAllCompetitorProducts();
        }

        public bool DeleteCompetitorProduct(CompetitorProductMaster competitorProductData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.DeleteCompetitorProduct(competitorProductData, out Message));
        }

        public CompetitorProductMaster GetCompetitorProductById(Int64 competitorProductId)
        {
            return _IMasterMaintenanceProvider.GetCompetitorProductById(competitorProductId);
        }

        public bool UpdateCompetitorProduct(CompetitorProductMaster competitorProductData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.UpdateCompetitorProduct(competitorProductData, out Message));
        }
        #endregion Competitor Product Master API

        #region Competitor Model Master API
        public bool CreateCompetitorModel(CompetitorModelMaster competitorModelData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.CreateCompetitorModel(competitorModelData, out Message));
        }

        public IEnumerable<CompetitorModelMaster> GetAllCompetitorModels()
        {
            return _IMasterMaintenanceProvider.GetAllCompetitorModels();
        }

        public bool DeleteCompetitorModel(CompetitorModelMaster competitorModelData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.DeleteCompetitorModel(competitorModelData, out Message));
        }

        public CompetitorModelList GetCompetitorModelById(CompetitorDataInput competitorModelId)
        {
            return _IMasterMaintenanceProvider.GetCompetitorModelById(competitorModelId);
        }

        public bool UpdateCompetitorModel(CompetitorModelMaster competitorModelData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.UpdateCompetitorModel(competitorModelData, out Message));
        }
        #endregion Competitor Model Master API

        #region Dealer Master API
        public bool CreateDealer(DealerMaster dealerData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.CreateDealer(dealerData, out Message));
        }

        public IEnumerable<DealerMaster> GetAllDealers()
        {
            return _IMasterMaintenanceProvider.GetAllDealers();
        }

        public bool DeleteDealer(DealerMaster dealerData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.DeleteDealer(dealerData, out Message));
        }

        public bool CheckPSIOutlet(DealerMaster dealerData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.CheckPSIOutlet(dealerData, out Message));
        }

        public DealerMaster GetDealerById(Int64 dealerId, string dealerCode)
        {
            return _IMasterMaintenanceProvider.GetDealerById(dealerId, dealerCode);
        }
        public PayerDetails GetDealerCode(string SAPCode)
        {
            return _IMasterMaintenanceProvider.GetDealerCode(SAPCode);
        }
        public bool UpdateDealer(DealerMaster dealerData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.UpdateDealer(dealerData, out Message));
        }
        #endregion Dealer Master API

        #region Division Master API
        public bool CreateDivision(DivisionMaster divisionData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.CreateDivision(divisionData, out Message));
        }

        public IEnumerable<DivisionMaster> GetAllDivisions()
        {
            return _IMasterMaintenanceProvider.GetAllDivisions();
        }

        public bool DeleteDivision(DivisionMaster divisionData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.DeleteDivision(divisionData, out Message));
        }

        public DivisionMaster GetDivisionById(Int64 divisionId)
        {
            return _IMasterMaintenanceProvider.GetDivisionById(divisionId);
        }

        public bool UpdateDivision(DivisionMaster divisionData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.UpdateDivision(divisionData, out Message));
        }
        #endregion Division Master API

        #region Asset Master API
        public bool CreateAsset(AssetMaster assetData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.CreateAsset(assetData, out Message));
        }

        public IEnumerable<AssetMaster> GetAllAssets()
        {
            return _IMasterMaintenanceProvider.GetAllAssets();
        }

        public bool DeleteAsset(AssetMaster assetData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.DeleteAsset(assetData, out Message));
        }

        public AssetMaster GetAssetById(Int64 assetId)
        {
            return _IMasterMaintenanceProvider.GetAssetById(assetId);
        }

        public bool UpdateAsset(AssetMaster assetData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.UpdateAsset(assetData, out Message));
        }
        #endregion Asset Master API

        #region Product Target Category Master API
        public bool CreateProductTargetCategory(ProductTargetCategoryMaster productTargetCategoryData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.CreateProductTargetCategory(productTargetCategoryData, out Message));
        }

        public IEnumerable<ProductTargetCategoryMaster> GetAllProductTargetCategories()
        {
            return _IMasterMaintenanceProvider.GetAllProductTargetCategories();
        }

        public bool DeleteProductTargetCategory(ProductTargetCategoryMaster productTargetCategoryData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.DeleteProductTargetCategory(productTargetCategoryData, out Message));
        }

        public ProductTargetCategoryMaster GetProductTargetCategoryById(Int64 productTargetCategoryId)
        {
            return _IMasterMaintenanceProvider.GetProductTargetCategoryById(productTargetCategoryId);
        }

        public bool UpdateProductTargetCategory(ProductTargetCategoryMaster productTargetCategoryData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.UpdateProductTargetCategory(productTargetCategoryData, out Message));
        }
        #endregion Product Target Category Master API

        #region Product Definition Under Target Category Master API
        public bool CreateProductDefinitionUnderTargetCategory(ProductDefinitionUnderTargetCategory productDefinitionUnderTargetCategoryData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.CreateProductDefinitionUnderTargetCategory(productDefinitionUnderTargetCategoryData, out Message));
        }

        public IEnumerable<ProductDefinitionUnderTargetCategory> GetAllProductDefinitionUnderTargetCategories()
        {
            return _IMasterMaintenanceProvider.GetAllProductDefinitionUnderTargetCategories();
        }

        public bool DeleteProductDefinitionUnderTargetCategory(ProductDefinitionUnderTargetCategory productDefinitionUnderTargetCategoryData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.DeleteProductDefinitionUnderTargetCategory(productDefinitionUnderTargetCategoryData, out Message));
        }

        public ProductDefinitionUnderTargetCategory GetProductDefinitionUnderTargetCategoryById(Int64 productDefinitionUnderTargetCategoryId)
        {
            return _IMasterMaintenanceProvider.GetProductDefinitionUnderTargetCategoryById(productDefinitionUnderTargetCategoryId);
        }

        public bool UpdateProductDefinitionUnderTargetCategory(ProductDefinitionUnderTargetCategory productDefinitionUnderTargetCategoryData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.UpdateProductDefinitionUnderTargetCategory(productDefinitionUnderTargetCategoryData, out Message));
        }
        #endregion Product Definition Under Target Category Master API

        #region SFA Salary Master API
        public bool DeleteSFASalaryMaster(SFASalaryMasterDelete sfaSalaryData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.DeleteSFASalaryMaster(sfaSalaryData, out Message));
        }

        public bool UpdateSFASalaryMaster(SFASalaryMasterGrid sfaSalaryData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.UpdateSFASalaryMaster(sfaSalaryData, out Message));
        }

        public SFASalaryMasterGrid GetSFASalaryMasterById(Int64 sfaSalaryMasterId)
        {
            return _IMasterMaintenanceProvider.GetSFASalaryMasterById(sfaSalaryMasterId);
        }

        public bool CreateSFASalaryMaster(SFASalaryMasterGrid sfaSalaryData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.CreateSFASalaryMaster(sfaSalaryData, out Message));
        }

        public IEnumerable<SFASalaryMasterGrid> SFASalaryMasterDataDownload(SFASalaryMasterDownload InputParam)
        {
            return _IMasterMaintenanceProvider.SFASalaryMasterDataDownload(InputParam);
        }

        public DataTable ManageSFASalaryMasterData(List<SFASalaryMasterGrid> InputParam)
        {
            return _IMasterMaintenanceProvider.ManageSFASalaryMasterData(InputParam);
        }
        #endregion SFA Salary Master API

        #region Target Date Setting Master API
        public bool CreateTargetDateSettingMaster(TargetDateSettingMaster targetDateSetting, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.CreateTargetDateSettingMaster(targetDateSetting, out Message));
        }

        public bool UpdateTargetDateSettingMaster(TargetDateSettingMaster targetDateSetting, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.UpdateTargetDateSettingMaster(targetDateSetting, out Message));
        }

        public TargetDateSettingMaster GetTargetDateSettingMasterById(long targetDateSettingId)
        {
            return _IMasterMaintenanceProvider.GetTargetDateSettingMasterById(targetDateSettingId);
        }
        #endregion Target Date Setting Master API

        #region Broadcast Message
        public CreateBroadcastMessageFormOutput CreateBroadcastMessage(CreateBroadcastMessageForm objBroadcastData, out string Message)
        {
            return _IMasterMaintenanceProvider.CreateBroadcastMessage(objBroadcastData, out Message);
        }

        public bool InActiveBroadcastMessage(CreateBroadcastMessageForm objBroadcastData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.InActiveBroadcastMessage(objBroadcastData, out Message));
        }
        #endregion Broadcast Message

        #region Role Rights Mapping Data Service Methods
        public bool CreateRoleRightsMapping(RoleRightsMappingMaster rolerightsmappingData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.CreateRoleRightsMapping(rolerightsmappingData, out Message));
        }

        public bool UpdateRoleRightsMapping(RoleRightsMappingMaster rolerightsmappingData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.UpdateRoleRightsMapping(rolerightsmappingData, out Message));
        }
        #endregion Role Rights Mapping Data Service Methods

        #region Feedback & Trainings
        public bool CreateFeedbackForm(CreateFeedbackForm objFormData, out string Message)
        {
            return _IMasterMaintenanceProvider.CreateFeedbackForm(objFormData, out Message);
        }
        public bool DeleteFeedbackForm(DeleteFeedbackForm objFormData, out string Message)
        {
            return _IMasterMaintenanceProvider.DeleteFeedbackForm(objFormData, out Message);
        }

        public CreateFeedbackForm ViewFeedbackFormDesign(ViewFeedbackForm objFormData, out string Message)
        {
            return _IMasterMaintenanceProvider.ViewFeedbackFormDesign(objFormData, out Message);
        }

        public bool CreateTrainingForm(CreateTrainingForm objFormData, out string Message)
        {
            return _IMasterMaintenanceProvider.CreateTrainingForm(objFormData, out Message);
        }


        public bool InActiveTrainingMessage(CreateTrainingForm objFormData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.InActiveTrainingMessage(objFormData, out Message));
        }

        #endregion Feedback & Trainings

        #region Shift Master API
        public bool CreateShiftTiming(ShiftMaster shift, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.CreateShiftTiming(shift, out Message));
        }

        public IEnumerable<ShiftMaster> GetShiftTiming()
        {
            return _IMasterMaintenanceProvider.GetShiftTiming();
        }

        public bool DeleteShift(ShiftMaster shift, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.DeleteShift(shift, out Message));
        }

        public ShiftMaster GetShiftTimingById(Int64 shiftNameId)
        {
            return _IMasterMaintenanceProvider.GetShiftTimingById(shiftNameId);
        }

        public bool UpdateShiftTiming(ShiftMaster shift, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.UpdateShiftTiming(shift, out Message));
        }
        #endregion Shift Master API

        public bool UpdateMonthlyReport(UpdateMonthlyReport objFormData, out string Message)
        {
            return Convert.ToBoolean(_IMasterMaintenanceProvider.UpdateMonthlyReport(objFormData, out Message));
        }
        
    }
}
