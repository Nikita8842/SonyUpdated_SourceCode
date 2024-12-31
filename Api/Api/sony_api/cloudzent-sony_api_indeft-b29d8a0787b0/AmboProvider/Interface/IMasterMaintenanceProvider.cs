using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboLibrary.MasterMaintenance;
using System.Data;
using AmboLibrary.WebReports;

namespace AmboProvider.Interface
{
    /// <summary>
    /// Interface contains Master Maintenace task only
    /// </summary>
    public interface IMasterMaintenanceProvider
    {
        #region Region Master API
        int CreateRegion(RegionMaster regionData, out string Message);

        int UpdateRegion(RegionMaster regionData, out string Message);

        int DeleteRegion(RegionMaster regionData, out string Message);

        IEnumerable<RegionMaster> GetAllRegions();

        RegionMaster GetRegionById(Int64 regionId);
        #endregion Region Master API

        #region State Master Provider Methods

        int CreateState(CreateStateForm stateData, out string Message);

        int UpdateState(UpdateStateForm stateData, out string Message);

        int DeleteState(DeleteStateForm stateData, out string Message);

        IEnumerable<StateMaster> GetAllStates();

        StateMaster GetStateById(Int64 stateId);

        #endregion State Master Provider Methods

        #region Branch Master Provider Methods

        int AddNewBrachMaster(CreateBranchForm List, out string Message);

        int UpdateBrachMaster(UpdateBranchForm List, out string Message);

        int DeleteBrach(DeleteBranchForm List, out string Message);

        GetBranchDetail GetAllBranchDetails();

        #endregion Branch Master Provider Methods

        #region City Master API
        int CreateCity(CreateCityForm cityData, out string Message);

        int UpdateCity(UpdateCityForm cityData, out string Message);

        int DeleteCity(DeleteCityForm cityData, out string Message);

        IEnumerable<CityMaster> GetAllCities();

        CityMaster GetCityById(Int64 cityId);
        #endregion City Master API

        #region Location Master API
        int CreateLocation(CreateLocationForm locationData, out string Message);

        int UpdateLocation(UpdateLocationForm locationData, out string Message);

        int DeleteLocation(DeleteLocationForm locationData, out string Message);

        IEnumerable<LocationMaster> GetAllLocations();

        LocationMaster GetLocationById(Int64 locationId);
        #endregion Location Master API

        #region Product Category Master API
        int CreateProductCategory(CreateProductCategoryForm productCategoryData, out string Message);

        int UpdateProductCategory(UpdateProductCategoryForm productCategoryData, out string Message);

        int DeleteProductCategory(DeleteProductCategoryForm productCategoryData, out string Message);

        IEnumerable<ProductCategoryMaster> GetAllProductCategories();

        ProductCategoryMaster GetProductCategoryById(Int64 productCategoryId);
        #endregion Product Category Master API

        #region Product Sub Category Master API
        int CreateProductSubCategory(ProductSubCategoryMaster productSubCategoryData, out string Message);

        int UpdateProductSubCategory(ProductSubCategoryMaster productSubCategoryData, out string Message);

        int DeleteProductSubCategory(ProductSubCategoryMaster productSubCategoryData, out string Message);

        IEnumerable<ProductSubCategoryMaster> GetAllProductSubCategories();

        ProductSubCategoryMaster GetProductSubCategoryById(Int64 productSubCategoryId);
        #endregion Product Sub Category Master API

        #region Material Master API
        int CreateMaterial(MaterialMaster materialData, out string Message);

        int UpdateMaterial(MaterialMaster materialData, out string Message);

        int DeleteMaterial(MaterialMaster materialData, out string Message);

        IEnumerable<MaterialMaster> GetAllMaterials();

        MaterialMaster GetMaterialById(MaterialMaster InputParam);

        MaterialMaster GetMaterialByMaterialCode(MaterialMaster InputParam);

        MaterialCodeGet GetMaterialIdByMaterialCode(MaterialCodeGet InputParam, out string Message);
        #endregion Material Master API

        #region Channel Master API
        int CreateChannel(ChannelMaster channelData, out string Message);

        int UpdateChannel(ChannelMaster channelData, out string Message);

        int DeleteChannel(ChannelMaster channelData, out string Message);

        IEnumerable<ChannelMaster> GetAllChannels();

        ChannelMaster GetChannelById(Int64 channelId);
        #endregion Channel Master API

        #region SFA Level Master API
        int CreateSFALevel(SFALevelMaster SFALevelData, out string Message);

        int UpdateSFALevel(SFALevelMaster SFALevelData, out string Message);

        int DeleteSFALevel(SFALevelInput SFALevelData, out string Message);

        IEnumerable<SFALevelMaster> GetAllSFALevels();

        SFALevelMaster GetSFALevelById(Int64 SFALevelId);
        #endregion SFA Level Master API

        #region SFA Sub Level Master API
        int CreateSFASubLevel(SFASubLevelMaster SFASubLevelData, out string Message);

        int UpdateSFASubLevel(SFASubLevelMaster SFASubLevelData, out string Message);

        int DeleteSFASubLevel(SFASubLevelInput SFASubLevelData, out string Message);

        IEnumerable<SFASubLevelMaster> GetAllSFASubLevels();

        SFASubLevelMaster GetSFASubLevelById(Int64 SFASubLevelId);
        #endregion SFA Sub Level Master API

        #region Competitor Master API
        int CreateCompetitor(CompetitorMaster competitorData, out string Message);

        int UpdateCompetitor(CompetitorMaster competitorData, out string Message);

        int DeleteCompetitor(CompetitorMaster competitorData, out string Message);

        IEnumerable<CompetitorMaster> GetAllCompetitors();

        CompetitorMaster GetCompetitorById(Int64 competitorId);
        #endregion Competitor Master API

        #region Competitor Product Master API
        int CreateCompetitorProduct(CompetitorProductMaster competitorProductData, out string Message);

        int UpdateCompetitorProduct(CompetitorProductMaster competitorProductData, out string Message);

        int DeleteCompetitorProduct(CompetitorProductMaster competitorProductData, out string Message);

        IEnumerable<CompetitorProductMaster> GetAllCompetitorProducts();

        CompetitorProductMaster GetCompetitorProductById(Int64 competitorProductId);
        #endregion Competitor Product Master API

        #region Competitor Model Master API
        int CreateCompetitorModel(CompetitorModelMaster competitorModelData, out string Message);

        int UpdateCompetitorModel(CompetitorModelMaster competitorModelData, out string Message);

        int DeleteCompetitorModel(CompetitorModelMaster competitorModelData, out string Message);

        IEnumerable<CompetitorModelMaster> GetAllCompetitorModels();

        CompetitorModelList GetCompetitorModelById(CompetitorDataInput competitorModelId);
        #endregion Competitor Model Master API

        #region Dealer Master API
        int CreateDealer(DealerMaster dealerData, out string Message);

        int UpdateDealer(DealerMaster dealerData, out string Message);

        int DeleteDealer(DealerMaster dealerData, out string Message);

        int CheckPSIOutlet(DealerMaster dealerData, out string Message);

        IEnumerable<DealerMaster> GetAllDealers();

        PayerDetails GetDealerCode(string SAPCode);

        DealerMaster GetDealerById(Int64 dealerId, string dealerCode);
        #endregion Dealer Master API

        #region Division Master API
        int CreateDivision(DivisionMaster divisionData, out string Message);

        int UpdateDivision(DivisionMaster divisionData, out string Message);

        int DeleteDivision(DivisionMaster divisionData, out string Message);

        IEnumerable<DivisionMaster> GetAllDivisions();

        DivisionMaster GetDivisionById(Int64 divisionId);
        #endregion Division Master API

        #region Asset Master API
        int CreateAsset(AssetMaster assetData, out string Message);

        int UpdateAsset(AssetMaster assetData, out string Message);

        int DeleteAsset(AssetMaster assetData, out string Message);

        IEnumerable<AssetMaster> GetAllAssets();

        AssetMaster GetAssetById(Int64 assetId);
        #endregion Asset Master API

        #region Product Target Category Master API
        int CreateProductTargetCategory(ProductTargetCategoryMaster productTargetCategoryData, out string Message);

        int UpdateProductTargetCategory(ProductTargetCategoryMaster productTargetCategoryData, out string Message);

        int DeleteProductTargetCategory(ProductTargetCategoryMaster productTargetCategoryData, out string Message);

        IEnumerable<ProductTargetCategoryMaster> GetAllProductTargetCategories();

        ProductTargetCategoryMaster GetProductTargetCategoryById(Int64 productTargetCategoryId);
        #endregion Product Target Category Master API

        #region Product Definition Under Target Category Master API
        int CreateProductDefinitionUnderTargetCategory(ProductDefinitionUnderTargetCategory productDefinitionUnderTargetCategoryData, out string Message);

        int UpdateProductDefinitionUnderTargetCategory(ProductDefinitionUnderTargetCategory productDefinitionUnderTargetCategoryData, out string Message);

        int DeleteProductDefinitionUnderTargetCategory(ProductDefinitionUnderTargetCategory productDefinitionUnderTargetCategoryData, out string Message);

        IEnumerable<ProductDefinitionUnderTargetCategory> GetAllProductDefinitionUnderTargetCategories();

        ProductDefinitionUnderTargetCategory GetProductDefinitionUnderTargetCategoryById(Int64 productDefinitionUnderTargetCategoryId);
        #endregion Product Definition Under Target Category Master API

        #region SFA Salary Master API
        int CreateSFASalaryMaster(SFASalaryMasterGrid sfaSalaryData, out string Message);

        int UpdateSFASalaryMaster(SFASalaryMasterGrid sfaSalaryData, out string Message);

        int DeleteSFASalaryMaster(SFASalaryMasterDelete sfaSalaryData, out string Message);

        SFASalaryMasterGrid GetSFASalaryMasterById(Int64 sfaSalaryMasterId);

        IEnumerable<SFASalaryMasterGrid> SFASalaryMasterDataDownload(SFASalaryMasterDownload InputParam);

        DataTable ManageSFASalaryMasterData(List<SFASalaryMasterGrid> InputParam);
        #endregion SFA Salary Master API

        #region Target Date Setting Master API
        int UpdateTargetDateSettingMaster(TargetDateSettingMaster targetDateSetting, out string Message);

        int CreateTargetDateSettingMaster(TargetDateSettingMaster targetDateSetting, out string Message);

        TargetDateSettingMaster GetTargetDateSettingMasterById(Int64 targetDateSettingId);
        #endregion Target Date Setting Master API

        #region Broadcast Message
        CreateBroadcastMessageFormOutput CreateBroadcastMessage(CreateBroadcastMessageForm objBroadcastData, out string Message);
        int InActiveBroadcastMessage(CreateBroadcastMessageForm objBroadcastData, out string Message);

        #endregion Broadcast Message

        #region Role Rights Mapping API
        int CreateRoleRightsMapping(RoleRightsMappingMaster rolerightsmappingData, out string Message);

        int UpdateRoleRightsMapping(RoleRightsMappingMaster rolerightsmappingData, out string Message);
        #endregion Role Rights Mapping API

        #region Feedback & Trainings
        bool CreateFeedbackForm(CreateFeedbackForm objFormData, out string Message);
        bool DeleteFeedbackForm(DeleteFeedbackForm objFormData, out string Message);
        CreateFeedbackForm ViewFeedbackFormDesign(ViewFeedbackForm objFormData, out string Message);

        bool CreateTrainingForm(CreateTrainingForm objFormData, out string Message);

        int InActiveTrainingMessage(CreateTrainingForm objFormData, out string Message);


        #endregion Feedback & Trainings

        #region Shift Master API
        int CreateShiftTiming(ShiftMaster shift, out string Message);

        int UpdateShiftTiming(ShiftMaster shift, out string Message);

        int DeleteShift(ShiftMaster shift, out string Message);

        IEnumerable<ShiftMaster> GetShiftTiming();

        ShiftMaster GetShiftTimingById(Int64 shiftNameId);
        #endregion Shift Master API

        int UpdateMonthlyReport(UpdateMonthlyReport objFormData, out string Message);


        

    }
}
