using AmboLibrary.MasterMaintenance;
using AmboLibrary.WebReports;
using AmboUtilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboServices.Interface
{
    public interface IMasterMaintenanceService
    {
        #region Region Master Service Methods
        Envelope<bool> CreateRegion(RegionMaster regionData);

        Envelope<IEnumerable<RegionMaster>> GetAllRegions();

        Envelope<bool> DeleteRegion(RegionMaster regionData);

        Envelope<RegionMaster> GetRegionById(Int64 regionId);

        Envelope<bool> UpdateRegion(RegionMaster regionData);
        #endregion Region Master Service Methods

        #region State Master Service Methods
        Envelope<bool> CreateState(CreateStateForm stateData);

        Envelope<IEnumerable<StateMaster>> GetAllStates();

        Envelope<bool> DeleteState(DeleteStateForm stateData);

        Envelope<StateMaster> GetStateById(Int64 stateId);

        Envelope<bool> UpdateState(UpdateStateForm stateData);
        #endregion State Master Service Methods

        #region Branch Master Service Methods
        Envelope<bool> AddNewBrachMaster(CreateBranchForm list);

        Envelope<bool> UpdateBrachMaster(UpdateBranchForm List);

        Envelope<bool> DeleteBrach(DeleteBranchForm List);

        Envelope<GetBranchDetail> GetAllBranchDetails();
        #endregion Branch Master Service Methods

        #region City Master Service Methods
        Envelope<bool> CreateCity(CreateCityForm cityData);

        Envelope<IEnumerable<CityMaster>> GetAllCities();

        Envelope<bool> DeleteCity(DeleteCityForm cityData);

        Envelope<CityMaster> GetCityById(Int64 cityId);

        Envelope<bool> UpdateCity(UpdateCityForm cityData);
        #endregion City Master Service Methods

        #region Location Master Service Methods
        Envelope<bool> CreateLocation(CreateLocationForm locationData);

        Envelope<IEnumerable<LocationMaster>> GetAllLocations();

        Envelope<bool> DeleteLocation(DeleteLocationForm locationData);

        Envelope<LocationMaster> GetLocationById(Int64 locationId);

        Envelope<bool> UpdateLocation(UpdateLocationForm locationData);
        #endregion Location Master Service Methods

        #region Product Category Master Service Methods
        Envelope<bool> CreateProductCategory(CreateProductCategoryForm productCategoryData);

        Envelope<IEnumerable<ProductCategoryMaster>> GetAllProductCategories();

        Envelope<bool> DeleteProductCategory(DeleteProductCategoryForm productCategoryData);

        Envelope<ProductCategoryMaster> GetProductCategoryById(Int64 productCategoryId);

        Envelope<bool> UpdateProductCategory(UpdateProductCategoryForm productCategoryData);
        #endregion Product Category Master Service Methods

        #region Product Sub Category Master Service Methods
        Envelope<bool> CreateProductSubCategory(ProductSubCategoryMaster productSubCategoryData);

        Envelope<IEnumerable<ProductSubCategoryMaster>> GetAllProductSubCategories();

        Envelope<bool> DeleteProductSubCategory(ProductSubCategoryMaster productSubCategoryData);

        Envelope<ProductSubCategoryMaster> GetProductSubCategoryById(Int64 productSubCategoryId);

        Envelope<bool> UpdateProductSubCategory(ProductSubCategoryMaster productSubCategoryData);
        #endregion Product Sub Category Master Service Methods

        #region Material Master Service Methods
        Envelope<bool> CreateMaterial(MaterialMaster materialData);

        Envelope<IEnumerable<MaterialMaster>> GetAllMaterials();

        Envelope<bool> DeleteMaterial(MaterialMaster materialData);

        Envelope<MaterialMaster> GetMaterialById(MaterialMaster InputParam);

        Envelope<MaterialMaster> GetMaterialByMaterialCode(MaterialMaster InputParam);

        Envelope<bool> UpdateMaterial(MaterialMaster materialData);

        Envelope<MaterialCodeGet> GetMaterialIdByMaterialCode(MaterialCodeGet InputParam);
        #endregion Material Master Service Methods

        #region Channel Master Service Methods
        Envelope<bool> CreateChannel(ChannelMaster channelData);

        Envelope<IEnumerable<ChannelMaster>> GetAllChannels();

        Envelope<bool> DeleteChannel(ChannelMaster channelData);

        Envelope<ChannelMaster> GetChannelById(Int64 channelId);

        Envelope<bool> UpdateChannel(ChannelMaster channelData);
        #endregion Channel Master Service Methods

        #region SFA Level Master Service Methods
        Envelope<bool> CreateSFALevel(SFALevelMaster SFALevelData);

        Envelope<IEnumerable<SFALevelMaster>> GetAllSFALevels();

        Envelope<bool> DeleteSFALevel(SFALevelInput SFALevelData);

        Envelope<SFALevelMaster> GetSFALevelById(Int64 SFALevelId);

        Envelope<bool> UpdateSFALevel(SFALevelMaster SFALevelData);
        #endregion SFA Level Master Service Methods

        #region SFA Sub Level Master Service Methods
        Envelope<bool> CreateSFASubLevel(SFASubLevelMaster SFASubLevelData);

        Envelope<IEnumerable<SFASubLevelMaster>> GetAllSFASubLevels();

        Envelope<bool> DeleteSFASubLevel(SFASubLevelInput SFASubLevelData);

        Envelope<SFASubLevelMaster> GetSFASubLevelById(Int64 SFASubLevelId);

        Envelope<bool> UpdateSFASubLevel(SFASubLevelMaster SFASubLevelData);
        #endregion SFA Sub Level Master Service Methods

        #region Competitor Master Service Methods
        Envelope<bool> CreateCompetitor(CompetitorMaster competitorData);

        Envelope<IEnumerable<CompetitorMaster>> GetAllCompetitors();

        Envelope<bool> DeleteCompetitor(CompetitorMaster competitorData);

        Envelope<CompetitorMaster> GetCompetitorById(Int64 competitorId);

        Envelope<bool> UpdateCompetitor(CompetitorMaster competitorData);
        #endregion Competitor Master Service Methods

        #region Competitor Product Master Service Methods
        Envelope<bool> CreateCompetitorProduct(CompetitorProductMaster competitorProductData);

        Envelope<IEnumerable<CompetitorProductMaster>> GetAllCompetitorProducts();

        Envelope<bool> DeleteCompetitorProduct(CompetitorProductMaster competitorProductData);

        Envelope<CompetitorProductMaster> GetCompetitorProductById(Int64 competitorProductId);

        Envelope<bool> UpdateCompetitorProduct(CompetitorProductMaster competitorProductData);
        #endregion Competitor Product Master Service Methods

        #region Competitor Model Master Service Methods
        Envelope<bool> CreateCompetitorModel(CompetitorModelMaster competitorModelData);

        Envelope<IEnumerable<CompetitorModelMaster>> GetAllCompetitorModels();

        Envelope<bool> DeleteCompetitorModel(CompetitorModelMaster competitorModelData);

        Envelope<CompetitorModelList> GetCompetitorModelById(CompetitorDataInput competitorModelId);

        Envelope<bool> UpdateCompetitorModel(CompetitorModelMaster competitorModelData);
        #endregion Competitor Model Master Service Methods

        #region Dealer Master Service Methods
        Envelope<bool> CreateDealer(DealerMaster dealerData);

        Envelope<IEnumerable<DealerMaster>> GetAllDealers();

        Envelope<bool> DeleteDealer(DealerMaster dealerData);

        Envelope<bool> CheckPSIOutlet(DealerMaster dealerData);

        Envelope<DealerMaster> GetDealerById(Int64 dealerId, string dealerCode);

        Envelope<bool> UpdateDealer(DealerMaster dealerData);

        Envelope<PayerDetails> GetDealerCode(string SAPCode);
        #endregion Dealer Master Service Methods

        #region Division Master Service Methods
        Envelope<bool> CreateDivision(DivisionMaster divisionData);

        Envelope<IEnumerable<DivisionMaster>> GetAllDivisions();

        Envelope<bool> DeleteDivision(DivisionMaster divisionData);

        Envelope<DivisionMaster> GetDivisionById(Int64 divisionId);

        Envelope<bool> UpdateDivision(DivisionMaster divisionData);
        #endregion Division Master Service Methods

        #region Asset Master Service Methods
        Envelope<bool> CreateAsset(AssetMaster assetData);

        Envelope<IEnumerable<AssetMaster>> GetAllAssets();

        Envelope<bool> DeleteAsset(AssetMaster assetData);

        Envelope<AssetMaster> GetAssetById(Int64 assetId);

        Envelope<bool> UpdateAsset(AssetMaster assetData);
        #endregion Asset Master Service Methods

        #region Product Target Category Master Service Methods
        Envelope<bool> CreateProductTargetCategory(ProductTargetCategoryMaster productTargetCategoryData);

        Envelope<IEnumerable<ProductTargetCategoryMaster>> GetAllProductTargetCategories();

        Envelope<bool> DeleteProductTargetCategory(ProductTargetCategoryMaster productTargetCategoryData);

        Envelope<ProductTargetCategoryMaster> GetProductTargetCategoryById(Int64 productTargetCategoryId);

        Envelope<bool> UpdateProductTargetCategory(ProductTargetCategoryMaster productTargetCategoryData);
        #endregion Product Target Category Master Service Methods

        #region Product Definition Under Target Category Master Service Methods
        Envelope<bool> CreateProductDefinitionUnderTargetCategory(ProductDefinitionUnderTargetCategory productDefinitionUnderTargetCategoryData);

        Envelope<IEnumerable<ProductDefinitionUnderTargetCategory>> GetAllProductDefinitionUnderTargetCategories();

        Envelope<bool> DeleteProductDefinitionUnderTargetCategory(ProductDefinitionUnderTargetCategory productDefinitionUnderTargetCategoryData);

        Envelope<ProductDefinitionUnderTargetCategory> GetProductDefinitionUnderTargetCategoryById(Int64 productDefinitionUnderTargetCategoryId);

        Envelope<bool> UpdateProductDefinitionUnderTargetCategory(ProductDefinitionUnderTargetCategory productDefinitionUnderTargetCategoryData);
        #endregion Product Definition Under Target Category Master Service Methods

        #region SFA Salary Master Service Methods
        Envelope<bool> CreateSFASalaryMaster(SFASalaryMasterGrid sfaSalaryData);

        Envelope<bool> DeleteSFASalaryMaster(SFASalaryMasterDelete sfaSalaryData);

        Envelope<bool> UpdateSFASalaryMaster(SFASalaryMasterGrid sfaSalaryData);

        Envelope<SFASalaryMasterGrid> GetSFASalaryMasterById(Int64 sfaSalaryMasterId);

        Envelope<IEnumerable<SFASalaryMasterGrid>> SFASalaryMasterDataDownload(SFASalaryMasterDownload InputParam);

        Envelope<DataTable> ManageSFASalaryMasterData(List<SFASalaryMasterGrid> InputParam);
        #endregion SFA Salary Master Service Methods

        #region Target Date Setting Master Service Methods
        Envelope<bool> CreateTargetDateSettingMaster(TargetDateSettingMaster targetDateSetting);

        Envelope<bool> UpdateTargetDateSettingMaster(TargetDateSettingMaster targetDateSetting);

        Envelope<TargetDateSettingMaster> GetTargetDateSettingMasterById(Int64 targetDateSettingId);
        #endregion Target Date Setting Master Service Methods

        #region Broadcast Message
        Envelope<CreateBroadcastMessageFormOutput> CreateBroadcastMessage(CreateBroadcastMessageForm objBroadcastData);
        Envelope<bool> InActiveBroadcastMessage(CreateBroadcastMessageForm objBroadcastData);

        #endregion Broadcast Message

        #region Role Rights Mapping Service Methods
        Envelope<bool> CreateRoleRightsMapping(RoleRightsMappingMaster rolerightsmappingData);

        Envelope<bool> UpdateRoleRightsMapping(RoleRightsMappingMaster rolerightsmappingData);
        #endregion Role Rights Mapping Service Methods

        #region Feedback & Trainings
        Envelope<bool> CreateFeedbackForm(CreateFeedbackForm objFormData);
        Envelope<bool> DeleteFeedbackForm(DeleteFeedbackForm objFormData);
        Envelope<CreateFeedbackForm> ViewFeedbackFormDesign(ViewFeedbackForm objFormData);

        Envelope<bool> CreateTrainingForm(CreateTrainingForm objFormData);
        Envelope<bool> InActiveTrainingMessage(CreateTrainingForm objFormData);
        Envelope<bool> UpdateMonthlyReport(UpdateMonthlyReport objFormData);



        #endregion Feedback & Trainings

        #region Shift Master Service Methods
        Envelope<bool> CreateShiftTiming(ShiftMaster shift);

        Envelope<IEnumerable<ShiftMaster>> GetShiftTiming();

        Envelope<bool> DeleteShift(ShiftMaster shift);

        Envelope<ShiftMaster> GetShiftTimingById(Int64 shiftNameId);

        Envelope<bool> UpdateShiftTiming(ShiftMaster shift);

        #endregion Shift Master Service Methods
    }
}
