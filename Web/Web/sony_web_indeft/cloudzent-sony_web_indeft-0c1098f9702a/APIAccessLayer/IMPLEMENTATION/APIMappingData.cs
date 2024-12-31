using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMBOModels.Mappings;
using APIAccessLayer.Helper;
using Newtonsoft.Json;
using AMBOModels.UserManagement;
using System.Data;
using APIAccessLayer.INTERFACE;
using AMBOModels.GlobalAccessible;

namespace APIAccessLayer.IMPLEMENTATION
{
    public class APIMappingData : IAPIMappingData
    {
        public readonly IAPICommon _IAPICommon;

        public APIMappingData(IAPICommon IAPICommon)
        {
            _IAPICommon = IAPICommon;
        }
        #region Dealer SFA Mapping
        public Envelope<bool> CreateDealerSFAMapping(DealerSFAMapping mappingData)
        {
            Envelope<DealerSFAMapping> postData = new Envelope<DealerSFAMapping>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<DealerSFAMapping>> client;
            postData.Data = mappingData;
            try
            {
                client = new APIClient<Envelope<DealerSFAMapping>>();
                string apiUrl = "api/Mapping/CreateDealerSFAMapping";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> DeleteDealerSFAMapping(DealerSFAMapping mappingData)
        {
            Envelope<DealerSFAMapping> postData = new Envelope<DealerSFAMapping>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<DealerSFAMapping>> client;
            postData.Data = mappingData;
            try
            {
                client = new APIClient<Envelope<DealerSFAMapping>>();
                string apiUrl = "api/Mapping/DeleteDealerSFAMapping";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> UpdateDealerSFAMapping(DealerSFAMapping mappingData)
        {
            Envelope<DealerSFAMapping> postData = new Envelope<DealerSFAMapping>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<DealerSFAMapping>> client;
            postData.Data = mappingData;
            try
            {
                client = new APIClient<Envelope<DealerSFAMapping>>();
                string apiUrl = "api/Mapping/UpdateDealerSFAMapping";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public DealerSFAMapping GetSFADetails(string LoginId)
        {
            Envelope<string> postData = new Envelope<string>();
            Envelope<DealerSFAMapping> output = new Envelope<DealerSFAMapping>();
            APIClient<Envelope<string>> client;
            postData.Data = LoginId;
            try
            {
                client = new APIClient<Envelope<string>>();
                string apiUrl = "api/UserManagement/GetSFADetails";//need to port
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<DealerSFAMapping>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output.Data;
        }
        #endregion Dealer SFA Mapping

        #region Dealer Classification Mapping
        public Envelope<DealerClassificationMappingTable> GetDealerClassificationMappingTable(DealerClassificationMappingSearch mappingData)
        {
            Envelope<DealerClassificationMappingSearch> postData = new Envelope<DealerClassificationMappingSearch>();
            Envelope<DealerClassificationMappingTable> output = new Envelope<DealerClassificationMappingTable>();
            APIClient<Envelope<DealerClassificationMappingSearch>> client;
            postData.Data = mappingData;
            try
            {
                client = new APIClient<Envelope<DealerClassificationMappingSearch>>();
                string apiUrl = "api/Mapping/GetDealerClassificationMappingTable";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<DealerClassificationMappingTable>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> CreateDealerClassificationMapping(DealerClassificationMappingTable mappingData)
        {
            Envelope<DealerClassificationMappingTable> postData = new Envelope<DealerClassificationMappingTable>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<DealerClassificationMappingTable>> client;
            postData.Data = mappingData;
            try
            {
                client = new APIClient<Envelope<DealerClassificationMappingTable>>();
                string apiUrl = "api/Mapping/CreateDealerClassificationMapping";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> DeleteDealerClassificationMapping(DealerClassificationMapping mappingData)
        {
            Envelope<DealerClassificationMapping> postData = new Envelope<DealerClassificationMapping>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<DealerClassificationMapping>> client;
            postData.Data = mappingData;
            try
            {
                client = new APIClient<Envelope<DealerClassificationMapping>>();
                string apiUrl = "api/Mapping/DeleteDealerClassificationMapping";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> UpdateDealerClassificationMapping(DealerClassificationMapping mappingData)
        {
            Envelope<DealerClassificationMapping> postData = new Envelope<DealerClassificationMapping>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<DealerClassificationMapping>> client;
            postData.Data = mappingData;
            try
            {
                client = new APIClient<Envelope<DealerClassificationMapping>>();
                string apiUrl = "api/Mapping/UpdateDealerClassificationMapping";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> ManageDealerClassificationMapping(DealerProductMapping mappingData)
        {
            Envelope<DealerProductMapping> postData = new Envelope<DealerProductMapping>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<DealerProductMapping>> client;
            postData.Data = mappingData;
            try
            {
                client = new APIClient<Envelope<DealerProductMapping>>();
                string apiUrl = "api/Mapping/ManageDealerClassificationMapping";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }
        #endregion Dealer Classification Mapping

        #region Product Category SFA Mapping
        public Envelope<bool> CreateProductCategorySFAMapping(ProductCategorySFAMapping mappingData)
        {
            Envelope<ProductCategorySFAMapping> postData = new Envelope<ProductCategorySFAMapping>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<ProductCategorySFAMapping>> client;
            postData.Data = mappingData;
            try
            {
                client = new APIClient<Envelope<ProductCategorySFAMapping>>();
                string apiUrl = "api/Mapping/CreateProductCategorySFAMapping";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> DeleteProductCategorySFAMapping(ProductCategorySFAMapping mappingData)
        {
            Envelope<ProductCategorySFAMapping> postData = new Envelope<ProductCategorySFAMapping>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<ProductCategorySFAMapping>> client;
            postData.Data = mappingData;
            try
            {
                client = new APIClient<Envelope<ProductCategorySFAMapping>>();
                string apiUrl = "api/Mapping/DeleteProductCategorySFAMapping";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }
        #endregion Product Category SFA Mapping

        #region Employee Structure Mapping
        public Envelope<bool> CreateEmployeeStructureMapping(EmployeeStructureMapping mappingData)
        {
            Envelope<EmployeeStructureMapping> postData = new Envelope<EmployeeStructureMapping>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<EmployeeStructureMapping>> client;
            postData.Data = mappingData;
            try
            {
                client = new APIClient<Envelope<EmployeeStructureMapping>>();
                string apiUrl = "api/Mapping/CreateEmployeeStructureMapping";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> DeleteEmployeeStructureMapping(EmployeeStructureMapping mappingData)
        {
            Envelope<EmployeeStructureMapping> postData = new Envelope<EmployeeStructureMapping>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<EmployeeStructureMapping>> client;
            postData.Data = mappingData;
            try
            {
                client = new APIClient<Envelope<EmployeeStructureMapping>>();
                string apiUrl = "api/Mapping/DeleteEmployeeStructureMapping";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public List<SFAFormData> GetSFAForStructureMapping(SFAForStructureMappingInput objSearchData)
        {
            Envelope<SFAForStructureMappingInput> PostData = new Envelope<SFAForStructureMappingInput>();
            PostData.Data = objSearchData;
            APIClient<Envelope<SFAForStructureMappingInput>> client = new APIClient<Envelope<SFAForStructureMappingInput>>();
            try
            {
                string apiUrl = "api/Mapping/GetSFAForStructureMapping";//checked
                var apiOutput = client.Post(apiUrl, PostData);
                var output = JsonConvert.DeserializeObject<Envelope<List<SFAFormData>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion Employee Structure Mapping

        #region User Branch Channel Product Category Mapping
        public Envelope<bool> ManageUserBranchChannelPCMapping(UserBranchChannelPCMapping mappingData)
        {
            Envelope<UserBranchChannelPCMapping> postData = new Envelope<UserBranchChannelPCMapping>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<UserBranchChannelPCMapping>> client;
            postData.Data = mappingData;
            try
            {
                client = new APIClient<Envelope<UserBranchChannelPCMapping>>();
                string apiUrl = "api/Mapping/ManageUserBranchChannelPCMapping";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public UserBranchChannelPCMapping GetUserBranchChannelPCMapping(Int64 MappingUserId)
        {
            Envelope<Int64?> postData = new Envelope<Int64?>();
            Envelope<UserBranchChannelPCMapping> output = new Envelope<UserBranchChannelPCMapping>();
            APIClient<Envelope<Int64?>> client;
            postData.Data = MappingUserId;
            try
            {
                client = new APIClient<Envelope<Int64?>>();
                string apiUrl = "api/Mapping/GetUserBranchChannelPCMapping";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<UserBranchChannelPCMapping>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output.Data;
        }
        #endregion User Branch Channel Product Category Mapping

        #region Incentive Target Category Mapping
        public Envelope<bool> ManageIncentiveTargetCategoryMapping(IncentiveTargetCategoryMapping mappingData)
        {
            Envelope<IncentiveTargetCategoryMapping> postData = new Envelope<IncentiveTargetCategoryMapping>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<IncentiveTargetCategoryMapping>> client;
            postData.Data = mappingData;
            try
            {
                client = new APIClient<Envelope<IncentiveTargetCategoryMapping>>();
                string apiUrl = "api/Mapping/ManageIncentiveTargetCategoryMapping";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public IncentiveTargetCategoryMapping GetIncentiveTargetCategoryMapping(Int64 IncentiveCatId)
        {
            Envelope<Int64?> postData = new Envelope<Int64?>();
            Envelope<IncentiveTargetCategoryMapping> output = new Envelope<IncentiveTargetCategoryMapping>();
            APIClient<Envelope<Int64?>> client;
            postData.Data = IncentiveCatId;
            try
            {
                client = new APIClient<Envelope<Int64?>>();
                string apiUrl = "api/Mapping/GetIncentiveTargetCategoryMapping";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<IncentiveTargetCategoryMapping>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output.Data;
        }
        #endregion Incentive Target Category Mapping

        #region Assign Incentive Category to SFA
        public Envelope<IncentiveCategorySFAMapping> ManageIncentiveCategorySFAMapping(IncentiveCategorySFAMapping mappingData)
        {
            Envelope<IncentiveCategorySFAMapping> postData = new Envelope<IncentiveCategorySFAMapping>();
            Envelope<IncentiveCategorySFAMapping> output = new Envelope<IncentiveCategorySFAMapping>();
            APIClient<Envelope<IncentiveCategorySFAMapping>> client;
            postData.Data = mappingData;
            try
            {
                client = new APIClient<Envelope<IncentiveCategorySFAMapping>>();
                string apiUrl = "api/Mapping/ManageIncentiveCategorySFAMapping";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<IncentiveCategorySFAMapping>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public IncentiveCategorySFAMapping GetSFAForIncentiveCategorySFAMapping(Int64 RDIId)
        {
            Envelope<Int64?> postData = new Envelope<Int64?>();
            Envelope<IncentiveCategorySFAMapping> output = new Envelope<IncentiveCategorySFAMapping>();
            APIClient<Envelope<Int64?>> client;
            postData.Data = RDIId;
            try
            {
                client = new APIClient<Envelope<Int64?>>();
                string apiUrl = "api/Mapping/GetSFAForIncentiveCategorySFAMapping";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<IncentiveCategorySFAMapping>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output.Data;
        }
        #endregion Assign Incentive Category to SFA

        #region Product Definition Under Target Category
        public Envelope<bool> ManageProductDefinitionUnderTargetCategory(ProdDefUnderTargetCatGridOutput mappingData)
        {
            Envelope<ProdDefUnderTargetCatGridOutput> postData = new Envelope<ProdDefUnderTargetCatGridOutput>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<ProdDefUnderTargetCatGridOutput>> client;
            postData.Data = mappingData;
            try
            {
                client = new APIClient<Envelope<ProdDefUnderTargetCatGridOutput>>();
                string apiUrl = "api/Mapping/ManageProductDefinitionUnderTargetCategory";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<ProdDefUnderTargetCatGridOutput> GetProductsByCategorySubCategory(ProdDefUnderTargetCat mappingData)
        {
            Envelope<ProdDefUnderTargetCat> postData = new Envelope<ProdDefUnderTargetCat>();
            Envelope<ProdDefUnderTargetCatGridOutput> output = new Envelope<ProdDefUnderTargetCatGridOutput>();
            APIClient<Envelope<ProdDefUnderTargetCat>> client;
            postData.Data = mappingData;
            try
            {
                client = new APIClient<Envelope<ProdDefUnderTargetCat>>();
                string apiUrl = "api/Mapping/GetProductsByCategorySubCategory";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<ProdDefUnderTargetCatGridOutput>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<ProdDefUnderTargetCatGridOutput> GetProductsByCategorySubCategoryforallMat(ProdDefUnderTargetCatforAllMat mappingData)
        {
            Envelope<ProdDefUnderTargetCatforAllMat> postData = new Envelope<ProdDefUnderTargetCatforAllMat>();
            Envelope<ProdDefUnderTargetCatGridOutput> output = new Envelope<ProdDefUnderTargetCatGridOutput>();
            APIClient<Envelope<ProdDefUnderTargetCatforAllMat>> client;
            postData.Data = mappingData;
            try
            {
                client = new APIClient<Envelope<ProdDefUnderTargetCatforAllMat>>();
                string apiUrl = "api/Mapping/GetProductsByCategorySubCategoryforallMat";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<ProdDefUnderTargetCatGridOutput>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }
        #endregion Product Definition Under Target Category

        #region Assign Target To SFA API
        public Envelope<DataTable> UploadTargetToSFAByMonth(AssignTargetToSFAUpload targetData)
        {
            Envelope<AssignTargetToSFAUpload> postData = new Envelope<AssignTargetToSFAUpload>();
            Envelope<DataTable> output = new Envelope<DataTable>();
            APIClient<Envelope<AssignTargetToSFAUpload>> client;
            postData.Data = targetData;
            try
            {
                client = new APIClient<Envelope<AssignTargetToSFAUpload>>();
                string apiUrl = "api/Mapping/UploadTargetToSFAByMonth";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<DataTable>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
                ErrorDetailsInput inp = new ErrorDetailsInput();
                inp.ErrorDetails = "error in access layer";
                inp.ErrorMessage = ex.Message;
                inp.ErrorSource = "AssignTargetToSFAExcelUpload";
                _IAPICommon.CreateErrorLogWeb(inp);
                
            }
            return output;
        }

        public AssignTargetToSFAGet GetBranchByUserId(Int64 UserId)
        {
            Envelope<Int64> postData = new Envelope<Int64>();
            Envelope<AssignTargetToSFAGet> output = new Envelope<AssignTargetToSFAGet>();
            APIClient<Envelope<Int64>> client;
            postData.Data = UserId;
            try
            {
                client = new APIClient<Envelope<Int64>>();
                string apiUrl = "api/Mapping/GetBranchByUserId";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<AssignTargetToSFAGet>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output.Data;
        }
        #endregion Assign Target To SFA API

        #region Navigation Overloads
        public Envelope<bool> CreateIncentiveCategorySFAMapping(NavigationIncentiveCategorySFAMapping mappingData)
        {
            Envelope<NavigationIncentiveCategorySFAMapping> postData = new Envelope<NavigationIncentiveCategorySFAMapping>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<NavigationIncentiveCategorySFAMapping>> client;
            postData.Data = mappingData;
            try
            {
                client = new APIClient<Envelope<NavigationIncentiveCategorySFAMapping>>();
                string apiUrl = "api/Mapping/CreateIncentiveCategorySFAMapping";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }
        #endregion Navigation Overloads

        #region Sales PIC Outlet Mapping
        public Envelope<bool> ManageSalesPICOutletMapping(ManageSalesPICOutletMappingForm objFormData)
        {
            Envelope<ManageSalesPICOutletMappingForm> postData = new Envelope<ManageSalesPICOutletMappingForm>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<ManageSalesPICOutletMappingForm>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<ManageSalesPICOutletMappingForm>>();
                string apiUrl = "api/Mapping/ManageSalesPICOutletMapping";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<ManageSalesPICOutletMappingForm> GetSalesPICOutletMapping(ManageSalesPICOutletMappingForm objFormData)
        {
            Envelope<ManageSalesPICOutletMappingForm> postData = new Envelope<ManageSalesPICOutletMappingForm>();
            Envelope<ManageSalesPICOutletMappingForm> output = new Envelope<ManageSalesPICOutletMappingForm>();
            APIClient<Envelope<ManageSalesPICOutletMappingForm>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<ManageSalesPICOutletMappingForm>>();
                string apiUrl = "api/Mapping/GetSalesPICOutletMapping";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<ManageSalesPICOutletMappingForm>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> DeleteSalesPICOutletMapping(DeleteSalesPICOutletMappingForm objFormData)
        {
            Envelope<DeleteSalesPICOutletMappingForm> postData = new Envelope<DeleteSalesPICOutletMappingForm>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<DeleteSalesPICOutletMappingForm>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<DeleteSalesPICOutletMappingForm>>();
                string apiUrl = "api/Mapping/DeleteSalesPICOutletMapping";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }
        #endregion Sales PIC Outlet Mapping

        #region Assign Festival Target To SFA API
        public List<AssignFestivalTargetGrid> GetFestivalTargetToSFABySchemeId(AssignFestivalTargetGet objInput)
        {
            Envelope<AssignFestivalTargetGet> postData = new Envelope<AssignFestivalTargetGet>();
            Envelope<List<AssignFestivalTargetGrid>> output;
            APIClient<Envelope<AssignFestivalTargetGet>> client;
            postData.Data = objInput;
            try
            {
                client = new APIClient<Envelope<AssignFestivalTargetGet>>();
                string apiUrl = "api/Mapping/GetFestivalTargetToSFABySchemeId";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<List<AssignFestivalTargetGrid>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<AssignFestivalTargetGrid> ShowFestivalTargetToSFABySchemeId(AssignFestivalTargetGet objInput)
        {
            Envelope<AssignFestivalTargetGet> postData = new Envelope<AssignFestivalTargetGet>();
            Envelope<List<AssignFestivalTargetGrid>> output;
            APIClient<Envelope<AssignFestivalTargetGet>> client;
            postData.Data = objInput;
            try
            {
                client = new APIClient<Envelope<AssignFestivalTargetGet>>();
                string apiUrl = "api/Mapping/ShowFestivalTargetToSFABySchemeId";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<List<AssignFestivalTargetGrid>>>(apiOutput.Result);
                return output.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Envelope<DataTable> UploadFestivalTargetToSFAByScheme(AssignFestivalTargetUpload objInput)
        {
            Envelope<AssignFestivalTargetUpload> postData = new Envelope<AssignFestivalTargetUpload>();
            Envelope<DataTable> output = new Envelope<DataTable>();
            APIClient<Envelope<AssignFestivalTargetUpload>> client;
            postData.Data = objInput;
            try
            {
                client = new APIClient<Envelope<AssignFestivalTargetUpload>>();
                string apiUrl = "api/Mapping/UploadFestivalTargetToSFAByScheme";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<DataTable>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }
        #endregion Assign Festival Target To SFA API

        #region Nikita 9/9/2024
        public Envelope<DataTable> UploadQSRQuantity(QSRQuantityUpload targetData)
        {
            Envelope<QSRQuantityUpload> postData = new Envelope<QSRQuantityUpload>();
            Envelope<DataTable> output = new Envelope<DataTable>();
            APIClient<Envelope<QSRQuantityUpload>> client;
            postData.Data = targetData;
            try
            {
                client = new APIClient<Envelope<QSRQuantityUpload>>();
                string apiUrl = "api/Mapping/UploadReportQSRQuantity";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<DataTable>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
                ErrorDetailsInput inp = new ErrorDetailsInput();
                inp.ErrorDetails = "error in access layer";
                inp.ErrorMessage = ex.Message;
                inp.ErrorSource = "AssignTargetToSFAExcelUpload";
                _IAPICommon.CreateErrorLogWeb(inp);

            }
            return output;
        }

        #endregion
    }
}
