using AmboLibrary.MasterMaintenance;
using AmboLibrary.WebReports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboDataServices.Interface
{
    /// <summary>
    /// Master Maintenace DataService Ambo 
    /// </summary>
    public interface IMasterMaintenanceDataService
    {
        #region Region Master Data Service Methods
        bool CreateRegion(RegionMaster regionData, out string Message);

        IEnumerable<RegionMaster> GetAllRegions();

        bool DeleteRegion(RegionMaster regionData, out string Message);

        RegionMaster GetRegionById(Int64 regionId);

        bool UpdateRegion(RegionMaster regionData, out string Message);
        #endregion Region Master Data Service Methods

        #region State Master Data Service Methods
        bool CreateNewState(CreateStateForm stateData, out string Message);

        IEnumerable<StateMaster> GetAllStates();

        bool DeleteState(DeleteStateForm stateData, out string Message);

        StateMaster GetStateById(Int64 stateId);

        bool UpdateState(UpdateStateForm stateData, out string Message);
        #endregion State Master Data Service Methods

        #region Branch Master Data Service Methods
        bool AddNewBrachMaster(CreateBranchForm list, out string Message);

        GetBranchDetail GetAllBranchDetails();

        bool UpdateBrachMaster(UpdateBranchForm List, out string Message);

        bool DeleteBrach(DeleteBranchForm List, out string Message);
        #endregion Branch Master Data Service Methods

        #region City Master Data Service Methods
        bool CreateCity(CreateCityForm cityData, out string Message);

        IEnumerable<CityMaster> GetAllCities();

        bool DeleteCity(DeleteCityForm cityData, out string Message);

        CityMaster GetCityById(Int64 cityId);

        bool UpdateCity(UpdateCityForm cityData, out string Message);
        #endregion City Master Data Service Methods

        #region Location Master Data Service Methods
        bool CreateLocation(CreateLocationForm locationData, out string Message);

        IEnumerable<LocationMaster> GetAllLocations();

        bool DeleteLocation(DeleteLocationForm locationData, out string Message);

        LocationMaster GetLocationById(Int64 locationId);

        bool UpdateLocation(UpdateLocationForm locationData, out string Message);
        #endregion Location Master Data Service Methods

        #region Product Category Master Data Service Methods
        bool CreateProductCategory(CreateProductCategoryForm productCategoryData, out string Message);

        bool UpdateProductCategory(UpdateProductCategoryForm productCategoryData, out string Message);

        bool DeleteProductCategory(DeleteProductCategoryForm productCategoryData, out string Message);

        IEnumerable<ProductCategoryMaster> GetAllProductCategories();

        ProductCategoryMaster GetProductCategoryById(Int64 productCategoryId);
        #endregion Product Category Master Data Service Methods

        #region Product Sub Category Master Data Service Methods
        bool CreateProductSubCategory(ProductSubCategoryMaster productSubCategoryData, out string Message);

        bool UpdateProductSubCategory(ProductSubCategoryMaster productSubCategoryData, out string Message);

        bool DeleteProductSubCategory(ProductSubCategoryMaster productSubCategoryData, out string Message);

        IEnumerable<ProductSubCategoryMaster> GetAllProductSubCategories();

        ProductSubCategoryMaster GetProductSubCategoryById(Int64 productSubCategoryId);
        #endregion Product Sub Category Master Data Service Methods

        #region Material Master Data Service Methods
        bool CreateMaterial(MaterialMaster materialData, out string Message);

        IEnumerable<MaterialMaster> GetAllMaterials();

        bool DeleteMaterial(MaterialMaster materialData, out string Message);

        MaterialMaster GetMaterialById(MaterialMaster InputParam);

        MaterialMaster GetMaterialByMaterialCode(MaterialMaster InputParam);

        bool UpdateMaterial(MaterialMaster materialData, out string Message);

        MaterialCodeGet GetMaterialIdByMaterialCode(MaterialCodeGet InputParam, out string Message);
        #endregion Material Master Data Service Methods

        #region Channel Master Data Service Methods
        bool CreateChannel(ChannelMaster channelData, out string Message);

        IEnumerable<ChannelMaster> GetAllChannels();

        bool DeleteChannel(ChannelMaster channelData, out string Message);

        ChannelMaster GetChannelById(Int64 channelId);

        bool UpdateChannel(ChannelMaster channelData, out string Message);
        #endregion Channel Master Data Service Methods

        #region SFA Level Master Data Service Methods
        bool CreateSFALevel(SFALevelMaster SFALevelData, out string Message);

        IEnumerable<SFALevelMaster> GetAllSFALevels();

        bool DeleteSFALevel(SFALevelInput SFALevelData, out string Message);

        SFALevelMaster GetSFALevelById(Int64 SFALevelId);

        bool UpdateSFALevel(SFALevelMaster SFALevelData, out string Message);
        #endregion SFA Level Master Data Service Methods

        #region SFA Sub Level Master Data Service Methods
        bool CreateSFASubLevel(SFASubLevelMaster SFASubLevelData, out string Message);

        IEnumerable<SFASubLevelMaster> GetAllSFASubLevels();

        bool DeleteSFASubLevel(SFASubLevelInput SFASubLevelData, out string Message);

        SFASubLevelMaster GetSFASubLevelById(Int64 SFASubLevelId);

        bool UpdateSFASubLevel(SFASubLevelMaster SFASubLevelData, out string Message);
        #endregion SFA Sub Level Master Data Service Methods

        #region Competitor Master Data Service Methods
        bool CreateCompetitor(CompetitorMaster competitorData, out string Message);

        IEnumerable<CompetitorMaster> GetAllCompetitors();

        bool DeleteCompetitor(CompetitorMaster competitorData, out string Message);

        CompetitorMaster GetCompetitorById(Int64 competitorId);

        bool UpdateCompetitor(CompetitorMaster competitorData, out string Message);
        #endregion Competitor Master Data Service Methods

        #region Competitor Product Master Data Service Methods
        bool CreateCompetitorProduct(CompetitorProductMaster competitorProductData, out string Message);

        IEnumerable<CompetitorProductMaster> GetAllCompetitorProducts();

        bool DeleteCompetitorProduct(CompetitorProductMaster competitorProductData, out string Message);

        CompetitorProductMaster GetCompetitorProductById(Int64 competitorProductId);

        bool UpdateCompetitorProduct(CompetitorProductMaster competitorProductData, out string Message);
        #endregion Competitor Product Master Data Service Methods

        #region Competitor Model Master Data Service Methods
        bool CreateCompetitorModel(CompetitorModelMaster competitorModelData, out string Message);

        IEnumerable<CompetitorModelMaster> GetAllCompetitorModels();

        bool DeleteCompetitorModel(CompetitorModelMaster competitorModelData, out string Message);

        CompetitorModelList GetCompetitorModelById(CompetitorDataInput competitorModelId);

        bool UpdateCompetitorModel(CompetitorModelMaster competitorModelData, out string Message);
        #endregion Competitor Model Master Data Service Methods

        #region Dealer Master Data Service Methods
        bool CreateDealer(DealerMaster dealerData, out string Message);

        IEnumerable<DealerMaster> GetAllDealers();

        bool DeleteDealer(DealerMaster dealerData, out string Message);

        bool CheckPSIOutlet(DealerMaster dealerData, out string Message);

        DealerMaster GetDealerById(Int64 dealerId, string dealerCode);
        PayerDetails GetDealerCode(string SAPCode);

        bool UpdateDealer(DealerMaster dealerData, out string Message);
        #endregion Dealer Master Data Service Methods

        #region Division Master Data Service Methods
        bool CreateDivision(DivisionMaster divisionData, out string Message);

        IEnumerable<DivisionMaster> GetAllDivisions();

        bool DeleteDivision(DivisionMaster divisionData, out string Message);

        DivisionMaster GetDivisionById(Int64 divisionId);

        bool UpdateDivision(DivisionMaster divisionData, out string Message);
        #endregion Division Master Data Service Methods

        #region Asset Master Data Service Methods
        bool CreateAsset(AssetMaster assetData, out string Message);

        IEnumerable<AssetMaster> GetAllAssets();

        bool DeleteAsset(AssetMaster assetData, out string Message);

        AssetMaster GetAssetById(Int64 assetId);

        bool UpdateAsset(AssetMaster assetData, out string Message);
        #endregion Asset Master Data Service Methods

        #region Product Target Category Master Data Service Methods
        bool CreateProductTargetCategory(ProductTargetCategoryMaster productTargetCategoryData, out string Message);

        IEnumerable<ProductTargetCategoryMaster> GetAllProductTargetCategories();

        bool DeleteProductTargetCategory(ProductTargetCategoryMaster productTargetCategoryData, out string Message);

        ProductTargetCategoryMaster GetProductTargetCategoryById(Int64 productTargetCategoryId);

        bool UpdateProductTargetCategory(ProductTargetCategoryMaster productTargetCategoryData, out string Message);
        #endregion Product Target Category Master Data Service Methods

        #region Product Definition Under Target Category Master Data Service Methods
        bool CreateProductDefinitionUnderTargetCategory(ProductDefinitionUnderTargetCategory productDefinitionUnderTargetCategoryData, out string Message);

        IEnumerable<ProductDefinitionUnderTargetCategory> GetAllProductDefinitionUnderTargetCategories();

        bool DeleteProductDefinitionUnderTargetCategory(ProductDefinitionUnderTargetCategory productDefinitionUnderTargetCategoryData, out string Message);

        ProductDefinitionUnderTargetCategory GetProductDefinitionUnderTargetCategoryById(Int64 productDefinitionUnderTargetCategoryId);

        bool UpdateProductDefinitionUnderTargetCategory(ProductDefinitionUnderTargetCategory productDefinitionUnderTargetCategoryData, out string Message);
        #endregion Product Definition Under Target Category Master Data Service Methods

        #region SFA Salary Master Data Service Methods
        bool CreateSFASalaryMaster(SFASalaryMasterGrid sfaSalaryData, out string Message);

        bool UpdateSFASalaryMaster(SFASalaryMasterGrid sfaSalaryData, out string Message);

        bool DeleteSFASalaryMaster(SFASalaryMasterDelete sfaSalaryData, out string Message);

        SFASalaryMasterGrid GetSFASalaryMasterById(Int64 sfaSalaryMasterId);

        IEnumerable<SFASalaryMasterGrid> SFASalaryMasterDataDownload(SFASalaryMasterDownload InputParam);

        DataTable ManageSFASalaryMasterData(List<SFASalaryMasterGrid> InputParam);
        #endregion SFA Salary Master Data Service Methods

        #region Target Date Setting Master Data Service Methods
        bool CreateTargetDateSettingMaster(TargetDateSettingMaster targetDateSetting, out string Message);

        bool UpdateTargetDateSettingMaster(TargetDateSettingMaster targetDateSetting, out string Message);

        TargetDateSettingMaster GetTargetDateSettingMasterById(Int64 targetDateSettingId);
        #endregion Target Date Setting Master Data Service Methods

        #region Broadcast Message
        CreateBroadcastMessageFormOutput CreateBroadcastMessage(CreateBroadcastMessageForm objBroadcastData, out string Message);

        bool InActiveBroadcastMessage(CreateBroadcastMessageForm objBroadcastData, out string Message);
        #endregion Broadcast Message

        #region Role Rights Mapping Data Service Methods
        bool CreateRoleRightsMapping(RoleRightsMappingMaster rolerightsmappingData, out string Message);

        bool UpdateRoleRightsMapping(RoleRightsMappingMaster rolerightsmappingData, out string Message);
        #endregion Role Rights Mapping Data Service Methods

        #region Feedback & Trainings
        bool CreateFeedbackForm(CreateFeedbackForm objFormData, out string Message);
        bool DeleteFeedbackForm(DeleteFeedbackForm objFormData, out string Message);
        CreateFeedbackForm ViewFeedbackFormDesign(ViewFeedbackForm objFormData, out string Message);

        bool CreateTrainingForm(CreateTrainingForm objFormData, out string Message);

        bool InActiveTrainingMessage(CreateTrainingForm objFormData, out string Message);


        #endregion Feedback & Trainings


        #region Shift Master Data Service Methods

        bool CreateShiftTiming(ShiftMaster shift, out string Message);
        IEnumerable<ShiftMaster> GetShiftTiming();
        bool DeleteShift(ShiftMaster shift, out string Message);
        ShiftMaster GetShiftTimingById(Int64 shiftNameId);
        bool UpdateShiftTiming(ShiftMaster shift, out string Message);

        #endregion Shift Master Data Service Methods

        bool UpdateMonthlyReport(UpdateMonthlyReport objFormData, out string Message);

    }
}
