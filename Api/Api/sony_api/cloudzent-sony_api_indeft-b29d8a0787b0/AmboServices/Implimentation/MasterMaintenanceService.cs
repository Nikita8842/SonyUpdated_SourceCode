using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboDataServices.Interface;
using AmboLibrary.MasterMaintenance;
using AmboServices.Interface;
using AmboUtilities;
using AmboUtilities.Helper;
using AmboLibrary.WebReports;

namespace AmboServices.Implimentation
{
    public class MasterMaintenanceService : IMasterMaintenanceService
    {
        private readonly IMasterMaintenanceDataService _IMasterMaintenanceDataService;
        public MasterMaintenanceService(IMasterMaintenanceDataService IMasterMaintenanceDataService)
        {
            _IMasterMaintenanceDataService = IMasterMaintenanceDataService;
        }

        #region Region Master API
        public Envelope<bool> CreateRegion(RegionMaster regionData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.CreateRegion(regionData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<IEnumerable<RegionMaster>> GetAllRegions()
        {
            var output = _IMasterMaintenanceDataService.GetAllRegions();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<RegionMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<RegionMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "All region's data fetched successfully." };
        }

        public Envelope<bool> DeleteRegion(RegionMaster regionData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.DeleteRegion(regionData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<RegionMaster> GetRegionById(Int64 regionId)
        {
            var output = _IMasterMaintenanceDataService.GetRegionById(regionId);
            return (output == null) ?
                new Envelope<RegionMaster> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<RegionMaster> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Region data fetched successfully." };
        }

        public Envelope<bool> UpdateRegion(RegionMaster regionData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.UpdateRegion(regionData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotModified, Message = Message };
        }
        #endregion Region Master API

        #region State Master API
        public Envelope<bool> CreateState(CreateStateForm stateData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.CreateNewState(stateData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<bool> DeleteState(DeleteStateForm stateData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.DeleteState(stateData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<bool> UpdateState(UpdateStateForm stateData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.UpdateState(stateData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotModified, Message = Message };
        }

        public Envelope<IEnumerable<StateMaster>> GetAllStates()
        {
            var output = _IMasterMaintenanceDataService.GetAllStates();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<StateMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found."} :
                new Envelope<IEnumerable<StateMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "All states data fetched successfully." };
        }

        public Envelope<StateMaster> GetStateById(Int64 stateId)
        {
            var output = _IMasterMaintenanceDataService.GetStateById(stateId);
            return (output == null) ?
                new Envelope<StateMaster> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<StateMaster> { Data = output, MessageCode = (int)Acceptable.Found, Message = "State data fetched successfully." };
        }
        #endregion State Master API

        #region Branch Master API
        public Envelope<bool> AddNewBrachMaster(CreateBranchForm list)
        {
            var Message = string.Empty;
            var IsAdded = _IMasterMaintenanceDataService.AddNewBrachMaster(list, out Message);

            return (IsAdded != false) ?
                new Envelope<bool> { Data = IsAdded, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = IsAdded, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<bool> DeleteBrach(DeleteBranchForm List)
        {
            var Message = string.Empty;
            var IsDeleted = _IMasterMaintenanceDataService.DeleteBrach(List, out Message);
            return (IsDeleted != false) ?
                new Envelope<bool> { Data = IsDeleted, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = IsDeleted, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<GetBranchDetail> GetAllBranchDetails()
        {
            var getList = _IMasterMaintenanceDataService.GetAllBranchDetails();
            return ((getList.List != null) && (getList.List.Count() >0 )) ?
                new Envelope<GetBranchDetail> {Data=getList, MessageCode=(int)Acceptable.Found, Message="Branch list is as::" } :
                new Envelope<GetBranchDetail> {Data=getList, MessageCode=(int)NotAcceptable.NotFound, Message="Brach not registered Yet" };
        }

        public Envelope<bool> UpdateBrachMaster(UpdateBranchForm List)
        {
            var Message = string.Empty;
            var IsUpdate = _IMasterMaintenanceDataService.UpdateBrachMaster(List, out Message);
            return (IsUpdate != false) ?
               new Envelope<bool> { Data = IsUpdate, MessageCode = (int)Acceptable.Created, Message = Message } :
               new Envelope<bool> { Data = IsUpdate, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }
        #endregion Branch Master API

        #region City Master API
        public Envelope<bool> CreateCity(CreateCityForm cityData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.CreateCity(cityData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<IEnumerable<CityMaster>> GetAllCities()
        {
            var output = _IMasterMaintenanceDataService.GetAllCities();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<CityMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<CityMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "City data fetched successfully." };
        }

        public Envelope<bool> DeleteCity(DeleteCityForm cityData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.DeleteCity(cityData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<CityMaster> GetCityById(Int64 cityId)
        {
            var output = _IMasterMaintenanceDataService.GetCityById(cityId);
            return (output == null) ?
                new Envelope<CityMaster> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<CityMaster> { Data = output, MessageCode = (int)Acceptable.Found, Message = "City data fetched successfully." };
        }

        public Envelope<bool> UpdateCity(UpdateCityForm cityData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.UpdateCity(cityData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotModified, Message = Message };
        }
        #endregion City Master API

        #region Location Master API
        public Envelope<bool> CreateLocation(CreateLocationForm locationData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.CreateLocation(locationData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<IEnumerable<LocationMaster>> GetAllLocations()
        {
            var output = _IMasterMaintenanceDataService.GetAllLocations();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<LocationMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<LocationMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Location data fetched successfully." };
        }

        public Envelope<bool> DeleteLocation(DeleteLocationForm locationData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.DeleteLocation(locationData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<LocationMaster> GetLocationById(Int64 locationId)
        {
            var output = _IMasterMaintenanceDataService.GetLocationById(locationId);
            return (output == null) ?
                new Envelope<LocationMaster> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<LocationMaster> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Location data fetched successfully." };
        }

        public Envelope<bool> UpdateLocation(UpdateLocationForm locationData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.UpdateLocation(locationData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotModified, Message = Message };
        }
        #endregion Location Master API

        #region Product Category Master API
        public Envelope<bool> CreateProductCategory(CreateProductCategoryForm productCategoryData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.CreateProductCategory(productCategoryData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<IEnumerable<ProductCategoryMaster>> GetAllProductCategories()
        {
            var output = _IMasterMaintenanceDataService.GetAllProductCategories();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<ProductCategoryMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<ProductCategoryMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Product category data fetched successfully." };
        }

        public Envelope<bool> DeleteProductCategory(DeleteProductCategoryForm productCategoryData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.DeleteProductCategory(productCategoryData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<ProductCategoryMaster> GetProductCategoryById(Int64 productCategoryId)
        {
            var output = _IMasterMaintenanceDataService.GetProductCategoryById(productCategoryId);
            return (output == null) ?
                new Envelope<ProductCategoryMaster> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<ProductCategoryMaster> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Product category data fetched successfully." };
        }

        public Envelope<bool> UpdateProductCategory(UpdateProductCategoryForm productCategoryData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.UpdateProductCategory(productCategoryData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotModified, Message = Message };
        }
        #endregion Product Category Master API

        #region Product Sub Category Master API
        public Envelope<bool> CreateProductSubCategory(ProductSubCategoryMaster productSubCategoryData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.CreateProductSubCategory(productSubCategoryData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<IEnumerable<ProductSubCategoryMaster>> GetAllProductSubCategories()
        {
            var output = _IMasterMaintenanceDataService.GetAllProductSubCategories();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<ProductSubCategoryMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<ProductSubCategoryMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Product sub category data fetched successfully." };
        }

        public Envelope<bool> DeleteProductSubCategory(ProductSubCategoryMaster productSubCategoryData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.DeleteProductSubCategory(productSubCategoryData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<ProductSubCategoryMaster> GetProductSubCategoryById(Int64 productSubCategoryId)
        {
            var output = _IMasterMaintenanceDataService.GetProductSubCategoryById(productSubCategoryId);
            return (output == null) ?
                new Envelope<ProductSubCategoryMaster> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<ProductSubCategoryMaster> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Product sub category data fetched successfully." };
        }

        public Envelope<bool> UpdateProductSubCategory(ProductSubCategoryMaster productSubCategoryData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.UpdateProductSubCategory(productSubCategoryData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotModified, Message = Message };
        }
        #endregion Product Sub Category Master API

        #region Material Master API
        public Envelope<bool> CreateMaterial(MaterialMaster materialData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.CreateMaterial(materialData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<IEnumerable<MaterialMaster>> GetAllMaterials()
        {
            var output = _IMasterMaintenanceDataService.GetAllMaterials();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<MaterialMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<MaterialMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Material data fetched successfully." };
        }

        public Envelope<bool> DeleteMaterial(MaterialMaster materialData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.DeleteMaterial(materialData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<MaterialMaster> GetMaterialById(MaterialMaster InputParam)
        {
            var output = _IMasterMaintenanceDataService.GetMaterialById(InputParam);
            return (output == null) ?
                new Envelope<MaterialMaster> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<MaterialMaster> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Material data fetched successfully." };
        }

        public Envelope<MaterialMaster> GetMaterialByMaterialCode(MaterialMaster InputParam)
        {
            var output = _IMasterMaintenanceDataService.GetMaterialByMaterialCode(InputParam);
            return (output == null) ?
                new Envelope<MaterialMaster> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<MaterialMaster> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Material data fetched successfully." };
        }

        public Envelope<bool> UpdateMaterial(MaterialMaster materialData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.UpdateMaterial(materialData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotModified, Message = Message };
        }

        public Envelope<MaterialCodeGet> GetMaterialIdByMaterialCode(MaterialCodeGet materialData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.GetMaterialIdByMaterialCode(materialData, out Message);
            return (output !=null ) ?
                new Envelope<MaterialCodeGet> { Data = output, MessageCode = (int)Acceptable.Found, Message = Message } :
                new Envelope<MaterialCodeGet> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = Message };
        }
        #endregion Material Master API

        #region Channel Master API
        public Envelope<bool> CreateChannel(ChannelMaster channelData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.CreateChannel(channelData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<IEnumerable<ChannelMaster>> GetAllChannels()
        {
            var output = _IMasterMaintenanceDataService.GetAllChannels();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<ChannelMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<ChannelMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Channel data fetched successfully." };
        }

        public Envelope<bool> DeleteChannel(ChannelMaster channelData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.DeleteChannel(channelData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<ChannelMaster> GetChannelById(Int64 channelId)
        {
            var output = _IMasterMaintenanceDataService.GetChannelById(channelId);
            return (output == null) ?
                new Envelope<ChannelMaster> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<ChannelMaster> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Channel data fetched successfully." };
        }

        public Envelope<bool> UpdateChannel(ChannelMaster channelData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.UpdateChannel(channelData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotModified, Message = Message };
        }
        #endregion Channel Master API

        #region SFA Level Master API
        public Envelope<bool> CreateSFALevel(SFALevelMaster SFALevelData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.CreateSFALevel(SFALevelData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<IEnumerable<SFALevelMaster>> GetAllSFALevels()
        {
            var output = _IMasterMaintenanceDataService.GetAllSFALevels();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<SFALevelMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<SFALevelMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "SFA Level data fetched successfully." };
        }

        public Envelope<bool> DeleteSFALevel(SFALevelInput SFALevelData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.DeleteSFALevel(SFALevelData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<SFALevelMaster> GetSFALevelById(Int64 SFALevelId)
        {
            var output = _IMasterMaintenanceDataService.GetSFALevelById(SFALevelId);
            return (output == null) ?
                new Envelope<SFALevelMaster> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<SFALevelMaster> { Data = output, MessageCode = (int)Acceptable.Found, Message = "SFA Level data fetched successfully." };
        }

        public Envelope<bool> UpdateSFALevel(SFALevelMaster SFALevelData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.UpdateSFALevel(SFALevelData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotModified, Message = Message };
        }
        #endregion SFA Level Master API

        #region SFA Sub Level Master API
        public Envelope<bool> CreateSFASubLevel(SFASubLevelMaster SFASubLevelData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.CreateSFASubLevel(SFASubLevelData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<IEnumerable<SFASubLevelMaster>> GetAllSFASubLevels()
        {
            var output = _IMasterMaintenanceDataService.GetAllSFASubLevels();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<SFASubLevelMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<SFASubLevelMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "SFA Sub Level data fetched successfully." };
        }

        public Envelope<bool> DeleteSFASubLevel(SFASubLevelInput SFASubLevelData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.DeleteSFASubLevel(SFASubLevelData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<SFASubLevelMaster> GetSFASubLevelById(Int64 SFASubLevelId)
        {
            var output = _IMasterMaintenanceDataService.GetSFASubLevelById(SFASubLevelId);
            return (output == null) ?
                new Envelope<SFASubLevelMaster> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<SFASubLevelMaster> { Data = output, MessageCode = (int)Acceptable.Found, Message = "SFA Sub Level data fetched successfully." };
        }

        public Envelope<bool> UpdateSFASubLevel(SFASubLevelMaster SFASubLevelData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.UpdateSFASubLevel(SFASubLevelData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotModified, Message = Message };
        }
        #endregion SFA Sub Level Master API

        #region Competitor Master API
        public Envelope<bool> CreateCompetitor(CompetitorMaster competitorData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.CreateCompetitor(competitorData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<IEnumerable<CompetitorMaster>> GetAllCompetitors()
        {
            var output = _IMasterMaintenanceDataService.GetAllCompetitors();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<CompetitorMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<CompetitorMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Competitor data fetched successfully." };
        }

        public Envelope<bool> DeleteCompetitor(CompetitorMaster CompetitorData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.DeleteCompetitor(CompetitorData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<CompetitorMaster> GetCompetitorById(Int64 CompetitorId)
        {
            var output = _IMasterMaintenanceDataService.GetCompetitorById(CompetitorId);
            return (output == null) ?
                new Envelope<CompetitorMaster> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<CompetitorMaster> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Competitor data fetched successfully." };
        }

        public Envelope<bool> UpdateCompetitor(CompetitorMaster CompetitorData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.UpdateCompetitor(CompetitorData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotModified, Message = Message };
        }
        #endregion Competitor Master API

        #region Competitor Product Master API
        public Envelope<bool> CreateCompetitorProduct(CompetitorProductMaster competitorProductData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.CreateCompetitorProduct(competitorProductData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<IEnumerable<CompetitorProductMaster>> GetAllCompetitorProducts()
        {
            var output = _IMasterMaintenanceDataService.GetAllCompetitorProducts();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<CompetitorProductMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<CompetitorProductMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Competitor Product data fetched successfully." };
        }

        public Envelope<bool> DeleteCompetitorProduct(CompetitorProductMaster CompetitorProductData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.DeleteCompetitorProduct(CompetitorProductData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<CompetitorProductMaster> GetCompetitorProductById(Int64 CompetitorProductId)
        {
            var output = _IMasterMaintenanceDataService.GetCompetitorProductById(CompetitorProductId);
            return (output == null) ?
                new Envelope<CompetitorProductMaster> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<CompetitorProductMaster> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Competitor Product data fetched successfully." };
        }

        public Envelope<bool> UpdateCompetitorProduct(CompetitorProductMaster CompetitorProductData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.UpdateCompetitorProduct(CompetitorProductData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotModified, Message = Message };
        }
        #endregion Competitor Product Master API

        #region Competitor Model Master API
        public Envelope<bool> CreateCompetitorModel(CompetitorModelMaster competitorModelData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.CreateCompetitorModel(competitorModelData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<IEnumerable<CompetitorModelMaster>> GetAllCompetitorModels()
        {
            var output = _IMasterMaintenanceDataService.GetAllCompetitorModels();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<CompetitorModelMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<CompetitorModelMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Competitor Model data fetched successfully." };
        }

        public Envelope<bool> DeleteCompetitorModel(CompetitorModelMaster CompetitorModelData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.DeleteCompetitorModel(CompetitorModelData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<CompetitorModelList> GetCompetitorModelById(CompetitorDataInput competitorModelId)
        {
            var output = _IMasterMaintenanceDataService.GetCompetitorModelById(competitorModelId);
            return (output == null) ?
                new Envelope<CompetitorModelList> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<CompetitorModelList> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Competitor Model data fetched successfully." };
        }

        public Envelope<bool> UpdateCompetitorModel(CompetitorModelMaster CompetitorModelData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.UpdateCompetitorModel(CompetitorModelData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotModified, Message = Message };
        }
        #endregion Competitor Model Master API

        #region Dealer Master API
        public Envelope<bool> CreateDealer(DealerMaster dealerData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.CreateDealer(dealerData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<IEnumerable<DealerMaster>> GetAllDealers()
        {
            var output = _IMasterMaintenanceDataService.GetAllDealers();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<DealerMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<DealerMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Dealer data fetched successfully." };
        }

        public Envelope<PayerDetails> GetDealerCode(string SAPCode)
        {
            var output = _IMasterMaintenanceDataService.GetDealerCode(SAPCode);
            return (output == null) ?
                new Envelope<PayerDetails> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No DealerCode Found." } :
                new Envelope<PayerDetails> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Dealer code fetched successfully." };
        }

        public Envelope<bool> DeleteDealer(DealerMaster dealerData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.DeleteDealer(dealerData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<bool> CheckPSIOutlet(DealerMaster dealerData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.CheckPSIOutlet(dealerData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<DealerMaster> GetDealerById(Int64 dealerId, string dealerCode)
        {
            var output = _IMasterMaintenanceDataService.GetDealerById(dealerId, dealerCode);
            return (output == null) ?
                new Envelope<DealerMaster> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<DealerMaster> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Dealer data fetched successfully." };
        }

        public Envelope<bool> UpdateDealer(DealerMaster dealerData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.UpdateDealer(dealerData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotModified, Message = Message };
        }
        #endregion Dealer Master API

        #region Division Master API
        public Envelope<bool> CreateDivision(DivisionMaster divisionData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.CreateDivision(divisionData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<IEnumerable<DivisionMaster>> GetAllDivisions()
        {
            var output = _IMasterMaintenanceDataService.GetAllDivisions();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<DivisionMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<DivisionMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Division data fetched successfully." };
        }

        public Envelope<bool> DeleteDivision(DivisionMaster divisionData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.DeleteDivision(divisionData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<DivisionMaster> GetDivisionById(Int64 divisionId)
        {
            var output = _IMasterMaintenanceDataService.GetDivisionById(divisionId);
            return (output == null) ?
                new Envelope<DivisionMaster> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<DivisionMaster> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Division data fetched successfully." };
        }

        public Envelope<bool> UpdateDivision(DivisionMaster divisionData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.UpdateDivision(divisionData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotModified, Message = Message };
        }
        #endregion Division Master API

        #region Asset Master API
        public Envelope<bool> CreateAsset(AssetMaster assetData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.CreateAsset(assetData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<IEnumerable<AssetMaster>> GetAllAssets()
        {
            var output = _IMasterMaintenanceDataService.GetAllAssets();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<AssetMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<AssetMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Asset data fetched successfully." };
        }

        public Envelope<bool> DeleteAsset(AssetMaster assetData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.DeleteAsset(assetData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<AssetMaster> GetAssetById(Int64 assetId)
        {
            var output = _IMasterMaintenanceDataService.GetAssetById(assetId);
            return (output == null) ?
                new Envelope<AssetMaster> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<AssetMaster> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Asset data fetched successfully." };
        }

        public Envelope<bool> UpdateAsset(AssetMaster assetData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.UpdateAsset(assetData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotModified, Message = Message };
        }
        #endregion Asset Master API

        #region Product Target Category Master API
        public Envelope<bool> CreateProductTargetCategory(ProductTargetCategoryMaster productTargetCategoryData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.CreateProductTargetCategory(productTargetCategoryData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<IEnumerable<ProductTargetCategoryMaster>> GetAllProductTargetCategories()
        {
            var output = _IMasterMaintenanceDataService.GetAllProductTargetCategories();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<ProductTargetCategoryMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<ProductTargetCategoryMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Product Target Category data fetched successfully." };
        }

        public Envelope<bool> DeleteProductTargetCategory(ProductTargetCategoryMaster productTargetCategoryData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.DeleteProductTargetCategory(productTargetCategoryData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<ProductTargetCategoryMaster> GetProductTargetCategoryById(Int64 productTargetCategoryId)
        {
            var output = _IMasterMaintenanceDataService.GetProductTargetCategoryById(productTargetCategoryId);
            return (output == null) ?
                new Envelope<ProductTargetCategoryMaster> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<ProductTargetCategoryMaster> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Product Target Category data fetched successfully." };
        }

        public Envelope<bool> UpdateProductTargetCategory(ProductTargetCategoryMaster productTargetCategoryData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.UpdateProductTargetCategory(productTargetCategoryData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotModified, Message = Message };
        }
        #endregion Product Target Category Master API

        #region Product Definition Under Target Category Master API
        public Envelope<bool> CreateProductDefinitionUnderTargetCategory(ProductDefinitionUnderTargetCategory productDefinitionUnderTargetCategoryData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.CreateProductDefinitionUnderTargetCategory(productDefinitionUnderTargetCategoryData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };

        }

        public Envelope<IEnumerable<ProductDefinitionUnderTargetCategory>> GetAllProductDefinitionUnderTargetCategories()
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.GetAllProductDefinitionUnderTargetCategories();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<ProductDefinitionUnderTargetCategory>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<ProductDefinitionUnderTargetCategory>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Product Definition Under Target Category data fetched successfully." };
        }

        public Envelope<bool> DeleteProductDefinitionUnderTargetCategory(ProductDefinitionUnderTargetCategory productDefinitionUnderTargetCategoryData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.DeleteProductDefinitionUnderTargetCategory(productDefinitionUnderTargetCategoryData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<ProductDefinitionUnderTargetCategory> GetProductDefinitionUnderTargetCategoryById(Int64 productDefinitionUnderTargetCategoryId)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.GetProductDefinitionUnderTargetCategoryById(productDefinitionUnderTargetCategoryId);
            return (output == null) ?
                new Envelope<ProductDefinitionUnderTargetCategory> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<ProductDefinitionUnderTargetCategory> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Product Definition Under Target Category data fetched successfully." };
        }

        public Envelope<bool> UpdateProductDefinitionUnderTargetCategory(ProductDefinitionUnderTargetCategory productDefinitionUnderTargetCategoryData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.UpdateProductDefinitionUnderTargetCategory(productDefinitionUnderTargetCategoryData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotModified, Message = Message };
        }
        #endregion Product Definition Under Target Category Master API

        #region SFA Salary Master API
        public Envelope<bool> DeleteSFASalaryMaster(SFASalaryMasterDelete sfaSalaryData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.DeleteSFASalaryMaster(sfaSalaryData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = Message };
        }

        public Envelope<bool> UpdateSFASalaryMaster(SFASalaryMasterGrid sfaSalaryData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.UpdateSFASalaryMaster(sfaSalaryData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotModified, Message = Message };
        }

        public Envelope<SFASalaryMasterGrid> GetSFASalaryMasterById(Int64 sfaSalaryMasterId)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.GetSFASalaryMasterById(sfaSalaryMasterId);
            return (output == null) ?
                new Envelope<SFASalaryMasterGrid> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<SFASalaryMasterGrid> { Data = output, MessageCode = (int)Acceptable.Found, Message = "SFA Salary Master data fetched successfully." };
        }

        public Envelope<bool> CreateSFASalaryMaster(SFASalaryMasterGrid sfaSalaryData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.CreateSFASalaryMaster(sfaSalaryData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<IEnumerable<SFASalaryMasterGrid>> SFASalaryMasterDataDownload(SFASalaryMasterDownload InputParam)
        {
            var output = _IMasterMaintenanceDataService.SFASalaryMasterDataDownload(InputParam);
            return (output == null) ?
                new Envelope<IEnumerable<SFASalaryMasterGrid>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<SFASalaryMasterGrid>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "SFA Salary Master data fetched successfully." };
        }


        public Envelope<DataTable> ManageSFASalaryMasterData(List<SFASalaryMasterGrid> InputParam)
        {
            var output = _IMasterMaintenanceDataService.ManageSFASalaryMasterData(InputParam);

            return (output != null && output.Rows.Count > 0) ?
                new Envelope<DataTable> { Data = output, MessageCode = (int)Acceptable.Created, Message = "Full/Partial data uploaded successfully." } :
                new Envelope<DataTable> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = "Error occured while uploading data." };
        }
        #endregion SFA Salary Master API

        #region Target Date Setting Master API
        public Envelope<bool> CreateTargetDateSettingMaster(TargetDateSettingMaster targetDateSetting)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.CreateTargetDateSettingMaster(targetDateSetting, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<bool> UpdateTargetDateSettingMaster(TargetDateSettingMaster targetDateSetting)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.UpdateTargetDateSettingMaster(targetDateSetting, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotModified, Message = Message };
        }

        public Envelope<TargetDateSettingMaster> GetTargetDateSettingMasterById(Int64 targetDateSettingId)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.GetTargetDateSettingMasterById(targetDateSettingId);
            return (output == null) ?
                new Envelope<TargetDateSettingMaster> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<TargetDateSettingMaster> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Target Date Setting Master data fetched successfully." };
        }
        #endregion Target Date Setting Master API

        #region Broadcast Message
        public Envelope<CreateBroadcastMessageFormOutput> CreateBroadcastMessage(CreateBroadcastMessageForm objBroadcastData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.CreateBroadcastMessage(objBroadcastData, out Message);
            return (output.MessageId == 0) ?
                new Envelope<CreateBroadcastMessageFormOutput> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message } :
                new Envelope<CreateBroadcastMessageFormOutput> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message };

        }

        public Envelope<bool> InActiveBroadcastMessage(CreateBroadcastMessageForm objBroadcastData)
        {
            var Message = string.Empty;
            var IsDeleted = _IMasterMaintenanceDataService.InActiveBroadcastMessage(objBroadcastData, out Message);
            return (IsDeleted != false) ?
                new Envelope<bool> { Data = IsDeleted, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = IsDeleted, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        #endregion Broadcast Message

        #region Role Rights Mapping Service Methods
        public Envelope<bool> CreateRoleRightsMapping(RoleRightsMappingMaster rolerightsmappingData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.CreateRoleRightsMapping(rolerightsmappingData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = Message };
        }

        public Envelope<bool> UpdateRoleRightsMapping(RoleRightsMappingMaster rolerightsmappingData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.UpdateRoleRightsMapping(rolerightsmappingData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Found, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotModified, Message = Message };
        }
        #endregion Role Rights Mapping Service Methods

        #region Feedback & Trainings
        public Envelope<bool> CreateFeedbackForm(CreateFeedbackForm objFormData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.CreateFeedbackForm(objFormData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<bool> DeleteFeedbackForm(DeleteFeedbackForm objFormData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.DeleteFeedbackForm(objFormData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<CreateFeedbackForm> ViewFeedbackFormDesign(ViewFeedbackForm objFormData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.ViewFeedbackFormDesign(objFormData, out Message);
            return (output != null) ?
                new Envelope<CreateFeedbackForm> { Data = output, MessageCode = (int)Acceptable.Found, Message = Message } :
                new Envelope<CreateFeedbackForm> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = Message };
        }

        public Envelope<bool> CreateTrainingForm(CreateTrainingForm objFormData)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.CreateTrainingForm(objFormData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }



        public Envelope<bool> InActiveTrainingMessage(CreateTrainingForm objCreateTrainingForm)
        {
            var Message = string.Empty;
            var IsDeleted = _IMasterMaintenanceDataService.InActiveTrainingMessage(objCreateTrainingForm, out Message);
            return (IsDeleted != false) ?
                new Envelope<bool> { Data = IsDeleted, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = IsDeleted, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        #endregion Feedback & Trainings


        #region Update MonthlyAttendance

        public Envelope<bool> UpdateMonthlyReport(UpdateMonthlyReport objUpdateMonthlyReport)
        {
            var Message = string.Empty;
            var IsDeleted = _IMasterMaintenanceDataService.UpdateMonthlyReport(objUpdateMonthlyReport, out Message);
            return (IsDeleted != false) ?
                new Envelope<bool> { Data = IsDeleted, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = IsDeleted, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        #endregion

        #region Shift Master API
        public Envelope<bool> CreateShiftTiming(ShiftMaster shift)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.CreateShiftTiming(shift, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<IEnumerable<ShiftMaster>> GetShiftTiming()
        {
            var output = _IMasterMaintenanceDataService.GetShiftTiming();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<ShiftMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<ShiftMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Shift data fetched successfully." };
        }

        public Envelope<bool> DeleteShift(ShiftMaster shift)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.DeleteShift(shift, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<ShiftMaster> GetShiftTimingById(Int64 shiftNameId)
        {
            var output = _IMasterMaintenanceDataService.GetShiftTimingById(shiftNameId);
            return (output == null) ?
                new Envelope<ShiftMaster> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<ShiftMaster> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Shift data fetched successfully." };
        }

        public Envelope<bool> UpdateShiftTiming(ShiftMaster shift)
        {
            var Message = string.Empty;
            var output = _IMasterMaintenanceDataService.UpdateShiftTiming(shift, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotModified, Message = Message };
        }
        #endregion Shift Master API
    }
}
