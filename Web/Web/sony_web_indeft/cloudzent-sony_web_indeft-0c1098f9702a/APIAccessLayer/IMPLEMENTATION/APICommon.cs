using AMBOModels.MasterMaintenance;
using AMBOModels.GlobalAccessible;
using APIAccessLayer.INTERFACE;
using APIAccessLayer.Helper;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using AMBOModels.UserManagement;
using AMBOModels.Modules;
using AMBOModels.Mappings;
using AMBOModels.Abstract;
using AMBOModels.IncentiveManagement;

namespace APIAccessLayer.IMPLEMENTATION
{
    public class APICommon : IAPICommon
    {

        public List<RegionMaster> GetRegion()
        {

            APIClient<DBNull> client = new APIClient<DBNull>();

            try
            {
                string apiUrl = "api/Common/GetRegion";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<RegionMaster>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public List<ChannelMaster> GetChannels()
        {
            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetChannels";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<ChannelMaster>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<StateMaster> GetStates()
        {
            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetStates";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<StateMaster>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public List<CityData> GetAllCities()
        {
            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetAllCities";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<CityData>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public List<LocationData> GetAllLocations()
        {
            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetAllLocations";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<LocationData>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public List<StateMaster> GetStateByRegion(Common InputParam)
        {
            Envelope<Common> PostData = new Envelope<Common>();
            PostData.Data = InputParam;
            APIClient<Envelope<Common>> client = new APIClient<Envelope<Common>>();

            try
            {
                string apiUrl = "api/Common/GetStatesByRegion";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<StateMaster>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public List<ProductSubCategoryMaster> GetProductSubCategoryByCategoryId(Common InputParam)
        {
            Envelope<Common> PostData = new Envelope<Common>();
            PostData.Data = InputParam;
            APIClient<Envelope<Common>> client = new APIClient<Envelope<Common>>();
            try
            {
                string apiUrl = "api/Common/GetProductSubCategoryByCategoryId";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<ProductSubCategoryMaster>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public List<CityMaster> GetCityByState(Common InputParam)
        {
            Envelope<Common> PostData = new Envelope<Common>();
            PostData.Data = InputParam;
            APIClient<Envelope<Common>> client = new APIClient<Envelope<Common>>();

            try
            {
                string apiUrl = "api/Common/GetCityByState";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<CityMaster>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<LocationMaster> GetLocationByCity(Common InputParam)
        {
            Envelope<Common> PostData = new Envelope<Common>();
            PostData.Data = InputParam;
            APIClient<Envelope<Common>> client = new APIClient<Envelope<Common>>();

            try
            {
                string apiUrl = "api/Common/GetLocationByCity";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<LocationMaster>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<DealerMaster> GetDealersByLocation(Common InputParam)
        {
            Envelope<Common> PostData = new Envelope<Common>();
            PostData.Data = InputParam;
            APIClient<Envelope<Common>> client = new APIClient<Envelope<Common>>();
            try
            {
                string apiUrl = "api/Common/GetDealersByLocation";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<DealerMaster>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<DealerMaster> GetDealersByLocationNonSFA(Common InputParam)
        {
            Envelope<Common> PostData = new Envelope<Common>();
            PostData.Data = InputParam;
            APIClient<Envelope<Common>> client = new APIClient<Envelope<Common>>();
            try
            {
                string apiUrl = "api/Common/GetDealersByLocationNonSFA";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<DealerMaster>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<DealerMaster> GetDealersByBranch(Common InputParam)
        {
            Envelope<Common> PostData = new Envelope<Common>();
            PostData.Data = InputParam;
            APIClient<Envelope<Common>> client = new APIClient<Envelope<Common>>();
            try
            {
                string apiUrl = "api/Common/GetDealersByBranch";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<DealerMaster>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Envelope<List<SearchSFAOutput>> GetAllActiveSFA()
        {

            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetAllActiveSFA";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<SearchSFAOutput>>>(apiOutput.Result);
                return output;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Envelope<List<DealerList>> GetAllActiveDealers()
        {

            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetAllActiveDealers";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<DealerList>>>(apiOutput.Result);
                return output;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Envelope<List<DealerList>> GetAllActiveDealersNonSFA()
        {

            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetAllActiveDealersNonSFA";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<DealerList>>>(apiOutput.Result);
                return output;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Envelope<List<BrandList>> GetActiveBrands()
        {
            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetActiveBrands";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<BrandList>>>(apiOutput.Result);
                return output;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Envelope<List<FeedbackForm>> GetActiveFeedbackForms()
        {
            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetActiveFeedbackForms";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<FeedbackForm>>>(apiOutput.Result);
                return output;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<DealerMasterCode> GetNonSFADealerMasterCodesByBranch(Common InputParam)
        {
            Envelope<Common> PostData = new Envelope<Common>();
            PostData.Data = InputParam;
            APIClient<Envelope<Common>> client = new APIClient<Envelope<Common>>();
            try
            {
                string apiUrl = "api/Common/GetNonSFADealerMasterCodesByBranch";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<DealerMasterCode>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<NonSFADealer> GetNonSFADealersByMasterCodes(NonSFAMasterCodeList objInput)
        {
            Envelope<NonSFAMasterCodeList> PostData = new Envelope<NonSFAMasterCodeList>();
            PostData.Data = objInput;
            APIClient<Envelope<NonSFAMasterCodeList>> client = new APIClient<Envelope<NonSFAMasterCodeList>>();
            try
            {
                string apiUrl = "api/Common/GetNonSFADealersByMasterCodes";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<NonSFADealer>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<SFAFormData> GetSFAByDealer(Common InputParam)
        {
            Envelope<Common> PostData = new Envelope<Common>();
            PostData.Data = InputParam;
            APIClient<Envelope<Common>> client = new APIClient<Envelope<Common>>();
            try
            {
                string apiUrl = "api/Common/GetSFAByDealer";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<SFAFormData>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public List<SFADropdown> GetSFADropdown()
        {
            APIClient<Envelope<Common>> client = new APIClient<Envelope<Common>>();
            try
            {
                string apiUrl = "api/Common/GetSFADropdown";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<SFADropdown>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<SFAFormData> GetSFAByBranch(Common InputParam)
        {
            Envelope<Common> PostData = new Envelope<Common>();
            PostData.Data = InputParam;
            APIClient<Envelope<Common>> client = new APIClient<Envelope<Common>>();
            try
            {
                string apiUrl = "api/Common/GetSFAByBranch";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<SFAFormData>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<SearchSFAOutput> GetSFAFromDivisionAndBranch(SearchSFA InputParam)
        {
            Envelope<SearchSFA> PostData = new Envelope<SearchSFA>();
            PostData.Data = InputParam;
            APIClient<Envelope<SearchSFA>> client = new APIClient<Envelope<SearchSFA>>();
            try
            {
                string apiUrl = "api/Common/GetSFAFromDivisionAndBranch";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<SearchSFAOutput>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<SearchSIDOutput> GetSIDFromBranchForBroadcast(SearchSID InputParam)
        {
            Envelope<SearchSID> PostData = new Envelope<SearchSID>();
            PostData.Data = InputParam;
            APIClient<Envelope<SearchSID>> client = new APIClient<Envelope<SearchSID>>();
            try
            {
                string apiUrl = "api/Common/GetSIDFromBranchForBroadcast";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<SearchSIDOutput>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<EmployeeFormData> GetAllActiveUsersByRole(Common InputParam)
        {
            Envelope<Common> PostData = new Envelope<Common>();
            PostData.Data = InputParam;
            APIClient<Envelope<Common>> client = new APIClient<Envelope<Common>>();
            try
            {
                string apiUrl = "api/Common/GetAllActiveUsersByRole";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<EmployeeFormData>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<SalesPIC> GetSalesPICByBranch(Common InputParam)
        {
            Envelope<Common> PostData = new Envelope<Common>();
            PostData.Data = InputParam;
            APIClient<Envelope<Common>> client = new APIClient<Envelope<Common>>();
            try
            {
                string apiUrl = "api/Common/GetSalesPICByBranch";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<SalesPIC>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<SFAFormData> GetAllActiveRDI()
        {
            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetAllActiveRDI";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<SFAFormData>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public Envelope<List<SFALevelData>> GetActiveSFALevels()
        {
            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetActiveSFALevels";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<SFALevelData>>>(apiOutput.Result);
                return output;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public List<SFAFormData> GetUnmappedSFAByBranch(Common InputParam)
        {
            Envelope<Common> PostData = new Envelope<Common>();
            PostData.Data = InputParam;
            APIClient<Envelope<Common>> client = new APIClient<Envelope<Common>>();
            try
            {
                string apiUrl = "api/Common/GetUnmappedSFAByBranch";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<SFAFormData>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<MasterMaintenance> GetBranch()
        {
            APIClient<DBNull> client = new APIClient<DBNull>();

            try
            {
                string apiUrl = "api/MasterMaintenance/GetAllBranch";//port it to common
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<GetBranchDetail>>(apiOutput.Result);
                return output.Data.List;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public List<ChannelMaster> GetChannel()
        {
            APIClient<DBNull> client = new APIClient<DBNull>();

            try
            {
                string apiUrl = "api/MasterMaintenance/GetAllChannels";//port it to common
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<ChannelMaster>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<DivisionMaster> GetDivisions()
        {
            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetDivisions";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<DivisionMaster>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<ProductCategoryMaster> GetProductCategories()
        {
            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetProductCategories";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<ProductCategoryMaster>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<ProductTargetCategoryMaster> GetProductTargetCategories()
        {
            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetProductTargetCategories";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<ProductTargetCategoryMaster>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Envelope<List<ProductTargetCategoryData>> GetTargetCategoriesByProductCategories(ProductTargetCategorySearch objSearchData)
        {
            Envelope<ProductTargetCategorySearch> PostData = new Envelope<ProductTargetCategorySearch>();
            PostData.Data = objSearchData;
            APIClient<Envelope<ProductTargetCategorySearch>> client = new APIClient<Envelope<ProductTargetCategorySearch>>();

            try
            {
                string apiUrl = "api/Common/GetTargetCategoriesByProductCategories";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<ProductTargetCategoryData>>>(apiOutput.Result);
                return output;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<TargetTypeMaster> GetTargetTypes()
        {
            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetTargetTypes";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<TargetTypeMaster>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<ProductCategoryMaster> GetUnmappedProdCatsForSFA(Common InputParam)
        {
            Envelope<Common> PostData = new Envelope<Common>();
            PostData.Data = InputParam;
            APIClient<Envelope<Common>> client = new APIClient<Envelope<Common>>();
            try
            {
                string apiUrl = "api/Common/GetUnmappedProdCatsForSFA";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<ProductCategoryMaster>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<ProductCategoryGroupDD> GetProductCategoryForGroupMapping(Common InputParam)
        {
            Envelope<Common> PostData = new Envelope<Common>();
            PostData.Data = InputParam;
            APIClient<Envelope<Common>> client = new APIClient<Envelope<Common>>();
            try
            {
                string apiUrl = "api/Common/GetProductCategoryForGroupMapping";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<ProductCategoryGroupDD>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<RoleMaster> GetRoles()
        {
            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetRole";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<RoleMaster>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<ClassificationTypeMaster> GetDealerClassificationTypes()
        {
            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetDealerClassificationTypes";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<ClassificationTypeMaster>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<DisplayOrder> GetDisplayOrder()
        {
            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetDisplayOrder";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<DisplayOrder>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public List<AgencyMaster> GetAgency(AgencyDropdownInput InputData)
        {
            Envelope<AgencyDropdownInput> PostData = new Envelope<AgencyDropdownInput>();
            PostData.Data = InputData;
            APIClient<Envelope<AgencyDropdownInput>> client = new APIClient<Envelope<AgencyDropdownInput>>();
            try
            {
                string apiUrl = "api/Common/GetAgency";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<AgencyMaster>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<SFASubLevelMaster> GetSFALevels()
        {
            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetSFALevels";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<SFASubLevelMaster>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public CityTypeList GetCityType()
        {
            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetCityType";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<CityTypeList>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public List<DivisionMaster> GetDivisionForProductCategory()
        {
            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetDivisionForProductCategory";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<DivisionMaster>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<ProductCategoryMaster> GetProductCategoryByDivision(Common InputParam)
        {
            Envelope<Common> PostData = new Envelope<Common>();
            PostData.Data = InputParam;
            APIClient<Envelope<Common>> client = new APIClient<Envelope<Common>>();

            try
            {
                string apiUrl = "api/Common/GetProductCategoryByDivision";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<ProductCategoryMaster>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public List<Size> GetSize(AttributeGet InputParam)
        {
            Envelope<AttributeGet> PostData = new Envelope<AttributeGet>();
            PostData.Data = InputParam;
            APIClient<Envelope<AttributeGet>> client = new APIClient<Envelope<AttributeGet>>();

            try
            {
                string apiUrl = "api/Common/GetSize";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<Size>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Segment> GetSegment(AttributeGet InputParam)
        {
            Envelope<AttributeGet> PostData = new Envelope<AttributeGet>();
            PostData.Data = InputParam;
            APIClient<Envelope<AttributeGet>> client = new APIClient<Envelope<AttributeGet>>();
            try
            {
                string apiUrl = "api/Common/GetSegment";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<Segment>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Resolution> GetResolution(AttributeGet InputParam)
        {
            Envelope<AttributeGet> PostData = new Envelope<AttributeGet>();
            PostData.Data = InputParam;
            APIClient<Envelope<AttributeGet>> client = new APIClient<Envelope<AttributeGet>>();
            try
            {
                string apiUrl = "api/Common/GetResolution";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<Resolution>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Internet> GetInternet(AttributeGet InputParam)
        {
            Envelope<AttributeGet> PostData = new Envelope<AttributeGet>();
            PostData.Data = InputParam;
            APIClient<Envelope<AttributeGet>> client = new APIClient<Envelope<AttributeGet>>();
            try
            {
                string apiUrl = "api/Common/GetInternet";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<Internet>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<TVType> GetTVType(AttributeGet InputParam)
        {
            Envelope<AttributeGet> PostData = new Envelope<AttributeGet>();
            PostData.Data = InputParam;
            APIClient<Envelope<AttributeGet>> client = new APIClient<Envelope<AttributeGet>>();
            try
            {
                string apiUrl = "api/Common/GetTVType";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<TVType>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<D3> Get3D(AttributeGet InputParam)
        {
            Envelope<AttributeGet> PostData = new Envelope<AttributeGet>();
            PostData.Data = InputParam;
            APIClient<Envelope<AttributeGet>> client = new APIClient<Envelope<AttributeGet>>();
            try
            {
                string apiUrl = "api/Common/GetD3";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<D3>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int ValidateMaterialCode(Common InputParam, out string result)
        {
            Envelope<Common> PostData = new Envelope<Common>();
            PostData.Data = InputParam;
            int isAvailable = 0;
            string message = string.Empty;
            APIClient<Envelope<Common>> client = new APIClient<Envelope<Common>>();

            try
            {
                string apiUrl = "api/Common/ValidateMaterialCode";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
                isAvailable = Convert.ToInt32(output.Data);
                message = output.Message;
                result = message;
                return isAvailable;
            }
            catch (Exception ex)
            {
                result = message;
                return isAvailable;
            }

        }

        /// <summary>
        /// To get list of Competitors.
        /// </summary>
        /// <returns>List of Competitors.</returns>
        public List<Competitors> GetCompetitors()
        {
            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetCompetitors";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<Competitors>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// To get Competitor's Product Categories.
        /// </summary>
        /// <param name="CompetitorId">Competitor ID</param>
        /// <returns>Competitor's Product Categories.</returns>
        public List<CompetitorProducts> GetCompetitorProducts(CompetitorProductsInput CompetitorId)
        {
            List<CompetitorProducts> Data = null;
            Envelope<CompetitorProductsInput> PostData = new Envelope<CompetitorProductsInput>();
            PostData.Data = CompetitorId;
            APIClient<Envelope<CompetitorProductsInput>> client = new APIClient<Envelope<CompetitorProductsInput>>();

            try
            {
                string apiUrl = "api/Common/GetCompetitorProducts";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<CompetitorProducts>>>(apiOutput.Result);
                Data = output.Data;
            }
            catch (Exception ex)
            {
                
            }
            return Data;
        }

        /// <summary>
        /// To get list of Materials(Models).
        /// </summary>
        /// <returns>Materials(Models)</returns>
        public List<Materials> GetMaterialDataforDD()
        {
            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetMaterialDataforDD";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<Materials>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Materials> GetMaterialDataforDDByProSub(MaterialDDInput Input)
        {
            Envelope<MaterialDDInput> PostData = new Envelope<MaterialDDInput>();
            PostData.Data = Input;
            APIClient<Envelope<MaterialDDInput>> client = new APIClient<Envelope<MaterialDDInput>>();
            try
            {
                string apiUrl = "api/Common/GetMaterialDataforDDByProSub";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<Materials>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// To fetch Sony Product Category and Sub Category data based on Competitor Category Id.
        /// </summary>
        /// <param name="Id">Competitor Category Id.</param>
        /// <returns>Sony Product Category and Sub Category data</returns>
        public CompetitorData GetSonyProducts(CompetitorDataInput Id)
        {
            Envelope<CompetitorDataInput> PostData = new Envelope<CompetitorDataInput>();
            PostData.Data = Id;
            CompetitorData CompetitorData = new CompetitorData();
            APIClient<Envelope<CompetitorDataInput>> client = new APIClient<Envelope<CompetitorDataInput>>();

            try
            {
                string apiUrl = "api/Common/GetSonyProducts";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<CompetitorData>>(apiOutput.Result);
                CompetitorData = output.Data;
                return CompetitorData;
            }
            catch (Exception ex)
            {
                CompetitorData = new CompetitorData();
            }
            return CompetitorData;
        }

        public Envelope<bool> ValidateSAPCode(Common InputParam)
        {
            Envelope<Common> PostData = new Envelope<Common>();
            Envelope<bool> output = new Envelope<bool>();
            output = null;
            PostData.Data = InputParam;
            string message = string.Empty;
            APIClient<Envelope<Common>> client = new APIClient<Envelope<Common>>();

            try
            {
                string apiUrl = "api/Common/ValidateSAPCode";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
                
            }
            catch (Exception ex)
            {
                
            }

            return output;

        }
		
		public List<SFAType> GetSFAType()
        {
            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetSFAType";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<SFAType>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<AssetAssignmentToRDIGet> GetReferenceId()
        {
            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetReferenceId";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<AssetAssignmentToRDIGet>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<string> GetMaterialMasterCodeList()
        {
            APIClient<Envelope<string>> client = new APIClient<Envelope<string>>();

            try
            {
                string apiUrl = "api/Common/GetMaterialMasterCodeList";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<string>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<string> GetDealerCodeList()
        {
            APIClient<Envelope<string>> client = new APIClient<Envelope<string>>();

            try
            {
                string apiUrl = "api/Common/GetDealerCodeList";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<string>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<LevelOfEducation> GetLevelOfEducation()
        {
            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetLevelOfEducation";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<LevelOfEducation>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Source> GetSource()
        {
            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetSource";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<Source>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Gender> GetGender()
        {
            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetGender";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<Gender>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<OutletType> GetOutletType()
        {
            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetOutletType";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<OutletType>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<IncentiveCategoryMaster> GetAllIncentiveCategory()
        {
            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetAllIncentiveCategory";//checked
                var apiOutput = client.Get(apiUrl);
                var output = JsonConvert.DeserializeObject<Envelope<List<IncentiveCategoryMaster>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<ModuleMaster> GetAllModules()
        {
            List<ModuleMaster> output = new List<ModuleMaster>();
            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetAllModules";//checked
                var apiOutput = client.Get(apiUrl);
                var data = JsonConvert.DeserializeObject<Envelope<List<ModuleMaster>>>(apiOutput.Result);
                output = data.Data;
            }
            catch (Exception ex)
            {
                output = null;
            }
            return output;
        }

        public List<SubModuleMaster> GetSubModulesByModuleId(SubModuleMasterGet InputParam)
        {
            List<SubModuleMaster> output = new List<SubModuleMaster>();
            Envelope<SubModuleMasterGet> PostData = new Envelope<SubModuleMasterGet>();
            PostData.Data = InputParam;
            APIClient<Envelope<SubModuleMasterGet>> client = new APIClient<Envelope<SubModuleMasterGet>>();

            try
            {
                string apiUrl = "api/Common/GetSubModulesByModuleId";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var data = JsonConvert.DeserializeObject<Envelope<List<SubModuleMaster>>>(apiOutput.Result);
                output = data.Data;
            }
            catch (Exception ex)
            {
                output = null;
            }
            return output;
        }

        public List<FestivalIncentiveScheme> GetAllFestivalScheme(FestivalIncentiveSchemeParam InputParam)
        {
            List<FestivalIncentiveScheme> output = new List<FestivalIncentiveScheme>();
            Envelope<FestivalIncentiveSchemeParam> PostData = new Envelope<FestivalIncentiveSchemeParam>();
            PostData.Data = InputParam;
            APIClient<Envelope<FestivalIncentiveSchemeParam>> client = new APIClient<Envelope<FestivalIncentiveSchemeParam>>();
            try
            {
                string apiUrl = "api/Common/GetAllFestivalScheme";//checked
                var apiOutput = client.Post(apiUrl,PostData);
                var data = JsonConvert.DeserializeObject<Envelope<List<FestivalIncentiveScheme>>>(apiOutput.Result);
                output = data.Data;
            }
            catch (Exception ex)
            {
                output = null;
            }
            return output;
        }

        /// <summary>
        /// To get all Attendance Type Data.
        /// Dhruv Sharma, ValueFirst, Gurgaon
        /// </summary>
        /// <returns>Attendance Type List</returns>
        public List<AttendanceType> GetAllAttendanceType()
        {
            List<AttendanceType> output = new List<AttendanceType>();
            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetAllAttendanceType";//checked
                var apiOutput = client.Get(apiUrl);
                var data = JsonConvert.DeserializeObject<Envelope<List<AttendanceType>>>(apiOutput.Result);
                output = data.Data;
            }
            catch (Exception ex)
            {
                output = null;
            }
            return output;
        }

        /// <summary>
        /// To get all deviation reasons .
        /// Nikhil Thakral, ValueFirst, Gurgaon
        /// </summary>
        /// <returns>DeviationReasons List</returns>
        public List<DeviationReasons> GetAllDeviationReasons()
        {
            List<DeviationReasons> output = new List<DeviationReasons>();
            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetAllDeviationReasons";//checked
                var apiOutput = client.Get(apiUrl);
                var data = JsonConvert.DeserializeObject<Envelope<List<DeviationReasons>>>(apiOutput.Result);
                output = data.Data;
            }
            catch (Exception ex)
            {
                output = null;
            }
            return output;
        }
		
		public List<ProductCategoryGroupMaster> GetProductCategoryGroupDropDown()
        {
            List<ProductCategoryGroupMaster> output = new List<ProductCategoryGroupMaster>();
            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetProductCategoryGroupDropDown";//checked
                var apiOutput = client.Get(apiUrl);
                var data = JsonConvert.DeserializeObject<Envelope<List<ProductCategoryGroupMaster>>>(apiOutput.Result);
                output = data.Data;
            }
            catch (Exception ex)
            {
                output = null;
            }
            return output;
        }

        public GetBranch GetBranchByUserId(GetBranch InputParam)
        {
            GetBranch output = new GetBranch();
            Envelope<GetBranch> PostData = new Envelope<GetBranch>();
            PostData.Data = InputParam;
            APIClient<Envelope<GetBranch>> client = new APIClient<Envelope<GetBranch>>();

            try
            {
                string apiUrl = "api/Common/GetBranchByUserId";
                var apiOutput = client.Post(apiUrl, PostData);
                var data = JsonConvert.DeserializeObject<Envelope<GetBranch>>(apiOutput.Result);
                output = data.Data;
            }
            catch (Exception ex)
            {
                output = null;
            }
            return output;
        }

        public List<TrainingDropdown> GetTrainings(GetTrainingDropdown InputParam)
        {
            Envelope<GetTrainingDropdown> PostData = new Envelope<GetTrainingDropdown>();
            PostData.Data = InputParam;
            APIClient<Envelope<GetTrainingDropdown>> client = new APIClient<Envelope<GetTrainingDropdown>>();
            try
            {
                string apiUrl = "api/Common/GetTrainings";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<TrainingDropdown>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<SearchSFAOutput> GetSFAFromDivisionAndBranchAndRole(SearchSFA InputParam)
        {
            Envelope<SearchSFA> PostData = new Envelope<SearchSFA>();
            PostData.Data = InputParam;
            APIClient<Envelope<SearchSFA>> client = new APIClient<Envelope<SearchSFA>>();
            try
            {
                string apiUrl = "api/Common/GetSFAFromDivisionAndBranchAndRole";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<SearchSFAOutput>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<SearchSIDOutput> GetSIDFromBranchAndRoleForBroadcast(SearchSID InputParam)
        {
            Envelope<SearchSID> PostData = new Envelope<SearchSID>();
            PostData.Data = InputParam;
            APIClient<Envelope<SearchSID>> client = new APIClient<Envelope<SearchSID>>();
            try
            {
                string apiUrl = "api/Common/GetSIDFromBranchAndRoleForBroadcast";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<SearchSIDOutput>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void CreateErrorLogWeb(ErrorDetailsInput ErrorDetailsInput)
        {
            Envelope<ErrorDetailsInput> PostData = new Envelope<ErrorDetailsInput>();
            PostData.Data = ErrorDetailsInput;
            APIClient<Envelope<ErrorDetailsInput>> client = new APIClient<Envelope<ErrorDetailsInput>>();
            string apiUrl = "api/ErrorLog/CreateErrorLogWeb";//checked
            var apiOutput = client.Post(apiUrl, PostData);
        }

        public List<SubCatgeory> GetSubCategoryforMultipleProducts(SubCatgeoryByProduct InputParam)
        {
            Envelope<SubCatgeoryByProduct> PostData = new Envelope<SubCatgeoryByProduct>();
            PostData.Data = InputParam;
            APIClient<Envelope<SubCatgeoryByProduct>> client = new APIClient<Envelope<SubCatgeoryByProduct>>();
            try
            {
                string apiUrl = "api/Common/GetSubCategoryforMultipleProducts";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<SubCatgeory>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Branchfohr GetBranchMappedForHR(GetBranch InputParam)
        {
            Envelope<GetBranch> PostData = new Envelope<GetBranch>();
            PostData.Data = InputParam;
            APIClient<Envelope<GetBranch>> client = new APIClient<Envelope<GetBranch>>();

            try
            {
                string apiUrl = "api/Common/GetBranchMappedForHR";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<Branchfohr>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return new Branchfohr();
            }
        }

        public List<ShiftTimingName> GetShiftTiming()
        {
            List<ShiftTimingName> output = new List<ShiftTimingName>();
            APIClient<DBNull> client = new APIClient<DBNull>();
            try
            {
                string apiUrl = "api/Common/GetShiftTiming";//checked
                var apiOutput = client.Get(apiUrl);
                var data = JsonConvert.DeserializeObject<Envelope<List<ShiftTimingName>>>(apiOutput.Result);
                output = data.Data;
            }
            catch (Exception ex)
            {
                output = null;
            }
            return output;
        }
    }
}
