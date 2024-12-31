using APIAccessLayer.Helper;
using APIAccessLayer.INTERFACE;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using AMBOModels.MasterMaintenance;
using AMBOModels.UserManagement;
using AMBOModels.Mappings;
using AMBOModels.Modules;
using AMBOModels.IncentiveManagement;
using System.Data;
using AMBOModels.Reports;

namespace APIAccessLayer.IMPLEMENTATION
{
    public class APIGridData : IAPIGridData
    {
        public GridOutput<IEnumerable<RegionGridData>> GetRegionMasterGrid(GridVariables objGridData)
        {
            Envelope<GridVariables> postData = new Envelope<GridVariables>();
            Envelope<GridOutput<IEnumerable<RegionGridData>>> output;
            APIClient<Envelope<GridVariables>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<GridVariables>>();
                string apiUrl = "api/Grid/GetRegionMasterGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<RegionGridData>>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public GridOutput<IEnumerable<StateGridData>> GetStateMasterGrid(GridVariables objGridData)
        {
            Envelope<GridVariables> postData = new Envelope<GridVariables>();
            Envelope<GridOutput<IEnumerable<StateGridData>>> output;
            APIClient<Envelope<GridVariables>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<GridVariables>>();
                string apiUrl = "api/Grid/GetStateMasterGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<StateGridData>>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public GridOutput<IEnumerable<CityGridData>> GetCityMasterGrid(GridVariables objGridData)
        {
            Envelope<GridVariables> postData = new Envelope<GridVariables>();
            Envelope<GridOutput<IEnumerable<CityGridData>>> output;
            APIClient<Envelope<GridVariables>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<GridVariables>>();
                string apiUrl = "api/Grid/GetCityMasterGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<CityGridData>>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public GridOutput<IEnumerable<LocationGridData>> GetLocationMasterGrid(LocationFilter objGridData)
        {
            Envelope<LocationFilter> postData = new Envelope<LocationFilter>();
            Envelope<GridOutput<IEnumerable<LocationGridData>>> output;
            APIClient<Envelope<LocationFilter>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<LocationFilter>>();
                string apiUrl = "api/Grid/GetLocationMasterGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<LocationGridData>>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public GridOutput<IEnumerable<BranchGridData>> GetBranchMasterGrid(GridVariables objGridData)
        {
            Envelope<GridVariables> postData = new Envelope<GridVariables>();
            Envelope<GridOutput<IEnumerable<BranchGridData>>> output;
            APIClient<Envelope<GridVariables>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<GridVariables>>();
                string apiUrl = "api/Grid/GetBranchMasterGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<BranchGridData>>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public GridOutput<IEnumerable<ProductCategoryGridData>> GetProductCategoryMasterGrid(GridVariables objGridData)
        {
            Envelope<GridVariables> postData = new Envelope<GridVariables>();
            Envelope<GridOutput<IEnumerable<ProductCategoryGridData>>> output;
            APIClient<Envelope<GridVariables>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<GridVariables>>();
                string apiUrl = "api/Grid/GetProductCategoryMasterGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<ProductCategoryGridData>>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public GridOutput<IEnumerable<ProductSubCategoryGridData>> GetProductSubCategoryMasterGrid(GridVariables objGridData)
        {
            Envelope<GridVariables> postData = new Envelope<GridVariables>();
            Envelope<GridOutput<IEnumerable<ProductSubCategoryGridData>>> output;
            APIClient<Envelope<GridVariables>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<GridVariables>>();
                string apiUrl = "api/Grid/GetProductSubCategoryMasterGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<ProductSubCategoryGridData>>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public GridOutput<IEnumerable<MaterialGridData>> GetMaterialMasterGrid(MaterialFilter objGridData)
        {
            Envelope<MaterialFilter> postData = new Envelope<MaterialFilter>();
            Envelope<GridOutput<IEnumerable<MaterialGridData>>> output;
            APIClient<Envelope<MaterialFilter>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<MaterialFilter>>();
                string apiUrl = "api/Grid/GetMaterialMasterGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<MaterialGridData>>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public GridOutput<IEnumerable<DealerGridData>> GetDealerMasterGrid(DealerFilter objGridData)
        {
            Envelope<DealerFilter> postData = new Envelope<DealerFilter>();
            Envelope<GridOutput<IEnumerable<DealerGridData>>> output;
            APIClient<Envelope<DealerFilter>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<DealerFilter>>();
                string apiUrl = "api/Grid/GetDealerMasterGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<DealerGridData>>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public GridOutput<IEnumerable<DealerSFAMappingGridData>> GetDealerSFAMappingGrid(DealerSFAFilter objGridData)
        {
            Envelope<DealerSFAFilter> postData = new Envelope<DealerSFAFilter>();
            Envelope<GridOutput<IEnumerable<DealerSFAMappingGridData>>> output;
            APIClient<Envelope<DealerSFAFilter>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<DealerSFAFilter>>();
                string apiUrl = "api/Grid/GetDealerSFAMappingGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<DealerSFAMappingGridData>>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public GridOutput<IEnumerable<DealerClassificationMappingGridData>> GetDealerClassificationMappingGrid(GridVariables objGridData)
        {
            Envelope<GridVariables> postData = new Envelope<GridVariables>();
            Envelope<GridOutput<IEnumerable<DealerClassificationMappingGridData>>> output;
            APIClient<Envelope<GridVariables>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<GridVariables>>();
                string apiUrl = "api/Grid/GetDealerClassificationMappingGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<DealerClassificationMappingGridData>>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public GridOutput<List<BroadcastMessageGridData>> GetBroadcastMessageMasterGrid(GridVariables objGridData)
        {
            Envelope<GridVariables> postData = new Envelope<GridVariables>();
            Envelope<GridOutput<List<BroadcastMessageGridData>>> output;
            APIClient<Envelope<GridVariables>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<GridVariables>>();
                string apiUrl = "api/Grid/GetBroadcastMessageMasterGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<List<BroadcastMessageGridData>>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public BroadcastMessageEDITData BroadcastMessageMasterGridBYID(BMessageInputModel objBMessageInputModel)
        {
            Envelope<BMessageInputModel> postData = new Envelope<BMessageInputModel>();
            Envelope<BroadcastMessageEDITData> output;
            APIClient<Envelope<BMessageInputModel>> client;
            postData.Data = objBMessageInputModel;
            try
            {
                client = new APIClient<Envelope<BMessageInputModel>>();
                string apiUrl = "api/Grid/BroadcastMessageMasterGridBYID";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<BroadcastMessageEDITData>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public GridOutput<IEnumerable<AssetGridData>> GetAssetMasterGrid(GridVariables objGridData)
        {
            Envelope<GridVariables> postData = new Envelope<GridVariables>();
            Envelope<GridOutput<IEnumerable<AssetGridData>>> output;
            APIClient<Envelope<GridVariables>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<GridVariables>>();
                string apiUrl = "api/Grid/GetAssetMasterGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<AssetGridData>>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public GridOutput<IEnumerable<ProductTargetCategoryGridData>> GetProductTargetCategoryMasterGrid(ProductTargetCategoryGridFilters objGridData)
        {
            Envelope<ProductTargetCategoryGridFilters> postData = new Envelope<ProductTargetCategoryGridFilters>();
            Envelope<GridOutput<IEnumerable<ProductTargetCategoryGridData>>> output;
            APIClient<Envelope<ProductTargetCategoryGridFilters>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<ProductTargetCategoryGridFilters>>();
                string apiUrl = "api/Grid/GetProductTargetCategoryMasterGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<ProductTargetCategoryGridData>>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
		
		public GridOutput<IEnumerable<EmployeeGridData>> GetEmployeeMasterGrid(EmployeeGridSearchFilters objGridData)
        {
            Envelope<EmployeeGridSearchFilters> postData = new Envelope<EmployeeGridSearchFilters>();
            Envelope<GridOutput<IEnumerable<EmployeeGridData>>> output;
            APIClient<Envelope<EmployeeGridSearchFilters>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<EmployeeGridSearchFilters>>();
                string apiUrl = "api/Grid/GetEmployeeMasterGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<EmployeeGridData>>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public GridOutput<IEnumerable<EmployeeStructureMappingGridData>> GetEmployeeStructureMappingGrid(EmployeeStructureMapGridFilters objGridData)
        {
            Envelope<EmployeeStructureMapGridFilters> postData = new Envelope<EmployeeStructureMapGridFilters>();
            Envelope<GridOutput<IEnumerable<EmployeeStructureMappingGridData>>> output;
            APIClient<Envelope<EmployeeStructureMapGridFilters>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<EmployeeStructureMapGridFilters>>();
                string apiUrl = "api/Grid/GetEmployeeStructureMappingGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<EmployeeStructureMappingGridData>>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public GridOutput<IEnumerable<SalesPICOutletMappingGridData>> GetSalesPICOutletMappingGrid(GridVariables objGridData)
        {
            Envelope<GridVariables> postData = new Envelope<GridVariables>();
            Envelope<GridOutput<IEnumerable<SalesPICOutletMappingGridData>>> output;
            APIClient<Envelope<GridVariables>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<GridVariables>>();
                string apiUrl = "api/Grid/GetSalesPICOutletMappingGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<SalesPICOutletMappingGridData>>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public GridOutput<IEnumerable<UserBranchChannelPCMappingGridData>> GetUserBranchChannelPCMappingGrid(UserBranchChannelPCMappingFilter objGridData)
        {
            Envelope<UserBranchChannelPCMappingFilter> postData = new Envelope<UserBranchChannelPCMappingFilter>();
            Envelope<GridOutput<IEnumerable<UserBranchChannelPCMappingGridData>>> output;
            APIClient<Envelope<UserBranchChannelPCMappingFilter>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<UserBranchChannelPCMappingFilter>>();
                string apiUrl = "api/Grid/GetUserBranchChannelPCMappingGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<UserBranchChannelPCMappingGridData>>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
		
		public GridOutput<IEnumerable<SFAMasterforHR>> GetSFAMasterforHR(SFAMasterforHRFilter objGridData)
        {
            Envelope<SFAMasterforHRFilter> postData = new Envelope<SFAMasterforHRFilter>();
            Envelope<GridOutput<IEnumerable<SFAMasterforHR>>> output;
            APIClient<Envelope<SFAMasterforHRFilter>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<SFAMasterforHRFilter>>();
                string apiUrl = "api/Grid/GetSFAMasterforHRGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<SFAMasterforHR>>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
		
		public IEnumerable<ChannelGridData> GetChannelMasterGrid(ChannelFilter objGridData)
        {
            Envelope<ChannelFilter> postData = new Envelope<ChannelFilter>();
            Envelope<IEnumerable<ChannelGridData>> output;
            APIClient<Envelope<ChannelFilter>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<ChannelFilter>>();
                string apiUrl = "api/Grid/GetChannelMasterGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<IEnumerable<ChannelGridData>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
		
		public GridOutput<IEnumerable<SFASalaryMasterGrid>> GetSFASalaryMaster(SFASalaryMasterFilter objGridData)
        {
            Envelope<SFASalaryMasterFilter> postData = new Envelope<SFASalaryMasterFilter>();
            Envelope<GridOutput<IEnumerable<SFASalaryMasterGrid>>> output;
            APIClient<Envelope<SFASalaryMasterFilter>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<SFASalaryMasterFilter>>();
                string apiUrl = "api/Grid/GetSFASalaryMasterGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<SFASalaryMasterGrid>>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public GridOutput<IEnumerable<TargetDateSettingMaster>> GetTargetDateSettingGrid(GridVariables objGridData)
        {
            Envelope<GridVariables> postData = new Envelope<GridVariables>();
            Envelope<GridOutput<IEnumerable<TargetDateSettingMaster>>> output;
            APIClient<Envelope<GridVariables>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<GridVariables>>();
                string apiUrl = "api/Grid/GetTargetDateSettingGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<TargetDateSettingMaster>>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public GridOutput<IEnumerable<IncentiveTargetCategoryMappingGridData>> GetIncentiveTargetCategoryMappingGrid(GridVariables objGridData)
        {
            Envelope<GridVariables> postData = new Envelope<GridVariables>();
            Envelope<GridOutput<IEnumerable<IncentiveTargetCategoryMappingGridData>>> output;
            APIClient<Envelope<GridVariables>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<GridVariables>>();
                string apiUrl = "api/Grid/GetIncentiveTargetCategoryMappingGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<IncentiveTargetCategoryMappingGridData>>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public GridOutput<IEnumerable<BaseIncentiveGridData>> GetBaseIncentiveGrid(GridVariables objGridData)
        {
            Envelope<GridVariables> postData = new Envelope<GridVariables>();
            Envelope<GridOutput<IEnumerable<BaseIncentiveGridData>>> output;
            APIClient<Envelope<GridVariables>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<GridVariables>>();
                string apiUrl = "api/Grid/GetBaseIncentiveGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<BaseIncentiveGridData>>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public GridOutput<IEnumerable<PerPieceIncentiveGridData>> GetPerPieceIncentiveGrid(GridVariables objGridData)
        {
            Envelope<GridVariables> postData = new Envelope<GridVariables>();
            Envelope<GridOutput<IEnumerable<PerPieceIncentiveGridData>>> output;
            APIClient<Envelope<GridVariables>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<GridVariables>>();
                string apiUrl = "api/Grid/GetPerPieceIncentiveGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<PerPieceIncentiveGridData>>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public GridOutput<IEnumerable<FestivalIncentiveGridData>> GetFestivalIncentiveGrid(GridVariables objGridData)
        {
            Envelope<GridVariables> postData = new Envelope<GridVariables>();
            Envelope<GridOutput<IEnumerable<FestivalIncentiveGridData>>> output;
            APIClient<Envelope<GridVariables>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<GridVariables>>();
                string apiUrl = "api/Grid/GetFestivalIncentiveGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<FestivalIncentiveGridData>>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public GridOutput<IEnumerable<SFAGridData>> GetSFAMasterGrid(SFAGridSearchFilters objGridData)
        {
            Envelope<SFAGridSearchFilters> postData = new Envelope<SFAGridSearchFilters>();
            Envelope<GridOutput<IEnumerable<SFAGridData>>> output = new Envelope<GridOutput<IEnumerable<SFAGridData>>>();
            APIClient<Envelope<SFAGridSearchFilters>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<SFAGridSearchFilters>>();
                string apiUrl = "api/Grid/GetSFAMasterGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<SFAGridData>>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return output.Data;
            }
        }

        public IEnumerable<AssetAssignmentToRDIGrid> GetAssetAssignmentToRDIByReference(AssetAssignmentToRDIGet objGridData)
        {
            Envelope<AssetAssignmentToRDIGet> postData = new Envelope<AssetAssignmentToRDIGet>();
            Envelope<IEnumerable<AssetAssignmentToRDIGrid>> output;
            APIClient<Envelope<AssetAssignmentToRDIGet>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<AssetAssignmentToRDIGet>>();
                string apiUrl = "api/Grid/GetAssetAssignmentToRDIByReference";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<IEnumerable<AssetAssignmentToRDIGrid>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
		
		public GridOutput<IEnumerable<ProductCategorySFAMappingGridData>> GetProductCategorySFAMappingGrid(ProdCatSFAMapGridSearchFilters objGridData)
        {
            Envelope<ProdCatSFAMapGridSearchFilters> postData = new Envelope<ProdCatSFAMapGridSearchFilters>();
            Envelope<GridOutput<IEnumerable<ProductCategorySFAMappingGridData>>> output;
            APIClient<Envelope<ProdCatSFAMapGridSearchFilters>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<ProdCatSFAMapGridSearchFilters>>();
                string apiUrl = "api/Grid/GetProductCategorySFAMappingGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<ProductCategorySFAMappingGridData>>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<AssignTargetToSFAGrid> GetTargetToSFAByMonth(AssignTargetToSFAGet objGridData)
        {
            Envelope<AssignTargetToSFAGet> postData = new Envelope<AssignTargetToSFAGet>();
            Envelope<List<AssignTargetToSFAGrid>> output;
            APIClient<Envelope<AssignTargetToSFAGet>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<AssignTargetToSFAGet>>();
                string apiUrl = "api/Grid/GetTargetToSFAByMonth";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<List<AssignTargetToSFAGrid>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<AssignTargetToSFAGrid> ShowTargetToSFAByMonth(AssignTargetToSFAGet objGridData)
        {
            Envelope<AssignTargetToSFAGet> postData = new Envelope<AssignTargetToSFAGet>();
            Envelope<List<AssignTargetToSFAGrid>> output;
            APIClient<Envelope<AssignTargetToSFAGet>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<AssignTargetToSFAGet>>();
                string apiUrl = "api/Grid/ShowTargetToSFAByMonth";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<List<AssignTargetToSFAGrid>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// To fetch all Product category group master Data.
        /// </summary>
        /// <returns>Product Category Group Master Data.</returns>
        public List<ProductCategoryGroupMaster> GetProductCategoryGroup(ProCatGroupFilter objGridData)
        {
            Envelope<ProCatGroupFilter> postData = new Envelope<ProCatGroupFilter>();
            Envelope<List<ProductCategoryGroupMaster>> output;
            APIClient<Envelope<ProCatGroupFilter>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<ProCatGroupFilter>>();
                string apiUrl = "api/ProductCategoryGroup/GetProductCategoryGroup";//need to port
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<List<ProductCategoryGroupMaster>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<RoleRightsMappingGet> GetRoleRightsMappingGrid()
        {
            List<RoleRightsMappingGet> output =new List<RoleRightsMappingGet>();
            APIClient<DBNull> client;
            try
            {
                client = new APIClient<DBNull>();
                string apiUrl = "api/Grid/GetRoleRightsMappingGrid";//checked
                var apiOutput = client.Get(apiUrl);
                var outputdata = JsonConvert.DeserializeObject<Envelope<List<RoleRightsMappingGet>>>(apiOutput.Result);
                output = outputdata.Data;
            }
            catch (Exception ex)
            {
                output = null;
            }
            return output;
        }


        /// <summary>
        /// To fetch Monthly Attendance Report from Database.
        /// Dhruv Sharma, ValueFirst, Gurgaon
        /// </summary>
        /// <param name="Input">Input Parameters</param>
        /// <returns>Monthly Attendance Report</returns>
        public Envelope<DataTable> GetMonthlyAttendanceReportGrid(MonthlyAttendanceReportInput Input)
        {
            Envelope<MonthlyAttendanceReportInput> postData = new Envelope<MonthlyAttendanceReportInput>();
            Envelope<DataTable> output = new Envelope<DataTable>();
            APIClient<Envelope<MonthlyAttendanceReportInput>> client;
            postData.Data = Input;
            try
            {
                client = new APIClient<Envelope<MonthlyAttendanceReportInput>>();
                string apiUrl = "api/WebReports/GetMonthlyAttendanceReport";//need to port
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<DataTable>>(apiOutput.Result);
                return output;
            }
            catch (Exception ex)
            {
                output.Data = null;
                output.MessageCode = (int)NotAcceptable.NotFound;
                output.Message = ex.Message;
                return output;
            }
        }
		
		/// <summary>
        /// To get Competitor Master data.
        /// </summary>
        /// <returns>Competitor Master data.</returns>
        public List<CompetitorMasterData> GetCompetitorMasterGrid(CompetitorFilter InputParam)
        {
            Envelope<CompetitorFilter> postData = new Envelope<CompetitorFilter>();
            postData.Data = InputParam;
            Envelope<List<CompetitorMasterData>> output;
            APIClient<Envelope<CompetitorFilter>> client;
            try
            {
                client = new APIClient<Envelope<CompetitorFilter>>();
                string apiUrl = "api/Grid/GetCompetitorMasterGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<List<CompetitorMasterData>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// To get Competitor Product Category Master Data.
        /// </summary>
        /// <returns>Competitor Product Category Master Data.</returns>
        public List<CompetitorProductData> GetCompetitorProductMasterGrid(CompetitorProductFilter InputParam)
        {
            Envelope<CompetitorProductFilter> postData = new Envelope<CompetitorProductFilter>();
            postData.Data = InputParam;
            Envelope<List<CompetitorProductData>> output;
            APIClient<Envelope<CompetitorProductFilter>> client;
            try
            {
                client = new APIClient<Envelope<CompetitorProductFilter>>();
                string apiUrl = "api/Grid/GetCompetitorProductMasterGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<List<CompetitorProductData>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// To get Competitor's model master grid data.
        /// </summary>
        /// <returns>Competitor's model master grid data.</returns>
        public List<CompetitorModelList> GetCompetitorModelMasterGrid(CompetitorModelFilter InputParam)
        {
            Envelope<CompetitorModelFilter> postData = new Envelope<CompetitorModelFilter>();
            postData.Data = InputParam;
            Envelope<List<CompetitorModelList>> output;
            APIClient<Envelope<CompetitorModelFilter>> client;
            try
            {
                client = new APIClient<Envelope<CompetitorModelFilter>>();
                string apiUrl = "api/Grid/GetCompetitorModelMasterGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<List<CompetitorModelList>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// To fetch all SFA Level Master Data.
        /// </summary>
        /// <returns>SFA Level Master Data.</returns>
        public List<SFALevel> GetSFALevelMasterData(SFALevelFilter InputParam)
        {
            Envelope<SFALevelFilter> postData = new Envelope<SFALevelFilter>();
            postData.Data = InputParam;
            Envelope<List<SFALevel>> output;
            APIClient<Envelope<SFALevelFilter>> client;
            try
            {
                client = new APIClient<Envelope<SFALevelFilter>>();
                string apiUrl = "api/Grid/GetSFALevelMasterData";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<List<SFALevel>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// To fetch all SFA Level Master Data.
        /// </summary>
        /// <returns>SFA Level Master Data.</returns>
        public List<ShiftMaster> GetShiftMaster(ShiftFilter InputParam)
        {
            Envelope<ShiftFilter> postData = new Envelope<ShiftFilter>();
            postData.Data = InputParam;
            Envelope<List<ShiftMaster>> output;
            APIClient<Envelope<ShiftFilter>> client;
            try
            {
                client = new APIClient<Envelope<ShiftFilter>>();
                string apiUrl = "api/Grid/GetShiftMasterData";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<List<ShiftMaster>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// To fetch all SFA Sub Level Master Data.
        /// </summary>
        /// <returns>SFA Sub Level Master Data.</returns>
        public List<SFASubLevel> GetSFASubLevelMasterData(SFASubLevelFilter InputParam)
        {
            Envelope<SFASubLevelFilter> postData = new Envelope<SFASubLevelFilter>();
            postData.Data = InputParam;
            Envelope<List<SFASubLevel>> output;
            APIClient<Envelope<SFASubLevelFilter>> client;
            try
            {
                client = new APIClient<Envelope<SFASubLevelFilter>>();
                string apiUrl = "api/Grid/GetSFASubLevelMasterData";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<List<SFASubLevel>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<RegionGridData> GetRegionGrid(RegionFilter InputParam)
        {
            Envelope<RegionFilter> postData = new Envelope<RegionFilter>();
            postData.Data = InputParam;
            Envelope<List<RegionGridData>> output;
            APIClient<Envelope<RegionFilter>> client;
            try
            {
                client = new APIClient<Envelope<RegionFilter>>();
                string apiUrl = "api/Grid/GetRegionGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<List<RegionGridData>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<StateGridData> GetStateGrid(StateFilter InputParam)
        {
            Envelope<StateFilter> postData = new Envelope<StateFilter>();
            postData.Data = InputParam;
            Envelope<List<StateGridData>> output;
            APIClient<Envelope<StateFilter>> client;
            try
            {
                client = new APIClient<Envelope<StateFilter>>();
                string apiUrl = "api/Grid/GetStateGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<List<StateGridData>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<CityGridData> GetCityGrid(CityFilter InputParam)
        {
            Envelope<CityFilter> postData = new Envelope<CityFilter>();
            postData.Data = InputParam;
            Envelope<List<CityGridData>> output;
            APIClient<Envelope<CityFilter>> client;
            try
            {
                client = new APIClient<Envelope<CityFilter>>();
                string apiUrl = "api/Grid/GetCityGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<List<CityGridData>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<BranchGridData> GetBranchGrid(BranchFilter InputParam)
        {
            Envelope<BranchFilter> postData = new Envelope<BranchFilter>();
            postData.Data = InputParam;
            Envelope<List<BranchGridData>> output;
            APIClient<Envelope<BranchFilter>> client;
            try
            {
                client = new APIClient<Envelope<BranchFilter>>();
                string apiUrl = "api/Grid/GetBranchGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<List<BranchGridData>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<ProductCategoryGridData> GetProductCategoryGrid(ProductCategoryFilter InputParam)
        {
            Envelope<ProductCategoryFilter> postData = new Envelope<ProductCategoryFilter>();
            postData.Data = InputParam;
            Envelope<List<ProductCategoryGridData>> output;
            APIClient<Envelope<ProductCategoryFilter>> client;
            try
            {
                client = new APIClient<Envelope<ProductCategoryFilter>>();
                string apiUrl = "api/Grid/GetProductCategoryGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<List<ProductCategoryGridData>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<ProductSubCategoryGridData> GetSubProductCategoryGrid(SubProductCategoryFilter InputParam)
        {
            Envelope<SubProductCategoryFilter> postData = new Envelope<SubProductCategoryFilter>();
            postData.Data = InputParam;
            Envelope<List<ProductSubCategoryGridData>> output;
            APIClient<Envelope<SubProductCategoryFilter>> client;
            try
            {
                client = new APIClient<Envelope<SubProductCategoryFilter>>();
                string apiUrl = "api/Grid/GetSubProductCategoryGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<List<ProductSubCategoryGridData>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public GridOutput<IEnumerable<TrainingGridData>> GetTrainingMasterGrid(GridVariables objGridData)
        {
            Envelope<GridVariables> postData = new Envelope<GridVariables>();
            Envelope<GridOutput<IEnumerable<TrainingGridData>>> output;
            APIClient<Envelope<GridVariables>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<GridVariables>>();
                string apiUrl = "api/Grid/GetTrainingMasterGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<TrainingGridData>>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public GridOutput<IEnumerable<FeedbackGridData>> GetFeedbackMasterGrid(GridVariables objGridData)
        {
            Envelope<GridVariables> postData = new Envelope<GridVariables>();
            Envelope<GridOutput<IEnumerable<FeedbackGridData>>> output;
            APIClient<Envelope<GridVariables>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<GridVariables>>();
                string apiUrl = "api/Grid/GetFeedbackMasterGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<FeedbackGridData>>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public TrainingMasterEditData TrainingDataBYID(TInputModel objTInputModel)
        {
            Envelope<TInputModel> postData = new Envelope<TInputModel>();
            Envelope<TrainingMasterEditData> output;
            APIClient<Envelope<TInputModel>> client;
            postData.Data = objTInputModel;
            try
            {
                client = new APIClient<Envelope<TInputModel>>();
                string apiUrl = "api/Grid/TrainingDataBYID";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<TrainingMasterEditData>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    

        public GridOutput<IEnumerable<DealerSFAMappingGridData>> GetSFADealerMappingHistoryGrid(DealerSFAFilter objGridData)
        {
            Envelope<DealerSFAFilter> postData = new Envelope<DealerSFAFilter>();
            Envelope<GridOutput<IEnumerable<DealerSFAMappingGridData>>> output;
            APIClient<Envelope<DealerSFAFilter>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<DealerSFAFilter>>();
                string apiUrl = "api/Grid/GetSFADealerMappingHistoryGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<DealerSFAMappingGridData>>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public GridOutput<IEnumerable<SFAGridData>> GetSFABranchChangeHistoryGrid(SFAGridSearchFilters objGridData)
        {
            Envelope<SFAGridSearchFilters> postData = new Envelope<SFAGridSearchFilters>();
            Envelope<GridOutput<IEnumerable<SFAGridData>>> output = new Envelope<GridOutput<IEnumerable<SFAGridData>>>();
            APIClient<Envelope<SFAGridSearchFilters>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<SFAGridSearchFilters>>();
                string apiUrl = "api/Grid/GetSFABranchChangeHistoryGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<GridOutput<IEnumerable<SFAGridData>>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return output.Data;
            }
        }
        #region NIkita kawade 9/9/2014
        public List<QsrReportsGrid> GetQuantityReport(QSRQuantityGet objGridData)
        {
            Envelope<QSRQuantityGet> postData = new Envelope<QSRQuantityGet>();
            Envelope<List<QsrReportsGrid>> output;
            APIClient<Envelope<QSRQuantityGet>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<QSRQuantityGet>>();
                string apiUrl = "api/Grid/GetReportQSRQuantity";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<List<QsrReportsGrid>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<QsrReportsGrid> ShowQuantityReport(QSRQuantityGet objGridData)
        {
            Envelope<QSRQuantityGet> postData = new Envelope<QSRQuantityGet>();
            Envelope<List<QsrReportsGrid>> output;
            APIClient<Envelope<QSRQuantityGet>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<QSRQuantityGet>>();
                string apiUrl = "api/Grid/ShowTargetToSFAByMonth";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<List<QsrReportsGrid>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

    }
}
